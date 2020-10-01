﻿using System;
using System.Collections.Generic;
using System.Linq;
using SabberStoneCore.Model.Zones;
using SabberStoneCore.Model.Entities;
using SabberStoneBasicAI.Score;

namespace SabberStoneCoreAi.LarsScore
{
	public interface IScore
	{
		Controller Controller { get; set; }
		Func<List<IPlayable>, List<int>> MulliganRule();
		int Rate();

		int BetterRate(int rewardOppNoBoard, int rewardOppNoHand, int rewardBoardDiff, int rewardTauntDiff, int rewardHeroDiff, int rewardHandDiff,
						int rewardDeckDiff, int rewardMinAttDiff, int rewardMinHealthDiff, int rewardHeroAttDiff, int rewardHandCost);
	}

	public abstract class LarsScore : Score
	{
		public Controller Controller { get; set; }

		public int HeroHp => Controller.Hero.Health;

		public int OpHeroHp => Controller.Opponent.Hero.Health;

		public int HeroAtk => Controller.Hero.TotalAttackDamage;

		public int OpHeroAtk => Controller.Opponent.Hero.TotalAttackDamage;

		public HandZone Hand => Controller.HandZone;

		public int HandTotCost => Hand.Sum(p => p.Cost);

		public int HandCnt => Controller.HandZone.Count;

		public int OpHandCnt => Controller.Opponent.HandZone.Count;

		public int DeckCnt => Controller.DeckZone.Count;

		public int OpDeckCnt => Controller.Opponent.DeckZone.Count;

		public BoardZone BoardZone => Controller.BoardZone;

		public BoardZone OpBoardZone => Controller.Opponent.BoardZone;

		public int MinionTotAtk => BoardZone.Sum(p => p.AttackDamage);

		public int OpMinionTotAtk => OpBoardZone.Sum(p => p.AttackDamage);

		public int MinionTotHealth => BoardZone.Sum(p => p.Health);

		public int OpMinionTotHealth => OpBoardZone.Sum(p => p.Health);

		public int MinionTotHealthTaunt => BoardZone.Where(p => p.HasTaunt).Sum(p => p.Health);

		public int OpMinionTotHealthTaunt => OpBoardZone.Where(p => p.HasTaunt).Sum(p => p.Health);

		public override int Rate()
		{
			return 0;
		}

		public override Func<List<IPlayable>, List<int>> MulliganRule()
		{
			return p => new List<int>();
		}

		public virtual int BetterRate(int rewardOppNoBoard, int rewardOppNoHand, int rewardBoardDiff, int rewardTauntDiff, int rewardHeroDiff, int rewardHandDiff, int rewardDeckDiff, int rewardMinAttDiff, int rewardMinHealthDiff, int rewardHeroAttDiff, int rewardHandCost)
		{
			throw new NotImplementedException();
		}
	}
}
