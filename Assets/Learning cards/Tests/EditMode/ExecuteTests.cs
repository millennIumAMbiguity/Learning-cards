using Learning_cards.Scripts.Data;
using Learning_cards.Scripts.Data.Classes;
using Learning_cards.Scripts.Data.InternalCode;
using NUnit.Framework;

namespace Learning_cards.Tests.EditMode
{
	public class ExecuteTests
	{
		[Test] public void RunAdd1P1() => Assert.AreEqual("2", new Add().Execute("1, 1"));

		[Test]
		public void Return1P1()
		{
			Dictionaries.Load();
			Assert.AreEqual("2", new Code("return 1 + 1;").Execute());
		}
	}
}