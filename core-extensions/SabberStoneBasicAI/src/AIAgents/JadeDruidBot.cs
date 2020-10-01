using System;
using System.Collections.Generic;
using SabberStoneCore.Tasks.PlayerTasks;
using SabberStoneBasicAI.PartialObservation;
using SabberStoneCore.Model;
using SabberStoneBasicAI.AIAgents;
using SabberStoneCore.Enums;
using SabberStoneCoreAi.LarsScore;
using SabberStoneCore.Model.Entities;
using System.Linq;


// Developed by Lars Wagner
namespace SabberStoneCoreAi.Agent
{
	class JadeDruidBot : AbstractAgent
	{
		private Random Rnd = new Random();
		private int countFlamestrike;
		List<PlayHistoryEntry> oppPlayedCards = new List<PlayHistoryEntry>();

		int _rewardOppNoBoard = 0;
		int _rewardOppNoHand = 0;
		int _rewardBoardDiff = 0;
		int _rewardTauntDiff = 0;
		int _rewardHeroDiff = 0;
		int _rewardHandDiff = 0;
		int _rewardDeckDiff = 0;
		int _rewardMinAttDiff = 0;
		int _rewardMinHealthDiff = 0;
		int _rewardHeroAttDiff = 0;
		int _rewardHandCost = 0;

		bool methodSwitch = true;
		

		public JadeDruidBot(bool option)
		{
			methodSwitch = option;
		}

		public JadeDruidBot() : this(268, 859, -206, 99, 426, 734, -988, 548, 461, -907, 813){
            preferedDeck = JadeDruidA;
            preferedHero = CardClass.DRUID;
		}

		public static List<Card> JadeDruidA => new List<Card>()
        {
            Cards.FromName("Jade Idol"),
            Cards.FromName("Jade Idol"),
            Cards.FromName("Lesser Jasper Spellstone"),
            Cards.FromName("Lesser Jasper Spellstone"),
            Cards.FromName("Wild Growth"),
            Cards.FromName("Wild Growth"),
            Cards.FromName("Wrath"),
            Cards.FromName("Wrath"),
            Cards.FromName("Jade Blossom"),
            Cards.FromName("Jade Blossom"),
            Cards.FromName("Fandral Staghelm"),
            Cards.FromName("Ironwood Golem"),
            Cards.FromName("Ironwood Golem"),
            Cards.FromName("Oaken Summons"),
            Cards.FromName("Oaken Summons"),
            Cards.FromName("Swipe"),
            Cards.FromName("Swipe"),
            Cards.FromName("Nourish"),
            Cards.FromName("Nourish"),
            Cards.FromName("Jade Behemoth"),
            Cards.FromName("Jade Behemoth"),
            Cards.FromName("Spreading Plague"),
            Cards.FromName("Spreading Plague"),
            Cards.FromName("Malfurion the Pestilent"),
            Cards.FromName("Ultimate Infestation"),
            Cards.FromName("Ultimate Infestation"),
            Cards.FromName("Arcane Tyrant"),
            Cards.FromName("Arcane Tyrant"),
            Cards.FromName("Aya Blackpaw"),
            Cards.FromName("Kun the Forgotten King")
        };

		public JadeDruidBot(int rewardOppNoBoard, int rewardOppNoHand, int rewardBoardDiff, int rewardTauntDiff, int rewardHeroDiff, int rewardHandDiff,
							int rewardDeckDiff, int rewardMinAttDiff, int rewardMinHealthDiff, int rewardHeroAttDiff, int rewardHandCost)
		{
			_rewardOppNoBoard = rewardOppNoBoard;
			_rewardOppNoHand = rewardOppNoHand;
			_rewardBoardDiff = rewardBoardDiff;
			_rewardTauntDiff = rewardTauntDiff;
			_rewardHeroDiff = rewardHeroDiff;
			_rewardHandDiff = rewardHandDiff;
			_rewardDeckDiff = rewardDeckDiff;
			_rewardMinAttDiff = rewardMinAttDiff;
			_rewardMinHealthDiff = rewardMinHealthDiff;
			_rewardHeroAttDiff = rewardHeroAttDiff;
		}

		public override void FinalizeAgent()
		{
		}

		public override void FinalizeGame()
		{
			countFlamestrike = 0;
		}

		public override PlayerTask GetMove(POGame poGame)
		{
			List<int> values = new List<int>();
			List<PlayerTask> turnList = new List<PlayerTask>();

			PlayerTask bestOption = null;
			int bestValue = int.MinValue;

			JadeScore jade = new JadeScore();

			Controller control = poGame.CurrentPlayer;

			Dictionary<PlayerTask, POGame> simulated = poGame.Simulate(control.Options());

			if (control.MulliganState == Mulligan.INPUT)
			{
				List<int> mulligan = jade.MulliganRule().Invoke(control.Choice.Choices.Select(p => poGame.getGame().IdEntityDic[p]).ToList());
				return ChooseTask.Mulligan(control, mulligan);
			}

			if (control.Options().Count == 1)
				return control.Options()[0];

			oppPlayedCards = poGame.CurrentOpponent.PlayHistory;

			foreach (PlayHistoryEntry h in oppPlayedCards)
			{
				if (h.SourceCard.Name == "Flamestrike")
				{
					countFlamestrike = 1;
				}

			}

			foreach (PlayerTask k in simulated.Keys)
			{

				if (k.PlayerTaskType == PlayerTaskType.END_TURN)
					continue;


				if (poGame.CurrentPlayer.BoardZone.Count >= 4 && k.PlayerTaskType == PlayerTaskType.PLAY_CARD && k.Source.Card.Type == CardType.MINION)
						continue;
				
				if (poGame.CurrentOpponent.HeroClass == CardClass.MAGE && poGame.CurrentOpponent.BaseMana >= 7 &&
					countFlamestrike == 0 && k.PlayerTaskType == PlayerTaskType.PLAY_CARD && k.Source.Card.Type == CardType.MINION && k.Source.Card[GameTag.HEALTH] <= 4 && poGame.CurrentPlayer.BoardZone.Count >= 2)
				{
					continue;
				}

				//controller of simulated option
				control = simulated.First(x => x.Key == k).Value?.CurrentPlayer;

				if (control == null)
					continue;

				//set controller on rating function
				//controlScore.Controller = control;
				jade.Controller = control;

				//rate current option
				var currentValue = jade.BetterRate( _rewardOppNoBoard, _rewardOppNoHand, _rewardBoardDiff, _rewardTauntDiff, _rewardHeroDiff,
													_rewardHandDiff, _rewardDeckDiff, _rewardMinAttDiff, _rewardMinHealthDiff, _rewardHeroAttDiff, _rewardHandCost);

				if (bestValue < currentValue)
				{
					bestValue = currentValue;
					bestOption = k;
				}
			}

			//debug(turnList, values, bestOption, bestValue, poGame);

			return bestOption ??
				   (bestOption = poGame.CurrentPlayer.Options().Find(x => x.PlayerTaskType == PlayerTaskType.END_TURN));
		}

		public override void InitializeAgent()
		{
			Rnd = new Random();
		}

		public override void InitializeGame()
		{
			countFlamestrike = 0;
		}

		public void debug(List<PlayerTask> options, List<int> values, PlayerTask bestOption, int bestValue, POGame poGame)
		{
			foreach (PlayerTask task in options)
			{
				Console.WriteLine("Current Turn Options: " + task.FullPrint());
			}

			Console.WriteLine("\n\n");

			foreach (int value in values)
			{
				Console.WriteLine("Current Turn Options values: " + value);
			}

			Console.WriteLine("\n\n");

			Console.WriteLine("Option: " + bestOption.FullPrint() + "     Value: " + bestValue);

			Console.WriteLine("My health: " + poGame.CurrentPlayer.Hero.Health);
			Console.WriteLine("Opponent health: " + poGame.CurrentOpponent.Hero.Health);
		}
	}
}
