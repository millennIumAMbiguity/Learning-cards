using TMPro;
using UnityEngine;

namespace Learning_cards.Scripts.UI.Messages
{
	public class MessageHandler : MonoBehaviour
	{
		private static           MessageHandler _messageHandler;
		public static            int            ActiveMessageWindows;
		[SerializeField] private GameObject     messageBoxPrefab;
		[SerializeField] private int            messageOffset          = 15;
		[SerializeField] private int            messageContainmentSize = 25;

		private void Awake() => _messageHandler = this;

		public static void ShowMessage(string text)
		{
			//When in the editor, use Debug.Log instead of MessageHandler
			#if UNITY_EDITOR
			if (!Application.isPlaying) {
				bool     isError = false;
				string   newText = "";
				string[] s       = text.Split('<', '>');
				//remove formatting
				for (int i = 0; i < s.Length; i++) {
					if ((i & 1) == 0) {
						if (s[i] == "") continue;
						if (!isError && s[i].Substring(0,6).ToUpper() == "ERROR:") isError = true;
						newText += s[i];
					} else if (s[i] != "" && s[i].Split('=')[0] != "size") {
						newText += '<'+s[i]+'>';
					}
				}
				if (isError) Debug.LogError(newText);
				else Debug.Log(newText);
				return;
			}
			#endif

			_messageHandler.Message(text);
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