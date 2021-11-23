using System;

namespace Learning_cards.Scripts.Mods.Mod
{
	[Flags]
	public enum ModContent
	{
		Cards        = 1 << 0,
		CardPacks    = 1 << 1,
		Characters   = 1 << 2,
		Functions    = 1 << 3,
		Translations = 1 << 4,
		Script       = 1 << 5,
		KeyEvents   = 1 << 6,
	}
}