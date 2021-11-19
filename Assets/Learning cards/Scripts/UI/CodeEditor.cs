using Learning_cards.Scripts.Data;
using TMPro;
using UnityEngine;

namespace Learning_cards.Scripts.UI
{
	public class CodeEditor : MonoBehaviour
	{
		[SerializeField] private TMP_InputField inputField;

		[SerializeField] private Terminal terminal;

		public void ActivateInput() => inputField.ActivateInputField();

		public void Execute()
		{
			Dictionaries.IsLoaded = false;
			terminal.Execute(inputField.text);
		}
	}
}