using System.Linq;
using Learning_cards.Scripts.UI.Messages;

namespace Learning_cards.Scripts.Data.Classes
{
	public partial struct Code
	{
		public string Execute(string input = null)
		{
			int loopCount = 0;
			//return CompiledCode;
			string compiledCode             = CompiledCode;
			if (input != null) compiledCode = compiledCode.Replace("?", input);

			string[] rows = compiledCode.Split(';');

			for (int index = 0; index < rows.Length; index++) {
				//run external functions
				string newRow = RunFunctions(rows[index]);

				string[] words = newRow.Split(' ');
				switch (words[0]) {
					case "":
					case "NaN":
						continue;
					case "return":
						return GetVars(newRow.Substring(7));
					case "goto": {
						//protects against indefinite loops.
						if (loopCount++ > 1_000_000) {
							MessageHandler.ShowError(
								"goto was used over a million times.\nAssuming its a dead loop, the execution have stopped.");
							return "NaN";
						}

						string target = newRow.Substring(5);
						string symbol = target.Substring(0, 1);
						switch (symbol) {
							case "+": {
								if (!int.TryParse(target.Substring(1), out int res)) goto gotoIsNotNumber;
								index += res - 1;
								continue;
							}
							case "-": {
								if (!int.TryParse(target.Substring(1), out int res)) goto gotoIsNotNumber;
								index -= res + 1;
								continue;
							}
							default: {
								if (!int.TryParse(target, out int res)) goto gotoIsNotNumber;
								index = res - 2;
								continue;
							}
						}

						gotoIsNotNumber:
						MessageHandler.ShowError($"\"{target}\" not recognised as a goto point.");
						return "NaN";
					}
					default:
						SetVar(newRow, words);
						break;
				}
			}

			return "NaN";
		}

//split functions and arguments.
//hello + world(word1, add(1, add(2, 3))); ->
//{ "hello + ", "world", "word1, ", "add(1, add(2, 3))", ";" }
//and then runs internal functions.

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
								functionNameOfChild =  "";
								functionContent     += ' ';
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
								functionContent = functionContent.Substring(
									0,
									functionContent.Length - functionNameOfChild.Length);
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
								newRow       += Dictionaries.Code(functionName)?.Code.Execute(GetVar(functionContent));
								functionName =  "";
								break;
							case 1:
								//if function in depth 1 contains function, call RunFunctions()
								if (functionContentOfChild.Contains('('))
									functionContentOfChild = RunFunctions(functionContentOfChild);
								functionContent += Dictionaries.Code(functionNameOfChild)?.Code
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
	}
}