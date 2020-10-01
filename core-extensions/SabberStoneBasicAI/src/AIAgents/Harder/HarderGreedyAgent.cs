using System.Linq;
using SabberStoneCore.Enums;
using SabberStoneBasicAI.Score;
using SabberStoneCore.Tasks.PlayerTasks;
using SabberStoneBasicAI.PartialObservation;
using System.Collections.Generic;
using SabberStoneCore.Model.Entities;
using SabberStoneBasicAI.Meta;

//Developed by Oskar Kirmis and Florian Koch and submitted to the 2018 Hearthstone AI Competition's Premade Deck Playing Track
namespace SabberStoneBasicAI.AIAgents
{
	// Plain old Greedy Bot
	class HarderGreedyAgent : AbstractAgent
	{
		private int score = -1;
		public override void InitializeAgent() { }
		public override void InitializeGame() { }
		public override void FinalizeGame() { }
		public override void FinalizeAgent() { }


		public override PlayerTask GetMove(POGame game)
		{
			Controller player = game.CurrentPlayer;

			if (score == -1)
			{
				determineScore(player);
			}
			// Implement a simple Mulligan Rule
			if (player.MulliganState == Mulligan.INPUT)
			{
				List<int> mulligan = new HarderMidRangeScore().MulliganRule().Invoke(player.Choice.Choices.Select(p => game.getGame().IdEntityDic[p]).ToList());
				return ChooseTask.Mulligan(player, mulligan);
			}

			// Get all simulation results for simulations that didn't fail
			IEnumerable<KeyValuePair<PlayerTask, POGame>> validOpts = game.Simulate(player.Options()).Where(x => x.Value != null);

			// If all simulations failed, play end turn option (always exists), else best according to score function
			return validOpts.Any() ?
				validOpts.OrderBy(x => Score(x.Value, player.PlayerId)).Last().Key :
				player.Options().First(x => x.PlayerTaskType == PlayerTaskType.END_TURN);
		}

		// Calculate different scores based on our hero's class
		public int Score(POGame state, int playerId)
		{
			Controller p = state.CurrentPlayer.PlayerId == playerId ? state.CurrentPlayer : state.CurrentOpponent;
			switch (score)
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
						score = 1;
					}
					else
					{
						score = 0;
					}
					break;
				case CardClass.HUNTER:
					score = 0;
					break;
				case CardClass.MAGE:
					score = 2;
					break;
				case CardClass.PALADIN:
					score = 1;
					break;
				case CardClass.PRIEST:
					score = 2;
					break;
				case CardClass.ROGUE:
					score = 0;
					break;
				case CardClass.SHAMAN:
					if (Decks.MidrangeJadeShaman.Contains(player.DeckZone[0].Card) &
						Decks.MidrangeJadeShaman.Contains(player.DeckZone[1].Card) &
						Decks.MidrangeJadeShaman.Contains(player.DeckZone[2].Card))
					{
						score = 1;
					}
					else
					{
						score = 0;
					}
					break;
				case CardClass.WARLOCK:
					score = 0;
					break;
				case CardClass.WARRIOR:
					if (Decks.AggroPirateWarrior.Contains(player.DeckZone[0].Card) &
						Decks.AggroPirateWarrior.Contains(player.DeckZone[1].Card) &
						Decks.AggroPirateWarrior.Contains(player.DeckZone[2].Card))
					{
						score = 0;
					}
					else
					{
						score = 2;
					}
					break;
				default:
					score = -2;
					break;
			}
		}
	}
}
