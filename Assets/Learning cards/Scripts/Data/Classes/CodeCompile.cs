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
		}
	}
}