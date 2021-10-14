namespace Learning_cards.Scripts.Data.Classes
{
	public partial class Code : ICode
	{
		private string _compiledCode;

		private bool   _isCompiled;
		private string _sourceCode;

		public Code(string scoreCode = null) => _sourceCode = scoreCode;

		public string SourceCode {
			get => _sourceCode;
			set {
				_sourceCode = value;
				_isCompiled = false;
			}
		}

		public string CompiledCode {
			get {
				if (!_isCompiled) Compile();
				return _compiledCode;
			}
		}
	}
}