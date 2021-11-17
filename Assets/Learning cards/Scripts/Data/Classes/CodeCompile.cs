using System.Collections.Generic;
using System.Text;

namespace Learning_cards.Scripts.Data.Classes
{
	public partial struct Code
	{
		public void Compile()
		{
			var jumpPoints = new Dictionary<string, int>();
			int newRowId   = 1;
			_compiledCode = "";
			if (SourceCode is null) return;
			var      stringBuilder = new StringBuilder("");
			string[] rows          = SourceCode.Replace("return;", "return NaN;").Split(';');

			foreach (string row in rows) {
				string trimmedRow = row.Trim();
				if (trimmedRow == "") continue;
				trimmedRow = trimmedRow.Replace("player.", "players[0].").Replace("opponent.", "players[1]")
									   .Replace(" (", "(").Replace("if(", "If(");
				var wordsInRow = new List<string>(trimmedRow.Split(' '));

				//Collect jump points
				if (wordsInRow[0][wordsInRow[0].Length - 1] == ':') {
					jumpPoints.Add(wordsInRow[0].Substring(0, wordsInRow[0].Length - 1), newRowId);
					if (wordsInRow.Count < 2) continue;
					wordsInRow.RemoveAt(0);
				}

				if (wordsInRow.Count == 1) {
					if (trimmedRow.Length > 2) {
						string last2 = trimmedRow.Substring(trimmedRow.Length - 2);
						switch (last2) {
							case "++":
								stringBuilder.Append(trimmedRow.Substring(0, trimmedRow.Length - 2));
								stringBuilder.Append(" += 1;");
								newRowId++;
								continue;
							case "--":
								stringBuilder.Append(trimmedRow.Substring(0, trimmedRow.Length - 2));
								stringBuilder.Append(" += -1;");
								newRowId++;
								continue;
						}
					}

					newRowId++;
					stringBuilder.Append(trimmedRow + ";");
				} else {
					FormatMath(
						ref wordsInRow, new[]
							{ "*", "/" }, new[]
							{ "Multiply", "Divide" });

					FormatMath(
						ref wordsInRow, new[]
							{ "+", "-" }, new[]
							{ "Add", "Subtract" });

					stringBuilder.Append(string.Join(" ", wordsInRow));
				}

				newRowId++;
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

			//insert jump points
			foreach (var jumpPoint in jumpPoints)
				stringBuilder.Replace(jumpPoint.Key, jumpPoint.Value.ToString());

			_compiledCode = stringBuilder.ToString();
			_isCompiled   = true;

			static void FormatMath(ref List<string> words, string[] mathSymbol, string[] mathFunctionName)
			{
				for (int wordId = 2; wordId < words.Count - 1; wordId++) {
					string word = words[wordId];
					for (int i = 0; i < mathSymbol.Length; i++) {
						if (word != mathSymbol[i]) continue;
						words[wordId - 1] = $"{mathFunctionName[i]}({words[wordId - 1]}, {words[wordId + 1]})";
						words.RemoveAt(wordId + 1);
						words.RemoveAt(wordId);
						wordId -= 2;
						break;
					}
				}
			}
		}
	}
}