using System;
using System.Linq;
using SabberStoneCore.Enums;
using SabberStoneBasicAI.Meta;
using SabberStoneBasicAI.Score;
using System.Collections.Generic;
using SabberStoneBasicAI.PartialObservation;
using SabberStoneCore.Tasks.PlayerTasks;
using SabberStoneCore.Model.Entities;

// Developed by Christian Wustrau
namespace SabberStoneBasicAI.AIAgents
{
	class HarderRollingHorizon : AbstractAgent
	{
		private int score = -1;

		public override void FinalizeAgent()
		{
		}

		public override void FinalizeGame()
		{
		}

		public override PlayerTask GetMove(POGame game)
		{
			Controller player = game.CurrentPlayer;

			if (score == -1)
			{
				determineScore(player);
			}

			if (player.MulliganState == Mulligan.INPUT)
			{
				List<int> mulligan = new HarderMidRangeScore().MulliganRule().Invoke(player.Choice.Choices.Select(p => game.getGame().IdEntityDic[p]).ToList());
				return ChooseTask.Mulligan(player, mulligan);
			}
			//Console.WriteLine("Beginne Auszurollen");
			PlayerTask move = GetBestMove(game);
			if (move == null)
			{
				move = game.CurrentPlayer.Options()[0];
			}
			//Console.WriteLine("Spiele");
			return move;
		}

		public PlayerTask GetBestMove(POGame game)
		{
			Dictionary<PlayerTask, POGame> SimulationData = SimulateGame(game);
			PlayerTask BestMove = EvaluateSimulation(SimulationData, game);
			return BestMove;
		}

		public Dictionary<PlayerTask, POGame> SimulateGame(POGame game)
		{
			Dictionary<PlayerTask, POGame> Simulation = game.Simulate(game.CurrentPlayer.Options());
			return Simulation;
		}

		public PlayerTask EvaluateSimulation(Dictionary<PlayerTask, POGame>  SimulationData, POGame game)
		{
			//TODO Simulate for the best 5, the next best horizon to determin a winner there
			PlayerTask BestTask = null;
			int BestTempo = -10000;
			foreach (KeyValuePair<PlayerTask, POGame> task in SimulationData)
			{
				if (task.Key.PlayerTaskType == PlayerTaskType.END_TURN)
				{
					continue;
				}
				int Tempo = CalculateTempo(task.Value.CurrentPlayer);
				/*Console.WriteLine("Evaluating task:");
				Console.WriteLine(task.Key);
				Console.WriteLine("Game looks like this:");
				Console.WriteLine(task.Value.FullPrint());
				Console.WriteLine("Computed Tempo:");
				Console.WriteLine(Tempo);*/
				if ( Tempo > BestTempo)
				{
					BestTempo = Tempo;
					BestTask = task.Key;
				}
			}
			/*Console.WriteLine("Playing:");
			if (BestTask == null)
			{
				Console.WriteLine("End Turn");
			}
			else
			{
				Console.WriteLine(BestTask);
			}*/
				
			return BestTask;
		}

		public int CalculateTempo(Controller player)
		{

			switch (score)
			{
				case 0:
					return new HarderAggroScore { Controller = player }.Rate();
				case 1:
					return new HarderMidRangeScore { Controller = player }.Rate();
				case 2:
					return new HarderControlScore { Controller = player }.Rate();
				default:
					return new HarderMidRangeScore { Controller = player }.Rate();
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
						score = 1;
					}
					else
					{
						score = 0;
					}
					break;
				case CardClass.HUNTER:
					score = 0;
					break;
				case CardClass.MAGE:
					score = 2;
					break;
				case CardClass.PALADIN:
					score = 1;
					break;
				case CardClass.PRIEST:
					score = 2;
					break;
				case CardClass.ROGUE:
					score = 0;
					break;
				case CardClass.SHAMAN:
					if (Decks.MidrangeJadeShaman.Contains(player.DeckZone[0].Card) &
						Decks.MidrangeJadeShaman.Contains(player.DeckZone[1].Card) &
						Decks.MidrangeJadeShaman.Contains(player.DeckZone[2].Card))
					{
						score = 1;
					}
					else
					{
						score = 0;
					}
					break;
				case CardClass.WARLOCK:
					score = 0;
					break;
				case CardClass.WARRIOR:
					if (Decks.AggroPirateWarrior.Contains(player.DeckZone[0].Card) &
						Decks.AggroPirateWarrior.Contains(player.DeckZone[1].Card) &
						Decks.AggroPirateWarrior.Contains(player.DeckZone[2].Card))
					{
						score = 0;
					}
					else
					{
						score = 2;
					}
					break;
				default:
					score = -2;
					break;
			}
		}

		public override void InitializeAgent()
		{
		}

		public override void InitializeGame()
		{
		}
	}
}
