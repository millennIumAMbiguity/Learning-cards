using System.Collections.Generic;
using Learning_cards.Scripts.Data.Classes;

namespace Learning_cards.Scripts.Mods.Mod
{
	public interface IMod
	{
		bool       Active  { get; set; }
		ModContent Content { get; set; }
		string     Path    { get; set; }
		string     Title   { get; set; }
		string     Version { get; set; }

		void GetFunctions(ref Dictionary<string, Function> dir, ref List<ICode> list);
	}
}