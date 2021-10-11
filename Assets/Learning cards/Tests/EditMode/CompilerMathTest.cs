using Learning_cards.Scripts.Data.Classes;
using NUnit.Framework;

namespace Learning_cards.Tests.EditMode
{
	public class CompilerMathTest
	{
		[Test]
		public void CompilePlusPlus() => Assert.AreEqual(
			"players[0].hp += 1",
			new Code("player.hp++;").CompiledCode);

		[Test]
		public void CompileMinusMinus() => Assert.AreEqual(
			"players[0].hp += -1",
			new Code("player.hp--;").CompiledCode);


		[Test]
		public void CompileSubtract() => Assert.AreEqual(
			"players[0].hp += Subtract(1, 1)",
			new Code("player.hp += 1 - 1;").CompiledCode);

		[Test]
		public void CompileAdd() => Assert.AreEqual(
			"players[0].hp += Add(1, 1)",
			new Code("player.hp += 1 + 1;").CompiledCode);

		[Test]
		public void CompileMultiply() => Assert.AreEqual(
			"players[0].hp += Multiply(1, 1)",
			new Code("player.hp += 1 * 1;").CompiledCode);

		[Test]
		public void CompileDivide() => Assert.AreEqual(
			"players[0].hp += Divide(1, 1)",
			new Code("player.hp += 1 / 1;").CompiledCode);


		[Test]
		public void CompileAddInFunc() => Assert.AreEqual(
			"players[0].hp += Add(1, Add(1, 1))",
			new Code("player.hp += Add(1, 1 + 1);").CompiledCode);
	}
}