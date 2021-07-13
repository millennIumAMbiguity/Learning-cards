using System.Collections.Generic;

namespace Learning_cards.Scripts.Data.Classes
{
	public class Player : ITitleId
	{
		public string                   Title { get; set; }
		public int                      Id    { get; set; }
		public Character                Character;
		public int                      HitPointsDelta;
		public Dictionary<string, Card> Cards;
	}
}