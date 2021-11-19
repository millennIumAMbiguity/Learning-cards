using System.Collections.Generic;
using Learning_cards.Scripts.Data.Classes;
using Learning_cards.Scripts.Data.InternalCode;
using Learning_cards.Scripts.Data.InternalCode.Comparators;
using Learning_cards.Scripts.Data.InternalCode.Layout;
using Learning_cards.Scripts.Data.InternalCode.Math;
using Learning_cards.Scripts.Mods;
using Learning_cards.Scripts.Mods.Mod;
using Learning_cards.Scripts.UI.Messages;

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
		public static  Dictionary<string, string>    DGlobalVariables;

		public static void Load()
		{
			if (IsLoaded) return;

			Players = new List<Player>();

			LCharacter = new List<Character>();
			LCards     = new List<Card>();
			LCode      = new List<ICode>();

			DCharacters      = new Dictionary<string, Character>();
			DCards           = new Dictionary<string, Card>();
			DCode            = new Dictionary<string, Function>();
			DGlobalVariables = new Dictionary<string, string>();

#region AddInternalCode

			DCode.Add("If", new Function { Code = new IfStatement() });

			DCode.Add("Add", new Function { Code      = new Add() });
			DCode.Add("Divide", new Function { Code   = new Divide() });
			DCode.Add("Multiply", new Function { Code = new Multiply() });
			DCode.Add("Subtract", new Function { Code = new Subtract() });

			DCode.Add("Equals", new Function { Code         = new Equals() });
			DCode.Add("NotEquals", new Function { Code      = new NotEquals() });
			DCode.Add("Greater", new Function { Code        = new Greater() });
			DCode.Add("GreaterOrEqual", new Function { Code = new GreaterOrEqual() });
			DCode.Add("Less", new Function { Code           = new Less() });
			DCode.Add("LessOrEqual", new Function { Code    = new LessOrEqual() });

			DCode.Add("Layout.Add", new Function { Code    = new LayoutAdd() });
			DCode.Add("Layout.Remove", new Function { Code = new LayoutRemove() });
			DCode.Add("Layout.Set", new Function { Code    = new LayoutSet() });
			DCode.Add("Layout.Clear", new Function { Code  = new LayoutClear() });

			DCode.Add("Msg", new Function { Code = new Msg() });

			//Add internal code to LCode
			foreach (var code in DCode)
				LCode.Add(code.Value.Code);

#endregion

			//load mods into dictionaries
			foreach (Mod activeMod in LoadMods.ActiveMods) {
				activeMod.GetFunctions(ref DCode, ref LCode);
				activeMod.GetCharacters(ref DCharacters, ref LCharacter);
			}

			IsLoaded = true;

			/*
			string s = LoadMods.ActiveMods.Count + " loaded functions:";
			foreach (var function in DCode)
				s += "\n" + function.Key + " {\n" + function.Value.Code.SourceCode + "\n}\n";
			MessageHandler.ShowMessage(s);
			*/
		}

		public static Character Character(int id) => LCharacter.Count > id ? LCharacter[id] : null;

		public static Character Character(string name)
		{
			if (DCharacters.TryGetValue(name, out var character))
				return character;

			MessageHandler.ShowMessage("Character \"" + name + "\" not found!");
			return null;
		}

		public static Card Card(int id) => LCards.Count > id ? LCards[id] : null;

		public static Card Card(string name)
		{
			if (DCards.TryGetValue(name, out var card))
				return card;

			MessageHandler.ShowMessage("Card \"" + name + "\"not found!");
			return null;
		}

		public static ICode Code(int id) => LCode.Count > id ? LCode[id] : null;

		public static Function Code(string name)
		{
			if (DCode.TryGetValue(name, out var function))
				return function;

			MessageHandler.ShowError("Function \"" + name + "\" not found.");
			return null;
		}

		public static string GlobalVariables(string name) =>
			DGlobalVariables.TryGetValue(name, out string value) ? value : "NaN";


#region staticFuntions

		public static void AddNewPlayer(Character character) => AddNewPlayer(new Player(character));

		public static void AddNewPlayer(Player player)
		{
			Players.Add(player);
			player.Code.Execute("start");
		}

		public static void AddToDictionary(Dictionary<string, string> dictionary, string name, string value)
		{
			if (dictionary.ContainsKey(name)) {
				if (float.TryParse(dictionary[name], out float val)) {
					if (!float.TryParse(value, out float valIn)) return;
					dictionary[name] = (val + valIn).ToString();
					return;
				}

				dictionary[name] += value;
			} else
				dictionary.Add(name, value);
		}

		public static void SubToDictionary(Dictionary<string, string> dictionary, string name, string value)
		{
			if (dictionary.ContainsKey(name)) {
				if (float.TryParse(dictionary[name], out float val)) {
					if (!float.TryParse(value, out float valIn)) return;
					dictionary[name] = (val - valIn).ToString();
					return;
				}

				dictionary[name] = value;
			} else
				dictionary.Add(name, value);
		}

		public static void SetToDictionary(Dictionary<string, string> dictionary, string name, string value)
		{
			if (dictionary.ContainsKey(name)) dictionary[name] = value;
			else dictionary.Add(name, value);
		}

#endregion
	}
}