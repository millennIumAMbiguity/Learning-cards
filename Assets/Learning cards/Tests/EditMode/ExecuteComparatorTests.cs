using System;
using Learning_cards.Scripts.Data;
using Learning_cards.Scripts.Data.Classes;
using Learning_cards.Scripts.Data.InternalCode.Comparators;
using NUnit.Framework;

namespace Learning_cards.Tests.EditMode
{
	[TestFixture(8, 7)]
	[TestFixture(5, 5)]
	[TestFixture(5, 6)]
	public class ExecuteComparatorNumberTests
	{
		private readonly float a;
		private readonly float b;

		public ExecuteComparatorNumberTests(float f1, float f2)
		{
			a = f1;
			b = f2;
		}

		[Test]
		public void Equals() =>
			Assert.AreEqual(Math.Abs(a - b) < 0.01f, bool.Parse(new Equals().Execute(a + ", " + b)));

		[Test]
		public void NotEquals() => Assert.AreEqual(
			Math.Abs(a - b) > 0.01f, bool.Parse(new NotEquals().Execute(a + ", " + b)));

		[Test] public void Greater() => Assert.AreEqual(a > b, bool.Parse(new Greater().Execute(a + ", " + b)));

		[Test]
		public void GreaterOrEqual() => Assert.AreEqual(a >= b, bool.Parse(new GreaterOrEqual().Execute(a + ", " + b)));

		[Test] public void Less() => Assert.AreEqual(a < b, bool.Parse(new Less().Execute(a + ", " + b)));

		[Test]
		public void LessOrEqual() => Assert.AreEqual(a <= b, bool.Parse(new LessOrEqual().Execute(a + ", " + b)));
	}

	[TestFixture("ab", "a")]
	[TestFixture("ab", "ab")]
	[TestFixture("ab", "abc")]
	public class ExecuteComparatorStringTests
	{
		private readonly string a;
		private readonly string b;

		public ExecuteComparatorStringTests(string f1, string f2)
		{
			a = f1;
			b = f2;
		}

		[Test]
		public void Equals() => Assert.AreEqual(
			a.Trim().Length == b.Trim().Length, bool.Parse(new Equals().Execute(a + ", " + b)));

		[Test]
		public void NotEquals() => Assert.AreEqual(
			a.Trim().Length != b.Trim().Length, bool.Parse(new NotEquals().Execute(a + ", " + b)));

		[Test]
		public void GreaterString() => Assert.AreEqual(
			a.Trim().Length > b.Trim().Length, bool.Parse(new Greater().Execute(a + ", " + b)));

		[Test]
		public void GreaterOrEqualString() => Assert.AreEqual(
			a.Trim().Length >= b.Trim().Length, bool.Parse(new GreaterOrEqual().Execute(a + ", " + b)));

		[Test]
		public void Less() => Assert.AreEqual(
			a.Trim().Length < b.Trim().Length, bool.Parse(new Less().Execute(a + ", " + b)));

		[Test]
		public void LessOrEqual() => Assert.AreEqual(
			a.Trim().Length <= b.Trim().Length, bool.Parse(new LessOrEqual().Execute(a + ", " + b)));
	}

	public class ExecuteComparatorTests
	{
		[Test]
		public void CallGreaterFromCode()
		{
			Dictionaries.Load();
			Assert.AreEqual(true, bool.Parse(new Code("return Greater(8, 7);").Execute()));
		}
	}
}