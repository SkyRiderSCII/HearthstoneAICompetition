using System;
using System.Collections.Generic;
using System.Linq;

using SabberStoneBasicAI.Score;
using SabberStoneCore.Tasks.PlayerTasks;
using SabberStoneBasicAI.PartialObservation;

using SabberStoneCore.Model.Entities;
using SabberStoneCore.Enums;
using SabberStoneBasicAI.Meta;

namespace SabberStoneBasicAI.AIAgents
{
    class HarderMinMaxAgent : AbstractAgent
    {
        // Contain current game state, actions to reach state
        private class MinMaxAgentState {
			public POGame State { get; }
			public List<PlayerTask> ActionsToReachState { get; }

			public List<PlayerTask> EntireActionList { get; }

			public IEnumerable<PlayerTask> AvailableActions => EntireActionList.Where(
                option => option.PlayerTaskType != PlayerTaskType.END_TURN
            );

            public bool OnlyActionIsEndTurn => EntireActionList.All(
                option => option.PlayerTaskType == PlayerTaskType.END_TURN
            );

            public MinMaxAgentState(MinMaxAgentState parent, KeyValuePair<PlayerTask, POGame> actionToGame) {
				State = actionToGame.Value;//.getCopy();
				ActionsToReachState =
						new List<PlayerTask>(parent.ActionsToReachState)
						{
							actionToGame.Key
						};

				EntireActionList = new List<PlayerTask>(State.CurrentPlayer.Options());
            }

            public MinMaxAgentState(POGame game) {
                State = game.getCopy();
                ActionsToReachState = new List<PlayerTask>();
                EntireActionList = new List<PlayerTask>(State.CurrentPlayer.Options());
            }
        }

        private bool finishedBuildingTree = false;

		private int h_score = -1;

		private List<PlayerTask> actionsToTake;
		private static readonly int MAX_CHILD_COUNT = 4;

		public override PlayerTask GetMove(POGame game)
		{
			Controller player = game.CurrentPlayer;

			if (h_score == -1)
			{
				determineScore(player);
			}
			// Implement a simple Mulligan Rule
			if (player.MulliganState == Mulligan.INPUT)
			{
				List<int> mulligan = new HarderMidRangeScore().MulliganRule().Invoke(player.Choice.Choices.Select(p => game.getGame().IdEntityDic[p]).ToList());
				return ChooseTask.Mulligan(player, mulligan);
			}

			finishedBuildingTree = false;

            if (!finishedBuildingTree) {
                actionsToTake = new List<PlayerTask>();
            }

            if (actionsToTake.Count != 0) {
				PlayerTask action = DequeueTask();
                finishedBuildingTree = actionsToTake.Count != 0;
                /*if (!finishedBuildingTree) {
                    Console.WriteLine("My health: " + poGame.CurrentPlayer.Hero.Health);
			        Console.WriteLine("Opponent health: " + poGame.CurrentOpponent.Hero.Health);
                    Console.WriteLine("\n\n\n");                
                }
                Console.WriteLine(String.Format("Action: {0}", action));*/
                return action;    
            }

            List<MinMaxAgentState> leaves = new List<MinMaxAgentState>();
            List<MinMaxAgentState> parentStates = new List<MinMaxAgentState>(){
                new MinMaxAgentState(game)
            };

            // while we have moves to play
            // int generation = 0;
            while (true) {
				// split currentLevel into leaves "partitioned[false]"
				// and non-leaves "partitioned[true]"
				ILookup<bool, MinMaxAgentState> partitioned = parentStates.ToLookup(
                    currentLevelNonLeaf => currentLevelNonLeaf.OnlyActionIsEndTurn || 
                        currentLevelNonLeaf.ActionsToReachState.Count >= 6
                );

				IEnumerable<MinMaxAgentState> parentLeaves = partitioned[true];
				List<MinMaxAgentState> nonParentLeaves = partitioned[false].ToList();

                leaves.AddRange(parentLeaves.ToList());

                if (nonParentLeaves.Count == 0) {
                    break;
                }

                //Console.WriteLine(String.Format("leaves.Count = {0}", leaves.Count));
                //Console.WriteLine(String.Format("haveMoreActions.Count = {0}\n", haveMoreActions.Count));

                //if (haveMoreActions.Count == 0) {
                //    break;
                //}

                List<MinMaxAgentState> childrenStates = new List<MinMaxAgentState>();

                //Console.WriteLine(String.Format("========== Actions My Bot Can Take =========="));
                //Console.WriteLine(String.Format("Iteration #{0}\n\n", ++generation));

                foreach (MinMaxAgentState hasMoreAction in nonParentLeaves) {
					Dictionary<PlayerTask, POGame> mapping = hasMoreAction.State.Simulate(
                        hasMoreAction.AvailableActions.ToList()
                    );//.Where( x => x.Value != null );
                    foreach (KeyValuePair<PlayerTask, POGame> actionState in mapping) {
						if (actionState.Value != null)
                        /*if (actionState.Key.FullPrint().Contains("Player1")) {
                            continue;
                        }
                        Console.WriteLine(actionState.Key.FullPrint());*/
							childrenStates.Add(new MinMaxAgentState(hasMoreAction, actionState));
                    }
                }

                // Get the top N childrenStates
                var childrenStatesWithScore = childrenStates.Zip(childrenStates.Select(
                    childState => Score(childState.State, childState.State.CurrentPlayer.PlayerId)
                ), (state, score) => new Tuple<MinMaxAgentState, double>(state, score)).ToList();
                childrenStatesWithScore.Sort((lhs, rhs) => rhs.Item2.CompareTo(lhs.Item2));

                parentStates = childrenStatesWithScore.Take(MAX_CHILD_COUNT).Select(
                    stateScore => stateScore.Item1
                ).ToList();
            }

            // No moves possible i.e. no leaves
            var finalCandidates = leaves.ToList();
            if (finalCandidates.Count == 0) {
                actionsToTake.Add(EndTurnTask.Any(parentStates[0].State.CurrentPlayer.Controller));
                return DequeueTask();
            }

			// Get the very best
			MinMaxAgentState bestState = MaxByKey(finalCandidates, (lhs, rhs) => {
                return Score(lhs.State, lhs.State.CurrentPlayer.Id) < Score(rhs.State, rhs.State.CurrentPlayer.Id) ? rhs : lhs;
            });
            actionsToTake = bestState.ActionsToReachState;
            if (actionsToTake.Count == 0 || actionsToTake[actionsToTake.Count - 1].PlayerTaskType != PlayerTaskType.END_TURN) {
                actionsToTake.Add(EndTurnTask.Any(bestState.State.CurrentPlayer.Controller));
            }

            finishedBuildingTree = true;
            // Console.WriteLine(String.Format("Turn Counter @ {0}", ++turnCounter));
            return DequeueTask();
        }

        private PlayerTask DequeueTask() {
			PlayerTask action = actionsToTake[0]; 
                actionsToTake.RemoveAt(0);

                // Console.WriteLine(String.Format("Action: {0}", action));
                return action;
        }

        private T MaxByKey<T>(List<T> enumerable, Func<T, T, T> comparator) {
            if (enumerable.Count == 0) {
                throw new Exception("Cannot get max of empty enumerable");
            }
            else if (enumerable.Count == 1) {
                return enumerable[0];
            }

            T maxValue = enumerable[0];
            for (int i = 1; i < enumerable.Count; ++i) {
                maxValue = comparator(maxValue, enumerable[i]);
            } 

            return maxValue;
        }

		public int Score(POGame state, int playerId)
		{
			Controller p = state.CurrentPlayer.PlayerId == playerId ? state.CurrentPlayer : state.CurrentOpponent;
			switch (h_score)
			{
				case 0:
					return new HarderAggroScore { Controller = p }.Rate();
				case 1:
					return new HarderMidRangeScore { Controller = p }.Rate();
				case 2:
					return new HarderControlScore { Controller = p }.Rate();
				default:
					return new HarderMidRangeScore { Controller = p }.Rate();
			}
		}

		private void determineScore(Controller player)
		{
			switch (player.HeroClass)
			{
				case CardClass.DRUID:
					if (Decks.MidrangeDruid.Contains(player.DeckZone[0].Card) &
						Decks.MidrangeDruid.Contains(player.DeckZone[1].Card) &
						Decks.MidrangeDruid.Contains(player.DeckZone[2].Card))
					{
						h_score = 1;
					}
					else
					{
						h_score = 0;
					}
					break;
				case CardClass.HUNTER:
					h_score = 0;
					break;
				case CardClass.MAGE:
					h_score = 2;
					break;
				case CardClass.PALADIN:
					h_score = 1;
					break;
				case CardClass.PRIEST:
					h_score = 2;
					break;
				case CardClass.ROGUE:
					h_score = 0;
					break;
				case CardClass.SHAMAN:
					if (Decks.MidrangeJadeShaman.Contains(player.DeckZone[0].Card) &
						Decks.MidrangeJadeShaman.Contains(player.DeckZone[1].Card) &
						Decks.MidrangeJadeShaman.Contains(player.DeckZone[2].Card))
					{
						h_score = 1;
					}
					else
					{
						h_score = 0;
					}
					break;
				case CardClass.WARLOCK:
					h_score = 0;
					break;
				case CardClass.WARRIOR:
					if (Decks.AggroPirateWarrior.Contains(player.DeckZone[0].Card) &
						Decks.AggroPirateWarrior.Contains(player.DeckZone[1].Card) &
						Decks.AggroPirateWarrior.Contains(player.DeckZone[2].Card))
					{
						h_score = 0;
					}
					else
					{
						h_score = 2;
					}
					break;
				default:
					h_score = -2;
					break;
			}
		}

		public override void InitializeAgent()
		{
		}

		public override void InitializeGame()
		{
		}

        public override void FinalizeAgent()
		{
		}

		public override void FinalizeGame()
		{
		}
    }
}
