namespace Learning_cards.Scripts.Data.InternalCode.Comparators
{
	public class Less : ComparatorComponent
	{
		protected override bool Comparer(float f1, float f2) => f1 < f2;
	}
}