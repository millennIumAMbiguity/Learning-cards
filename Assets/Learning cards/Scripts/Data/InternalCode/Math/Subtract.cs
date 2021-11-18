using Learning_cards.Scripts.Data.Classes;
using Learning_cards.Scripts.UI.Messages;

namespace Learning_cards.Scripts.Data.InternalCode.Math
{
	public class Subtract : ICode
	{
		public string Execute(string input) => SExecute(input);

		public static string SExecute(string input)
		{
			if (input is null) return "0";
			string[] arguments   = input.Split(',');
			
			if (!float.TryParse(arguments[0], out float returnValue)) {
				MessageHandler.ShowError($"Failed to convert {arguments[0]} to type float.\nSubtract({input})");
				return "NaN";
			}

			for (int index = 1; index < arguments.Length; index++) {
				if (float.TryParse(arguments[index], out float result)) returnValue -= result;
				else
					MessageHandler.ShowError(
						$"Failed to convert {arguments[index]} to type float.\nSubtract({input})");
			}

			return returnValue.ToString();
		}
	}
}