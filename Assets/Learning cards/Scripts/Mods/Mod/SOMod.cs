using System.Collections.Generic;
using Learning_cards.Scripts.Data.Classes;
using UnityEngine;

namespace Learning_cards.Scripts.Mods.Mod
{
	[CreateAssetMenu(fileName = "new mod", menuName = "Mod", order = 0)]
	public class SOMod : ScriptableObject, IMod
	{
		[SerializeField] private ModContent content;
		[SerializeField] private string     title;

		public bool Active {
			get => PlayerPrefs.GetInt(Title, 1) == 1;
			set => PlayerPrefs.SetInt(Title, value ? 1 : 0);
		}

		public ModContent Content {
			get => content;
			set => content = value;
		}

		public string Path { get; set; } = null;

		public string Title {
			get => title;
			set => title = value;
		}

		public string Version                                                                        { get; set; } = "";
		public void   GetFunctions(ref  Dictionary<string, Function>  dir, ref List<ICode>     list) { }
		public void   GetCharacters(ref Dictionary<string, Character> dir, ref List<Character> list) { }
	}
}