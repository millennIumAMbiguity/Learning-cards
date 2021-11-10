using System.Xml;
using UnityEngine;
using UnityEngine.UI;

namespace Learning_cards.Scripts.Parse
{
	public static partial class Parse
	{
		public static void ParseValue(this Outline outline, string name, string value)
		{
			switch (name) {
				case "color":
					if (ColorUtility.TryParseHtmlString(value, out Color c))
						outline.effectColor = c;
					break;
				case "width":
					float f = float.Parse(value);
					outline.effectDistance = new Vector2(f, f);
					break;
			}
		}

		public static void ParseAttributes(this Outline outline, XmlAttributeCollection attributes)
		{
			foreach (XmlAttribute attribute in attributes)
				outline.ParseValue(attribute.Name, attribute.Value);
		}
	}
}