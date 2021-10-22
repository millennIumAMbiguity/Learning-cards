using UnityEngine;

namespace Learning_cards.Scripts.UI.ColorPallet
{
	[CreateAssetMenu(fileName = "New ColorPallet", menuName = "Color Pallet", order = 0)]
	public class SOColorPallet : ScriptableObject
	{
		public Color[] pallet = new[] {
			new UnityEngine.Color(0.1019608f, 0.1019608f, 0.1019608f, 1f),
			new UnityEngine.Color(0.2196079f, 0.2196079f, 0.2196079f, 1f),
			new UnityEngine.Color(0.372549f, 0.372549f, 0.372549f, 1f),
			new UnityEngine.Color(0.7607843f, 0.7607843f, 0.7607843f, 1f),
			new UnityEngine.Color(1f, 1f, 1f, 1f)
		};
	}
}