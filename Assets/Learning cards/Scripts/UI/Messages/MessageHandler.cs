using TMPro;
using UnityEngine;

namespace Learning_cards.Scripts.UI.Messages
{
	public class MessageHandler : MonoBehaviour
	{
		[SerializeField] private GameObject messageBoxPrefab;
		[SerializeField] private int        messageOffset          = 15;
		[SerializeField] private int        messageContainmentSize = 25;

		private static MessageHandler _messageHandler;
		public static  int            ActiveMessageWindows;

		private void Awake() => _messageHandler = this;

		public static void ShowMessage(string text) => _messageHandler.Message(text);

		private void Message(string text)
		{
			GameObject obj = Instantiate(messageBoxPrefab, transform);
			obj.GetComponentInChildren<TMP_Text>().text = text;

			int offset = messageContainmentSize/2; //start in the middle of the screen
			for (int i = 0; i < ActiveMessageWindows - 1; i++)
				offset++;
			int baseOffset = (offset % messageContainmentSize) - messageContainmentSize / 2;
			obj.transform.position += new Vector3(
				(baseOffset + offset / messageContainmentSize) * messageOffset,
				-baseOffset * messageOffset);
		}
	}
}