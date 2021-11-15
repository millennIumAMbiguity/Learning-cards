using System;
using UnityEngine;
using UnityEngine.UI;

namespace Learning_cards.Scripts.UI.ColorPallet
{
	public class ColorPickerSelectable : MonoBehaviour
	{
		[SerializeField] private Selectable target;
		[SerializeField] private int    normalColor = 0, selectedColor = 0, pressedColor = 4, highlightedColor = 3, disabledColor = 0;

		private void Start()
		{
			if (this.enabled) UpdateColor();
		}

		public void UpdateColor()
		{
			ColorBlock colors = target.colors;
			colors.normalColor      = ColorManager.PalletColors[normalColor];
			colors.selectedColor    = ColorManager.PalletColors[selectedColor];
			colors.pressedColor     = ColorManager.PalletColors[pressedColor];
			colors.highlightedColor = ColorManager.PalletColors[highlightedColor];
			colors.disabledColor    = ColorManager.PalletColors[disabledColor];

			target.colors      = colors;
		}
	}
}