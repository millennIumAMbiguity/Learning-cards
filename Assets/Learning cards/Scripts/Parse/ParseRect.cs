using System.Xml;
using UnityEngine;

namespace Learning_cards.Scripts.Parse
{
	public static partial class Parse
	{
		public static void ParseValue(this RectTransform rect, string name, string value)
		{
			switch (name) {
				case "x": {
					var position = rect.position;
					rect.position = new Vector3(float.Parse(value), position.y, position.z);
					break;
				}
				case "y": {
					var position = rect.position;
					rect.position = new Vector3(position.x, float.Parse(value), position.z);
					break;
				}
				case "z": {
					var position = rect.position;
					rect.position = new Vector3(position.x, position.y, float.Parse(value));
					break;
				}
				case "width":
					rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, float.Parse(value));
					break;
				case "height":
					rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, float.Parse(value));
					break;
				case "anchorsMinX":
					rect.anchorMin = new Vector2(float.Parse(value), rect.anchorMin.y);
					break;
				case "anchorsMinY":
					rect.anchorMin = new Vector2(rect.anchorMin.x, float.Parse(value));
					break;
				case "anchorsMaxX":
					rect.anchorMax = new Vector2(float.Parse(value), rect.anchorMax.y);
					break;
				case "anchorsMaxY":
					rect.anchorMax = new Vector2(rect.anchorMax.x, float.Parse(value));
					break;
				case "pivotX":
					rect.pivot = new Vector2(float.Parse(value), rect.pivot.y);
					break;
				case "pivotY":
					rect.pivot = new Vector2(rect.pivot.x, float.Parse(value));
					break;
				case "rotationX": {
					var rotation = rect.rotation.eulerAngles;
					rect.rotation = Quaternion.Euler(float.Parse(value), rotation.y, rotation.z);
					break;
				}
				case "rotationY": {
					var rotation = rect.rotation.eulerAngles;
					rect.rotation = Quaternion.Euler(rotation.x, float.Parse(value), rotation.z);
					break;
				}
				case "rotationZ": {
					var rotation = rect.rotation.eulerAngles;
					rect.rotation = Quaternion.Euler(rotation.x, rotation.y, float.Parse(value));
					break;
				}
			}
		}

		public static void ApplyAttributes(this RectTransform rect, XmlAttributeCollection attributes)
		{
			Vector3 pos        = Vector3.zero;
			Vector3 scale      = Vector3.one;
			Vector3 rot        = Vector3.zero;
			var     size       = new Vector2(100, 100);
			var     anchorsMin = new Vector2(.5f, .5f);
			var     anchorsMax = new Vector2(.5f, .5f);
			var     pivot      = new Vector2(.5f, .5f);

			foreach (XmlAttribute attribute in attributes)
				switch (attribute.Name) {
					case "x":
						pos.x = float.Parse(attribute.Value);
						break;
					case "y":
						pos.y = float.Parse(attribute.Value);
						break;
					case "z":
						pos.z = float.Parse(attribute.Value);
						break;
					case "width":
						size.x = float.Parse(attribute.Value);
						break;
					case "height":
						size.y = float.Parse(attribute.Value);
						break;
					case "anchorsMinX":
						anchorsMin.x = float.Parse(attribute.Value);
						break;
					case "anchorsMinY":
						anchorsMin.y = float.Parse(attribute.Value);
						break;
					case "anchorsMaxX":
						anchorsMax.x = float.Parse(attribute.Value);
						break;
					case "anchorsMaxY":
						anchorsMax.y = float.Parse(attribute.Value);
						break;
					case "pivotX":
						pivot.x = float.Parse(attribute.Value);
						break;
					case "pivotY":
						pivot.y = float.Parse(attribute.Value);
						break;
					case "rotationX":
						rot.x = float.Parse(attribute.Value);
						break;
					case "rotationY":
						rot.y = float.Parse(attribute.Value);
						break;
					case "rotationZ":
						rot.z = float.Parse(attribute.Value);
						break;
				}

			rect.pivot         = pivot;
			rect.anchorMin     = anchorsMin;
			rect.anchorMax     = anchorsMax;
			rect.localPosition = pos;
			rect.localScale    = scale;
			rect.localRotation = Quaternion.Euler(rot);
			//rect.sizeDelta     = size;
			rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size.x);
			rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size.y);
		}
	}
}