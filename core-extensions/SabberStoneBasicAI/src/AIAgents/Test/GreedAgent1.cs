using SabberStoneBasicAI.AIAgents;
using SabberStoneBasicAI.Meta;
using System;
using System.Collections.Generic;
using System.Text;

namespace SabberStoneAICompetition.src.AIAgents.Test
{
	class GreedyAgent1 : GreedyAgent
	{
		public GreedyAgent1()
		{
			preferedDeck = Decks.AggroPirateWarrior;
			preferedHero = SabberStoneCore.Enums.CardClass.WARRIOR;
		}
	}

	class GreedyAgent2 : GreedyAgent
	{
		public GreedyAgent2()
		{
			preferedDeck = Decks.MidrangeJadeShaman;
			preferedHero = SabberStoneCore.Enums.CardClass.SHAMAN;
		}
	}

	class GreedyAgent3 : GreedyAgent
	{
		public GreedyAgent3()
		{
			preferedDeck = Decks.RenoKazakusMage;
			preferedHero = SabberStoneCore.Enums.CardClass.MAGE;
		}
	}

	class GreedyAgent4 : GreedyAgent
	{
		public GreedyAgent4()
		{
			preferedDeck = Decks.MidrangeBuffPaladin;
			preferedHero = SabberStoneCore.Enums.CardClass.PALADIN;
		}
	}

	class GreedyAgent5 : GreedyAgent
	{
		public GreedyAgent5()
		{
			preferedDeck = Decks.MiraclePirateRogue;
			preferedHero = SabberStoneCore.Enums.CardClass.ROGUE;
		}
	}

	class GreedyAgent6 : GreedyAgent
	{
		public GreedyAgent6()
		{
			preferedDeck = Decks.ZooDiscardWarlock;
			preferedHero = SabberStoneCore.Enums.CardClass.WARLOCK;
		}
	}
}
