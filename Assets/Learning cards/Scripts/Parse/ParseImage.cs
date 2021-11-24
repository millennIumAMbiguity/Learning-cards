using System.Xml;
using Learning_cards.Scripts.UI.Messages;
using Learning_cards.Scripts.UI.XML;
using UnityEngine;
using UnityEngine.UI;

namespace Learning_cards.Scripts.Parse
{
	public static partial class Parse
	{
		public static void ParseValue(this Image img, string name, string value)
		{
			switch (name) {
				case "color":
					img.color = img.color.ParseValue(value);
					break;
				case "src": {
					var tex = new Texture2D(1,1);
					if (tex.LoadImage(System.IO.File.ReadAllBytes(UIXmlDesigner.TargetPath + '\\' + value)))
                        img.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
					else MessageHandler.ShowError("Target texture not found at target path: \"" + value + "\".");
					break;
				}
			}
		}

		public static void ParseAttributes(this Image img, XmlAttributeCollection attributes)
		{
			foreach (XmlAttribute attribute in attributes)
				img.ParseValue(attribute.Name, attribute.Value);
		}
	}
}