using Learning_cards.Scripts.Data.Classes;
using Learning_cards.Scripts.UI.Messages;

namespace Learning_cards.Scripts.Data.InternalCode
{
	public class IfStatement : ICode
	{
		public string Execute(string input)
		{
			string[] args = input.Split(',');
			if (args.Length < 2)
				MessageHandler.ShowMessage(
					"<size=+6><b><color=red>ERROR:</color></b><size=-6>\nIf statement did not have enough arguments.");
			else if (!bool.TryParse(args[0], out bool result))
				MessageHandler.ShowMessage(
					$"<size=+6><b><color=red>ERROR:</color></b><size=-6>\n\"{args[0]}\" is not a boolean value.");
			else if (result) return args[1].Trim();
			else if (args.Length > 2) return args[2].Trim();

			return "NaN";
		}
	}
}