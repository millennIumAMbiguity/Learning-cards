using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Learning_cards.Scripts.UI.ColorPallet
{
	public class ColorPickerGraphic : ColorPicker
	{
		[SerializeField] private Graphic target;
		[SerializeField] private int     colorId;

		public override void UpdateColor()
		{
			target.color = ColorManager.PalletColors[colorId];
			EditorUtility.SetDirty(target);
		}
	}
}