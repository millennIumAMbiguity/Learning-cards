﻿using System.Xml;
using UnityEngine;
using UnityEngine.UI;

namespace Learning_cards.Scripts.Parse
{
	public static partial class Parse
	{
		public static void ParseValue(this RawImage img, string name, string value)
		{
			switch (name) {
				case "Color":
					if (ColorUtility.TryParseHtmlString(value, out Color c))
						img.color = c;
					break;
			}
		}

		public static void ParseAttributes(this RawImage img, XmlAttributeCollection attributes)
		{
			foreach (XmlAttribute attribute in attributes)
				img.ParseValue(attribute.Name, attribute.Value);
		}
	}
}