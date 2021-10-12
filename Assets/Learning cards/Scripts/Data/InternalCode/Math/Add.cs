using System.Linq;
using Learning_cards.Scripts.Data.Classes;

namespace Learning_cards.Scripts.Data.InternalCode.Math
{
	public class Add : ICode
	{
		public string Execute(string input = null) => SExecute(input);

		public static string SExecute(string input = null)
		{
			if (input is null) return "0";
			string[] arguments   = input.Split(',');
			float    returnValue = 0;
			foreach (string argument in arguments)
				//add current argument to the final result
				if (float.TryParse(argument, out float result)) returnValue += result;
				//input contains string, return a string containing all strings.
				else return arguments.Aggregate("", (current, arg) => current + arg.Trim());
				

			return returnValue.ToString();
		}
	}
}