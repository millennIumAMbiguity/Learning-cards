using System.Collections.Generic;
using Learning_cards.Scripts.Data.Classes;

namespace Learning_cards.Scripts.Data
{
	public static class Dictionaries
	{
		public static bool         IsLoaded;
		public static List<Player> Players;

		private static List<Character> LCharacter;
		private static List<Card>      LCards;
		
		private static Dictionary<string, Character> DCharacters;
		private static Dictionary<string, Card>      DCards;

		public static void Load()
		{
			if (IsLoaded) return;

			//TODO: load mods to memory

			IsLoaded = true;
		}

		public static Character Character(int id) => LCharacter[id];
		public static Character Character(string name) => DCharacters[name];
		
		public static Card Card(int    id)   => LCards[id];
		public static Card Card(string name) => DCards[name];
	}
}