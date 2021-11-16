
using System.Collections.Generic;
using Learning_cards.Scripts.Data;
using Learning_cards.Scripts.UI.Messages;

namespace Learning_cards.Scripts.Parse
{
	public static partial class Parse
	{
		public const string InvalidSetTypeErrorMsg   = "\"{0}\" was not recognized as a valid set action.";
		public const string InvalidSetTypeValidSets1 = "\nValid set actions are: \"=\", \"+=\", and \"-=\".";
		public const string InvalidSetTypeValidSets2 = "\nValid set actions are: \"=\" and \"+=\".";
		public const string InvalidSetTypeValidSets3 = "\nValid set actions are: \"=\".";
		
		private const string InvalidAttributeErrorMsg = "\"{0}\" was not recognized as a valid attribute.";
		
		private const string TargetNotFoundErrorMsg = "\"{0}\" was not found in dictionaries.";
        
	}
}