﻿using System.Collections.Generic;
using Learning_cards.Scripts.Data.Classes;
using Learning_cards.Scripts.Data.InternalCode;
using Learning_cards.Scripts.Mods;
using Learning_cards.Scripts.Mods.Mod;

namespace Learning_cards.Scripts.Data
{
	public static class Dictionaries
	{
		public static bool         IsLoaded;
		public static List<Player> Players;

		private static List<Character> LCharacter;
		private static List<Card>      LCards;
		private static List<ICode>     LCode;

		private static Dictionary<string, Character> DCharacters;
		private static Dictionary<string, Card>      DCards;
		private static Dictionary<string, Function>  DCode;

		public static void Load()
		{
			if (IsLoaded) return;

			LCharacter = new List<Character>();
			LCards     = new List<Card>();
			LCode      = new List<ICode>();

			DCharacters = new Dictionary<string, Character>();
			DCards      = new Dictionary<string, Card>();
			DCode       = new Dictionary<string, Function>();

#region AddInternalCode

			DCode.Add("Add",      new Function {Code = new Add()});
			DCode.Add("Divide",   new Function {Code = new Divide()});
			DCode.Add("Multiply", new Function {Code = new Multiply()});
			DCode.Add("Subtract", new Function {Code = new Subtract()});

			//Add internal code to LCode
			foreach (var code in DCode)
				LCode.Add(code.Value.Code);

#endregion

			foreach (IMod activeMod in LoadMods.ActiveMods) activeMod.GetFunctions(ref DCode, ref LCode);

			IsLoaded = true;

			/*
			string s = LoadMods.ActiveMods.Count + " loaded functions:";
			foreach (var function in DCode)
				s += "\n" + function.Key + " {\n" + function.Value.Code.SourceCode + "\n}\n";
			MessageHandler.ShowMessage(s);
			*/
		}

		public static Character Character(int    id)   => LCharacter[id];
		public static Character Character(string name) => DCharacters[name];

		public static Card Card(int    id)   => LCards[id];
		public static Card Card(string name) => DCards[name];

		public static ICode    Code(int    id)   => LCode[id];
		public static Function Code(string name) => DCode[name];
	}
}