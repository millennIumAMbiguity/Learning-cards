using System.Collections.Generic;
using Learning_cards.Scripts.Data.Classes;
using Learning_cards.Scripts.Data.InternalCode;
using Learning_cards.Scripts.Data.InternalCode.Comparators;
using Learning_cards.Scripts.Data.InternalCode.Math;
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
		public static  Dictionary<string, string>    DGlobalVariables;

		public static void Load()
		{
			if (IsLoaded) return;

			LCharacter = new List<Character>();
			LCards     = new List<Card>();
			LCode      = new List<ICode>();

			DCharacters      = new Dictionary<string, Character>();
			DCards           = new Dictionary<string, Card>();
			DCode            = new Dictionary<string, Function>();
			DGlobalVariables = new Dictionary<string, string>();

#region AddInternalCode

			DCode.Add("If", new Function {Code = new IfStatement()});

			DCode.Add("Add", new Function {Code      = new Add()});
			DCode.Add("Divide", new Function {Code   = new Divide()});
			DCode.Add("Multiply", new Function {Code = new Multiply()});
			DCode.Add("Subtract", new Function {Code = new Subtract()});

			DCode.Add("Equals", new Function {Code         = new Equals()});
			DCode.Add("NotEquals", new Function {Code      = new NotEquals()});
			DCode.Add("Greater", new Function {Code        = new Greater()});
			DCode.Add("GreaterOrEqual", new Function {Code = new GreaterOrEqual()});
			DCode.Add("Less", new Function {Code           = new Less()});
			DCode.Add("LessOrEqual", new Function {Code    = new LessOrEqual()});

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

		public static string GlobalVariables(string name) => DGlobalVariables[name];


#region staticFuntions

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