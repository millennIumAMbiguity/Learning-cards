using Learning_cards.Scripts.Data.Classes;
using Learning_cards.Scripts.UI.Messages;

namespace Learning_cards.Scripts.Data.InternalCode
{
	public class Msg : ICode
	{
		public string Execute(string input)
		{
			MessageHandler.ShowMessage(input);
			return "";
		}
	}
}