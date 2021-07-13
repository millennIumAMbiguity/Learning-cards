using System;
using UnityEngine;

namespace Learning_cards.Scripts.UI.Messages
{
	public class Message : MonoBehaviour
	{
		private void Awake()
		{
			MessageHandler.ActiveMessageWindows++;
			transform.localScale = new Vector3(.1f, .1f, 1);
		}

		private void OnDestroy()      => MessageHandler.ActiveMessageWindows--;
		public  void DestroyMessage() { Destroy(gameObject); }

		private void FixedUpdate()
		{
			Transform transform1 = transform;
			Vector3   localScale = transform1.localScale;
			localScale            = new Vector3(localScale.x * 1.2f, localScale.y * 1.2f, 1);
			if (localScale.x >= 1f) {
				localScale = new Vector3(1, 1, 1);
				enabled    = false;
			}
			transform1.localScale = localScale;
		}
	}
}