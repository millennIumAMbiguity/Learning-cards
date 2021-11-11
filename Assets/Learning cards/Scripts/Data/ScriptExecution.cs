using System;
using System.Linq;
using Learning_cards.Scripts.Mods;
using Learning_cards.Scripts.Mods.Mod;
using UnityEngine;

namespace Learning_cards.Scripts.Data
{
	public class ScriptExecution : MonoBehaviour
	{
		private void Start()
		{
			foreach (Mod mod in LoadMods.ActiveMods.Where(mod => mod.HaveScript)) mod.Script.Execute("start");
		}
	}
}