using Learning_cards.Scripts.Data.Classes;
using Learning_cards.Scripts.UI.Messages;
using Learning_cards.Scripts.UI.XML;

namespace Learning_cards.Scripts.Data.InternalCode.Layout
{
	public class LayoutAdd : ICode
	{
		public string Execute(string input) =>
			int.TryParse(input, out int index)
				? UIXmlDesigner.NewUIElement(index).ToString()
				: UIXmlDesigner.NewUIElement(input).ToString();
	}
}