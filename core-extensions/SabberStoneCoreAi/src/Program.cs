using System;
using System.Collections.Generic;
using SabberStoneCore.Config;
using SabberStoneCore.Enums;
using SabberStoneCore.Tasks;
using SabberStoneCoreAi.POGame;
using SabberStoneCoreAi.Agent.ExampleAgents;
using SabberStoneCoreAi.Agent;
using SabberStoneCoreAi.Meta;
using SabberStoneCoreAi.src;
using SabberStoneCore.Model;
using System.IO;
using System.Diagnostics;
using System.Linq;

namespace SabberStoneCoreAi
{
	internal class Program
	{
		static string fileSpecifier = "Shaman";

		private static void Main(string[] args)
		{
			int numberOfIndividuals = 60;
			int numberOfIterations = 3;
			int numberOfGames = 50;
			Random rnd = new Random();

			EvoAlg training = new EvoAlg();
			Individual[] trainingIndis = training.initializeTrainRun(numberOfIndividuals, rnd);

			for(int i = 0; i < numberOfIterations; i++)
			{
				Console.WriteLine("Iteration: " + (i + 1));

				string toLog = "\n\nIteration: " + (i + 1) + "\n";

				using (StreamWriter w = File.AppendText("test"+fileSpecifier+".txt"))
				{
					Log(toLog, w);
				}

				using (StreamWriter w = File.AppendText("best"+fileSpecifier+".txt"))
				{
					Log(toLog, w);
				}

				trainingIndis = TrainSetup(trainingIndis, numberOfGames);
				trainingIndis = CutWeight(trainingIndis);
				Individual[] childrens = training.Crossover(trainingIndis);
				childrens = training.Mutation(childrens, rnd);
				childrens = TrainSetup(childrens, numberOfGames);
				trainingIndis = training.Selection(trainingIndis, childrens);

				if (i == numberOfIterations - 1)
				{
					string best = "Training completed!\n\nBest individuals follows:\n\n";

					for (int j = 0; j < trainingIndis.Length / 5; j++)
					{
						best = "Win Rate: " + trainingIndis[j].getWeight(11) + " | Index: " + trainingIndis[j].getIndex() + " | Weights:";

						for (int k = 0; k < trainingIndis[j].getWeightsLength(); k++)
						{
							best += " " + trainingIndis[j].getWeight(k);
						}

						using (StreamWriter w = File.AppendText("best" + fileSpecifier + ".txt"))
						{
							Log(best, w);
						}
					}

				}

				trainingIndis = CutWeight(trainingIndis);
			}
		}

		//simple setup to run simulation with a specific agent for a number of runs
		private static void Setup1 (int runs, AbstractAgent agent)
		{
			Console.WriteLine("Test Mage-Deck\n");
			PlayMageSetup(runs, agent);
			//Console.WriteLine("Test Warrior-Deck\n");
			//PlayWarriorSetup(runs, agent);
			//Console.WriteLine("Test Shaman-Deck\n");
			//PlayShamanSetup(runs, agent);
		}

		//setup which does not play around board clears
		private static void Setup2 (int runs)
		{
			Console.WriteLine("Test Mage-Deck\n");
			PlayMageSetup(runs, new GreedyAgent());
			//Console.WriteLine("Test Warrior-Deck\n");
			//PlayWarriorSetup(runs, new MyAgent(true));
			//Console.WriteLine("Test Shaman-Deck\n");
			//PlayShamanSetup(runs, new MyAgent(true));
		}

		//setup for current Jade Druid deck
		private static void SetupDeck()
		{
			//Console.WriteLine("Test Priest-Deck\n");
			//PlayPriestSetup(100, new JadeDruidBot( -658, 350, 103, -905, -197, 135, -967, 669, 491, 415));
			Console.WriteLine("First Optimization\n");
			//PlayPriestSetup(1000, new JadeDruidBot( -794, 522, 656, 351, 619, -20, 301, 891, 243, 897, 0));

			//Console.WriteLine("\nBest of 9000 Games\n");
			//PlayPriestSetup(1000, new JadeDruidBot( 961, -797, 453, 706, 751, 992, 904, 752, 959, 758));
		}

		private static void TestWeights(int numberOfGames, AbstractAgent player1, AbstractAgent player2)
		{
			Console.WriteLine("Setup gameConfig");

			//todo: rename to Main
			GameConfig gameConfig = new GameConfig
			{
				StartPlayer = 1,
				Player1HeroClass = CardClass.DRUID,
				Player2HeroClass = CardClass.DRUID,
				Player1Deck = Decks.JadeDruidA,
				Player2Deck = Decks.JadeDruidA,
				FillDecks = false,
				Logging = false
			};

			Console.WriteLine("Setup POGameHandler");
			var gameHandler = new POGameHandler(gameConfig, player1, player2, repeatDraws: false);

			//Console.WriteLine("PlayGame against Warrior");
			//gameHandler.PlayGame();
			gameHandler.PlayGames(numberOfGames);
			GameStats gameStats = gameHandler.getGameStats();

			gameStats.printResults();
		}

		private static void PlayMageSetup(int n, AbstractAgent player1)
		{
			Console.WriteLine("Setup gameConfig");

			//todo: rename to Main
			GameConfig gameConfig = new GameConfig
			{
				StartPlayer = 1,
				Player1HeroClass = CardClass.DRUID,
				Player2HeroClass = CardClass.WARRIOR,
				Player1Deck = Decks.JadeDruidA,
				Player2Deck = Decks.AggroPirateWarrior,
				FillDecks = false,
				Logging = false
			};

			Console.WriteLine("Setup POGameHandler");
			AbstractAgent player2 = new MyAgent();
			var gameHandler = new POGameHandler(gameConfig, player1, player2, repeatDraws: false);

			Console.WriteLine("PlayGame against Warrior");
			//gameHandler.PlayGame();
			gameHandler.PlayGames(n);
			GameStats gameStats = gameHandler.getGameStats();

			gameStats.printResults();

			Console.WriteLine("\n\n");

			gameConfig.Player2Deck = Decks.RenoKazakusMage;
			gameConfig.Player2HeroClass = CardClass.MAGE;

			gameHandler = new POGameHandler(gameConfig, player1, player2, repeatDraws: false);

			Console.WriteLine("PlayGame against Mage");
			//gameHandler.PlayGame();
			gameHandler.PlayGames(n);
			gameStats = gameHandler.getGameStats();

			gameStats.printResults();

			Console.WriteLine("\n\n");

			gameConfig.Player2Deck = Decks.MidrangeJadeShaman;
			gameConfig.Player2HeroClass = CardClass.SHAMAN;

			gameHandler = new POGameHandler(gameConfig, player1, player2, repeatDraws: false);

			Console.WriteLine("PlayGame against Shaman");
			//gameHandler.PlayGame();
			gameHandler.PlayGames(n);
			gameStats = gameHandler.getGameStats();

			gameStats.printResults();

			//Console.WriteLine("\n\n");

			//gameConfig.Player2HeroClass = CardClass.WARRIOR;
			//gameConfig.FillDecks = true;

			//gameHandler = new POGameHandler(gameConfig, player1, player2, debug: false);

			//Console.WriteLine("PlayGame with random decks");
			////gameHandler.PlayGame();
			//gameHandler.PlayGames(1000);
			//gameStats = gameHandler.getGameStats();

			//gameStats.printResults();

			Console.WriteLine("Test successful\n\n\n");
		}

		private static void TestSetup()
		{
			Console.WriteLine("Setup gameConfig");

			//todo: rename to Main
			GameConfig gameConfig = new GameConfig
			{
				StartPlayer = 1,
				Player1HeroClass = CardClass.MAGE,
				Player2HeroClass = CardClass.MAGE,
				Player1Deck = Decks.RenoKazakusMage,
				Player2Deck = Decks.RenoKazakusMage,
				FillDecks = false,
				Logging = false
			};

			Console.WriteLine("Setup POGameHandler");
			AbstractAgent player1 = new MyAgent();
			AbstractAgent player2 = new GreedyAgent();
			var gameHandler = new POGameHandler(gameConfig, player1, player2, repeatDraws: false);

			Console.WriteLine("PlayGame against Flamestrike");
			//gameHandler.PlayGame();
			gameHandler.PlayGames(10);
			GameStats gameStats = gameHandler.getGameStats();

			gameStats.printResults();
			Console.ReadLine();
		}

		private static Individual[] TrainSetup(Individual[] individuals, int numberOfGames)
		{
			Random rnd = new Random();
			List<int> bestSolutions = new List<int>();

			foreach (Individual indi in individuals)
			{
				int temp = EvoRunGame(	new JadeDruidBot(indi.getWeight(0), indi.getWeight(1), indi.getWeight(2), indi.getWeight(3), indi.getWeight(4),
										indi.getWeight(5), indi.getWeight(6), indi.getWeight(7), indi.getWeight(8), indi.getWeight(9), indi.getWeight(10)), numberOfGames);

				string toLog = "Win Rate: "+temp+" | Index: "+indi.getIndex()+" | Weights:";

				for (int i = 0; i < indi.getWeightsLength(); i++)
				{
					toLog += " " + indi.getWeight(i);
				}

				using (StreamWriter w = File.AppendText("test" + fileSpecifier + ".txt"))
				{
					Log(toLog, w);
				}

				individuals[indi.getIndex()].addWeight(temp);
				bestSolutions.Add(temp);
			}

			individuals = Sort(individuals);

			string best = "Best Individuals:";

			foreach(int score in bestSolutions)
				best += " "+score;

			using (StreamWriter w = File.AppendText("best" + fileSpecifier + ".txt"))
			{
				Log(best, w);
			}

			return individuals;
		}

		private static int EvoRunGame (AbstractAgent player1, int numberOfGames)
		{
			GameConfig gameConfig = new GameConfig
			{
				StartPlayer = 1,
				Player1HeroClass = CardClass.SHAMAN,
				Player1Deck = Decks.MidrangeJadeShaman,
				FillDecks = false,
				Logging = false
			};

			AbstractAgent player2 = new GreedyAgent();
			int result = 0;

			for (int i = 0; i < 9; i++)
			{
				switch (i)
				{
					case 0:
						gameConfig.Player2HeroClass = CardClass.MAGE;
						gameConfig.Player2Deck = Decks.RenoKazakusMage;
						break;



					case 1:
						gameConfig.Player2HeroClass = CardClass.WARRIOR;
						gameConfig.Player2Deck = Decks.AggroPirateWarrior;
						break;

					case 2:
						gameConfig.Player2HeroClass = CardClass.WARLOCK;
						gameConfig.Player2Deck = Decks.ZooDiscardWarlock;
						break;

					case 3:
						gameConfig.Player2HeroClass = CardClass.PALADIN;
						gameConfig.Player2Deck = Decks.MidrangeBuffPaladin;
						break;

					case 4:
						gameConfig.Player2HeroClass = CardClass.PRIEST;
						gameConfig.Player2Deck = Decks.RenoKazakusDragonPriest;
						break;

					case 5:
						gameConfig.Player2HeroClass = CardClass.SHAMAN;
						gameConfig.Player2Deck = Decks.MidrangeJadeShaman;
						break;

					case 6:
						gameConfig.Player2HeroClass = CardClass.ROGUE;
						gameConfig.Player2Deck = Decks.MiraclePirateRogue;
						break;

					case 7:
						gameConfig.Player2HeroClass = CardClass.DRUID;
						gameConfig.Player2Deck = Decks.MurlocDruid;
						break;

					case 8:
						gameConfig.Player2HeroClass = CardClass.HUNTER;
						gameConfig.Player2Deck = Decks.MidrangeSecretHunter;
						break;

					default: Console.Error.WriteLine("No Match up");
							break;
				}

				POGameHandler gameHandler = new POGameHandler(gameConfig, player1, player2, repeatDraws: false);
				gameHandler.PlayGames(numberOfGames);
				GameStats gameStats = gameHandler.getGameStats();
				result += (int)(gameStats.PlayerA_Wins);
			}

			return result;
		}

		//sort array of individuals in descendant order of fitness
		public static Individual[] Sort(Individual[] population)
		{
			int best = -1;
			Individual bestIndi = new Individual();
			Individual[] temp = new Individual[population.Length];

			for (int i = 0; i < population.Length; i++)
			{
				foreach (Individual indi in population)
				{

					if (indi.getWeight(11) > best)
					{
						best = indi.getWeight(11);
						bestIndi = indi;
					}
				}

				temp[i] = population[bestIndi.getIndex()];
				best = -1;
			}

			return temp;
		}

		public static Individual[] CutWeight(Individual[] population)
		{
			Individual[] temp = population;

			for(int i = 0; i < population.Length; i++)
			{
				temp[i].removeWeight(11);
			}

			return temp;
		}

		public static void Log(string logMessage, TextWriter w)
		{
			w.Write("\r\nLog Entry : ");
			w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
				DateTime.Now.ToLongDateString());
			w.WriteLine("  :{0}", logMessage);
			w.WriteLine("-------------------------------");
		}
	}
}
