using System.Collections.Generic;
using System.IO;
using System.Linq;
using Learning_cards.Scripts.Data.Classes;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

namespace Learning_cards.Scripts.Mods.Mod
{
	public class Mod
	{
		public bool _isBultIn;
		
		public Mod(string path, bool builtIn = false)
		{
			if (path is null) return;
			_isBultIn = builtIn;
			var json = JsonUtility.FromJson<JsonModData>(File.ReadAllText($"{path}\\version.json"));
			Path    = path;
			Xml     = path+'\\'+json.xml;
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
			get => _isBultIn || PlayerPrefs.GetInt(Title, 0) == 1;
			set {
				if (!_isBultIn) PlayerPrefs.SetInt(Title, value ? 1 : 0);
			}
		}

		public readonly ModContent Content;
		public readonly string     Path;

		public string Title   { get; set; }
		public string Version { get; set; }
		public string Xml { get; set; }

		public void GetFunctions(ref Dictionary<string, Function> dir, ref List<ICode> list)
		{
			if (!Content.HasFlag(ModContent.Functions) || PlayerPrefs.GetInt(Title + ".Functions", 1) == 0) return;
			string[] functions = Directory.GetFiles(Path + "\\Functions");

			foreach (string function in functions) {
				string title = function.Split('\\').Last().Split('.')[0];
				title = title.First().ToString().ToUpper() + title.Substring(1);
				var f = new Function { Code = new Code(File.ReadAllText(function)), Id = list.Count };
				list.Add(f.Code);
				dir.Add(title, f);
			}
		}

		public void GetCharacters(ref Dictionary<string, Character> dir, ref List<Character> list)
		{
			if (!Content.HasFlag(ModContent.Characters) || PlayerPrefs.GetInt(Title + ".Characters", 1) == 0) return;
			string[] characters = Directory.GetFiles(Path + "\\Characters");

			foreach (string character in characters) {
				string[] split = character.Split('\\').Last().Split('.');
				if (split.Last() != "json") continue;
				var c = JsonConvert.DeserializeObject<Character>(File.ReadAllText(character));
				c.Title = split[0];
				dir.Add(c.Title, c);
			}
		}
	}
}