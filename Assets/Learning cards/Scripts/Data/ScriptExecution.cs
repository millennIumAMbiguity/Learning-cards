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
		
		void OnGUI()
		{
			if (Event.current.isKey && Event.current.type == EventType.KeyDown && Event.current.keyCode != KeyCode.None)
			{
				foreach (Mod mod in LoadMods.ActiveMods.Where(mod => mod.HaveKeyEvents)) mod.KeyEventScript.Execute(Event.current.keyCode.ToString());
			}
		}
	}
}