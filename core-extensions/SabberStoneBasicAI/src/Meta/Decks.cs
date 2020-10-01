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
using System.Collections.Generic;
using SabberStoneCore.Model;

namespace SabberStoneBasicAI.Meta
{
	public class Decks
	{
		/// <summary>
		/// Questing Miracle Rogue Deck List Guide (January 2017, Standard) – Season 34
		/// http://www.hearthstonetopdecks.com/decks/miracle-pirate-rogue-deck-list-guide-standard/
		/// </summary>
		public static List<Card> MiraclePirateRogue => new List<Card>()
		{
			Cards.FromName("Backstab"),
			Cards.FromName("Backstab"),
			Cards.FromName("Counterfeit Coin"),
			Cards.FromName("Preparation"),
			Cards.FromName("Preparation"),
			Cards.FromName("Cold Blood"),
			Cards.FromName("Cold Blood"),
			Cards.FromName("Conceal"),
			Cards.FromName("Conceal"),
			Cards.FromName("Swashburglar"),
			Cards.FromName("Eviscerate"),
			Cards.FromName("Eviscerate"),
			Cards.FromName("Sap"),
			Cards.FromName("Sap"),
			Cards.FromName("Edwin VanCleef"),
			Cards.FromName("Fan of Knives"),
			Cards.FromName("Fan of Knives"),
			Cards.FromName("Tomb Pillager"),
			Cards.FromName("Tomb Pillager"),
			Cards.FromName("Patches the Pirate"),
			Cards.FromName("Small-Time Buccaneer"),
			Cards.FromName("Small-Time Buccaneer"),
			Cards.FromName("Bloodmage Thalnos"),
			Cards.FromName("Questing Adventurer"),
			Cards.FromName("Questing Adventurer"),
			Cards.FromName("Azure Drake"),
			Cards.FromName("Azure Drake"),
			Cards.FromName("Leeroy Jenkins"),
			Cards.FromName("Gadgetzan Auctioneer"),
			Cards.FromName("Gadgetzan Auctioneer")
		};

		/// <summary>
		/// Warlock Zoo (Discard) Deck List Guide (December 2016, Standard) – Season 33
		/// http://www.hearthstonetopdecks.com/decks/warlock-zoo-standard-wotg-deck-list-guide/
		/// </summary>
		public static List<Card> ZooDiscardWarlock => new List<Card>()
		{
			Cards.FromName("Flame Imp"),
			Cards.FromName("Flame Imp"),
			Cards.FromName("Malchezaar's Imp"),
			Cards.FromName("Malchezaar's Imp"),
			Cards.FromName("Possessed Villager"),
			Cards.FromName("Possessed Villager"),
			Cards.FromName("Soulfire"),
			Cards.FromName("Soulfire"),
			Cards.FromName("Voidwalker"),
			Cards.FromName("Voidwalker"),
			Cards.FromName("Dark Peddler"),
			Cards.FromName("Dark Peddler"),
			Cards.FromName("Darkshire Librarian"),
			Cards.FromName("Darkshire Librarian"),
			Cards.FromName("Darkshire Councilman"),
			Cards.FromName("Darkshire Councilman"),
			Cards.FromName("Imp Gang Boss"),
			Cards.FromName("Imp Gang Boss"),
			Cards.FromName("Silverware Golem"),
			Cards.FromName("Silverware Golem"),
			Cards.FromName("Doomguard"),
			Cards.FromName("Doomguard"),
			Cards.FromName("Abusive Sergeant"),
			Cards.FromName("Crazed Alchemist"),
			Cards.FromName("Dire Wolf Alpha"),
			Cards.FromName("Dire Wolf Alpha"),
			Cards.FromName("Knife Juggler"),
			Cards.FromName("Knife Juggler"),
			Cards.FromName("Defender of Argus"),
			Cards.FromName("Defender of Argus")
		};

		/// <summary>
		/// Reno Kazakus Dragon Priest Deck List Guide (January 2017, Standard) – Season 34
		/// http://www.hearthstonetopdecks.com/decks/reno-kazakus-dragon-priest-deck-list-guide-december-2016-season-33/
		/// </summary>
		public static List<Card> RenoKazakusDragonPriest => new List<Card>()
		{
			Cards.FromName("Northshire Cleric"),
			Cards.FromName("Potion of Madness"),
			Cards.FromName("Power Word: Shield"),
			Cards.FromName("Twilight Whelp"),
			Cards.FromName("Shadow Word: Pain"),
			Cards.FromName("Wyrmrest Agent"),
			Cards.FromName("Kabal Talonpriest"),
			Cards.FromName("Shadow Word: Death"),
			Cards.FromName("Priest of the Feast"),
			Cards.FromName("Drakonid Operative"),
			Cards.FromName("Holy Nova"),
			Cards.FromName("Raza the Chained"),
			Cards.FromName("Cabal Shadow Priest"),
			Cards.FromName("Dragonfire Potion"),
			Cards.FromName("Entomb"),
			Cards.FromName("Acidic Swamp Ooze"),
			Cards.FromName("Dirty Rat"),
			Cards.FromName("Doomsayer"),
			Cards.FromName("Netherspite Historian"),
			Cards.FromName("Brann Bronzebeard"),
			Cards.FromName("Kabal Courier"),
			Cards.FromName("Kazakus"),
			Cards.FromName("Twilight Drake"),
			Cards.FromName("Twilight Guardian"),
			Cards.FromName("Azure Drake"),
			Cards.FromName("Blackwing Corruptor"),
			Cards.FromName("Book Wyrm"),
			Cards.FromName("Reno Jackson"),
			Cards.FromName("Chillmaw"),
			Cards.FromName("Ysera")
		};

		/// <summary>
		/// FACE HUNTER DECK LIST GUIDE – APRIL 2016 (SEASON 25, WILD)
		/// https://www.hearthstonetopdecks.com/decks/face-hunter-deck-list/
		/// </summary>
		public static List<Card> FaceHunter => new List<Card>()
		{
			Cards.FromName("Abusive Sergeant"),
			Cards.FromName("Abusive Sergeant"),
			Cards.FromName("Leper Gnome"),
			Cards.FromName("Leper Gnome"),
			Cards.FromName("Explosive Trap"),
			Cards.FromName("Explosive Trap"),
			Cards.FromName("Glaivezooka"),
			Cards.FromName("Glaivezooka"),
			Cards.FromName("Hunter's Mark"),
			Cards.FromName("Quick Shot"),
			Cards.FromName("Quick Shot"),
			Cards.FromName("Haunted Creeper"),
			Cards.FromName("Haunted Creeper"),
			Cards.FromName("Knife Juggler"),
			Cards.FromName("Knife Juggler"),
			Cards.FromName("Mad Scientist"),
			Cards.FromName("Mad Scientist"),
			Cards.FromName("Animal Companion"),
			Cards.FromName("Animal Companion"),
			Cards.FromName("Eaglehorn Bow"),
			Cards.FromName("Eaglehorn Bow"),
			Cards.FromName("Kill Command"),
			Cards.FromName("Kill Command"),
			Cards.FromName("Unleash the Hounds"),
			Cards.FromName("Unleash the Hounds"),
			Cards.FromName("Arcane Golem"),
			Cards.FromName("Arcane Golem"),
			Cards.FromName("Argent Horserider"),
			Cards.FromName("Argent Horserider"),
			Cards.FromName("Ironbeak Owl")
		};

		/// <summary>
		/// Kolento's Midrange Buff Paladin (January 2017, Season 34)
		/// http://www.hearthstonetopdecks.com/decks/kolentos-midrange-buff-paladin-january-2017-season-34/
		/// </summary>
		public static List<Card> MidrangeBuffPaladin => new List<Card>()
		{
			Cards.FromName("Smuggler's Run"),
			Cards.FromName("Smuggler's Run"),
			Cards.FromName("Argent Lance"),
			Cards.FromName("Grimestreet Outfitter"),
			Cards.FromName("Grimestreet Outfitter"),
			Cards.FromName("Aldor Peacekeeper"),
			Cards.FromName("Aldor Peacekeeper"),
			Cards.FromName("Wickerflame Burnbristle"),
			Cards.FromName("Truesilver Champion"),
			Cards.FromName("Truesilver Champion"),
			Cards.FromName("Grimestreet Enforcer"),
			Cards.FromName("Grimestreet Enforcer"),
			Cards.FromName("Tirion Fordring"),
			Cards.FromName("Sir Finley Mrrgglton"),
			Cards.FromName("Worgen Infiltrator"),
			Cards.FromName("Worgen Infiltrator"),
			Cards.FromName("Flame Juggler"),
			Cards.FromName("Flame Juggler"),
			Cards.FromName("Acolyte of Pain"),
			Cards.FromName("Acolyte of Pain"),
			Cards.FromName("Argent Horserider"),
			Cards.FromName("Argent Horserider"),
			Cards.FromName("Sen'jin Shieldmasta"),
			Cards.FromName("Sen'jin Shieldmasta"),
			Cards.FromName("Psych-o-Tron"),
			Cards.FromName("Second-Rate Bruiser"),
			Cards.FromName("Second-Rate Bruiser"),
			Cards.FromName("Argent Commander"),
			Cards.FromName("Argent Commander"),
			Cards.FromName("Don Han'Cho")
		};

		/// <summary>
		/// AGGRO TOKEN DRUID DECK LIST GUIDE (POST NERF) – KOBOLDS – FEBRUARY 2018
		/// https://www.hearthstonetopdecks.com/decks/aggro-token-druid-deck-list-guide-standard/
		/// </summary>
		public static List<Card> TokenDruid = new List<Card>()
		{
			Cards.FromName("Snowflipper Penguin"),
			Cards.FromName("Snowflipper Penguin"),
			Cards.FromName("Enchanted Raven"),
			Cards.FromName("Enchanted Raven"),
			Cards.FromName("Mark of the Lotus"),
			Cards.FromName("Mark of the Lotus"),
			Cards.FromName("Dire Mole"),
			Cards.FromName("Dire Mole"),
			Cards.FromName("Fire Fly"),
			Cards.FromName("Fire Fly"),
			Cards.FromName("Druid of the Swarm"),
			Cards.FromName("Druid of the Swarm"),
			Cards.FromName("Mark of Y'Shaarj"),
			Cards.FromName("Mark of Y'Shaarj"),
			Cards.FromName("Power of the Wild"),
			Cards.FromName("Power of the Wild"),
			Cards.FromName("Dire Wolf Alpha"),
			Cards.FromName("Dire Wolf Alpha"),
			Cards.FromName("Savage Roar"),
			Cards.FromName("Savage Roar"),
			Cards.FromName("Vicious Fledgling"),
			Cards.FromName("Vicious Fledgling"),
			Cards.FromName("Branching Paths"),
			Cards.FromName("Spellbreaker"),
			Cards.FromName("Spellbreaker"),
			Cards.FromName("Living Mana"),
			Cards.FromName("Living Mana"),
			Cards.FromName("Bittertide Hydra"),
			Cards.FromName("Bittertide Hydra"),
			Cards.FromName("Leeroy Jenkins")
		};

		/// <summary>
		/// Midrange Jade Shaman Deck List Guide (January 2017, Standard) – Season 34
		/// http://www.hearthstonetopdecks.com/decks/midrange-jade-shaman-deck-list-guide/
		/// </summary>
		public static List<Card> MidrangeJadeShaman => new List<Card>()
		{
			Cards.FromName("Tunnel Trogg"),
			Cards.FromName("Tunnel Trogg"),
			Cards.FromName("Totem Golem"),
			Cards.FromName("Totem Golem"),
			Cards.FromName("Thing from Below"),
			Cards.FromName("Thing from Below"),
			Cards.FromName("Spirit Claws"),
			Cards.FromName("Spirit Claws"),
			Cards.FromName("Maelstrom Portal"),
			Cards.FromName("Maelstrom Portal"),
			Cards.FromName("Lightning Storm"),
			Cards.FromName("Lightning Bolt"),
			Cards.FromName("Jade Lightning"),
			Cards.FromName("Jade Lightning"),
			Cards.FromName("Jade Claws"),
			Cards.FromName("Jade Claws"),
			Cards.FromName("Hex"),
			Cards.FromName("Hex"),
			Cards.FromName("Flametongue Totem"),
			Cards.FromName("Flametongue Totem"),
			Cards.FromName("Al'Akir the Windlord"),
			Cards.FromName("Patches the Pirate"),
			Cards.FromName("Small-Time Buccaneer"),
			Cards.FromName("Small-Time Buccaneer"),
			Cards.FromName("Bloodmage Thalnos"),
			Cards.FromName("Barnes"),
			Cards.FromName("Azure Drake"),
			Cards.FromName("Azure Drake"),
			Cards.FromName("Aya Blackpaw"),
			Cards.FromName("Ragnaros the Firelord")
		};

		/// <summary>
		/// SuperJJ's Aggro Pirate Warrior – Zagreb Gaming Arena, January 2017
		/// http://www.hearthstonetopdecks.com/decks/superjjs-aggro-pirate-warrior-zagreb-gaming-arena-january-2017/
		/// </summary>
		public static List<Card> AggroPirateWarrior => new List<Card>()
		{
			Cards.FromName("Sir Finley Mrrgglton"),
			Cards.FromName("Fiery War Axe"),
			Cards.FromName("Fiery War Axe"),
			Cards.FromName("Heroic Strike"),
			Cards.FromName("Heroic Strike"),
			Cards.FromName("N'Zoth's First Mate"),
			Cards.FromName("N'Zoth's First Mate"),
			Cards.FromName("Upgrade!"),
			Cards.FromName("Upgrade!"),
			Cards.FromName("Bloodsail Cultist"),
			Cards.FromName("Bloodsail Cultist"),
			Cards.FromName("Frothing Berserker"),
			Cards.FromName("Frothing Berserker"),
			Cards.FromName("Kor'kron Elite"),
			Cards.FromName("Kor'kron Elite"),
			Cards.FromName("Arcanite Reaper"),
			Cards.FromName("Arcanite Reaper"),
			Cards.FromName("Patches the Pirate"),
			Cards.FromName("Small-Time Buccaneer"),
			Cards.FromName("Small-Time Buccaneer"),
			Cards.FromName("Southsea Deckhand"),
			Cards.FromName("Southsea Deckhand"),
			Cards.FromName("Bloodsail Raider"),
			Cards.FromName("Bloodsail Raider"),
			Cards.FromName("Southsea Captain"),
			Cards.FromName("Southsea Captain"),
			Cards.FromName("Dread Corsair"),
			Cards.FromName("Dread Corsair"),
			Cards.FromName("Naga Corsair"),
			Cards.FromName("Naga Corsair"),
		};

		/// <summary>
		/// RENO MAGE DECK LIST GUIDE (JANUARY 2017, STANDARD) – SEASON 34
		/// http://www.hearthstonetopdecks.com/decks/reno-kazakus-mage-deck-list-guide-standard/
		/// </summary>
		public static List<Card> RenoKazakusMage => new List<Card>()
		{
			Cards.FromName("Forbidden Flame"),
			Cards.FromName("Arcane Blast"),
			Cards.FromName("Babbling Book"),
			Cards.FromName("Frostbolt"),
			Cards.FromName("Arcane Intellect"),
			Cards.FromName("Forgotten Torch"),
			Cards.FromName("Ice Barrier"),
			Cards.FromName("Ice Block"),
			Cards.FromName("Manic Soulcaster"),
			Cards.FromName("Volcanic Potion"),
			Cards.FromName("Fireball"),
			Cards.FromName("Polymorph"),
			Cards.FromName("Water Elemental"),
			Cards.FromName("Cabalist's Tome"),
			Cards.FromName("Blizzard"),
			Cards.FromName("Firelands Portal"),
			Cards.FromName("Flamestrike"),
			Cards.FromName("Acidic Swamp Ooze"),
			Cards.FromName("Bloodmage Thalnos"),
			Cards.FromName("Dirty Rat"),
			Cards.FromName("Doomsayer"),
			Cards.FromName("Brann Bronzebeard"),
			Cards.FromName("Kabal Courier"),
			Cards.FromName("Mind Control Tech"),
			Cards.FromName("Kazakus"),
			Cards.FromName("Refreshment Vendor"),
			Cards.FromName("Azure Drake"),
			Cards.FromName("Reno Jackson"),
			Cards.FromName("Sylvanas Windrunner"),
			Cards.FromName("Alexstrasza")
		};

		/// <summary>
		/// CONTROL WARRIOR DECK LIST GUIDE (APRIL 2016, WILD) – SEASON 25
		/// https://www.hearthstonetopdecks.com/decks/fibonaccis-na-winter-prelims-2016-control-warrior/
		/// </summary>
		public static List<Card> ControlWarrior => new List<Card>()
		{
			Cards.FromName("Shield Slam"),
			Cards.FromName("Shield Slam"),
			Cards.FromName("Execute"),
			Cards.FromName("Execute"),
			Cards.FromName("Revenge"),
			Cards.FromName("Revenge"),
			Cards.FromName("Slam"),
			Cards.FromName("Doomsayer"),
			Cards.FromName("Jeweled Scarab"),
			Cards.FromName("Jeweled Scarab"),
			Cards.FromName("Bash"),
			Cards.FromName("Bash"),
			Cards.FromName("Fiery War Axe"),
			Cards.FromName("Shield Block"),
			Cards.FromName("Shield Block"),
			Cards.FromName("Acolyte of Pain"),
			Cards.FromName("Acolyte of Pain"),
			Cards.FromName("Ironbeak Owl"),
			Cards.FromName("Death's Bite"),
			Cards.FromName("Death's Bite"),
			Cards.FromName("Brawl"),
			Cards.FromName("Brawl"),
			Cards.FromName("Big Game Hunter"),
			Cards.FromName("Harrison Jones"),
			Cards.FromName("Sludge Belcher"),
			Cards.FromName("Sludge Belcher"),
			Cards.FromName("Shieldmaiden"),
			Cards.FromName("Shieldmaiden"),
			Cards.FromName("Justicar Trueheart"),
			Cards.FromName("Grommash Hellscream")
		};

		/// <summary>
		/// MIDRANGE FAST DRUID DECK LIST GUIDE (APRIL 2016, WILD) – SEASON 25
		/// https://www.hearthstonetopdecks.com/decks/midrange-fast-druid-deck-list/
		/// </summary>
		public static List<Card> MidrangeDruid => new List<Card>()
		{
			Cards.FromName("Innervate"),
			Cards.FromName("Innervate"),
			Cards.FromName("Living Roots"),
			Cards.FromName("Living Roots"),
			Cards.FromName("Darnassus Aspirant"),
			Cards.FromName("Wrath"),
			Cards.FromName("Wrath"),
			Cards.FromName("Savage Roar"),
			Cards.FromName("Savage Roar"),
			Cards.FromName("Wild Growth"),
			Cards.FromName("Wild Growth"),
			Cards.FromName("Mind Control Tech"),
			Cards.FromName("Keeper of the Grove"),
			Cards.FromName("Keeper of the Grove"),
			Cards.FromName("Swipe"),
			Cards.FromName("Swipe"),
			Cards.FromName("Piloted Shredder"),
			Cards.FromName("Piloted Shredder"),
			Cards.FromName("Druid of the Claw"),
			Cards.FromName("Druid of the Claw"),
			Cards.FromName("Force of Nature"),
			Cards.FromName("Force of Nature"),
			Cards.FromName("Azure Drake"),
			Cards.FromName("Azure Drake"),
			Cards.FromName("Big Game Hunter"),
			Cards.FromName("Loatheb"),
			Cards.FromName("Emperor Thaurissan"),
			Cards.FromName("Ancient of Lore"),
			Cards.FromName("Ancient of Lore"),
			Cards.FromName("Dr. Boom"),
		};

		/// <summary>
		/// AGGRO SHAMAN DECK LIST GUIDE (MARCH 2017, STANDARD) – SEASON 36
		/// https://www.hearthstonetopdecks.com/decks/aggro-jade-shaman-deck-list-guide/
		/// </summary>
		public static List<Card> AggroShaman => new List<Card>()
		{
			Cards.FromName("Lightning Bolt"),
			Cards.FromName("Lightning Bolt"),
			Cards.FromName("Tunnel Trogg"),
			Cards.FromName("Tunnel Trogg"),
			Cards.FromName("Bloodsail Corsair"),
			Cards.FromName("Patches the Pirate"),
			Cards.FromName("Sir Finley Mrrgglton"),
			Cards.FromName("Southsea Deckhand"),
			Cards.FromName("Southsea Deckhand"),
			Cards.FromName("Jade Claws"),
			Cards.FromName("Jade Claws"),
			Cards.FromName("Maelstrom Portal"),
			Cards.FromName("Maelstrom Portal"),
			Cards.FromName("Totem Golem"),
			Cards.FromName("Totem Golem"),
			Cards.FromName("Bloodmage Thalnos"),
			Cards.FromName("Feral Spirit"),
			Cards.FromName("Feral Spirit"),
			Cards.FromName("Flametongue Totem"),
			Cards.FromName("Flametongue Totem"),
			Cards.FromName("Lava Burst"),
			Cards.FromName("Lava Burst"),
			Cards.FromName("Flamewreathed Faceless"),
			Cards.FromName("Flamewreathed Faceless"),
			Cards.FromName("Jade Lightning"),
			Cards.FromName("Jade Lightning"),
			Cards.FromName("Hammer of Twilight"),
			Cards.FromName("Hammer of Twilight"),
			Cards.FromName("Azure Drake"),
			Cards.FromName("Azure Drake")
		};

		public static List<Card> MechHunter => new List<Card>()
		{
			Cards.FromName("Faithful Lumi"),
			Cards.FromName("Faithful Lumi"),
			Cards.FromName("Mecharoo"),
			Cards.FromName("Mecharoo"),
			Cards.FromName("Annoy-o-Tron"),
			Cards.FromName("Annoy-o-Tron"),
			Cards.FromName("Galvanizer"),
			Cards.FromName("Galvanizer"),
			Cards.FromName("Mechwarper"),
			Cards.FromName("Mechwarper"),
			Cards.FromName("Upgradeable Framebot"),
			Cards.FromName("Upgradeable Framebot"),
			Cards.FromName("Venomizer"),
			Cards.FromName("Venomizer"),
			Cards.FromName("Metaltooth Leaper"),
			Cards.FromName("Metaltooth Leaper"),
			Cards.FromName("Spider Bomb"),
			Cards.FromName("Spider Bomb"),
			Cards.FromName("Spider Tank"),
			Cards.FromName("Explodinator"),
			Cards.FromName("Explodinator"),
			Cards.FromName("Jeeves"),
			Cards.FromName("Jeeves"),
			Cards.FromName("Piloted Shredder"),
			Cards.FromName("Piloted Shredder"),
			Cards.FromName("Replicating Menace"),
			Cards.FromName("Replicating Menace"),
			Cards.FromName("Wargear"),
			Cards.FromName("Wargear"),
			Cards.FromName("Zilliax"),
		};
	}
}
