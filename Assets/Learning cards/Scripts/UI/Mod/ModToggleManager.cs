using Learning_cards.Scripts.Mods;
using Learning_cards.Scripts.Mods.Mod;
using UnityEngine;

namespace Learning_cards.Scripts.UI.Mod
{
	public class ModToggleManager : MonoBehaviour
	{
		[SerializeField] private GameObject togglePrefab;
		[SerializeField] private Transform content;
			
		private void Start()
		{
			foreach (IMod mod in LoadMods.ActiveMods) {
				var toggle = Instantiate(togglePrefab, content).GetComponent<ModToggle>();
				toggle.Mod = mod;
			}

			foreach (IMod mod in LoadMods.InactiveMods) {
				var toggle = Instantiate(togglePrefab, content).GetComponent<ModToggle>();
				toggle.Mod = mod;
			}
		}
	}
}