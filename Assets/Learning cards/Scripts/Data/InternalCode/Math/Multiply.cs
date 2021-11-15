﻿using Learning_cards.Scripts.Data.Classes;
using Learning_cards.Scripts.UI.Messages;

namespace Learning_cards.Scripts.Data.InternalCode.Math
{
	public class Multiply : ICode
	{
		public string Execute(string input) => SExecute(input);

		public static string SExecute(string input)
		{
			if (input is null) return "0";
			string[] arguments   = input.Split(',');
			float    returnValue = 0;
			foreach (string argument in arguments)
				if (float.TryParse(argument, out float result)) returnValue *= result;
				else
					MessageHandler.ShowMessage(
						$"<size=+6><b><color=red>ERROR:</color></b><size=-6>\nFailed to convert {argument} to type float.\nAdd( {input} )");

			return returnValue.ToString();
		}
	}
}