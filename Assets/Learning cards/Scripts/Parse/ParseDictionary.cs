using System.Collections.Generic;
using Learning_cards.Scripts.Data;
using Learning_cards.Scripts.UI.Messages;

namespace Learning_cards.Scripts.Parse
{
	public static partial class Parse
	{
		public static void ParseDictionary(
			this Dictionary<string, string> dictionary, string name, string value, string setType)
		{
			switch (setType) {
				case "=":
					Dictionaries.SetToDictionary(dictionary, name, value);
					return;
				case "+=":
					Dictionaries.AddToDictionary(dictionary, name, value);
					return;
				case "-=":
					Dictionaries.SubToDictionary(dictionary, name, value);
					return;
				default:
					MessageHandler.ShowError(string.Format(InvalidSetTypeErrorMsg + InvalidSetTypeValidSets1, setType));
					return;
			}
		}

		public static void ParseDictionary(this Dictionary<string, string> dictionary, string value, string name) =>
			Dictionaries.SetToDictionary(dictionary, name, value);
	}
}