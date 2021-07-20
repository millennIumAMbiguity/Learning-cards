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
				string newRow = "";
				{
					newRow = RunFunctions(row);

					if (row.Substring(0, 7) == "return ")
						return newRow.Substring(7);
				}
				string[] words = newRow.Split(' ');
				if (words.Length > 2)
					switch (words[1]) {
						case "=":
							break;
						case "+=":
							break;
						case "-=":
							break;
					}
			}

			return default;
		}

		private static void AddToDictionary(Dictionary<string, string> dictionary, string name, string value)
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
						if (depth == 0) {
							newRow       += functionName + ' ';
							functionName =  "";
						} else if (depth == 1) functionNameOfChild =  "";
						else functionContentOfChild                += ' ';

						break;
					case '(':
						if (depth      == 0) functionContent = "";
						else if (depth == 1) {
							functionContentOfChild = "";
							functionContent        = functionContent.Substring(0, functionNameOfChild.Length - 1);
						} else functionContentOfChild += '(';

						depth++;
						break;
					case ')':
						depth--;
						switch (depth) {
							case 0:
								newRow       += Dictionaries.Code(functionName).Code.Execute(functionContent);
								functionName =  "";
								break;
							case 1:
								if (functionContentOfChild.Contains('('))
									functionContentOfChild = RunFunctions(functionContentOfChild);
								functionContent += Dictionaries.Code(functionNameOfChild).Code
								                               .Execute(functionContentOfChild);
								//functionNameOfChild =  "";
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

		/*
		 * player.hp++;
		 * target.hp += 1;
		 * players[0].deck += this as card;
		 * player.deck += GetCard(myCard);
		 */

		public void Compile()
		{
			_compiledCode = "";
			if (SourceCode is null) return;
			var      stringBuilder = new StringBuilder("");
			string[] rows          = SourceCode.Split(';');

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