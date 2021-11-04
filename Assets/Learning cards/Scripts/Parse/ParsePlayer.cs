using System;
using Learning_cards.Scripts.Data;
using Learning_cards.Scripts.Data.Classes;
using Learning_cards.Scripts.UI.Messages;

namespace Learning_cards.Scripts.Parse
{
	public static partial class Parse
	{
		public static string ParseGet(this Player player, string name)
		{
			return name switch {
				"cards"     => throw new NotImplementedException(),
				"character" => throw new NotImplementedException(),
				"code"      => player.Code.SourceCode,
				"title"     => player.Title,
				_           => player.Variables[name]
			};
		}

		public static void ParseValue(this Player player, string name, string value, string setType = "=")
		{
			switch (name) {
				case "cards":
					throw new NotImplementedException();
				case "character":
					throw new NotImplementedException();
				case "code":
					switch (setType) {
						case "=":
							player.Code.SourceCode = value;
							return;
						case "+=":
							player.Code.SourceCode += value;
							return;
						default:
							MessageHandler.ShowError(string.Format(InvalidSetTypeErrorMsg + InvalidSetTypeValidSets2, setType));
							return;
					}
				case "title":
					switch (setType) {
						case "=":
							player.Title = value;
							return;
						case "+=":
							player.Title += value;
							return;
						default:
							MessageHandler.ShowError(string.Format(InvalidSetTypeErrorMsg + InvalidSetTypeValidSets2, setType));
							return;
					}
				default:
					player.Variables.ParseDictionary(name, value, setType);
					return;
			}
		}
	}
}