using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Learning_cards.Scripts.Data.Classes
{
	public class Code : ICode
	{
		private string _compiledCode;

		private bool   _isCompiled;
		private string _sourceCode;

		public Code(string scoreCode = null) => _sourceCode = scoreCode;

		public string SourceCode {
			get => _sourceCode;
			set {
				_sourceCode = value;
				_isCompiled = false;
			}
		}

		public string CompiledCode {
			get {
				if (!_isCompiled) Compile();
				return _compiledCode;
			}
		}

		public string Execute(string input = null)
		{
			//return CompiledCode;
			string compiledCode             = CompiledCode;
			if (input != null) compiledCode = compiledCode.Replace("?", input);

			string[] rows = compiledCode.Split(';');

			foreach (string row in rows) {
				//run external functions
				string newRow = RunFunctions(row);
				//check for return
				if (row.Substring(0, 7) == "return ")
					return newRow.Substring(7);

				SetVar(newRow);
			}

			return default;
		}

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
					switch (spit[1]) {
						case "cards":
							throw new NotImplementedException();
						case "character":
							throw new NotImplementedException();
						case "code":
							return targetPlayer.Code._sourceCode;
						case "title":
							return targetPlayer.Title;
						default:
							return targetPlayer.Variables[spit[1]];
					}
				}
				default: throw new Exception("Specified type not found.");
			}
		}

		private static void SetVar(string newRow)
		{
			string[] words = newRow.Split(' ');
			if (words.Length <= 2) return;
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

					default: throw new Exception(words[1] + " was not recognized as a valid action.");
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
							}

							throw new Exception(words[1] + " was not recognized as a valid action.");
						case "title":
							switch (words[1]) {
								case "=":
									targetPlayer.Title = deltaValue;
									return;
								case "+=":
									targetPlayer.Title += deltaValue;
									return;
							}

							throw new Exception(words[1] + " was not recognized as a valid action.");
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
							}

							throw new Exception(words[1] + " was not recognized as a valid action.");
					}
				}
				default: throw new Exception("Specified type not found.");
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

			if (id == -1) throw new Exception("was expecting an index");
			return spit;
		}

		private static string RunFunctions(string row)
		{
			string newRow = "";

			string functionName           = "";
			string functionContent        = "";
			string functionNameOfChild    = "";
			string functionContentOfChild = "";
			int    depth                  = 0;
			foreach (char rowChar in row)
				switch (rowChar) {
					case ' ':
						switch (depth) {
							case 0:
								newRow       += functionName + ' ';
								functionName =  "";
								break;
							case 1:
								functionNameOfChild = "";
								break;
							default:
								functionContentOfChild += ' ';
								break;
						}

						break;
					case '(':
						switch (depth) {
							case 0:
								functionContent = "";
								break;
							case 1:
								functionContentOfChild = "";
								functionContent        = functionContent.Substring(0, functionNameOfChild.Length - 1);
								break;
							default:
								functionContentOfChild += '(';
								break;
						}

						depth++;
						break;
					case ')':
						depth--;
						switch (depth) {
							case 0:
								newRow       += Dictionaries.Code(functionName).Code.Execute(GetVar(functionContent));
								functionName =  "";
								break;
							case 1:
								//if function in depth 1 contains function, call RunFunctions()
								if (functionContentOfChild.Contains('('))
									functionContentOfChild = RunFunctions(functionContentOfChild);
								functionContent += Dictionaries.Code(functionNameOfChild).Code
															   .Execute(GetVar(functionContentOfChild));
								break;
							default:
								functionContentOfChild += ')';
								break;
						}

						break;
					default:
						switch (depth) {
							case 0:
								functionName += rowChar;
								break;
							case 1:
								functionContent     += rowChar;
								functionNameOfChild += rowChar;
								break;
							default:
								functionContentOfChild += rowChar;
								break;
						}

						break;
				}

			if (functionName != "") newRow += functionName;

			return newRow;
		}

		public void Compile()
		{
			_compiledCode = "";
			if (SourceCode is null) return;
			var      stringBuilder = new StringBuilder("");
			string[] rows          = SourceCode.Replace("return;", "return NaN;").Split(';');

			foreach (string row in rows) {
				string trimmedRow = row.Trim();
				if (trimmedRow == "") continue;
				trimmedRow = trimmedRow.Replace("player.", "players[0].").Replace("opponent.", "players[1]");
				var wordsInRow = new List<string>(trimmedRow.Split(' '));
				if (wordsInRow.Count == 1) {
					if (trimmedRow.Length > 2) {
						string last2 = trimmedRow.Substring(trimmedRow.Length - 2);
						switch (last2) {
							case "++":
								stringBuilder.Append(trimmedRow.Substring(0, trimmedRow.Length - 2));
								stringBuilder.Append(" += 1;");
								continue;
							case "--":
								stringBuilder.Append(trimmedRow.Substring(0, trimmedRow.Length - 2));
								stringBuilder.Append(" += -1;");
								continue;
						}
					}

					stringBuilder.Append(trimmedRow + ";");
				} else {
					for (int i = 2; i < wordsInRow.Count; i++) {
						switch (wordsInRow[i]) {
							//if (wordsInRow.Length < i + 1) break;
							case "+":
								wordsInRow[i - 1] = "Add(" + wordsInRow[i - 1];
								goto Ending;
							case "-":
								wordsInRow[i - 1] = "Subtract(" + wordsInRow[i - 1];
								goto Ending;
							case "*":
								wordsInRow[i - 1] = "Multiply(" + wordsInRow[i - 1];
								goto Ending;
							case "/":
								wordsInRow[i - 1] = "Divide(" + wordsInRow[i - 1];
								goto Ending;
							default:
								continue;
						}

						Ending:
						wordsInRow[i - 1] += ",";
						wordsInRow[i + 1] += ")";
						wordsInRow.RemoveAt(i);
						i++;
					}

					stringBuilder.Append(string.Join(" ", wordsInRow));
				}

				stringBuilder.Append(';');
			}

			//add space after coma
			for (int i = 0; i < stringBuilder.Length - 1; i++)
				if (stringBuilder[i] == ',' && stringBuilder[i + 1] != ' ') {
					stringBuilder.Insert(i + 1, ' ');
					i++;
				}

			//remove ending semicolon
			while (stringBuilder.Length > 0 && stringBuilder[stringBuilder.Length - 1] == ';')
				stringBuilder.Remove(stringBuilder.Length - 1, 1);

			_compiledCode = stringBuilder.ToString();
			_isCompiled   = true;
		}
	}
}