using System;
using UnityEngine;

namespace Learning_cards.Scripts.Data
{
	public class LoadModsIntoMemory : MonoBehaviour
	{
		private void Start() => Dictionaries.Load();
	}
}