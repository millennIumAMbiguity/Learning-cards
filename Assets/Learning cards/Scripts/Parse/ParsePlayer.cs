using System;
using System.Collections.Generic;
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
				"cards"     => player.Cards.Count.ToString(),
				"character" => player.Character.Title,
				"code"      => player.Character.Code.SourceCode,
				"title"     => player.Title,
				_           => player.Variables[name]
			};
		}

		public static void ParseValue(this Player player, string name, string value, string setType = "=")
		{
			switch (name) {
				case "cards":
					Card card = int.TryParse(value, out int cardId)
						? new Card(Dictionaries.Card(cardId))
						: new Card(Dictionaries.Card(value));
					if (card.Title == "NaN") {
						MessageHandler.ShowError(string.Format(TargetNotFoundErrorMsg, "Card with key: "+ value));
						return;
					}
					switch (setType) {
						case "=":
							player.Cards = new Dictionary<string, Card>();
							player.Cards.Add(card.Title, card);
							return;
						case "+=":
							player.Cards.Add(card.Title, card);
							return;
						default:
							MessageHandler.ShowError(
								string.Format(InvalidSetTypeErrorMsg + InvalidSetTypeValidSets2, setType));
							return;
					}
				case "character":
					throw new NotImplementedException();
				case "code":
					switch (setType) {
						case "=":
							player.Character.Code.SetCode(value);
							return;
						case "+=":
							player.Character.Code.SetCode(player.Character.Code.SourceCode + value);
							return;
						default:
							MessageHandler.ShowError(
								string.Format(InvalidSetTypeErrorMsg + InvalidSetTypeValidSets2, setType));
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
							MessageHandler.ShowError(
								string.Format(InvalidSetTypeErrorMsg + InvalidSetTypeValidSets2, setType));
							return;
					}
				default:
					player.Variables.ParseDictionary(name, value, setType);
					return;
			}
		}
	}
}