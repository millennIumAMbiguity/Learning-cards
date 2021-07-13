using UnityEngine;

namespace Learning_cards.Scripts.Data.Classes
{
	public class Card : ITitleId
	{
		public string Title { get; set; }
		public int    Id    { get; set; }
		public Sprite Image;
		public string Code;
	}
}