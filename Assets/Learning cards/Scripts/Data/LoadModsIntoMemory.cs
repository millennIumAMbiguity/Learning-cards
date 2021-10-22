using UnityEngine;

namespace Learning_cards.Scripts.Data
{
	public class LoadModsIntoMemory : MonoBehaviour
	{
		public void Start() => Dictionaries.Load();
	}
}