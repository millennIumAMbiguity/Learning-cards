using Learning_cards.Scripts.Data.Classes;
using Learning_cards.Scripts.UI.Messages;
using Learning_cards.Scripts.UI.XML;

namespace Learning_cards.Scripts.Data.InternalCode.Layout
{
	public class LayoutClear : ICode
	{
		public string Execute(string input)
		{
			for (int i = 0; i < UIXmlDesigner.UIElements.Length; i++) {
				UIXmlDesigner.UIElements[i].Destroy();
				UIXmlDesigner.UIElements[i] = null;
			}

			return "NaN";
		}
	}
}