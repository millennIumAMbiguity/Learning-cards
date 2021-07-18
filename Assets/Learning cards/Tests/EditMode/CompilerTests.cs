using Learning_cards.Scripts.Data.Classes;
using NUnit.Framework;

namespace Learning_cards.Tests.EditMode
{
	public class CompilerTests
	{
		[Test] public void CompileNull()       => Assert.AreEqual("", new Code().CompiledCode);
		[Test] public void CompileEmpty()      => Assert.AreEqual("", new Code("").CompiledCode);
		[Test] public void CompileBackslashN() => Assert.AreEqual("", new Code("\n").CompiledCode);
		[Test] public void CompileSemicolon()  => Assert.AreEqual("", new Code(";").CompiledCode);

		[Test]
		public void CompilePlusPlus() => Assert.AreEqual(
			"players[0].hp += 1;",
			new Code("player.hp++;").CompiledCode);

		[Test]
		public void CompileAdd() => Assert.AreEqual(
			"players[0].hp += Add(1, 1);",
			new Code("player.hp += 1 + 1;").CompiledCode);
	}
}