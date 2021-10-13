using System.Collections.Generic;
using System.IO;
using Learning_cards.Scripts.Mods.Mod;
using Learning_cards.Scripts.UI.Messages;
using UnityEngine;

namespace Learning_cards.Scripts.Mods
{
	public class LoadMods : MonoBehaviour
	{
		private const string ModsPath = "mods";

		public static List<IMod> ActiveMods   = new List<IMod>();
		public static List<IMod> InactiveMods = new List<IMod>();

		[SerializeField] private SOMod[] defaultContent;

		private void Awake()
		{
			//load default built in content
			foreach (SOMod mod in defaultContent)
				if (mod.Active) ActiveMods.Add(mod);
				else InactiveMods.Add(mod);

			//load mods
			Directory.CreateDirectory(ModsPath);
			foreach (string modPath in Directory.GetDirectories(ModsPath)) {
				var mod = new Mod.Mod(modPath);
				if (mod.Active) ActiveMods.Add(mod);
				else InactiveMods.Add(mod);
			}

			Debug.Log(
				$"ActiveMods: {ActiveMods.Count}\nInactiveMods: {InactiveMods.Count}\ndefaultContent: {defaultContent.Length}");
			//MessageHandler.ShowMessage("Hello World!");
		}
	}
}