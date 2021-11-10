using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Learning_cards.Scripts.UI.XML.Layouts
{
	public class XmlLayout : MonoBehaviour
	{
		public static List<XmlLayout> Layouts = new List<XmlLayout>();
		public static XmlLayout       GetLayout(string name) => Layouts.FirstOrDefault(layout => layout.name == name);

		[HideInInspector] public List<TMP_Text> textReferences;
	}
}