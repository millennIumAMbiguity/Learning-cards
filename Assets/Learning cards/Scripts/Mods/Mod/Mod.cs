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
		internal readonly bool IsBuiltIn;
		internal          Code Script;

		private readonly ModContent _content;
		private readonly string     _path;

		public readonly string Title;
		public readonly string Version;
		public readonly string Xml;

		internal bool HaveScript => (_content & ModContent.Script) != 0;

		public Mod(string path, bool builtIn = false)
		{
			if (path is null) return;
			IsBuiltIn = builtIn;
			var json = JsonUtility.FromJson<JsonModData>(File.ReadAllText($"{path}\\version.json"));
			_path    = path;
			Xml     = path+'\\'+json.xml;
			Title   = json.title;
			Version = json.version;

			_content = 0;
			if (Directory.Exists(path + "\\Cards"))
				_content |= ModContent.Cards;
			if (Directory.Exists(path + "\\Characters"))
				_content |= ModContent.Characters;
			if (Directory.Exists(path + "\\Functions"))
				_content |= ModContent.Functions;
			if (Directory.Exists(path + "\\Translations"))
				_content |= ModContent.Translations;
			if (Directory.Exists(path + "\\CardPacks"))
				_content |= ModContent.CardPacks;
			if (File.Exists(json.script = path + '\\' + json.script)) {
				_content |= ModContent.Script;
				Script   =  new Code(File.ReadAllText(json.script));
			}
		}

		public bool Active {
			get => IsBuiltIn || PlayerPrefs.GetInt(Title, 0) == 1;
			set {
				if (!IsBuiltIn) PlayerPrefs.SetInt(Title, value ? 1 : 0);
			}
		}

		public void GetFunctions(ref Dictionary<string, Function> dir, ref List<ICode> list)
		{
			if (!_content.HasFlag(ModContent.Functions) || PlayerPrefs.GetInt(Title + ".Functions", 1) == 0) return;
			string[] functions = Directory.GetFiles(_path + "\\Functions");

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
			if (!_content.HasFlag(ModContent.Characters) || PlayerPrefs.GetInt(Title + ".Characters", 1) == 0) return;
			string[] characters = Directory.GetFiles(_path + "\\Characters");

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