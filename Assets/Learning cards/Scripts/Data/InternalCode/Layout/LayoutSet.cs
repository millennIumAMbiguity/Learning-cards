using Learning_cards.Scripts.Data.Classes;
using Learning_cards.Scripts.UI.Messages;
using Learning_cards.Scripts.UI.XML;

namespace Learning_cards.Scripts.Data.InternalCode.Layout
{
	public class LayoutSet : ICode
	{
		public string Execute(string input)
		{
			for (int i = 1; i < UIXmlDesigner.UIElements.Length; i++) {
				UIXmlDesigner.UIElements[i].Destroy();
				UIXmlDesigner.UIElements[i] = null;
			}

			return int.TryParse(input, out int index)
				? UIXmlDesigner.NewUIElement(index, 0).ToString()
				: UIXmlDesigner.NewUIElement(input, 0).ToString();
		}
	}
}