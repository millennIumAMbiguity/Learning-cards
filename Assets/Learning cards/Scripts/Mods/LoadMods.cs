using System.Collections.Generic;
using System.IO;
using Learning_cards.Scripts.Mods.Mod;
using UnityEngine;

namespace Learning_cards.Scripts.Mods
{
	public class LoadMods : MonoBehaviour
	{
		private const string ModsPath = "mods";
		private const string FilesPath = "Files";

		public static List<Mod.Mod> ActiveMods   = new List<Mod.Mod>();
		public static List<Mod.Mod> InactiveMods = new List<Mod.Mod>();

		private void Awake()
		{
			ActiveMods.Clear();
			InactiveMods.Clear();
			
			//load built in mods (mods that are always enabled)
			Directory.CreateDirectory(FilesPath);
			foreach (string modPath in Directory.GetDirectories(FilesPath)) {
				var mod = new Mod.Mod(modPath, true);
				ActiveMods.Add(mod);
			}
			
			//load mods
			Directory.CreateDirectory(ModsPath);
			foreach (string modPath in Directory.GetDirectories(ModsPath)) {
				var mod = new Mod.Mod(modPath);
				if (mod.Active) ActiveMods.Add(mod);
				else InactiveMods.Add(mod);
			}

			Debug.Log(
				$"ActiveMods: {ActiveMods.Count}\nInactiveMods: {InactiveMods.Count}");
			//MessageHandler.ShowMessage("Hello World!");
		}
	}
}