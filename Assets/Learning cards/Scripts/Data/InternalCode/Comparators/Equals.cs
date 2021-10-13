﻿namespace Learning_cards.Scripts.Data.InternalCode.Comparators
{
	public class Equals : ComparatorComponent
	{
		protected override bool Comparer(float f1, float f2) => System.Math.Abs(f1 - f2) < 0.01f;
	}
}