using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Learning_cards.Scripts.UI.ColorPallet
{
	public class ColorPickerOutline : ColorPicker
	{
		[SerializeField] private Shadow target;
		[SerializeField] private int    colorId;

		public override void UpdateColor()
		{
			target.effectColor = ColorManager.PalletColors[colorId];
			#if UNITY_EDITOR
			EditorUtility.SetDirty(target);
			#endif
		}
	}
}