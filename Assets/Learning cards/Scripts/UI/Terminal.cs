using System;
using JetBrains.Annotations;
using Learning_cards.Scripts.Data;
using Learning_cards.Scripts.Data.Classes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Learning_cards.Scripts.UI
{
	public class Terminal : MonoBehaviour
	{
		[SerializeField] private TMP_InputField inputField;
		[SerializeField] private TMP_Text       text;
		[SerializeField] private RectTransform layoutGroup;

		private bool   _isExecuting;
		private string _newMessage = "";
		
		public void Log(string message)
        {
			if (_isExecuting)
				_newMessage += message + "\n";
			else
				text.text += message + "\n";
        }
		
		private void Update()
		{
			if (!Input.GetKeyDown(KeyCode.Return)) return;
			_isExecuting = true;
			Dictionaries.Load();
			_newMessage = $"<color=green>></color>{inputField.text}\n";
			string output = new Code(inputField.text).Execute("Terminal");
			if (output != "" && output != "NaN")
				_newMessage  += $"<color=green>Output: </color>{output}\n";
			text.text += _newMessage;
			inputField.text = "";
			_isExecuting    = false;
			LayoutRebuilder.ForceRebuildLayoutImmediate(layoutGroup);
			inputField.ActivateInputField();
		}

		public void ActivateInput() => inputField.ActivateInputField();
	}
}