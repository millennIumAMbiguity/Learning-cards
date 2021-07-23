using System.Collections.Generic;
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
		public void CallFunction()
		{
			Dictionaries.Load();
			Assert.AreEqual("2", new Code("return Add(1, 1);").Execute());
		}

		[Test]
		public void Return1P1()
		{
			Dictionaries.Load();
			Assert.AreEqual("2", new Code("return 1 + 1;").Execute());
		}

		[Test]
		public void ChildFunction()
		{
			Dictionaries.Load();
			Assert.AreEqual("3", new Code("return Add(1, Add(1, 1));").Execute());
		}

		[Test]
		public void ChildFunctionOfChild()
		{
			Dictionaries.Load();
			Assert.AreEqual("4", new Code("return Add(1, Add(1, Add(1, 1)));").Execute());
		}

		[Test]
		public void ChangeGlobalVariable()
		{
			Dictionaries.Load();
			new Code(
				"my_var = 5;" +
				"my_var += 5;").Execute();
			Assert.AreEqual("10", Dictionaries.GlobalVariables("my_var"));
		}

		[Test]
		public void ChangePlayerTitle()
		{
			Dictionaries.Players = new List<Player>(new[] {new Player()});
			new Code("player.title = new name;").Execute();
			Assert.AreEqual("new name", Dictionaries.Players[0].Title);
		}

		[Test]
		public void ChangePlayerHp()
		{
			Dictionaries.Players = new List<Player>(new[] {new Player()});
			new Code(
				"player.hp = 20;" +
				"player.hp -= 5;").Execute();
			Assert.AreEqual("15", Dictionaries.Players[0].Variables["hp"]);
		}
	}
}