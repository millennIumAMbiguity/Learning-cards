using Learning_cards.Scripts.Data.Classes;
using Learning_cards.Scripts.UI.Messages;

namespace Learning_cards.Scripts.Data.InternalCode.Comparators
{
	public class ComparatorComponent : ICode
	{
		public    string Execute(string input) => SExecute(input);
		protected bool   StringComparator = false;

		private string SExecute(string input)
		{
			string[] arguments = input.Split(',');
			float[]  floatArgs = new float[arguments.Length];

			if (arguments.Length != 2) {
				MessageHandler.ShowError($"Unexpected amount of arguments. expected 2, received {arguments.Length}.");
				return "NaN";
			}
			
			//convert to numbers
			for (int i = 0; i < 2; i++) {
				if (float.TryParse(arguments[i], out float result)) floatArgs[i] = result;
				//if any numbers are not a number, use the string lenght instead.
				else {
					if (StringComparator) return Comparer(arguments[0].Trim(), arguments[1].Trim()).ToString();
					for (int k = 0; k < 2; k++)
						floatArgs[k] = arguments[k].Trim().Length;
					break;
				}
			}
			
			return Comparer(floatArgs[0], floatArgs[1]).ToString();
		}

		protected virtual bool Comparer(float f1, float f2) => System.Math.Abs(f1 - f2) < 0.001f;
		
		protected virtual bool Comparer(string f1, string f2) => f1 == f2;
        
	}
}