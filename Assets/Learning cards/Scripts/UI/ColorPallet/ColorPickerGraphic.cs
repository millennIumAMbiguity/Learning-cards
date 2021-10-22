using System;
using UnityEngine;
using UnityEngine.UI;

namespace Learning_cards.Scripts.UI.ColorPallet
{
	public class ColorPickerGraphic : MonoBehaviour
	{
		[SerializeField] private Graphic target;
		[SerializeField] private int     colorId;

		private void Awake()
		{
			if (this.enabled) UpdateColor();
		}

		public void UpdateColor()
		{
			target.color = ColorManager.PalletColors[colorId];
		}
	}
}