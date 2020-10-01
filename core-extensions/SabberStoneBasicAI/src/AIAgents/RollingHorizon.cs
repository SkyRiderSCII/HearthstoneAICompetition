using System;
using System.Collections.Generic;
using SabberStoneBasicAI.PartialObservation;
using SabberStoneCore.Tasks.PlayerTasks;


// Developed by Christian Wustrau
namespace SabberStoneBasicAI.AIAgents
{
	class RollingHorizon : AbstractAgent
	{
		private Random Rnd = new Random();

		public override void FinalizeAgent()
		{
		}

		public override void FinalizeGame()
		{
		}

		public override PlayerTask GetMove(POGame game)
		{
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
			PlayerTask BestMove = null;
			Dictionary<PlayerTask, POGame> SimulationData = SimulateGame(game);
			BestMove = EvaluateSimulation(SimulationData, game);
			return BestMove;
		}

		public Dictionary<PlayerTask, POGame> SimulateGame(POGame game)
		{
			Dictionary<PlayerTask, POGame> Simulation = game.Simulate(game.CurrentPlayer.Options());
			return Simulation;
		}

		public PlayerTask EvaluateSimulation(Dictionary<PlayerTask, POGame>  SimulationData, POGame game)
		{
			PlayerTask BestTask = null;
			double BestTempo = CalculateTempo(game);
			foreach (KeyValuePair<PlayerTask, POGame> task in SimulationData)
			{
				if (task.Key.PlayerTaskType == PlayerTaskType.END_TURN)
				{
					continue;
				}
				double Tempo = CalculateTempo(task.Value);
				if ( Tempo > BestTempo)
				{
					BestTempo = Tempo;
					BestTask = task.Key;
				}
			}
			return BestTask;
		}

		public double CalculateTempo(POGame game)
		{
			if(game == null)
			{
				return -999;
			}
			if(game.CurrentOpponent.Hero.Health == 0)
			{
				return 999;
			}
			if (game.CurrentPlayer.Hero.Health == 0)
			{
				return -999;
			}

			double Alpha = 0.2; double Beta = 0.6; double Gamma = 0; double Delta = 0.7; double Epsilon = 0; double Zeta = 0;
			double Theta = 0.3; double Iota = 0.1;

			if (game.CurrentPlayer.HeroClass == SabberStoneCore.Enums.CardClass.MAGE)
			{
				Alpha = 0.4; Beta = 0.2; Gamma = 0.1; Delta = 0.35; Epsilon = 0.2; Zeta = 0.2; Theta = 0.35; Iota = 0;
			} else if (game.CurrentPlayer.HeroClass == SabberStoneCore.Enums.CardClass.SHAMAN)
			{
				Alpha = 0.4; Beta = 0.2; Gamma = 0; Delta = 0.35; Epsilon = 0.1; Zeta = 0; Theta = 0.35; Iota = 0.1;
			}else if (game.CurrentPlayer.HeroClass == SabberStoneCore.Enums.CardClass.WARRIOR &&
					  game.CurrentOpponent.HeroClass == SabberStoneCore.Enums.CardClass.WARRIOR)
			{
				Alpha = 0.4; Beta = 0.2; Gamma = 0; Delta = 0.35; Epsilon = 0; Zeta = 0; Theta = 0.35; Iota = 0.05;
			}

			double OpponentBoardValue = 0;
			for (int i = 0; i < game.CurrentOpponent.BoardZone.Count; ++i)
			{
				OpponentBoardValue += Alpha * game.CurrentOpponent.BoardZone[i].AttackDamage;
				OpponentBoardValue += Alpha * game.CurrentOpponent.BoardZone[i].Health;
				if(game.CurrentOpponent.Hero.Weapon != null)
				{
					OpponentBoardValue += 0 * game.CurrentOpponent.Hero.Weapon.AttackDamage;
				}
			}
			double OpponentTempo = Beta * (game.CurrentOpponent.Hero.Health + game.CurrentOpponent.Hero.Armor ) + OpponentBoardValue + Gamma * game.CurrentOpponent.HandZone.Count;

			double PlayerBoardValue = 0;
			for (int i = 0; i < game.CurrentPlayer.BoardZone.Count; ++i)
			{
				PlayerBoardValue += Delta * game.CurrentPlayer.BoardZone[i].AttackDamage;
				PlayerBoardValue += Theta * game.CurrentPlayer.BoardZone[i].Health;
				if (game.CurrentPlayer.Hero.Weapon != null)
				{
					PlayerBoardValue += Iota * game.CurrentPlayer.Hero.Weapon.AttackDamage;
				}
			}
			double PlayerTempo = Epsilon * (game.CurrentPlayer.Hero.Health + game.CurrentPlayer.Hero.Armor) + PlayerBoardValue + Zeta * game.CurrentPlayer.HandZone.Count; ;

			double Tempo = PlayerTempo - OpponentTempo;

			return Tempo;
		}

		public override void InitializeAgent()
		{
		}

		public override void InitializeGame()
		{
		}
	}
}
