using Learning_cards.Scripts.Data;
using Learning_cards.Scripts.Data.Classes;
using Learning_cards.Scripts.Data.InternalCode.Math;
using NUnit.Framework;

namespace Learning_cards.Tests.EditMode
{
	public class ExecuteMathTests
	{
		[Test] public void Add() => Assert.AreEqual(2, float.Parse(new Add().Execute("1, 1")));

		[Test] public void AddString() => Assert.AreEqual("11a", new Add().Execute("1, 1a"));

		[Test]
		public void CallAddFromCode()
		{
			Dictionaries.Load();
			Assert.AreEqual(2, float.Parse(new Code("return Add(1, 1);").Execute()));
		}

		[Test]
		public void Return1P1()
		{
			Dictionaries.Load();
			Assert.AreEqual(2, float.Parse(new Code("return 1 + 1;").Execute()));
		}
	}
}