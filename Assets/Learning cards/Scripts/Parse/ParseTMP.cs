using System.Xml;
using Learning_cards.Scripts.UI.Messages;
using Learning_cards.Scripts.UI.XML.Layouts;
using TMPro;
using UnityEngine;

namespace Learning_cards.Scripts.Parse
{
	public static partial class Parse
	{
		public static bool ParseValue(this TMP_Text text, string name, string value)
		{
			switch (name) {
				case "fontSize":
					text.fontSize = float.Parse(value);
					return true;
				case "color":
					text.color.ParseValue(value);
					return true;
			}

			MessageHandler.ShowError(string.Format(InvalidAttributeErrorMsg, name));
			return false;
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
		
		public static bool ParseValue(this TMP_Text text, string name, string value, XmlLayout baseLayout)
		{
			if (text.ParseValue(name, value)) return true;
			if (name == "reference") {
				baseLayout.textReferences.Add(text);
				text.name = value;
				return true;
			}

			MessageHandler.ShowError(string.Format(InvalidAttributeErrorMsg, name));
			return false;

		}

		public static void ParseAttributes(this TMP_Text text, XmlAttributeCollection attributes, XmlLayout baseLayout)
		{
			foreach (XmlAttribute attribute in attributes)
				text.ParseValue(attribute.Name, attribute.Value, baseLayout);
		}

		public static void ParseNode(this TMP_Text text, XmlNode node, XmlLayout baseLayout)
		{
			text.ParseAttributes(node.Attributes, baseLayout);
			text.text = node.InnerText;
		}
	}
}