using UnityEngine;
using UnityEngine.UI;

namespace Learning_cards.Scripts.UI
{
	public class PlayerPrefsToggle : MonoBehaviour
	{
		[SerializeField] private Toggle toggle;
		[SerializeField] private string propertyName;
		[SerializeField] private bool   defaultValue;

		private void Awake()
		{
			if (propertyName != "" && toggle is { })
				toggle.isOn = PlayerPrefs.GetInt(propertyName, defaultValue ? 1 : 0) == 1;
		}

		public void UpdateProperty(bool state)
		{
			if (propertyName != "")
				PlayerPrefs.SetInt(propertyName, state ? 1 : 0);
		}

		public void UpdateProperty()
		{
			if (propertyName != "" && toggle is { })
				PlayerPrefs.SetInt(propertyName, toggle.isOn ? 1 : 0);
		}
	}
}