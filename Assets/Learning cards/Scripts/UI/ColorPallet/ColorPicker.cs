using System;
using UnityEngine;
using UnityEngine.UI;

namespace Learning_cards.Scripts.UI.ColorPallet
{
	public class ColorPicker : MonoBehaviour
	{
		private void Start()
		{
			if (this.enabled) UpdateColor();
		}

		public virtual void UpdateColor() { }
	}
}