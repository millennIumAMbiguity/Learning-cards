using System.Collections.Generic;
using UnityEngine;

namespace Learning_cards.Scripts.Data.Classes
{
	public class Card : ITitleId
	{
		public string                     Code;
		public Sprite                     Image;
		public string                     Title;
		public Dictionary<string, string> Variables;
		public int                        Id { get; set; }
	}
}