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
using System.Diagnostics;
using System.Linq;
using SabberStoneCore.Config;
using SabberStoneCore.Enums;
using SabberStoneCore.Model;
using SabberStoneCore.Tasks.PlayerTasks;
using SabberStoneBasicAI.Meta;
using SabberStoneBasicAI.Nodes;
using SabberStoneBasicAI.Score;
using SabberStoneBasicAI.AIAgents;
using SabberStoneBasicAI.PartialObservation;
using SabberStoneBasicAI.CompetitionEvaluation;
using SabberStoneCoreAi.Agent;
using SabberStoneAICompetition.src.AIAgents;
using System.IO;
using SabberStoneAICompetition.src.AIAgents.Test;
using SabberStoneBasicAI.AIAgents.minimax;
using SabberStoneBasicAI.AIAgents.BetaStone;
using SabberStoneBasicAI.AIAgents.submission_tag;
using SabberStoneBasicAI.AIAgents.MaBuSaBu;
using SabberStoneBasicAI.AIAgents.DepthFour_DefenceAgent;
using SabberStoneBasicAI.AIAgents.FinalAgent;
using SabberStoneBasicAI.AIAgents.JoachimKnobi;
using SabberStoneBasicAI.AIAgents.mudgal_kumar;
using SabberStoneBasicAI.AIAgents.MAGEntMann;
using SabberStoneBasicAI.AIAgents.Otto007;
using SabberStoneBasicAI.AIAgents.Clearvoyant_Paladin;
using SabberStoneBasicAI.AIAgents.manuelliebchen;
using SabberStoneBasicAI.AIAgents.CopyCats;
using SabberStoneBasicAI.AIAgents.c_isnt_sharp;
using SabberStoneBasicAI.AIAgents.BotterThanYouThink;
using SabberStoneBasicAI.AIAgents.Costume;
using SabberStoneBasicAI.AIAgents.FrankenStein;
using SabberStoneBasicAI.AIAgents.magic_number;
using SabberStoneBasicAI.AIAgents.BetterGreedyBot;
using SabberStoneBasicAI.AIAgents.TomPeters;

namespace SabberStoneBasicAI
{
	internal class Program
	{
		private static readonly Random Rnd = new Random();

		private static void Main()
		{
			Console.WriteLine("Starting test setup.");

			// TEST BASIC AI

			//OneTurn();
			//FullGame();
			//RandomGames();
			//TestPOGame();
			//TestFullGames();
			TestBachelorTournament();
			//TestMasterTournament();

			//setupMaster();
			//setupBachelor();
			

			Console.WriteLine("Test ended!");
			Console.ReadLine();
		}

		public static void TestBachelorTournament()
		{
			Agent[] agents = new Agent[18
                ];
			agents[0] = new Agent(typeof(JoachimKnobi), "KnobiStelter");
			agents[1] = new Agent(typeof(DrunkenAggroWarriorAgent), "DrunkenWarriorKiasBabel");
			agents[2] = new Agent(typeof(DrunkenAggroWarriorAgent2), "DrunkenWarrior2KiasBabel");
			agents[3] = new Agent(typeof(Otto007Paladin), "OttoPaladinSchmidtDierkes");
			agents[4] = new Agent(typeof(Otto007Warrior), "OttoWarriorSchmidtDierkes");
			agents[5] = new Agent(typeof(MAGEnt), "MAGEntMann");
			agents[6] = new Agent(typeof(MAGEntLookahead), "MAGELookaheadMann");
			//agents[7] = new Agent(typeof(MyAgentLiebchen), "MyAgentLiebchen");
			agents[7] = new Agent(typeof(ThreeTypeDynLooker), "ThreeDynLookerSchotteKrebs");
			agents[8] = new Agent(typeof(TreePala), "TreePalaAlbrechtBerndt");
			agents[9] = new Agent(typeof(MyAgentMohamed), "MyAgentShaabanAbdelrazek");
			agents[10] = new Agent(typeof(MyAgentMohamed2), "MyAgent2ShaabanAbdelrazek");
			agents[11] = new Agent(typeof(LynamicDookaheadAgentV1Bachelor), "LynamicDookaheadBohnhofGraeffe");
			agents[12] = new Agent(typeof(Tree_Search_Agent_Rick_Richter), "TreeSearchRichter");
			agents[13] = new Agent(typeof(Tree_Search_Agent_Rick_Richter_7_30), "TreeSearch2Richter");
			agents[14] = new Agent(typeof(CIsntSharpAgent), "IsntSharpDoNamAlemann");
			agents[15] = new Agent(typeof(ShroudedYmir), "YmirHartwig");
			agents[16] = new Agent(typeof(MechaSMOrcAgent), "MechaAgentPeters");
			agents[17] = new Agent(typeof(MechaSMOrcAgent2), "MechaAgent2Peters");


			CompetitionEvaluation.Deck[] decks = new CompetitionEvaluation.Deck[18];
			decks[0] = new CompetitionEvaluation.Deck(Decks.MidrangeJadeShaman, CardClass.SHAMAN, "Shaman");
			decks[1] = new CompetitionEvaluation.Deck(Decks.AggroPirateWarrior, CardClass.WARRIOR, "Warrior");
			decks[2] = new CompetitionEvaluation.Deck(Decks.AggroPirateWarrior, CardClass.WARRIOR, "Warrior");
			decks[3] = new CompetitionEvaluation.Deck(Decks.MidrangeBuffPaladin, CardClass.PALADIN, "Paladin");
			decks[4] = new CompetitionEvaluation.Deck(Decks.AggroPirateWarrior, CardClass.WARRIOR, "Warrior");
			decks[5] = new CompetitionEvaluation.Deck(Decks.RenoKazakusMage, CardClass.MAGE, "Mage");
			decks[6] = new CompetitionEvaluation.Deck(Decks.RenoKazakusMage, CardClass.MAGE, "Mage");
			//decks[7] = new CompetitionEvaluation.Deck(Decks.MidrangeJadeShaman, CardClass.SHAMAN, "Shaman");
			decks[7] = new CompetitionEvaluation.Deck(Decks.MidrangeJadeShaman, CardClass.SHAMAN, "Shaman");
			decks[8] = new CompetitionEvaluation.Deck(Decks.MidrangeBuffPaladin, CardClass.PALADIN, "Paladin");
			decks[9] = new CompetitionEvaluation.Deck(Decks.RenoKazakusDragonPriest, CardClass.PRIEST, "Priest");
			decks[10] = new CompetitionEvaluation.Deck(Decks.AggroPirateWarrior, CardClass.WARRIOR, "Warrior");
			decks[11] = new CompetitionEvaluation.Deck(Decks.MidrangeJadeShaman, CardClass.SHAMAN, "Shaman");
			decks[12] = new CompetitionEvaluation.Deck(Decks.MidrangeJadeShaman, CardClass.SHAMAN, "Shaman");
			decks[13] = new CompetitionEvaluation.Deck(Decks.MidrangeJadeShaman, CardClass.SHAMAN, "Shaman");
			decks[14] = new CompetitionEvaluation.Deck(Decks.AggroPirateWarrior, CardClass.WARRIOR, "Warrior");
			decks[15] = new CompetitionEvaluation.Deck(Decks.RenoKazakusMage, CardClass.MAGE, "Mage");
			decks[16] = new CompetitionEvaluation.Deck(Decks.MechHunter, CardClass.HUNTER, "Hunter");
			decks[17] = new CompetitionEvaluation.Deck(Decks.MechHunter, CardClass.HUNTER, "Hunter");

			RoundRobinCompetition competition = new RoundRobinCompetition(agents, decks, "InternalCompetitionBachelor.txt");
			competition.CreateTasksBachelor(100);
			competition.startEvaluation(8);

			Console.WriteLine("Total Games Played: " + competition.GetTotalGamesPlayed());
			competition.PrintAgentStats();
		}

		public static void TestMasterTournament()
		{
			Agent[] agents = new Agent[4];
			agents[0] = new Agent(typeof(PickleRick), "PickleRickHempelHeise");
			agents[1] = new Agent(typeof(Jerry), "JerryHempelHeise");
			agents[2] = new Agent(typeof(BetaStone), "BetaStoneKnors");
			agents[3] = new Agent(typeof(BetaStone2), "GreedStoneKnors");
			//agents[4] = new Agent(typeof(AllMe), "AllMeThoms");
			//agents[5] = new Agent(typeof(iWillBeatOpenAIFive), "BeatOpenAiThoms");
			//agents[6] = new Agent(typeof(LynamicDookaheadAgentV1Master), "LynamicDookaheadBohnhofGraeffe");
			//agents[7] = new Agent(typeof(MyTurnDeepLookaheadAgent), "DeepLookaheadBuetnerMurugan");
			//agents[8] = new Agent(typeof(MyTurnLookaheadBalancedAgent), "BalancedAgentBuetnerMurugan");
			//agents[9] = new Agent(typeof(GreedyLookaheadAgent), "GreedyLookaheadMudgalKumar");
			//agents[10] = new Agent(typeof(GreedyLookaheadAgent2), "GreedyLookahead2MudgalKumar");
			//agents[11] = new Agent(typeof(MyAgentJulian), "MyAgentLamprecht");
			//agents[12] = new Agent(typeof(AheadAgent), "AheadAgentTrachtMozheiko");
			//agents[13] = new Agent(typeof(BasicAgent), "BasicAgentTrachtMozheiko");
			//agents[14] = new Agent(typeof(MyAgentMiller), "MyAgentMiller");
			//agents[15] = new Agent(typeof(MyAgentMiller2), "MyAgent2Miller");


			CompetitionEvaluation.Deck[] decks = new CompetitionEvaluation.Deck[9];
			decks[0] = new CompetitionEvaluation.Deck(Decks.RenoKazakusMage, CardClass.MAGE, "Mage");
			decks[1] = new CompetitionEvaluation.Deck(Decks.AggroPirateWarrior, CardClass.WARRIOR, "Warrior");
			decks[2] = new CompetitionEvaluation.Deck(Decks.MidrangeJadeShaman, CardClass.SHAMAN, "Shaman");
			decks[3] = new CompetitionEvaluation.Deck(Decks.MidrangeBuffPaladin, CardClass.PALADIN, "Paladin");
			decks[4] = new CompetitionEvaluation.Deck(Decks.ZooDiscardWarlock, CardClass.WARLOCK, "Warlock");
			decks[5] = new CompetitionEvaluation.Deck(Decks.MiraclePirateRogue, CardClass.ROGUE, "Rogue");
			decks[6] = new CompetitionEvaluation.Deck(Decks.FaceHunter, CardClass.HUNTER, "Hunter");
			decks[7] = new CompetitionEvaluation.Deck(Decks.MidrangeDruid, CardClass.DRUID, "Druid");
			decks[8] = new CompetitionEvaluation.Deck(Decks.RenoKazakusDragonPriest, CardClass.PRIEST, "Priest");

			RoundRobinCompetition competition = new RoundRobinCompetition(agents, decks, "InternalCompetitionMaster.txt");
			competition.CreateTasks(100);
			competition.startEvaluation(8);

			Console.WriteLine("Total Games Played: " + competition.GetTotalGamesPlayed());
			competition.PrintAgentStats();
		}

		public static void TestPOGame()
		{
			Console.WriteLine("Setup gameConfig");
			
			var gameConfig = new GameConfig()
			{
				StartPlayer = 1,
				Player1HeroClass = CardClass.MAGE,
				Player2HeroClass = CardClass.MAGE,
				Player1Deck = Decks.RenoKazakusMage,
				Player2Deck = Decks.RenoKazakusMage,
				FillDecks = false,
				Shuffle = true,
				Logging = false
			};

			Console.WriteLine("Setup POGameHandler");
			AbstractAgent player1 = new GreedyAgent();
			AbstractAgent player2 = new GreedyAgent();
			var gameHandler = new POGameHandler(gameConfig, player1, player2, repeatDraws: false);

			Console.WriteLine("Simulate Games");
			//gameHandler.PlayGame();
			gameHandler.PlayGames(nr_of_games: 1000, addResultToGameStats: true, debug: false);
			GameStats gameStats = gameHandler.getGameStats();

			gameStats.printResults();

			Console.WriteLine("Test successful");
			Console.ReadLine();
		}

		public static void RandomGames()
		{
			int total = 1;
			var watch = Stopwatch.StartNew();

			var gameConfig = new GameConfig()
			{
				StartPlayer = -1,
				Player1Name = "FitzVonGerald",
				Player1HeroClass = CardClass.PALADIN,
				Player1Deck = new List<Card>()
						{
						Cards.FromName("Blessing of Might"),
						Cards.FromName("Blessing of Might"),
						Cards.FromName("Gnomish Inventor"),
						Cards.FromName("Gnomish Inventor"),
						Cards.FromName("Goldshire Footman"),
						Cards.FromName("Goldshire Footman"),
						Cards.FromName("Hammer of Wrath"),
						Cards.FromName("Hammer of Wrath"),
						Cards.FromName("Hand of Protection"),
						Cards.FromName("Hand of Protection"),
						Cards.FromName("Holy Light"),
						Cards.FromName("Holy Light"),
						Cards.FromName("Ironforge Rifleman"),
						Cards.FromName("Ironforge Rifleman"),
						Cards.FromName("Light's Justice"),
						Cards.FromName("Light's Justice"),
						Cards.FromName("Lord of the Arena"),
						Cards.FromName("Lord of the Arena"),
						Cards.FromName("Nightblade"),
						Cards.FromName("Nightblade"),
						Cards.FromName("Raid Leader"),
						Cards.FromName("Raid Leader"),
						Cards.FromName("Stonetusk Boar"),
						Cards.FromName("Stonetusk Boar"),
						Cards.FromName("Stormpike Commando"),
						Cards.FromName("Stormpike Commando"),
						Cards.FromName("Stormwind Champion"),
						Cards.FromName("Stormwind Champion"),
						Cards.FromName("Stormwind Knight"),
						Cards.FromName("Stormwind Knight")
						},
				Player2Name = "RehHausZuckFuchs",
				Player2HeroClass = CardClass.PALADIN,
				Player2Deck = new List<Card>()
						{
						Cards.FromName("Blessing of Might"),
						Cards.FromName("Blessing of Might"),
						Cards.FromName("Gnomish Inventor"),
						Cards.FromName("Gnomish Inventor"),
						Cards.FromName("Goldshire Footman"),
						Cards.FromName("Goldshire Footman"),
						Cards.FromName("Hammer of Wrath"),
						Cards.FromName("Hammer of Wrath"),
						Cards.FromName("Hand of Protection"),
						Cards.FromName("Hand of Protection"),
						Cards.FromName("Holy Light"),
						Cards.FromName("Holy Light"),
						Cards.FromName("Ironforge Rifleman"),
						Cards.FromName("Ironforge Rifleman"),
						Cards.FromName("Light's Justice"),
						Cards.FromName("Light's Justice"),
						Cards.FromName("Lord of the Arena"),
						Cards.FromName("Lord of the Arena"),
						Cards.FromName("Nightblade"),
						Cards.FromName("Nightblade"),
						Cards.FromName("Raid Leader"),
						Cards.FromName("Raid Leader"),
						Cards.FromName("Stonetusk Boar"),
						Cards.FromName("Stonetusk Boar"),
						Cards.FromName("Stormpike Commando"),
						Cards.FromName("Stormpike Commando"),
						Cards.FromName("Stormwind Champion"),
						Cards.FromName("Stormwind Champion"),
						Cards.FromName("Stormwind Knight"),
						Cards.FromName("Stormwind Knight")
						},
				FillDecks = false,
				Shuffle = true,
				SkipMulligan = false,
				Logging = true,
				History = true
			};

			int turns = 0;
			int[] wins = new[] { 0, 0 };
			for (int i = 0; i < total; i++)
			{
				var game = new Game(gameConfig);
				game.StartGame();

				game.Process(ChooseTask.Mulligan(game.Player1, new List<int>()));
				game.Process(ChooseTask.Mulligan(game.Player2, new List<int>()));

				game.MainReady();

				while (game.State != State.COMPLETE)
				{
					List<PlayerTask> options = game.CurrentPlayer.Options();
					PlayerTask option = options[Rnd.Next(options.Count)];
					//Console.WriteLine(option.FullPrint());
					game.Process(option);


				}
				turns += game.Turn;
				if (game.Player1.PlayState == PlayState.WON)
					wins[0]++;
				if (game.Player2.PlayState == PlayState.WON)
					wins[1]++;
				Console.WriteLine("game ended");
				// Console.Write(game.PowerHistory.ToString());
				using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"powerhistory.log")) {
							file.WriteLine(game.PowerHistory.Print());
				}
				using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"logger.log"))
				{
					foreach (LogEntry log in game.Logs)
					{
						file.WriteLine(log.ToString());
					}
				}
			}

			watch.Stop();

			Console.WriteLine($"{total} games with {turns} turns took {watch.ElapsedMilliseconds} ms => " +
							  $"Avg. {watch.ElapsedMilliseconds / total} per game " +
							  $"and {watch.ElapsedMilliseconds / (total * turns)} per turn!");
			Console.WriteLine($"playerA {wins[0] * 100 / total}% vs. playerB {wins[1] * 100 / total}%!");
		}

		public static void OneTurn()
		{
			var game = new Game(
				new GameConfig()
				{
					StartPlayer = 1,
					Player1Name = "FitzVonGerald",
					Player1HeroClass = CardClass.WARRIOR,
					Player1Deck = Decks.AggroPirateWarrior,
					Player2Name = "RehHausZuckFuchs",
					Player2HeroClass = CardClass.SHAMAN,
					Player2Deck = Decks.MidrangeJadeShaman,
					FillDecks = false,
					Shuffle = false,
					SkipMulligan = false
				});
			game.Player1.BaseMana = 10;
			game.StartGame();

			var aiPlayer1 = new AggroScore();
			var aiPlayer2 = new AggroScore();

			game.Process(ChooseTask.Mulligan(game.Player1, aiPlayer1.MulliganRule().Invoke(game.Player1.Choice.Choices.Select(p => game.IdEntityDic[p]).ToList())));
			game.Process(ChooseTask.Mulligan(game.Player2, aiPlayer2.MulliganRule().Invoke(game.Player2.Choice.Choices.Select(p => game.IdEntityDic[p]).ToList())));

			game.MainReady();

			while (game.CurrentPlayer == game.Player1)
			{
				Console.WriteLine($"* Calculating solutions *** Player 1 ***");

				List<OptionNode> solutions = OptionNode.GetSolutions(game, game.Player1.Id, aiPlayer1, 10, 500);

				var solution = new List<PlayerTask>();
				solutions.OrderByDescending(p => p.Score).First().PlayerTasks(ref solution);
				Console.WriteLine($"- Player 1 - <{game.CurrentPlayer.Name}> ---------------------------");

				foreach (PlayerTask task in solution)
				{
					Console.WriteLine(task.FullPrint());
					game.Process(task);
					if (game.CurrentPlayer.Choice != null)
						break;
				}
			}

			Console.WriteLine(game.Player1.HandZone.FullPrint());
			Console.WriteLine(game.Player1.BoardZone.FullPrint());
		}

		public static void FullGame()
		{
			var game = new Game(
				new GameConfig()
				{
					StartPlayer = 1,
					Player1Name = "FitzVonGerald",
					Player1HeroClass = CardClass.WARRIOR,
					Player1Deck = Decks.AggroPirateWarrior,
					Player2Name = "RehHausZuckFuchs",
					Player2HeroClass = CardClass.WARRIOR,
					Player2Deck = Decks.AggroPirateWarrior,
					FillDecks = false,
					Shuffle = true,
					SkipMulligan = false,
					History = false
				});
			game.StartGame();

			var aiPlayer1 = new AggroScore();
			var aiPlayer2 = new AggroScore();

			List<int> mulligan1 = aiPlayer1.MulliganRule().Invoke(game.Player1.Choice.Choices.Select(p => game.IdEntityDic[p]).ToList());
			List<int> mulligan2 = aiPlayer2.MulliganRule().Invoke(game.Player2.Choice.Choices.Select(p => game.IdEntityDic[p]).ToList());

			Console.WriteLine($"Player1: Mulligan {String.Join(",", mulligan1)}");
			Console.WriteLine($"Player2: Mulligan {String.Join(",", mulligan2)}");

			game.Process(ChooseTask.Mulligan(game.Player1, mulligan1));
			game.Process(ChooseTask.Mulligan(game.Player2, mulligan2));

			game.MainReady();

			while (game.State != State.COMPLETE)
			{
				Console.WriteLine("");
				Console.WriteLine($"Player1: {game.Player1.PlayState} / Player2: {game.Player2.PlayState} - " +
								  $"ROUND {(game.Turn + 1) / 2} - {game.CurrentPlayer.Name}");
				Console.WriteLine($"Hero[P1]: {game.Player1.Hero.Health} / Hero[P2]: {game.Player2.Hero.Health}");
				Console.WriteLine("");
				while (game.State == State.RUNNING && game.CurrentPlayer == game.Player1)
				{
					Console.WriteLine($"* Calculating solutions *** Player 1 ***");
					List<OptionNode> solutions = OptionNode.GetSolutions(game, game.Player1.Id, aiPlayer1, 10, 500);
					var solution = new List<PlayerTask>();
					solutions.OrderByDescending(p => p.Score).First().PlayerTasks(ref solution);
					Console.WriteLine($"- Player 1 - <{game.CurrentPlayer.Name}> ---------------------------");
					foreach (PlayerTask task in solution)
					{
						Console.WriteLine(task.FullPrint());
						game.Process(task);
						if (game.CurrentPlayer.Choice != null)
						{
							Console.WriteLine($"* Recaclulating due to a final solution ...");
							break;
						}
					}
				}

				// Random mode for Player 2
				Console.WriteLine($"- Player 2 - <{game.CurrentPlayer.Name}> ---------------------------");
				while (game.State == State.RUNNING && game.CurrentPlayer == game.Player2)
				{
					//var options = game.Options(game.CurrentPlayer);
					//var option = options[Rnd.Next(options.Count)];
					//Log.Info($"[{option.FullPrint()}]");
					//game.Process(option);
					Console.WriteLine($"* Calculating solutions *** Player 2 ***");
					List<OptionNode> solutions = OptionNode.GetSolutions(game, game.Player2.Id, aiPlayer2, 10, 500);
					var solution = new List<PlayerTask>();
					solutions.OrderByDescending(p => p.Score).First().PlayerTasks(ref solution);
					Console.WriteLine($"- Player 2 - <{game.CurrentPlayer.Name}> ---------------------------");
					foreach (PlayerTask task in solution)
					{
						Console.WriteLine(task.FullPrint());
						game.Process(task);
						if (game.CurrentPlayer.Choice != null)
						{
							Console.WriteLine($"* Recaclulating due to a final solution ...");
							break;
						}
					}
				}
			}
			Console.WriteLine($"Game: {game.State}, Player1: {game.Player1.PlayState} / Player2: {game.Player2.PlayState}");

		}

		public static void TestFullGames()
		{

			int maxGames = 100;
			int maxDepth = 10;
			int maxWidth = 14;
			int[] player1Stats = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
			int[] player2Stats = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };

			var gameConfig = new GameConfig()
			{
				StartPlayer = -1,
				Player1Name = "FitzVonGerald",
				Player1HeroClass = CardClass.PALADIN,
				Player1Deck = new List<Card>()
						{
						Cards.FromName("Blessing of Might"),
						Cards.FromName("Blessing of Might"),
						Cards.FromName("Gnomish Inventor"),
						Cards.FromName("Gnomish Inventor"),
						Cards.FromName("Goldshire Footman"),
						Cards.FromName("Goldshire Footman"),
						Cards.FromName("Hammer of Wrath"),
						Cards.FromName("Hammer of Wrath"),
						Cards.FromName("Hand of Protection"),
						Cards.FromName("Hand of Protection"),
						Cards.FromName("Holy Light"),
						Cards.FromName("Holy Light"),
						Cards.FromName("Ironforge Rifleman"),
						Cards.FromName("Ironforge Rifleman"),
						Cards.FromName("Light's Justice"),
						Cards.FromName("Light's Justice"),
						Cards.FromName("Lord of the Arena"),
						Cards.FromName("Lord of the Arena"),
						Cards.FromName("Nightblade"),
						Cards.FromName("Nightblade"),
						Cards.FromName("Raid Leader"),
						Cards.FromName("Raid Leader"),
						Cards.FromName("Stonetusk Boar"),
						Cards.FromName("Stonetusk Boar"),
						Cards.FromName("Stormpike Commando"),
						Cards.FromName("Stormpike Commando"),
						Cards.FromName("Stormwind Champion"),
						Cards.FromName("Stormwind Champion"),
						Cards.FromName("Stormwind Knight"),
						Cards.FromName("Stormwind Knight")
						},
				Player2Name = "RehHausZuckFuchs",
				Player2HeroClass = CardClass.PALADIN,
				Player2Deck = new List<Card>()
						{
						Cards.FromName("Blessing of Might"),
						Cards.FromName("Blessing of Might"),
						Cards.FromName("Gnomish Inventor"),
						Cards.FromName("Gnomish Inventor"),
						Cards.FromName("Goldshire Footman"),
						Cards.FromName("Goldshire Footman"),
						Cards.FromName("Hammer of Wrath"),
						Cards.FromName("Hammer of Wrath"),
						Cards.FromName("Hand of Protection"),
						Cards.FromName("Hand of Protection"),
						Cards.FromName("Holy Light"),
						Cards.FromName("Holy Light"),
						Cards.FromName("Ironforge Rifleman"),
						Cards.FromName("Ironforge Rifleman"),
						Cards.FromName("Light's Justice"),
						Cards.FromName("Light's Justice"),
						Cards.FromName("Lord of the Arena"),
						Cards.FromName("Lord of the Arena"),
						Cards.FromName("Nightblade"),
						Cards.FromName("Nightblade"),
						Cards.FromName("Raid Leader"),
						Cards.FromName("Raid Leader"),
						Cards.FromName("Stonetusk Boar"),
						Cards.FromName("Stonetusk Boar"),
						Cards.FromName("Stormpike Commando"),
						Cards.FromName("Stormpike Commando"),
						Cards.FromName("Stormwind Champion"),
						Cards.FromName("Stormwind Champion"),
						Cards.FromName("Stormwind Knight"),
						Cards.FromName("Stormwind Knight")
						},
				FillDecks = false,
				Shuffle = true,
				SkipMulligan = false,
				Logging = false,
				History = false
			};

			for (int i = 0; i < maxGames; i++)
			{
				var game = new Game(gameConfig);
				game.StartGame();

				var aiPlayer1 = new AggroScore();
				var aiPlayer2 = new AggroScore();

				List<int> mulligan1 = aiPlayer1.MulliganRule().Invoke(game.Player1.Choice.Choices.Select(p => game.IdEntityDic[p]).ToList());
				List<int> mulligan2 = aiPlayer2.MulliganRule().Invoke(game.Player2.Choice.Choices.Select(p => game.IdEntityDic[p]).ToList());

				game.Process(ChooseTask.Mulligan(game.Player1, mulligan1));
				game.Process(ChooseTask.Mulligan(game.Player2, mulligan2));

				game.MainReady();

				while (game.State != State.COMPLETE)
				{
					while (game.State == State.RUNNING && game.CurrentPlayer == game.Player1)
					{
						List<OptionNode> solutions = OptionNode.GetSolutions(game, game.Player1.Id, aiPlayer1, maxDepth, maxWidth);
						var solution = new List<PlayerTask>();
						solutions.OrderByDescending(p => p.Score).First().PlayerTasks(ref solution);
						foreach (PlayerTask task in solution)
						{
							game.Process(task);
							if (game.CurrentPlayer.Choice != null)
								break;
						}
					}
					while (game.State == State.RUNNING && game.CurrentPlayer == game.Player2)
					{
						List<OptionNode> solutions = OptionNode.GetSolutions(game, game.Player2.Id, aiPlayer2, maxDepth, maxWidth);
						var solution = new List<PlayerTask>();
						solutions.OrderByDescending(p => p.Score).First().PlayerTasks(ref solution);
						foreach (PlayerTask task in solution)
						{
							game.Process(task);
							if (game.CurrentPlayer.Choice != null)
								break;
						}
					}
				}

				player1Stats[(int)game.Player1.PlayState]++;
				player2Stats[(int)game.Player2.PlayState]++;

				Console.WriteLine($"{i}.Game: {game.State}, Player1: {game.Player1.PlayState} / Player2: {game.Player2.PlayState}");
			}

			Console.WriteLine($"Player1: {String.Join(",", player1Stats)}");
			Console.WriteLine($"Player2: {String.Join(",", player2Stats)}");
		}

		/// <summary>
		/// setup the benchmark for the bachelor track
		/// initialization of the submission agent
		/// initializaiton of the pool of agents used by the benchmark
		/// </summary>
		public static void setupBachelor()
		{
			//student submissions
			//if not implemented, add following constructor to the provided class
			/*
			public studentClass()
			{
				preferedDeck = Decks.MidrangeJadeShaman; //defualt value if no deck is provided
				preferedHero = CardClass.SHAMAN; //default value
			}
			 */
			//tuple consists of the agent and an identifier for that agent
			List<Tuple<AbstractAgent, string>> submissions = new List<Tuple<AbstractAgent, string>>();

			//competition
			//submissions.Add(new Tuple<AbstractAgent, string>(new JoachimKnobi(), "DanielStelter"));
			//submissions.Add(new Tuple<AbstractAgent, string>(new DrunkenAggroWarriorAgent(), "Jann-Marten"));
			//submissions.Add(new Tuple<AbstractAgent, string>(new DrunkenAggroWarriorAgent2(), "Jan-Marten"));
			//submissions.Add(new Tuple<AbstractAgent, string>(new MAGEnt(), "MalikMann"));
			//submissions.Add(new Tuple<AbstractAgent, string>(new MAGEntLookahead(), "MalikMann"));
			//submissions.Add(new Tuple<AbstractAgent, string>(new Otto007Warrior(), "JoelDierkes"));
			//submissions.Add(new Tuple<AbstractAgent, string>(new Otto007Paladin(), "JoelDierkes"));
			//submissions.Add(new Tuple<AbstractAgent, string>(new TreePala(), "MichaelAlbrecht"));
			//submissions.Add(new Tuple<AbstractAgent, string>(new MyAgentManuelLiebchen(), "ManuelLiebchen"));
			//submissions.Add(new Tuple<AbstractAgent, string>(new ThreeTypeDynLooker(), "MaximilianSchotte"));
			//submissions.Add(new Tuple<AbstractAgent, string>(new Tree_Search_Agent_Rick_Richter(), "RickRichter"));
			//submissions.Add(new Tuple<AbstractAgent, string>(new Tree_Search_Agent_Rick_Richter_7_30(), "RickRichter"));
			//submissions.Add(new Tuple<AbstractAgent, string>(new MyAgentMohamed(), "Mohamed"));
			//submissions.Add(new Tuple<AbstractAgent, string>(new MyAgentMohamed2(), "Mohamed"));
			//submissions.Add(new Tuple<AbstractAgent, string>(new LynamicDookaheadAgentV1Bachelor(), "NilsBohnhof"));
			//submissions.Add(new Tuple<AbstractAgent, string>(new CIsntSharpAgent(), "TienDoNam"));
			//submissions.Add(new Tuple<AbstractAgent, string>(new ShroudedYmir(), "TilmanHartwig"));

			//evaluation
			//submissions.Add(new Tuple<AbstractAgent, string>(new MechaSMOrcAgent(), "TomPeters"));
			//submissions.Add(new Tuple<AbstractAgent, string>(new MechaSMOrcAgent2(), "TomPeters2"));

			List<BenchmarkInstance> instances = new List<BenchmarkInstance>();
			instances.Add(new BenchmarkInstance(new GreedyAgent(), CardClass.PALADIN, Decks.MidrangeBuffPaladin, 0));
			instances.Add(new BenchmarkInstance(new BeamSearchAgent(), CardClass.SHAMAN, Decks.MidrangeJadeShaman, 1));
			instances.Add(new BenchmarkInstance(new RollingHorizon(), CardClass.ROGUE, Decks.MiraclePirateRogue, 2));
			instances.Add(new BenchmarkInstance(new MinMaxAgent(), CardClass.HUNTER, Decks.FaceHunter, 3));
			instances.Add(new BenchmarkInstance(new DynamicLookaheadAgent(), CardClass.MAGE, Decks.RenoKazakusMage, 4));

			//filepath where the output file should be created
			string filePath = "D:/Dokumente/Unistoff/Tutorien/CIG/HearthstoneAICompetition/core-extensions/SabberStoneBasicAI/bin/Debug/netcoreapp2.1/";

			BachelorBenchmark(submissions.ToArray(), instances.ToArray(), 100, filePath);
		}

		/// <summary>
		/// benchmark for the bachelor branch, which plays the matchups two times
		/// first round submission goes first
		/// second round the benchmark agents go first
		/// </summary>
		/// <param name="submissions"> submitted agents of the students </param>
		/// <param name="benchmarkInstances"> instances of the benchmark </param>
		/// <param name="numberOfGames"> number of games each matchup has</param>
		/// <param name="pathToFile"> path to the output file</param>
		public static void BachelorBenchmark(Tuple<AbstractAgent, string>[] submissions, BenchmarkInstance[] benchmarkInstances, int numberOfGames, string pathToFile)
		{
			Console.WriteLine("Bachelor-Track started");
			//goes through the student submissions
			foreach (Tuple<AbstractAgent, string> submission in submissions)
			{
				int totalWins = 0;
				int totalGames = 0;
				double winRate = 0;
				int[] wonBattles = new int[benchmarkInstances.Length];
				double[] winRates = new double[benchmarkInstances.Length];

				Console.WriteLine("Setup gameConfig");
				Console.WriteLine("Submission goes first in match up");

				#region first half
				//play against each benchmark agent, benchmark goes second
				foreach (BenchmarkInstance instance in benchmarkInstances)
				{
					var gameConfig = new GameConfig()
					{
						StartPlayer = 1,
						Player1HeroClass = submission.Item1.preferedHero,
						Player2HeroClass = instance.getHero(),
						Player1Deck = submission.Item1.preferedDeck,
						Player2Deck = instance.getDeck(),
						FillDecks = false,
						Shuffle = true,
						Logging = false,
						SkipMulligan = false
					};

					Console.WriteLine("Setup POGameHandler");
					AbstractAgent player1 = submission.Item1;
					AbstractAgent player2 = instance.getAgent();
					var gameHandler = new POGameHandler(gameConfig, player1, player2, repeatDraws: false);

					Console.WriteLine("Simulate Games");
					//gameHandler.PlayGame();
					gameHandler.PlayGames(numberOfGames, addResultToGameStats: true, debug: false);
					GameStats gameStats = gameHandler.getGameStats();

					wonBattles[instance.getId()] += gameStats.PlayerA_Wins;

					totalWins += gameStats.PlayerA_Wins;
					totalGames += gameStats.GamesPlayed;

					gameStats.printResults();
				}
				#endregion

				Console.WriteLine("Setup gameConfig");
				Console.WriteLine("Benchmark goes first in match up");

				#region second half
				//play against each benchmark agent, benchmark goes first
				foreach (BenchmarkInstance instance in benchmarkInstances)
				{
					var gameConfig = new GameConfig()
					{
						StartPlayer = 1,
						Player1HeroClass = instance.getHero(),
						Player2HeroClass = submission.Item1.preferedHero,
						Player1Deck = instance.getDeck(),
						Player2Deck = submission.Item1.preferedDeck,
						FillDecks = false,
						Shuffle = true,
						Logging = false,
						SkipMulligan = false
					};

					Console.WriteLine("Setup POGameHandler");
					AbstractAgent player1 = instance.getAgent();
					AbstractAgent player2 = submission.Item1;
					var gameHandler = new POGameHandler(gameConfig, player1, player2, repeatDraws: false);

					Console.WriteLine("Simulate Games");
					//gameHandler.PlayGame();
					gameHandler.PlayGames(numberOfGames, addResultToGameStats: true, debug: false);
					GameStats gameStats = gameHandler.getGameStats();

					wonBattles[instance.getId()] += gameStats.PlayerB_Wins;

					totalWins += gameStats.PlayerB_Wins;
					totalGames += gameStats.GamesPlayed;

					gameStats.printResults();
				}
				#endregion

				#region Win Rate Calculation
				winRate = totalWins * 1.0 / totalGames;
				Console.WriteLine("Win Rate of submitted agent is: " + winRate + "\n");

				Console.WriteLine("Win Rate against the different agents of the benchmark\n");

				for (int i = 0; i < benchmarkInstances.Length; i += 1)
				{
					double winRateAgent = 0.0;
					winRateAgent = wonBattles[i] * 1.0 / (numberOfGames * 2);
					winRates[i] = winRateAgent;
					Console.WriteLine("Win Rate against Deck with Id " + i + " was: " + winRateAgent);
				}
				#endregion

				//creates the file path for the file
				//pathToFile is the directory path
				//submission.item2 is the identifier given in the setup for this agent
				string outputPath = pathToFile + submission.Item2 + "BachelorResults.txt";

				//writes file
				using (StreamWriter w = File.AppendText(outputPath))
				{
					Log(ToLog( submission.Item2, winRate, winRates), w);
				}
			}

			Console.WriteLine("Test successful");
		}

		/// <summary>
		/// setup the benchmark for the master track
		/// initialization of the different agents which compete in the tournament
		/// initializaiton of the different decks that are played
		/// </summary>
		public static void setupMaster()
		{
			#region setup Alex Tournament

			//Random random = new Random(7);

			//List<List<Card>> deckpool = new List<List<Card>>();
			//deckpool.Add(Decks.AggroShaman);
			//deckpool.Add(Decks.ControlWarrior);
			//deckpool.Add(Decks.MidrangeDruid);
			//deckpool.Add(Decks.FaceHunter);
			//deckpool.Add(Decks.RenoKazakusDragonPriest);
			//deckpool.Add(Decks.TokenDruid);

			//List<int> chosenDeck = new List<int>();
			//chosenDeck.Add(random.Next(7));
			//chosenDeck.Add(random.Next(6));
			//chosenDeck.Add(random.Next(5));


			//Agent[] agents = new Agent[4];

			////student submission
			//agents[0] = new Agent(typeof(LynamicDookaheadAgent), "CopyCats");

			////agents[0] = new Agent(typeof(RandomAgent), "Random Agent");
			////agents[0] = new Agent(typeof(GreedyAgent), "Greedy Agent");
			////agents[2] = new Agent(typeof(MinMaxAgent), "MinMax");
			//agents[3] = new Agent(typeof(DynamicLookaheadAgent), "DynamicLookaheadAgent");
			//agents[1] = new Agent(typeof(BeamSearchAgent), "BeamSearchAgent");
			//agents[2] = new Agent(typeof(RollingHorizon), "RollingHorizon");


			//CompetitionEvaluation.Deck[] decks = new CompetitionEvaluation.Deck[9];
			//decks[0] = new CompetitionEvaluation.Deck(Decks.RenoKazakusMage, CardClass.MAGE, "1");
			//decks[1] = new CompetitionEvaluation.Deck(Decks.AggroPirateWarrior, CardClass.WARRIOR, "2");
			//decks[2] = new CompetitionEvaluation.Deck(Decks.MidrangeBuffPaladin, CardClass.PALADIN, "3");
			//decks[3] = new CompetitionEvaluation.Deck(Decks.MidrangeJadeShaman, CardClass.SHAMAN, "4");
			//decks[4] = new CompetitionEvaluation.Deck(Decks.ZooDiscardWarlock, CardClass.WARLOCK, "5");
			//decks[5] = new CompetitionEvaluation.Deck(Decks.MiraclePirateRogue, CardClass.ROGUE, "6");
			//decks[6] = new CompetitionEvaluation.Deck(Decks.FaceHunter, CardClass.HUNTER, "7");
			//decks[7] = new CompetitionEvaluation.Deck(Decks.MidrangeDruid, CardClass.DRUID, "8");
			//decks[8] = new CompetitionEvaluation.Deck(Decks.RenoKazakusDragonPriest, CardClass.PRIEST, "9");

			//MasterBenchmark(agents, decks, 10);

			#endregion

			#region setup own benchmark
			//student submissions
			//tuple consists of the agent and an identifier for that agent
			List<Tuple<AbstractAgent, string>> submissions = new List<Tuple<AbstractAgent, string>>();

			//competition
			//submissions.Add(new Tuple<AbstractAgent, string>(new GreedyLookaheadAgent(), "KartikMudgal"));
			//submissions.Add(new Tuple<AbstractAgent, string>(new GreedyLookaheadAgent2(), "KartikMudgal"));
			//submissions.Add(new Tuple<AbstractAgent, string>(new LynamicDookaheadAgentV1Master(), "NilsBohnhof"));
			//submissions.Add(new Tuple<AbstractAgent, string>(new Jerry(), "MarkusHempel"));x
			//submissions.Add(new Tuple<AbstractAgent, string>(new PickleRick(), "MarkusHempel"));
			//submissions.Add(new Tuple<AbstractAgent, string>(new BasicAgent(), "AlexanderTracht"));
			//submissions.Add(new Tuple<AbstractAgent, string>(new AheadAgent(), "AlexanderTracht"));
			//submissions.Add(new Tuple<AbstractAgent, string>(new iWillBeatOpenAIFive(), "PhilipThoms"));
			//submissions.Add(new Tuple<AbstractAgent, string>(new AllMe(), "PhilipThoms"));
			//submissions.Add(new Tuple<AbstractAgent, string>(new MagicNumberAgent(), "RubenOrtlam"));
			//submissions.Add(new Tuple<AbstractAgent, string>(new MyAgentSebastianMiller(), "SebstianMiller"));
			//submissions.Add(new Tuple<AbstractAgent, string>(new MyAgentSebastianMiller2(), "SebstianMiller"));

			//evaluation

			//submissions.Add(new Tuple<AbstractAgent, string>(new MyAgentJulian(), "JulianLamprecht"));
			//submissions.Add(new Tuple<AbstractAgent, string>(new BetaStone(), "WelfBetaStone"));
			//submissions.Add(new Tuple<AbstractAgent, string>(new BetaStone2(), "WelfGreedStone"));
			//submissions.Add(new Tuple<AbstractAgent, string>(new MyAgentJonathan1(), "JonathanOneForAll"));
			//submissions.Add(new Tuple<AbstractAgent, string>(new MyAgentJonathan2(), "JonathanMulti02"));
			//submissions.Add(new Tuple<AbstractAgent, string>(new MyAgentJonathan3(), "JonathanMulti50"));
			//submissions.Add(new Tuple<AbstractAgent, string>(new MyTurnLookaheadBalancedAgent(), "MaikBalanced"));
			//submissions.Add(new Tuple<AbstractAgent, string>(new MyTurnDeepLookaheadAgent(), "MaikDeep"));
			//submissions.Add(new Tuple<AbstractAgent, string>(new MyTurnLookaheadDefensiveAgent(), "MaikDefensive"));
			//submissions.Add(new Tuple<AbstractAgent, string>(new MyTurnLookaheadAggressiveAgent(), "MaikAggressive"));
			//submissions.Add(new Tuple<AbstractAgent, string>(new MonteCarloTreeSearch(), "BasicAgentAlex"));


			//benchmark agents
			List<AbstractAgent> agents = new List<AbstractAgent>();
			agents.Add(new HarderDynamicLookaheadAgent());
			agents.Add(new HarderBeamSearchAgent());
			agents.Add(new HarderRollingHorizon());
			//agents[0] = new HarderGreedyAgent();
			//agents[1] = new HarderMinMaxAgent();


			//decks which are played
			List<Tuple<List<Card>,CardClass>> deckpool = new List<Tuple<List<Card>, CardClass>>();
			deckpool.Add(new Tuple<List<Card>, CardClass>(Decks.RenoKazakusMage, CardClass.MAGE));
			deckpool.Add(new Tuple<List<Card>, CardClass>(Decks.AggroPirateWarrior, CardClass.WARRIOR));
			deckpool.Add(new Tuple<List<Card>, CardClass>(Decks.MidrangeBuffPaladin, CardClass.PALADIN));
			deckpool.Add(new Tuple<List<Card>, CardClass>(Decks.MidrangeJadeShaman, CardClass.SHAMAN));
			deckpool.Add(new Tuple<List<Card>, CardClass>(Decks.ZooDiscardWarlock, CardClass.WARLOCK));
			deckpool.Add(new Tuple<List<Card>, CardClass>(Decks.MiraclePirateRogue, CardClass.ROGUE));
			deckpool.Add(new Tuple<List<Card>, CardClass>(Decks.FaceHunter, CardClass.HUNTER));
			deckpool.Add(new Tuple<List<Card>, CardClass>(Decks.MidrangeDruid, CardClass.DRUID));
			deckpool.Add(new Tuple<List<Card>, CardClass>(Decks.RenoKazakusDragonPriest, CardClass.PRIEST));

			//filepath where the output file should be created
			string filePath = "D:/Dokumente/Unistoff/Tutorien/CIG/HearthstoneAICompetition/core-extensions/SabberStoneBasicAI/bin/Debug/netcoreapp2.1/";

			MasterBenchmark2(submissions.ToArray(), agents.ToArray(), deckpool.ToArray(), 5, filePath);
			#endregion
		}

		/// <summary>
		/// benchmark for the master branch which is an round robin tournament
		/// </summary>
		/// <param name="agents"> agents which play in the tournament </param>
		/// <param name="decks"> decks which are used by the agents </param>
		/// <param name="numberOfGames"> number of games each match up has </param>
		public static void MasterBenchmark(Agent[] agents, CompetitionEvaluation.Deck[] decks, int numberOfGames)
		{
			RoundRobinCompetition competition = new RoundRobinCompetition(agents, decks, "results.txt");
			competition.CreateTasksProgrammingAssignment(numberOfGames);
			competition.startEvaluation(8);

			Console.WriteLine("Total Games Played: " + competition.GetTotalGamesPlayed());
			competition.PrintAgentStats();
		}

		/// <summary>
		/// benchmark tool for the programming assignment
		/// </summary>
		/// <param name="submissions"> agents submitted by the students</param>
		/// <param name="agents"> benchmark agents</param>
		/// <param name="deckSetup"> decks which are played</param>
		/// <param name="numberOfGames"> number of games for each matchup</param>
		/// <param name="pathToFile"> path to the output file</param>
		public static void MasterBenchmark2(Tuple<AbstractAgent, string>[] submissions, AbstractAgent[] agents, Tuple<List<Card>, CardClass>[] deckSetup, int numberOfGames, string pathToFile)
		{
			Console.WriteLine("Master-Track started");
			//goes through the student submissions
			foreach (Tuple<AbstractAgent, string> submission in submissions)
			{
				int totalWins = 0;
				int totalGames = 0;
				double winRate = 0;
				int[] wonBattles = new int[agents.Length];
				double[] winRates = new double[agents.Length];

				#region first half
				int counter = 0;

				Console.WriteLine("Setup gameConfig");
				Console.WriteLine("Submission goes first in match up");

				//plays against each benchmark agent, benchmark agent goes second
				foreach (AbstractAgent agent in agents)
				{
					Console.WriteLine("\nFirst Half ----- Evaluation of " + counter + "/" + (agents.Length - 1) + "\n");
					int d1 = 0;
					int d2 = 0;

					//free for all setup of the decks
					foreach (Tuple<List<Card>, CardClass> deck1 in deckSetup)
					{
						d1 += 1;
						d2 = 0;
						foreach (Tuple<List<Card>, CardClass> deck2 in deckSetup)
						{
							d2 += 1;
							Console.WriteLine("Matchup ( " + d1 + " / " + d2 + " )");

							var gameConfig = new GameConfig()
							{
								StartPlayer = 1,
								Player1HeroClass = deck1.Item2,
								Player2HeroClass = deck2.Item2,
								Player1Deck = deck1.Item1,
								Player2Deck = deck2.Item1,
								FillDecks = false,
								Shuffle = true,
								Logging = false,
								SkipMulligan = false
							};

							Console.WriteLine("Setup POGameHandler");
							AbstractAgent player1 = submission.Item1;
							AbstractAgent player2 = agent;
							var gameHandler = new POGameHandler(gameConfig, player1, player2, repeatDraws: false);

							Console.WriteLine("Simulate Games");
							//gameHandler.PlayGame();
							gameHandler.PlayGames(numberOfGames, addResultToGameStats: true, debug: false);
							GameStats gameStats = gameHandler.getGameStats();

							wonBattles[counter] += gameStats.PlayerA_Wins;

							totalWins += gameStats.PlayerA_Wins;
							totalGames += gameStats.GamesPlayed;

							gameStats.printResults();
						}
					}
					counter += 1;
				}
				#endregion

				#region second half
				counter = 0;

				Console.WriteLine("Setup gameConfig");
				Console.WriteLine("Benchmark goes first in match up");

				//plays each benchmark agent, benchmark agent goes first
				foreach (AbstractAgent agent in agents)
				{
					Console.WriteLine("Second Half ----- Evaluation of " + counter + "/" + (agents.Length - 1) + "\n");

					int d1 = 0;
					int d2 = 0;

					//free for all setup of the decks
					foreach (Tuple<List<Card>, CardClass> deck1 in deckSetup)
					{
						d1 += 1;
						d2 = 0;
						foreach (Tuple<List<Card>, CardClass> deck2 in deckSetup)
						{
							d2 += 1;
							Console.WriteLine("Matchup ( " + d1 + " / " + d2 + " )");

							var gameConfig = new GameConfig()
							{
								StartPlayer = 1,
								Player1HeroClass = deck1.Item2,
								Player2HeroClass = deck2.Item2,
								Player1Deck = deck1.Item1,
								Player2Deck = deck2.Item1,
								FillDecks = false,
								Shuffle = true,
								Logging = false,
								SkipMulligan = false
							};

							Console.WriteLine("Setup POGameHandler");
							AbstractAgent player1 = agent;
							AbstractAgent player2 = submission.Item1;
							var gameHandler = new POGameHandler(gameConfig, player1, player2, repeatDraws: false);

							Console.WriteLine("Simulate Games");
							//gameHandler.PlayGame();
							gameHandler.PlayGames(numberOfGames, addResultToGameStats: true, debug: false);
							GameStats gameStats = gameHandler.getGameStats();

							wonBattles[counter] += gameStats.PlayerB_Wins;

							totalWins += gameStats.PlayerB_Wins;
							totalGames += gameStats.GamesPlayed;

							gameStats.printResults();
						}
					}
					counter += 1;
				}
				#endregion

				#region Win Rate Calculation
				winRate = totalWins * 1.0 / totalGames;
				Console.WriteLine("Win Rate of submitted agent is: " + winRate + "\n");

				Console.WriteLine("Win Rate against the different agents of the benchmark\n");

				for (int i = 0; i < agents.Length; i += 1)
				{
					double winRateAgent = 0.0;
					winRateAgent = wonBattles[i] * 1.0 / (numberOfGames * deckSetup.Length * deckSetup.Length * 2);
					winRates[i] = winRateAgent;
					Console.WriteLine("Win Rate against Deck with Id " + i + " was: " + winRateAgent);
				}
				#endregion

				//creates the file path for the file
				//pathToFile is the directory path
				//submission.item2 is the identifier given in the setup for this agent
				string outputPath = pathToFile + submission.Item2 + "MasterResults.txt";

				//writes file
				using (StreamWriter w = File.AppendText(outputPath))
				{
					Log(ToLog( submission.Item2, winRate, winRates), w);
				}
			}
			
			Console.WriteLine("Test successful");
		}

		/// <summary>
		/// creates the output for the log file
		/// </summary>
		/// <param name="identifier"> agent identifier</param>
		/// <param name="totalRate"> total win rate of the agent</param>
		/// <param name="rates"> win rates of the different matchups</param>
		/// <returns></returns>
		public static string ToLog(string identifier, double totalRate, double[] rates)
		{
			string output = "Results of Submission: " + identifier + "\n\n\n";

			output += "Total Win Rate: " + totalRate + "\n\n";
			output += "Win Rate against the different agents of the benchmark\n";

			for (int i = 0; i < rates.Length; i += 1)
			{
				output += "Win Rate against Agent with Id " + i + " was: " + rates[i]+"\n";
			}

			return output;
		}

		/// <summary>
		/// writes the file
		/// </summary>
		/// <param name="logMessage"> message of the log</param>
		/// <param name="w"> streamwriter which writes the file</param>
		public static void Log(string logMessage, TextWriter w)
		{
			w.WriteLine(logMessage);
		}

	}

	class BenchmarkInstance
	{
		AbstractAgent agent;
		CardClass heroClass;
		List<Card> deck;
		int id;

		public BenchmarkInstance(AbstractAgent chosenAgent, CardClass chosenHeroClass, List<Card> chosenDeck, int chosenId)
		{
			agent = chosenAgent;
			heroClass = chosenHeroClass;
			deck = chosenDeck;
			id = chosenId;
		}

		public AbstractAgent getAgent()
		{
			return agent;
		}

		public CardClass getHero()
		{
			return heroClass;
		}

		public List<Card> getDeck()
		{
			return deck;
		}

		public int getId()
		{
			return id;
		}
	}
}
