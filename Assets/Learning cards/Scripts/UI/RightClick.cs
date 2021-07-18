using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Learning_cards.Scripts.UI
{
	public class RightClick : MonoBehaviour, IPointerClickHandler
	{
		public UnityEvent onLeftClick;

		public void OnPointerClick(PointerEventData eventData)
		{
			if (eventData.button == PointerEventData.InputButton.Left) onLeftClick.Invoke();
		}
	}
}