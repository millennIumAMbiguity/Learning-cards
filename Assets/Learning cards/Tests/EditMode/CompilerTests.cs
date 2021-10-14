using Learning_cards.Scripts.Data.Classes;
using NUnit.Framework;

namespace Learning_cards.Tests.EditMode
{
	public class CompilerTests
	{
		[Test] public void CompileAddSpaceAfterColon() => Assert.AreEqual(", , , ", new Code(", ,,").CompiledCode);

		[Test]
		public void CompileReturnReformatting() => Assert.AreEqual("return NaN", new Code("return;").CompiledCode);

		[Test]
		public void CompileSpaceAfterFunction() => Assert.AreEqual("myFunc()", new Code("myFunc ();").CompiledCode);

		[Test] public void CompilerRenameIf() => Assert.AreEqual("If()", new Code("if();").CompiledCode);
	}

	public class CompilerNullTests
	{
		[Test] public void CompileNull()       => Assert.AreEqual("", new Code().CompiledCode);
		[Test] public void CompileEmpty()      => Assert.AreEqual("", new Code("").CompiledCode);
		[Test] public void CompileBackslashN() => Assert.AreEqual("", new Code("\n").CompiledCode);
		[Test] public void CompileSemicolon()  => Assert.AreEqual("", new Code(";").CompiledCode);
	}
}