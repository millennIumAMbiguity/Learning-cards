using Learning_cards.Scripts.Data.Classes;
using Learning_cards.Scripts.UI.Messages;
using Learning_cards.Scripts.UI.XML;

namespace Learning_cards.Scripts.Data.InternalCode.Layout
{
	public class LayoutRemove : ICode
	{
		public string Execute(string input)
		{
			//check if input is index
			if (!int.TryParse(input, out int index))
				//get index from UI element name
				for (int k = 0; k < UIXmlDesigner.UIElements.Length; k++) {
					if (UIXmlDesigner.UIElements[k].name != input) continue;
					index = k;
					break;
				}
			
			if (UIXmlDesigner.UIElements.Length <= index) return "NaN";
			UIXmlDesigner.UIElements[index].Destroy();
			UIXmlDesigner.UIElements[index] = null;
			return index.ToString();
		}
	}
}