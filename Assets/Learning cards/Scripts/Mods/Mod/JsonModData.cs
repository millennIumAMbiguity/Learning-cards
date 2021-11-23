using System;

namespace Learning_cards.Scripts.Mods.Mod
{
	[Serializable]
	public class JsonModData
	{
		public string title;
		public string version;
		public string xml    = "Layouts.xml";
		public string script = "script.txt";
		public string key = "key event.txt";
	}
}