using System;
using UnityEditor;
using UnityEngine;

namespace Learning_cards.Scripts.UI.ColorPallet
{
	public class ColorManager : MonoBehaviour
	{
		public static UnityEngine.Color[] PalletColors = new[] {
			new UnityEngine.Color(0.1019608f, 0.1019608f, 0.1019608f, 1f),
			new UnityEngine.Color(0.2196079f, 0.2196079f, 0.2196079f, 1f),
			new UnityEngine.Color(0.372549f, 0.372549f, 0.372549f, 1f),
			new UnityEngine.Color(0.7607843f, 0.7607843f, 0.7607843f, 1f),
			new UnityEngine.Color(1f, 1f, 1f, 1f)
		};

		[SerializeField] private SOColorPallet pallet;

		private ColorManager() { RefreshColor(); }

		public void RefreshColor()
		{
			if (pallet is { })
				PalletColors = pallet.pallet;
		}
	}

	//BaseClass[] components = Resources.FindObjectsOfTypeAll<BaseClass>();

	[CustomEditor(typeof(ColorManager))]
	public class ColorManagerEditor : Editor
	{
		public override void OnInspectorGUI()
		{
			if (GUILayout.Button("Update all in scene")) {
				(target as ColorManager).RefreshColor();
				var colorPickers = Resources.FindObjectsOfTypeAll<ColorPickerGraphic>();
				foreach (var colorPicker in colorPickers) { colorPicker.UpdateColor(); }
			}

			base.OnInspectorGUI();
		}
	}
}