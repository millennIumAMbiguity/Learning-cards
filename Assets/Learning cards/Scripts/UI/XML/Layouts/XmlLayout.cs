using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Learning_cards.Scripts.UI.XML.Layouts
{
	public class XmlLayout : MonoBehaviour
	{
		public void Destroy() => Destroy(gameObject);

		[HideInInspector] public List<TMP_Text> textReferences;
	}
}