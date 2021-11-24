using System;
using TMPro;
using UnityEngine;

namespace Learning_cards.Scripts.UI.Messages
{
	public class MessageHandler : MonoBehaviour
	{
		private static           MessageHandler _messageHandler;
		public static            int            ActiveMessageWindows;
		[SerializeField] private GameObject     messageBoxPrefab;
		[SerializeField] private Terminal       log;
		[SerializeField] private int            messageOffset          = 15;
		[SerializeField] private int            messageContainmentSize = 25;

		private static int _messageCount = 0;
		
		private void Awake()
		{
			if (PlayerPrefs.GetInt("MessageHandler", 1) == 0) return;
			if (_messageHandler is { })
				Debug.LogWarning("Multiple MessageHandler detected!");
			_messageHandler = this;
		}

		private void FixedUpdate() => _messageCount = 0;

		private void OnDestroy() => _messageHandler = null;

		public static void ShowWarning(string text) =>
			ShowMessage("<size=+6><b><color=yellow>WARNING:</color></b></size>\n" + text);

		public static void ShowError(string text) =>
			ShowMessage("<size=+6><b><color=red>ERROR:</color></b></size>\n" + text);

		public static void ShowMessage(string text)
		{
			if (_messageCount++ > 16 || text == "" || text == "NaN") return;
			
			//When in the editor, use Debug.Log instead of MessageHandler
			#if UNITY_EDITOR
			if (!Application.isPlaying || _messageHandler.log) {
				bool     isError   = false;
				bool     isWarning = false;
				string   newText   = "";
				string[] s         = text.Split('<', '>');
				//remove formatting
				for (int i = 0; i < s.Length; i++)
					if ((i & 1) == 0) {
						if (s[i] == "") continue;
						if (!isError) {
							if (s[i].Length < 6) {
								newText += s[i];
								continue;
							}
							if (s[i].Substring(0, 6).ToUpper() == "ERROR:") isError = true;
							if (s[i].Length < 8) {
								newText += s[i];
								continue;
							}
							if (!isWarning && s[i].Substring(0, 8).ToUpper() == "WARNING:") isWarning = true;
						}

						newText += s[i];
					} else if (s[i] != "" && s[i].Split('=')[0] != "size") newText += '<' + s[i] + '>';

				if (_messageHandler.log)
					_messageHandler.log.Log(newText);

				if (isError) Debug.LogError(newText);
				else if (isWarning) Debug.LogWarning(newText);
				else Debug.Log(newText);
				if (!Application.isPlaying) return;
			}
			#else
			if (_messageHandler.log) {
				bool     isError = false;
				bool     isWarning = false;
				string   newText = "";
				string[] s = text.Split('<', '>');
				//remove formatting
				for (int i = 0; i < s.Length; i++)
					if ((i & 1) == 0) {
						if (s[i] == "") continue;
						if (!isError) {
							if (s[i].Length < 6) {
								newText += s[i];
								continue;
							}
							if (s[i].Substring(0, 6).ToUpper() == "ERROR:") isError = true;
							if (s[i].Length < 8) {
								newText += s[i];
								continue;
							}
							if (!isWarning && s[i].Substring(0, 8).ToUpper() == "WARNING:") isWarning = true;
						}

						newText += s[i];
					} else if (s[i] != "" && s[i].Split('=')[0] != "size") newText += '<' + s[i] + '>';

				_messageHandler.log.Log(newText);
			}
			#endif

			// ReSharper disable once Unity.NoNullPropagation
			_messageHandler?.Message(text);
		}

		private void Message(string text)
		{
			GameObject obj = Instantiate(messageBoxPrefab, transform);
			obj.GetComponentInChildren<TMP_Text>().text = text;

			int offset = messageContainmentSize / 2; //start in the middle of the screen
			for (int i = 0; i < ActiveMessageWindows - 1; i++)
				offset++;
			int baseOffset = offset % messageContainmentSize - messageContainmentSize / 2;
			obj.transform.position += new Vector3(
				(baseOffset + offset / messageContainmentSize) * messageOffset,
				-baseOffset * messageOffset);
		}
	}
}