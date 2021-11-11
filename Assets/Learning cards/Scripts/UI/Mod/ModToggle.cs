using System;
using Learning_cards.Scripts.Data;
using Learning_cards.Scripts.Mods;
using Learning_cards.Scripts.Mods.Mod;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Learning_cards.Scripts.UI.Mod
{
	[RequireComponent(typeof(Toggle))]
	public class ModToggle : MonoBehaviour
	{
		[SerializeField] private TMP_Text title;
		[SerializeField] private TMP_Text version;

		private Mods.Mod.Mod _mod;

		public Mods.Mod.Mod Mod {
			private get => _mod;
			set {
				if (value.IsBuiltIn) {
					transform.GetComponentsInChildren<Transform>()[1].gameObject.SetActive(false);
				} else if (_mod is { IsBuiltIn: true }) {
					transform.GetComponentsInChildren<Transform>()[1].gameObject.SetActive(true);
				}
				_mod                        = value;
				title.text                  = value.Title;
				version.text                = value.Version;
				GetComponent<Toggle>().isOn = value.Active;
			}
		}

		public void Toggle(bool state)
		{
			#if UNITY_EDITOR
			if (Mod is null) throw new NullReferenceException("Mod is null");
			#endif
			if (Mod.Active == state) return;
			Dictionaries.IsLoaded = false;
			if (Mod.Active) {
				LoadMods.InactiveMods.Add(Mod);
				LoadMods.ActiveMods.Remove(Mod);
			} else {
				LoadMods.ActiveMods.Add(Mod);
				LoadMods.InactiveMods.Remove(Mod);
			}

			Mod.Active = state;
		}
	}
}