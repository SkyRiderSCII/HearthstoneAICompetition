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
using SabberStoneCore.Model.Entities;

namespace SabberStoneBasicAI.Score
{
	public class HarderMidRangeScore : Score2
	{
		public override int Rate()
		{
			if (OpHeroHp < 1)
				return Int32.MaxValue;

			if (HeroHp < 1)
				return Int32.MinValue;

			int result = 0;

			int BoardValue = Convert.ToInt32(25 * Math.Log10(MinionTotAtk + 1) + 15 * Math.Log10(MinionTotHealth + 1));
			int OpBoardValue = Convert.ToInt32(30 * Math.Log10(OpMinionTotAtk + 1) + 20 * Math.Log10(OpMinionTotHealth + 1));

			result += 2 * (BoardValue - OpBoardValue);

			result += Spellpower;
			result += 2 * MinionTotHealthTaunt;
			result += -1 * OpMinionTotHealthTaunt;
			result += MinionToAttackCharge;
			result += MinionToHealthDeathrattle;
			result += MinionToHealthDivineShield;
			result += MinionToAttackLifeSteal;
			result += MinionToAttackOverkill;
			result += MinionToAttackWindfury;
			result += OpMinionToAttackFreeze;
			result += MinionToAttackPoisonous;

			result += 2 * (HeroHp + HeroArmor - OpHeroHp - OpHeroArmor);
			result += (HeroAtk - OpHeroAtk);
			result += (HeroMana - OpMana);
			result += TempMana;

			result += 2 * (HandCnt - OpHandCnt) + 2 * (DeckCnt - OpDeckCnt) + 2 * (BoardCnt - OpBoardCnt);
			
			return result;
		}

		public override Func<List<IPlayable>, List<int>> MulliganRule()
		{
			return p => p.Where(t => t.Cost > 3).Select(t => t.Id).ToList();
		}
	}
}
