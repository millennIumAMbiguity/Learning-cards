using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;

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
			string compiledCode             = CompiledCode;
			if (input != null) {
				compiledCode = compiledCode.Replace("?", input);
			}
			
			string[] rows = compiledCode.Split(';');
			
			foreach (string row in rows) {

				//run external functions
				string[] s      = row.Split('(', ')');
				string   newRow = "";
				for (int i = 1; i < s.Length; i += 2) {
					string targetFunction = s[i - 1].Split(' ').Last();

					newRow += 
						s[i - 1].Substring(0, s[i - 1].Length - targetFunction.Length) + 
						Dictionaries.Code(targetFunction).Code.Execute(s[i]);
				}

				if ((s.Length & 1) == 1) newRow += s.Last();
				
				if (row.Substring(0, 7) == "return ")
					return newRow.Substring(7);
			}

			return default;
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
						wordsInRow[i - 1] +=  ",";
						wordsInRow[i + 1] += ")";
						wordsInRow.RemoveAt(i);
						i++;
					}

					stringBuilder.Append(string.Join(" ", wordsInRow));

				}

				stringBuilder.Append(';');
			}

			_compiledCode = stringBuilder.ToString();
			_isCompiled   = true;
		}
	}
}