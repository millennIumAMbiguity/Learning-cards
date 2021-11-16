using System;
using System.Collections.Generic;
using System.Linq;
using Learning_cards.Scripts.Parse;
using Learning_cards.Scripts.UI.Messages;
using Learning_cards.Scripts.UI.XML;
using Learning_cards.Scripts.UI.XML.Layouts;

namespace Learning_cards.Scripts.Data.Classes
{
	public partial struct Code
	{
		private static string GetVars(string s)
		{
			string[] arr                                                = s.Split(' ');
			for (int index = 0; index < arr.Length; index++) arr[index] = GetVar(arr[index]);
			return string.Join(" ", arr);
		}

		private static string GetVar(string s)
		{
			if (s[0] != '$') return s;
			s = s.Substring(1);
			string[] spit;

			//get global variable
			if ((spit = s.Split('.')).Length <= 1)
				return Dictionaries.GlobalVariables(s);

			//get object variable
			switch (GetId(spit[0], out int id)) {
				case "players": 
					return Dictionaries.Players[id].ParseGet(spit[1]);
				default:
					MessageHandler.ShowError(
						$"\"{spit[0]}\" was not recognised as a target class in:\n{s}");
					return "NaN";
			}
		}

		private static void SetVar(string newRow) => SetVar(newRow, newRow.Split(' '));

		private static void SetVar(string newRow, IReadOnlyList<string> words)
		{
			if (words.Count <= 2) return;
			string[] target     = words[0].Split('.');
			string   deltaValue = GetVars(newRow.Substring(words[0].Length + words[1].Length + 1).Trim());

			if (!target[0].Contains('[')) {
				//set Global variable
				if (target.Length == 1) {
					Dictionaries.DGlobalVariables.ParseDictionary(words[0], deltaValue,words[1]);
					return;
				}

				switch (target[0]) {
					case "players": {
						break;
					}
					case "layout": {
						break;
					}
					default: {
                        MessageHandler.ShowError(
                            $"\"{target[0]}\" was not recognised as a target class in:\n{newRow}");
                        return;
                    }
				}
				return;
			}

			//set variable of object
			switch (GetId(target[0], out int id)) {
				case "players": {
					if (target.Length == 2)
						Dictionaries.Players[id].ParseValue(target[1], deltaValue, words[1]);
					else MessageHandler.ShowError("at " + newRow);
					break;
				}
				case "layout": {
					if (target.Length == 1) {
						if (words[1] == "=") {
							if (UIXmlDesigner.UIElements[id] is { }) {
								UIXmlDesigner.UIElements[id].Destroy();
							}

							if (int.TryParse(deltaValue, out int LayoutId)) 
								UIXmlDesigner.NewUIElement(LayoutId, id);
							else 
								UIXmlDesigner.NewUIElement(deltaValue, id);
							
						} else {
							MessageHandler.ShowError(string.Format(Parse.Parse.InvalidSetTypeErrorMsg, words[1]) + Parse.Parse.InvalidSetTypeValidSets3);
						}
					}
					break;
				}
				default:
					MessageHandler.ShowError(
						$"\"{target[0]}\" was not recognised as a target class in:\n{newRow}");
					return;
			}
		}

		private static string GetId(string spit, out int id)
		{
			id = -1;
			string[] splitIdSplit = spit.Split('[');
			if (splitIdSplit.Length > 1) {
				id   = int.Parse(splitIdSplit[1].Substring(0, splitIdSplit[1].Length - 1));
				spit = splitIdSplit[0];
			}

			if (id == -1) {
				MessageHandler.ShowError(
					"Index expected.");
				return "NaN";
			}

			return spit;
		}
	}
}