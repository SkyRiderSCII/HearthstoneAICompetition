using System;
using System.Collections.Generic;
using System.Linq;
using SabberStoneBasicAI.PartialObservation;
using SabberStoneCore.Tasks.PlayerTasks;
using SabberStoneCore.Model.Entities;
using SabberStoneCore.Enums;
using SabberStoneBasicAI.Score;
using SabberStoneBasicAI.Meta;


namespace SabberStoneBasicAI.AIAgents
{
	class HarderDynamicLookaheadAgent : AbstractAgent
	{
		private int h_score = -1;
		public override void FinalizeAgent()
		{
		}

		public override void FinalizeGame()
		{
		}

		public override PlayerTask GetMove(POGame game)
		{
			Controller player = game.CurrentPlayer;

			if (h_score == -1)
			{
				determineScore(player);
			}
			// Implement a simple Mulligan Rule
			if (player.MulliganState == Mulligan.INPUT)
			{
				List<int> mulligan = new HarderMidRangeScore().MulliganRule().Invoke(player.Choice.Choices.Select(p => game.getGame().IdEntityDic[p]).ToList());
				return ChooseTask.Mulligan(player, mulligan);
			}


			IEnumerable<KeyValuePair<PlayerTask, POGame>> validOpts = game.Simulate(player.Options()).Where(x => x.Value != null);
			int optcount = validOpts.Count();

			PlayerTask returnValue = validOpts.Any() ?
				validOpts.Select(x => score(x, player.PlayerId, (optcount >= 5) ? ((optcount >= 25) ? 1 : 2) : 3)).OrderBy(x => x.Value).Last().Key :
				player.Options().First(x => x.PlayerTaskType == PlayerTaskType.END_TURN);

			return returnValue;

			KeyValuePair<PlayerTask, int> score(KeyValuePair<PlayerTask, POGame> state, int player_id, int max_depth = 3)
			{
				int max_score = Int32.MinValue;
				if (max_depth > 0 && state.Value.CurrentPlayer.PlayerId == player_id)
				{
					IEnumerable<KeyValuePair<PlayerTask, POGame>> subactions = state.Value.Simulate(state.Value.CurrentPlayer.Options()).Where(x => x.Value != null);

					foreach (KeyValuePair<PlayerTask, POGame> subaction in subactions)
						max_score = Math.Max(max_score, score(subaction, player_id, max_depth - 1).Value);


				}
				max_score = Math.Max(max_score, Score(state.Value, player_id));
				return new KeyValuePair<PlayerTask, int>(state.Key, max_score);
			}
		}

		public int Score(POGame state, int playerId)
		{
			Controller p = state.CurrentPlayer.PlayerId == playerId ? state.CurrentPlayer : state.CurrentOpponent;
			switch (h_score)
			{
				case 0:
					return new HarderAggroScore { Controller = p }.Rate();
				case 1:
					return new HarderMidRangeScore { Controller = p }.Rate();
				case 2:
					return new HarderControlScore { Controller = p }.Rate();
				default:
					return new HarderMidRangeScore { Controller = p }.Rate();
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
						h_score = 1;
					}
					else
					{
						h_score = 0;
					}
					break;
				case CardClass.HUNTER:
					h_score = 0;
					break;
				case CardClass.MAGE:
					h_score = 2;
					break;
				case CardClass.PALADIN:
					h_score = 1;
					break;
				case CardClass.PRIEST:
					h_score = 2;
					break;
				case CardClass.ROGUE:
					h_score = 0;
					break;
				case CardClass.SHAMAN:
					if (Decks.MidrangeJadeShaman.Contains(player.DeckZone[0].Card) &
						Decks.MidrangeJadeShaman.Contains(player.DeckZone[1].Card) &
						Decks.MidrangeJadeShaman.Contains(player.DeckZone[2].Card))
					{
						h_score = 1;
					}
					else
					{
						h_score = 0;
					}
					break;
				case CardClass.WARLOCK:
					h_score = 0;
					break;
				case CardClass.WARRIOR:
					if (Decks.AggroPirateWarrior.Contains(player.DeckZone[0].Card) &
						Decks.AggroPirateWarrior.Contains(player.DeckZone[1].Card) &
						Decks.AggroPirateWarrior.Contains(player.DeckZone[2].Card))
					{
						h_score = 0;
					}
					else
					{
						h_score = 2;
					}
					break;
				default:
					h_score = -2;
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
