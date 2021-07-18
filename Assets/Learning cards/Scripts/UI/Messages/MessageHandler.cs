﻿using TMPro;
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

		public static void ShowMessage(string text) => _messageHandler.Message(text);

		private void Message(string text)
		{
			#if UNITY_EDITOR
			if (!Application.isPlaying) return;
			#endif

			GameObject obj = Instantiate(messageBoxPrefab, transform);
			obj.GetComponentInChildren<TMP_Text>().text = text;

			int offset = messageContainmentSize / 2; //start in the middle of the screen
			for (int i = 0; i < ActiveMessageWindows - 1; i++)
				offset++;
			int baseOffset = offset % messageContainmentSize - messageContainmentSize / 2;
			obj.transform.position += new Vector3(
				(baseOffset + offset / messageContainmentSize) * messageOffset,
				-baseOffset                                    * messageOffset);
		}
	}
}