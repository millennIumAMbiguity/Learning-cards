using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

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