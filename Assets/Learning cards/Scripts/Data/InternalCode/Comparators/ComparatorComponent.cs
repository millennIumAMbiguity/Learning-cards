using Learning_cards.Scripts.Data.Classes;

namespace Learning_cards.Scripts.Data.InternalCode.Comparators
{
	public class ComparatorComponent : ICode
	{
		public string Execute(string input = null) => SExecute(input);

		private string SExecute(string input)
		{
			string[] arguments = input.Split(',');
			float[]  floatArgs = new float[arguments.Length];

			//convert to numbers
			for (int i = 0; i < arguments.Length; i++)
				if (float.TryParse(arguments[i], out float result)) floatArgs[i] = result;
				//if any numbers are not a number, use the string lenght instead.
				else {
					for (i = 0; i < arguments.Length; i++)
						floatArgs[i] = arguments[i].Trim().Length;
					break;
				}

			for (int i = 1; i < arguments.Length; i++)
				if (Comparer(floatArgs[i - 1], floatArgs[i]))
					return "true";

			return "false";
		}

		protected virtual bool Comparer(float f1, float f2) => f1 == f2;
	}
}