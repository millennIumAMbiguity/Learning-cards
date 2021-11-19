using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Learning_cards.Scripts.UI.ColorPallet
{
	public class ColorPickerSelectable : ColorPicker
	{
		[SerializeField] private Selectable target;
		[SerializeField] private int    normalColor = 0, selectedColor = 0, pressedColor = 4, highlightedColor = 3, disabledColor = 0;

		public override void UpdateColor()
		{
			ColorBlock colors = target.colors;
			colors.normalColor      = ColorManager.PalletColors[normalColor];
			colors.selectedColor    = ColorManager.PalletColors[selectedColor];
			colors.pressedColor     = ColorManager.PalletColors[pressedColor];
			colors.highlightedColor = ColorManager.PalletColors[highlightedColor];
			colors.disabledColor    = ColorManager.PalletColors[disabledColor];

			target.colors      = colors;
			#if UNITY_EDITOR
			EditorUtility.SetDirty(target);
			#endif
		}
	}
}