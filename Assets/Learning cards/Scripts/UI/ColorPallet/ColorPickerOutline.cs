using System;
using UnityEngine;
using UnityEngine.UI;

namespace Learning_cards.Scripts.UI.ColorPallet
{
	public class ColorPickerOutline : MonoBehaviour
	{
		[SerializeField] private Shadow target;
		[SerializeField] private int    colorId;

		private void Awake()
		{
			if (this.enabled) UpdateColor();
		}

		public void UpdateColor()
		{
			target.effectColor = ColorManager.PalletColors[colorId];
		}
	}
}