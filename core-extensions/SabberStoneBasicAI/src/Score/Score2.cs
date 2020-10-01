#region copyright
// SabberStone, Hearthstone Simulator in C# .NET Core
// Copyright (C) 2017-2019 SabberStone Team, darkfriend77 & rnilva
//
// SabberStone is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as
// published by the Free Software Foundation, either version 3 of the
// License.
// SabberStone is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Affero General Public License for more details.
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using SabberStoneCore.Model.Zones;
using SabberStoneCore.Model.Entities;

namespace SabberStoneBasicAI.Score
{
	public abstract class Score2 : IScore
	{
		public Controller Controller { get; set; }

		public int HeroHp => Controller.Hero.Health;

		public int OpHeroHp => Controller.Opponent.Hero.Health;

		public int HeroArmor => Controller.Hero.Armor;

		public int OpHeroArmor => Controller.Opponent.Hero.Armor;

		public int HeroAtk => Controller.Hero.TotalAttackDamage;

		public int OpHeroAtk => Controller.Opponent.Hero.TotalAttackDamage;

		public HandZone Hand => Controller.HandZone;

		public int HandTotCost => Hand.Sum(p => p.Cost);

		public int HandCnt => Controller.HandZone.Count;

		public int OpHandCnt => Controller.Opponent.HandZone.Count;

		public int DeckCnt => Controller.DeckZone.Count;

		public int OpDeckCnt => Controller.Opponent.DeckZone.Count;

		public int HeroMana => Controller.BaseMana;

		public int OpMana => Controller.Opponent.BaseMana;

		public int Spellpower => Controller.CurrentSpellPower;

		public int TempMana => Controller.TemporaryMana;

		public BoardZone BoardZone => Controller.BoardZone;

		public BoardZone OpBoardZone => Controller.Opponent.BoardZone;

		public int BoardCnt => BoardZone.Count;

		public int OpBoardCnt => OpBoardZone.Count;

		public int MinionTotAtk => BoardZone.Sum(p => p.AttackDamage);

		public int OpMinionTotAtk => OpBoardZone.Sum(p => p.AttackDamage);

		public int MinionTotHealth => BoardZone.Sum(p => p.Health);

		public int OpMinionTotHealth => OpBoardZone.Sum(p => p.Health);

		public int MinionTotHealthTaunt => BoardZone.Where(p => p.HasTaunt).Sum(p => p.Health);

		public int OpMinionTotHealthTaunt => OpBoardZone.Where(p => p.HasTaunt).Sum(p => p.Health);

		public int MinionToAttackCharge => BoardZone.Where(p => p.HasCharge).Sum(p => p.AttackDamage);

		public int MinionToHealthDeathrattle => BoardZone.Where(p => p.HasDeathrattle).Sum(p => p.Health);

		public int MinionToHealthDivineShield => BoardZone.Where(p => p.HasDivineShield).Sum(p => p.Health);

		public int MinionToAttackLifeSteal => BoardZone.Where(p => p.HasLifeSteal).Sum(p => p.AttackDamage);

		public int MinionToAttackOverkill => BoardZone.Where(p => p.HasOverkill).Sum(p => p.AttackDamage);

		public int MinionToHealthStealth => BoardZone.Where(p => p.HasStealth).Sum(p => p.Health);

		public int MinionToAttackWindfury => BoardZone.Where(p => p.HasWindfury).Sum(p => p.AttackDamage);

		public int OpMinionToAttackFreeze => OpBoardZone.Where(p => p.Freeze).Sum(p => p.AttackDamage);

		public int MinionToAttackPoisonous => BoardZone.Where(p => p.Poisonous).Sum(p => p.AttackDamage);

		public virtual int Rate()
		{
			return 0;
		}

		public virtual Func<List<IPlayable>, List<int>> MulliganRule()
		{
			return p => new List<int>();
		}
	}
}
