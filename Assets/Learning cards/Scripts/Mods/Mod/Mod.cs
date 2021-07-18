using System.Collections.Generic;
using System.IO;
using System.Linq;
using Learning_cards.Scripts.Data.Classes;
using UnityEngine;

namespace Learning_cards.Scripts.Mods.Mod
{
	public class Mod : IMod
	{
		public Mod(string path)
		{
			var json = JsonUtility.FromJson<JsonModData>(File.ReadAllText($"{path}\\version.json"));
			Path    = path;
			Title   = json.title;
			Version = json.version;

			Content = 0;
			if (Directory.Exists(path + "\\Cards"))
				Content |= ModContent.Cards;
			if (Directory.Exists(path + "\\Characters"))
				Content |= ModContent.Characters;
			if (Directory.Exists(path + "\\Functions"))
				Content |= ModContent.Functions;
			if (Directory.Exists(path + "\\Translations"))
				Content |= ModContent.Translations;
			if (Directory.Exists(path + "\\CardPacks"))
				Content |= ModContent.CardPacks;
		}

		public bool Active {
			get => PlayerPrefs.GetInt(Title, 0) == 1;
			set => PlayerPrefs.SetInt(Title, value ? 1 : 0);
		}

		public ModContent Content { get; set; }
		public string     Path    { get; set; }
		public string     Title   { get; set; }
		public string     Version { get; set; }

		public void GetFunctions(ref Dictionary<string, Function> dir, ref List<ICode> list)
		{
			if (!Content.HasFlag(ModContent.Functions) || PlayerPrefs.GetInt(Title + ".Functions", 1) == 0) return;
			string[] cards = Directory.GetFiles(Path + "\\Functions");

			foreach (string card in cards) {
				string title = card.Split('\\').Last().Split('.')[0];
				title = title.First().ToString().ToUpper() + title.Substring(1);
				var f = new Function {Code = new Code(File.ReadAllText(card)), Id = list.Count};
				list.Add(f.Code);
				dir.Add(title, f);
			}
		}
	}
}