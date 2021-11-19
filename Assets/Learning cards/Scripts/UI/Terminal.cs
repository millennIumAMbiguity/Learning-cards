using System;
using System.IO;
using System.Linq;
using JetBrains.Annotations;
using Learning_cards.Scripts.Data;
using Learning_cards.Scripts.Data.Classes;
using Learning_cards.Scripts.UI.Messages;
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

		private void Start() => inputField.ActivateInputField();

		public void Execute(string code, bool logInput = false)
		{
			_isExecuting = true;
			Dictionaries.Load();
			_newMessage = logInput ? $"<color=green>></color>{code}\n" : "";

			string   argument    = "terminal";
			bool     OnlyCompile = false;
			string[] args        = code.Split('|');
			foreach (var arg in args) {
				string[] trimmed = arg.Trim().ToLower().Split(':');
				switch (trimmed[0].Trim()) {
					case "path":
					case "file": {
						if (!File.Exists(args[args.Length - 1])) {
							MessageHandler.ShowError("Target file not found.");
							goto End;
						}
						args[args.Length-1] = File.ReadAllText(args[args.Length-1]);
						break;
					}
					case "arg":
					case "args":
					case "argument":
					case "arguments": {
						if (args.Length < 2) {
							MessageHandler.ShowError("No sub-arguments specified.");
							goto End;
						}
						argument = trimmed[1].Trim();
						break;
					}
					case "show compile":
					case "showcompile":
					case "compile": {
						OnlyCompile = true;
						break;
					}
					
				}
			}

			if (OnlyCompile) {
				_newMessage += new Code(args.Last()).CompiledCode + "\n";
			} else {
				string output = new Code(args.Last()).Execute(argument);
				if (output != "" && output != "NaN")
					_newMessage += $"<color=green>Output: </color>{output}\n";
			}
			
			End:
			text.text    += _newMessage;
			_isExecuting =  false;
			LayoutRebuilder.ForceRebuildLayoutImmediate(layoutGroup);
		}
		private void Update()
		{
			if (!Input.GetKeyDown(KeyCode.Return)) return;
			Execute(inputField.text, true);
			inputField.text = "";
			inputField.ActivateInputField();
		}

		public void ActivateInput() => inputField.ActivateInputField();
	}
}