using System;
using System.Collections.Generic;
using Learning_cards.Scripts.UI.Messages;

namespace Learning_cards.Scripts.Data.Classes
{
	public partial class Code
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
				case "players": {
					Player targetPlayer = Dictionaries.Players[id];
					return spit[1] switch {
						"cards"     => throw new NotImplementedException(),
						"character" => throw new NotImplementedException(),
						"code"      => targetPlayer.Code._sourceCode,
						"title"     => targetPlayer.Title,
						_           => targetPlayer.Variables[spit[1]]
					};
				}
				default:
					MessageHandler.ShowMessage(
						"<size=+6><b><color=red>ERROR:</color></b><size=-6>\nSpecified type not found.");
					return "NaN";
			}
		}

		private static void SetVar(string newRow) => SetVar(newRow, newRow.Split(' '));

		private static void SetVar(string newRow, IReadOnlyList<string> words)
		{
			if (words.Count <= 2) return;
			string[] target     = words[0].Split('.');
			string   deltaValue = GetVars(newRow.Substring(words[0].Length + words[1].Length + 1).Trim());

			//set Global variable
			if (target.Length == 1)
				switch (words[1]) {
					case "=":
						Dictionaries.SetToDictionary(Dictionaries.DGlobalVariables, words[0], deltaValue);
						return;
					case "+=":
						Dictionaries.AddToDictionary(Dictionaries.DGlobalVariables, words[0], deltaValue);
						return;
					case "-=":
						Dictionaries.SubToDictionary(Dictionaries.DGlobalVariables, words[0], deltaValue);
						return;

					default:
						MessageHandler.ShowMessage(
							$"<size=+6><b><color=red>ERROR:</color></b><size=-6>\n\"{words[1]}\" was not recognized as a valid action.");
						return;
				}


			//set variable of object
			switch (GetId(target[0], out int id)) {
				case "players": {
					Player targetPlayer = Dictionaries.Players[id];
					switch (target[1]) {
						case "cards":
							throw new NotImplementedException();
						case "character":
							throw new NotImplementedException();
						case "code":
							switch (words[1]) {
								case "=":
									targetPlayer.Code.SourceCode = deltaValue;
									return;
								case "+=":
									targetPlayer.Code.SourceCode += deltaValue;
									return;
								default:
									MessageHandler.ShowMessage(
										$"<size=+6><b><color=red>ERROR:</color></b><size=-6>\n\"{words[1]}\" was not recognized as a valid action.");
									return;
							}
						case "title":
							switch (words[1]) {
								case "=":
									targetPlayer.Title = deltaValue;
									return;
								case "+=":
									targetPlayer.Title += deltaValue;
									return;
								default:
									MessageHandler.ShowMessage(
										$"<size=+6><b><color=red>ERROR:</color></b><size=-6>\n\"{words[1]}\" was not recognized as a valid action.");
									return;
							}
						default:
							switch (words[1]) {
								case "=":
									Dictionaries.SetToDictionary(targetPlayer.Variables, target[1], deltaValue);
									return;
								case "+=":
									Dictionaries.AddToDictionary(targetPlayer.Variables, target[1], deltaValue);
									return;
								case "-=":
									Dictionaries.SubToDictionary(targetPlayer.Variables, target[1], deltaValue);
									return;
								default:
									MessageHandler.ShowMessage(
										$"<size=+6><b><color=red>ERROR:</color></b><size=-6>\n\"{words[1]}\" was not recognized as a valid action.");
									return;
							}
					}
				}
				default:
					MessageHandler.ShowMessage(
						"<size=+6><b><color=red>ERROR:</color></b><size=-6>\nSpecified type not found.");
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
				MessageHandler.ShowMessage(
					"<size=+6><b><color=red>ERROR:</color></b><size=-6>\nIndex expected.");
				return "NaN";
			}

			return spit;
		}
	}
}