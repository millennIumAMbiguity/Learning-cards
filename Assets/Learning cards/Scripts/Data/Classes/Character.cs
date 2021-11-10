namespace Learning_cards.Scripts.Data.Classes
{
	public class Character : ITitleId
	{
		public string Title;
		public Code   Code { get; set; }
		public int    Id   { get; set; }

		public bool IsPlayable { get; set; } = true;
		public bool IsAI       { get; set; }
        
	}
}