using System.Collections.Generic;

namespace Learning_cards.Scripts.Data.Classes
{
	public class Player : ITitleId
	{
		public Dictionary<string, Card>   Cards = new Dictionary<string, Card>();
		public Character                  Character { get; set; }
		public string                     Title     { get; set; }
		public Dictionary<string, string> Variables = new Dictionary<string, string>();
		public int                        Id   { get; set; }
		public Code                       Code { get; set; }
		
		public Player()
        {}

		public Player(Character character)
		{
			Character = character;
			Title     = character.Title;
			Id        = character.Id;
			Code      = new Code(character.Code.SourceCode);
		}
        
	}
}