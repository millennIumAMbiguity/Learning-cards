using System.Xml;
using Learning_cards.Scripts.UI.ColorPallet;
using Learning_cards.Scripts.UI.Messages;
using UnityEngine;
using UnityEngine.UI;

namespace Learning_cards.Scripts.Parse
{
	public static partial class Parse
	{
		public static void ParseValue(this Color color, string value)
		{
			if (value.Length == 1 && int.TryParse(value, out int colorId) && colorId < ColorManager.PalletColors.Length) {
				color = ColorManager.PalletColors[colorId];
			} else if (ColorUtility.TryParseHtmlString(value, out Color c))
				color = c;
			else MessageHandler.ShowError("Input not a valid color.\n\"" + value + "\"");
		}
	}
}