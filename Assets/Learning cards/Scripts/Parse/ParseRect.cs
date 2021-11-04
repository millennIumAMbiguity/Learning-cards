using System.Xml;
using UnityEngine;

namespace Learning_cards.Scripts.Parse
{
	public static partial class Parse
	{
		public static void ParseValue(this RectTransform rect, string name, string value)
		{
			switch (name) {
				case "X": {
					var position = rect.position;
					rect.position = new Vector3(float.Parse(value), position.y, position.z);
					break;
				}
				case "Y": {
					var position = rect.position;
					rect.position = new Vector3(position.x, float.Parse(value), position.z);
					break;
				}
				case "Z": {
					var position = rect.position;
					rect.position = new Vector3(position.x, position.y, float.Parse(value));
					break;
				}
				case "Width":
					rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, float.Parse(value));
					break;
				case "Height":
					rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, float.Parse(value));
					break;
				case "AnchorsMinX":
					rect.anchorMin = new Vector2(float.Parse(value), rect.anchorMin.y);
					break;
				case "AnchorsMinY":
					rect.anchorMin = new Vector2(rect.anchorMin.x, float.Parse(value));
					break;
				case "AnchorsMaxX":
					rect.anchorMax = new Vector2(float.Parse(value), rect.anchorMax.y);
					break;
				case "AnchorsMaxY":
					rect.anchorMax = new Vector2(rect.anchorMax.x, float.Parse(value));
					break;
				case "PivotX":
					rect.pivot = new Vector2(float.Parse(value), rect.pivot.y);
					break;
				case "PivotY":
					rect.pivot = new Vector2(rect.pivot.x, float.Parse(value));
					break;
				case "RotationX": {
					var rotation = rect.rotation.eulerAngles;
					rect.rotation = Quaternion.Euler(float.Parse(value), rotation.y, rotation.z);
					break;
				}
				case "RotationY": {
					var rotation = rect.rotation.eulerAngles;
					rect.rotation = Quaternion.Euler(rotation.x, float.Parse(value), rotation.z);
					break;
				}
				case "RotationZ": {
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
					case "X":
						pos.x = float.Parse(attribute.Value);
						break;
					case "Y":
						pos.y = float.Parse(attribute.Value);
						break;
					case "Z":
						pos.z = float.Parse(attribute.Value);
						break;
					case "Width":
						size.x = float.Parse(attribute.Value);
						break;
					case "Height":
						size.y = float.Parse(attribute.Value);
						break;
					case "AnchorsMinX":
						anchorsMin.x = float.Parse(attribute.Value);
						break;
					case "AnchorsMinY":
						anchorsMin.y = float.Parse(attribute.Value);
						break;
					case "AnchorsMaxX":
						anchorsMax.x = float.Parse(attribute.Value);
						break;
					case "AnchorsMaxY":
						anchorsMax.y = float.Parse(attribute.Value);
						break;
					case "PivotX":
						pivot.x = float.Parse(attribute.Value);
						break;
					case "PivotY":
						pivot.y = float.Parse(attribute.Value);
						break;
					case "RotationX":
						rot.x = float.Parse(attribute.Value);
						break;
					case "RotationY":
						rot.y = float.Parse(attribute.Value);
						break;
					case "RotationZ":
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