using System;
using System.Collections.Generic;
using System.Linq;

using SabberStoneBasicAI.Score;
using SabberStoneCore.Tasks.PlayerTasks;
using SabberStoneBasicAI.PartialObservation;

using SabberStoneCore.Model.Entities;

namespace SabberStoneBasicAI.AIAgents
{
    class MinMaxAgent : AbstractAgent
    {
        // Contain current game state, actions to reach state
        private class MinMaxAgentState {
            private POGame currentGameState;
            private List<PlayerTask> actionsToReachCurrentGameState;
            private List<PlayerTask> entireActionList;

            public POGame State => currentGameState;
            public List<PlayerTask> ActionsToReachState => actionsToReachCurrentGameState;

            public  List<PlayerTask> EntireActionList => entireActionList;

            public IEnumerable<PlayerTask> AvailableActions => EntireActionList.Where(
                option => option.PlayerTaskType != PlayerTaskType.END_TURN
            );

            public bool OnlyActionIsEndTurn => entireActionList.All(
                option => option.PlayerTaskType == PlayerTaskType.END_TURN
            );

            public MinMaxAgentState(MinMaxAgentState parent, KeyValuePair<PlayerTask, POGame> actionToGame) {
				this.currentGameState = actionToGame.Value;//.getCopy();
                this.actionsToReachCurrentGameState = 
                        new List<PlayerTask>(parent.actionsToReachCurrentGameState);
                this.actionsToReachCurrentGameState.Add(actionToGame.Key);

                this.entireActionList = new List<PlayerTask>(currentGameState.CurrentPlayer.Options());
            }

            public MinMaxAgentState(POGame game) {
                this.currentGameState = game.getCopy();
                this.actionsToReachCurrentGameState = new List<PlayerTask>();
                this.entireActionList = new List<PlayerTask>(currentGameState.CurrentPlayer.Options());
            }
        }

        private bool finishedBuildingTree = false;

        private List<PlayerTask> actionsToTake;


        private int turnCounter = 0;

        private static readonly int MAX_CHILD_COUNT = 4;

		public override PlayerTask GetMove(POGame game)
		{
			finishedBuildingTree = false;

            if (!finishedBuildingTree) {
                actionsToTake = new List<PlayerTask>();
            }

            if (actionsToTake.Count != 0) {
                var action = DequeueTask();
                finishedBuildingTree = actionsToTake.Count != 0;
                if (!finishedBuildingTree) {
                    //Console.WriteLine("My health: " + poGame.CurrentPlayer.Hero.Health);
			        //Console.WriteLine("Opponent health: " + poGame.CurrentOpponent.Hero.Health);
                    //Console.WriteLine("\n\n\n");                
                }
                // Console.WriteLine(String.Format("Action: {0}", action));
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
                    var mapping = hasMoreAction.State.Simulate(
                        hasMoreAction.AvailableActions.ToList()
                    );//.Where( x => x.Value != null );
                    foreach (KeyValuePair<PlayerTask, POGame> actionState in mapping) {
						if (actionState.Value != null)
                        //if (actionState.Key.FullPrint().Contains("Player1")) {
                        //    continue;
                        //}
                        // Console.WriteLine(actionState.Key.FullPrint());
							childrenStates.Add(new MinMaxAgentState(hasMoreAction, actionState));
                    }
                }

                // Get the top N childrenStates
                var childrenStatesWithScore = childrenStates.Zip(childrenStates.Select(
                    childState => Score(childState.State)
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
            var bestState = MaxByKey(finalCandidates, (lhs, rhs) => {
                return Score(lhs.State) < Score(rhs.State) ? rhs : lhs;
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
                var action = actionsToTake[0]; 
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

        private static double Score(POGame state) {
            return new ControlScore { Controller = state.CurrentPlayer }.Rate();
        }

        private static double Score(POGame state, bool dummy) {
			return  MinMaxAgentScoring(state.CurrentPlayer, state.CurrentPlayer) -
                    MinMaxAgentScoring(state.CurrentPlayer, state.CurrentOpponent);
		}

        private static double MinMaxAgentScoring(Controller ourPlayer, Controller player) {
            double score = 0;
            if (player.Hero.Health <= 0) {
                score = Double.MinValue;
            }
            else {
                score = Math.Sqrt(player.Hero.Health) * 2;
            }

            foreach (Minion minion in player.BoardZone.GetAll()) {
                //Damage is evaluate at 1.0 per point
                score += minion.AttackDamage;
                // Health is evaluated at 1 per point
                score += minion.Health;
                // Cards default to a budget of 2*[mana cost]+1.
                if(minionHasPassiveAbility(minion)){
                    score += 2 * minion.Card.Cost + 1;
                }

            }
            int turnsTaken = player.Game.Turn/2;
            // (These two are not calculated for the opponent
            if (ourPlayer.Equals(player)) {
                //Having no minions on the board subtracts 2.0 + 1.0 * [turn count to a max of 10]
                if(player.BoardZone.GetAll().Count() == 0){
                    score -= 2 + (turnsTaken >= 10 ? 10 : turnsTaken);
                }
                // Having cleared the enemy board adds 2.0 + 1.0 * [turn count to a max of 10]
                if(player.Game.CurrentOpponent.BoardZone.GetAll().Count() == 0){
                    score += 2 + (turnsTaken >= 10 ? 10 : turnsTaken);
                }
            }
            
            for (int i = 0; i < player.HandZone.GetAll().Count(); ++i) {
                score += (i < 3) ? 3 : 2;
            }
            if (turnsTaken >=10){
                int cardsInDeck = player.DeckZone.GetAll().Count();
                if (cardsInDeck != 0) {
                    score += Math.Sqrt(cardsInDeck);
                } else {
                    score -= 2;
                }
            }

            return score;
        }


        private static bool minionHasPassiveAbility(Minion minion){
            return minion.HasTaunt 
                || minion.HasCharge 
                || minion.HasWindfury 
                || minion.HasDivineShield 
                || minion.HasStealth 
                || minion.HasDeathrattle 
                || minion.HasBattleCry 
                || minion.HasInspire 
                || minion.HasLifeSteal 
                || minion.IsImmune;

        }

        public override void InitializeAgent()
		{
            Console.WriteLine("Initialising MinMaxAgent");
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
