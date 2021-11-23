namespace Learning_cards.Scripts.Data.InternalCode.Comparators
{
	public class NotEquals : ComparatorComponent
	{
		public NotEquals() => StringComparator = true;
		protected override bool Comparer(float  f1, float  f2) => System.Math.Abs(f1 - f2) > 0.001f;
		protected override bool Comparer(string f1, string f2) => f1 != f2;
	}
}