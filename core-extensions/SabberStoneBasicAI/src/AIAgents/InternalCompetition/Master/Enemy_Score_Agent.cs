using System;
using System.Collections.Generic;
using SabberStoneCore.Tasks.PlayerTasks;
using SabberStoneBasicAI.PartialObservation;
using SabberStoneCore.Enums;
using System.Linq;
using SabberStoneBasicAI.Score;
using SabberStoneCore.Model.Entities;

//Submission for Master
namespace SabberStoneBasicAI.AIAgents.BetterGreedyBot
{
	class MyAgentMiller : AbstractAgent
	{

		public override void InitializeAgent()
		{

		}

		public override void FinalizeAgent()
		{
		}

		public override void FinalizeGame()
		{
		}

		public override PlayerTask GetMove(POGame poGame)
		{
			Controller player = poGame.CurrentPlayer;

			// Get all simulation results for simulations that didn't fail
			IEnumerable<KeyValuePair<PlayerTask, POGame>> validOpts = poGame.Simulate(player.Options()).Where(x => x.Value != null);

			// If all simulations failed, play end turn option (always exists), else best according to score function
			return validOpts.Any() ?
				validOpts.OrderBy(x => Score(x.Value, player.PlayerId)).Last().Key :
				player.Options().First(x => x.PlayerTaskType == PlayerTaskType.END_TURN);
		}

		private int Score(POGame state, int playerId)
		{
			Controller p = state.CurrentPlayer.PlayerId == playerId ? state.CurrentPlayer : state.CurrentOpponent;
			return new BetterScore { Controller = p }.Rate(state.Turn);
		}

		public override void InitializeGame()
		{

		}
	}

	class BetterScore : Score.Score
	{
		public int Rate(int turnCount)
		{
			if (OpHeroHp < 1)
				return Int32.MaxValue;

			if (HeroHp < 1)
				return Int32.MinValue;

			int result = 0;

			// calcluate value of the players board
			double playerVal = 0.0;

			playerVal += 2 * Math.Sqrt(HeroHp);

			// give incentive to keep cards in hand
			if (HandCnt <= 3)
				playerVal += HandCnt * 3;
			//but dont overdo it
			else
				playerVal += 9 + (HandCnt - 3) * 2;

			foreach (Minion minion in BoardZone)
			{
				playerVal += (minion.Health + minion.AttackDamage);
				if (minion.HasBattleCry || minion.HasDeathrattle || minion.HasDivineShield || minion.HasInspire || minion.HasStealth)
					playerVal += 3;
				if (minion.HasTaunt)
					playerVal += 3 * minion.Health;
				if (minion.HasCharge || minion.HasLifeSteal || minion.HasWindfury)
					playerVal += 3 * minion.AttackDamage;
			}

			if (OpBoardZone.Count == 0)
				playerVal += 2 + Math.Min(10, turnCount);



			// calcluate value for enemy

			double enemyVal = 0.0;

			enemyVal += 2 * Math.Sqrt(OpHeroHp);

			if (OpHandCnt <= 3)
				enemyVal += OpHandCnt * 3;
			else
				enemyVal += 9 + (OpHandCnt - 3) * 2;

			enemyVal += OpDeckCnt > 0 ? Math.Sqrt(OpDeckCnt) : -3;

			foreach (Minion minion in OpBoardZone)
			{
				enemyVal += (minion.Health + minion.AttackDamage);
				if (minion.HasBattleCry || minion.HasDeathrattle || minion.HasDivineShield || minion.HasInspire || minion.HasStealth)
					enemyVal += 3;
				if (minion.HasTaunt)
					enemyVal += 3 * minion.Health;
				if (minion.HasCharge || minion.HasLifeSteal || minion.HasWindfury)
					enemyVal += 3 * minion.AttackDamage;
			}

			if (BoardZone.Count == 0)
				enemyVal += 2 + Math.Min(10, turnCount);

			// score value is the difference between player value and bot value calculated above
			result = (int)(playerVal - enemyVal);

			return result;
		}
		public override Func<List<IPlayable>, List<int>> MulliganRule()
		{
			return p => p.Where(t => t.Cost > 3).Select(t => t.Id).ToList();
		}
	}
}
