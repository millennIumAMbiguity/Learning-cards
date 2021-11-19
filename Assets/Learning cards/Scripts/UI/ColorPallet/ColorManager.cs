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

		private void Awake() { RefreshColor(); }

		public void RefreshColor()
		{
			if (pallet is { })
				PalletColors = pallet.pallet;
		}
	}

	//BaseClass[] components = Resources.FindObjectsOfTypeAll<BaseClass>();
#if UNITY_EDITOR
	[CustomEditor(typeof(ColorManager))]
	public class ColorManagerEditor : Editor
	{
		public override void OnInspectorGUI()
		{
			if (GUILayout.Button("Update all in scene")) {
				ColorManager cm = target as ColorManager;
				if (!cm) {
					base.OnInspectorGUI();
					return;
				}
				cm.RefreshColor();
				var colorPickers = Resources.FindObjectsOfTypeAll<ColorPicker>();
				foreach (var colorPicker in colorPickers) colorPicker.UpdateColor();
				if (Application.isPlaying) return;
				EditorApplication.QueuePlayerLoopUpdate();
				UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(cm.gameObject.scene);
			} 
			base.OnInspectorGUI();
		}
	}
	#endif
}