using System.Collections.Generic;

namespace Learning_cards.Scripts.Data.Classes
{
	public class Player : ITitleId
	{
		public Dictionary<string, Card>   Cards;
		public Character                  Character;
		public int                        HitPointsDelta;
		public string                     Title;
		public Dictionary<string, string> Variables;
		public int                        Id { get; set; }
	}
}