using System.Collections.Generic;
using Learning_cards.Scripts.Data;
using Learning_cards.Scripts.Data.Classes;
using NUnit.Framework;

namespace Learning_cards.Tests.EditMode
{
	public class ExecuteTests
	{
		[Test]
		public void ChildFunction()
		{
			Dictionaries.Load();
			Assert.AreEqual(3, float.Parse(new Code("return Add(1, Add(1, 1));").Execute()));
		}

		[Test]
		public void ChildFunctionOfChild()
		{
			Dictionaries.Load();
			Assert.AreEqual(4, float.Parse(new Code("return Add(1, Add(1, Add(1, 1)));").Execute()));
		}

		[Test]
		public void ChangeGlobalVariable()
		{
			Dictionaries.Load();
			new Code(
				"my_var = 5;" +
				"my_var += 5;").Execute();
			Assert.AreEqual(10, float.Parse(Dictionaries.GlobalVariables("my_var")));
		}

		[Test]
		public void ReturnBreak()
		{
			Dictionaries.Load();
			new Code(
				"my_var = 5;" +
				"return;" +
				"my_var += 5;").Execute();
			Assert.AreEqual(5, float.Parse(Dictionaries.GlobalVariables("my_var")));
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
			Assert.AreEqual(15, float.Parse(Dictionaries.Players[0].Variables["hp"]));
		}

		[Test]
		public void SetPlayerHpFromGlobalVar()
		{
			Dictionaries.Load();
			Dictionaries.Players = new List<Player>(new[] {new Player()});
			new Code(
				"player.hp = 20;" +
				"my_var = 5;" +
				"player.hp -= $my_var;").Execute();
			Assert.AreEqual(15, float.Parse(Dictionaries.Players[0].Variables["hp"]));
		}

		[Test]
		public void SetGlobalVarFromPlayerHp()
		{
			Dictionaries.Load();
			Dictionaries.Players = new List<Player>(new[] {new Player()});
			new Code(
				"player.hp = 20;" +
				"my_var = $player.hp").Execute();
			Assert.AreEqual(20, float.Parse(Dictionaries.DGlobalVariables["my_var"]));
		}

		[Test]
		[MaxTime(100)]
		public void GotoTest()
		{
			Dictionaries.Load();
			new Code(
				"my_var = 5;" +
				"goto 4;" +
				"my_var = 10;" +
				"return;").Execute();
			Assert.AreEqual(5, float.Parse(Dictionaries.DGlobalVariables["my_var"]));
		}

		[Test]
		[MaxTime(100)]
		public void GotoRelativePositiveTest()
		{
			Dictionaries.Load();
			new Code(
				"my_var = 5;" +
				"goto +2;" +
				"my_var = 10;" +
				"return;").Execute();
			Assert.AreEqual(5, float.Parse(Dictionaries.DGlobalVariables["my_var"]));
		}

		[Test]
		[MaxTime(100)]
		public void GotoRelativeNegativeTest()
		{
			Dictionaries.Load();
			new Code(
				"goto 3;" +
				"return;" +
				"my_var = 5;" +
				"goto -2;" +
				"my_var = 10;").Execute();
			Assert.AreEqual(5, float.Parse(Dictionaries.DGlobalVariables["my_var"]));
		}

		[Test]
		public void IfStatementTest(
			[Values("false", "true")] string input1,
			[Values("a", "a, b")]     string input2)
		{
			Dictionaries.Load();
			new Code(
				$"my_var = If({input1}, {input2})").Execute();
			if (bool.Parse(input1))
				Assert.AreEqual(input2.Split(',')[0].Trim(), Dictionaries.DGlobalVariables["my_var"]);
			else {
				string[] s = input2.Split(',');
				Assert.AreEqual(s.Length < 2 ? "NaN" : s[1].Trim(), Dictionaries.DGlobalVariables["my_var"]);
			}
		}

		[Test]
		[MaxTime(100)]
		public void IfStatementGotoTest([Values("false", "true")] string input1)
		{
			Dictionaries.Load();
			Assert.AreEqual(bool.Parse(input1) ? "5" : "10", new Code(
				"my_var = 5;" +
				$"If({input1}, goto 4);" +
				"my_var = 10;" +
				"return $my_var;").Execute());
		}

		[Test]
		public void GotoJumpPointTest()
		{
			Dictionaries.Load();
			new Code(
				"myVar = 10;" +
				"goto myJumpPoint;" +
				"myVar = 0;" +
				"myJumpPoint:").Execute();
			Assert.AreEqual(10, float.Parse(Dictionaries.DGlobalVariables["myVar"]));
		}
	}
}