using System.Collections.Generic;
using UnityEngine;

namespace Learning_cards.Scripts.Data.Classes
{
	public class Card : ITitleId
	{
		public Code                     Code;
		public Sprite                     Image;
		public string                     Title;
		public Dictionary<string, string> Variables = new Dictionary<string, string>();
		public int                        Id { get; set; }
		
		public Card() {}

		public Card(Card card)
		{
			if (card == null) {
				Code  = new Code();
				Image = null;
				Title = "NaN";
				Id    = 0;
				return;
			}
			Code  = card.Code;
            Image = card.Image;
            Title = card.Title;
			Id    = card.Id;
		}
	}
}