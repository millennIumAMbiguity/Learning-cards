using System.Xml;
using TMPro;
using UnityEngine;

namespace Learning_cards.Scripts.Parse
{
	public static partial class Parse
	{
		public static void ParseValue(this TMP_Text text, string name, string value)
		{
			switch (name) {
				case "FontSize":
					text.fontSize = float.Parse(value);
					break;
				case "Color":
					if (ColorUtility.TryParseHtmlString(value, out Color c))
						text.color = c;
					break;
			}
		}

		public static void ParseAttributes(this TMP_Text text, XmlAttributeCollection attributes)
		{
			foreach (XmlAttribute attribute in attributes)
				text.ParseValue(attribute.Name, attribute.Value);
		}

		public static void ParseNode(this TMP_Text text, XmlNode node)
		{
			text.ParseAttributes(node.Attributes);
			text.text = node.InnerText;
		}
	}
}