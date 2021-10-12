using System.Collections.Generic;
using Learning_cards.Scripts.Data;
using Learning_cards.Scripts.Data.Classes;
using Learning_cards.Scripts.Data.InternalCode;
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
	}
}