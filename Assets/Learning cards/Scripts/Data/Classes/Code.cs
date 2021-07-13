namespace Learning_cards.Scripts.Data.Classes
{
	public class Code
	{
		private string _sourceCode;

		public string SourceCode {
			get => _sourceCode;
			set {
				_sourceCode = value;
				_isCompiled = false;
			}
		}

		private bool _isCompiled;

		public object Execute(ITitleId obj)
		{
			if (!_isCompiled) Compile();
			return null;
		}

		
		/*
		 * player.hp += 1;
		 * target.hp += 1;
		 * deck += this as card;
		 * deck += GetCard(myCard);
		 */
	
		private void Compile()
		{
			string[] functions = SourceCode.Split('{', '}');
			//if (_isCompiled) return;
		}
	}
}