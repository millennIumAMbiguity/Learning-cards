using System.IO;
using UnityEngine;

namespace Learning_cards.Scripts.Mods.Mod
{
	public class Mod : IMod
	{
		public bool Active {
			get => PlayerPrefs.GetInt(Title, 0) == 1;
			set => PlayerPrefs.SetInt(Title, value ? 1 : 0);
		}

		public ModContent Content { get; set; }
		public string     Path    { get; set; }
		public string     Title   { get; set; }
		public string     Version { get; set; }


		public Mod(string path)
		{
			var json = JsonUtility.FromJson<JsonModData>(File.ReadAllText($"{path}\\version.json"));
			Path    = path;
			Title   = json.title;
			Version = json.version;
		}
	}
}