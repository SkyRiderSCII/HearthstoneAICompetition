using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using SabberStoneBasicAI.PartialObservation;
using SabberStoneBasicAI.Score;
using SabberStoneCore.Enums;
using SabberStoneCore.Tasks.PlayerTasks;
using SabberStoneCore.Model.Entities;
using SabberStoneBasicAI.Meta;

namespace SabberStoneBasicAI.AIAgents
{
	class HarderBeamSearchAgent : AbstractAgent
	{
		private Stopwatch _watch;

		private int h_score = -1;

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

			int depth;
			int beamWidth;

			// Check how much time we have left on this turn. The hard limit is 75 seconds so we already stop
			// beam searching when 60 seconds have passed, just to be sure.
			if (_watch.ElapsedMilliseconds < 30 * 1000)
			{ // We still have ample time, proceed with beam search
				depth = 15;
				beamWidth = 12;
			}
			else
			{ // Time is running out, just simulate one timestep now
				depth = 1;
				beamWidth = 1;
				Console.WriteLine("Over 30s in turn already. Pausing beam search for this turn!");
			}

			_watch.Start();
			PlayerTask move = BeamSearch(game, depth, playerbeamWidth: beamWidth, opponentBeamWidth: 1);
			_watch.Stop();

			if (move.PlayerTaskType == PlayerTaskType.END_TURN)
			{
				_watch.Reset();
			}

			return move;
		}

		private PlayerTask BeamSearch(POGame game, int depth, int playerbeamWidth, int opponentBeamWidth)
		{
			Controller me = game.CurrentPlayer;

			List<HardSimulation> bestSimulations = Simulate(game, playerbeamWidth);
			LabelSimulations(bestSimulations, 0);

			for (int i = 1; i < depth; i++)
			{
				var newBestSimulations = new List<HardSimulation>();
				foreach (HardSimulation sim in bestSimulations)
				{
					int beamWidth = sim.Game.CurrentPlayer.PlayerId == me.PlayerId
						? playerbeamWidth
						: opponentBeamWidth;
					List<HardSimulation> childSims = Simulate(sim.Game, beamWidth);
					LabelSimulations(childSims, i);
					childSims.ForEach(x => x.Parent = sim);
					newBestSimulations.AddRange(childSims);
				}

				bestSimulations = newBestSimulations
					.OrderBy(x => Score(x.Game, me.PlayerId))
					.TakeLast(playerbeamWidth)
					.Reverse()
					.ToList();
			}

			PlayerTask nextMove = bestSimulations.Any()
				? bestSimulations.First().GetFirstTask()
				: me.Options().First(x => x.PlayerTaskType == PlayerTaskType.END_TURN);

			return nextMove;
		}


		private List<HardSimulation> Simulate(POGame game, int numSolutions)
		{
			var simulations = game
				.Simulate(game.CurrentPlayer.Options()).Where(x => x.Value != null)
				.Select(x => new HardSimulation
				{ Task = x.Key, Game = x.Value, Score = Score(x.Value, game.CurrentPlayer.PlayerId) })
				.OrderBy(x => x.Score)
				.TakeLast(numSolutions)
				.Reverse() // Best task first
				.ToList();

			return simulations;
		}

		// Calculate different scores based on our hero's class
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

		private void LabelSimulations(List<HardSimulation> simulations, int currentDepth)
		{
			for (int i = 0; i < simulations.Count; i++)
			{
				simulations[i].Label = currentDepth + "-" + i;
			}
		}


		public override void InitializeAgent()
		{
		}

		public override void InitializeGame()
		{
			_watch = new Stopwatch();
		}

		public override void FinalizeGame()
		{
		}

		public override void FinalizeAgent()
		{
		}
	}

	class HardSimulation
	{
		public PlayerTask Task { get; set; }
		public POGame Game { get; set; }
		public int Score { get; set; }
		public HardSimulation Parent { get; set; }
		public string Label { get; set; } = "<missing>";

		public PlayerTask GetFirstTask()
		{
			return Parent == null ? Task : Parent.GetFirstTask();
		}

		public string GetQualifiedLabel()
		{
			return Parent == null ? Label : Parent.GetQualifiedLabel() + " ==> " + Label;
		}

		public override string ToString()
		{
			return $"{nameof(Task)}: {Task}, {nameof(Score)}: {Score}";
		}
	}
}
