using UnityEngine;
using UnityEngine.EventSystems;

namespace Learning_cards.Scripts.UI
{
	[RequireComponent(typeof(RectTransform))]
	public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler
	{
		[SerializeField] private RectTransform window;

		private Vector3       _dragStartPos;
		private RectTransform _mDraggingPlane;
		private Vector3       _windowDragStartPos;
		private Vector3       _drag;

		public void OnBeginDrag(PointerEventData eventData)
		{
			_windowDragStartPos = window.localPosition;
			_mDraggingPlane     = transform as RectTransform;
			_drag               = Vector3.zero;
			_dragStartPos       = UpdateDraggedPosition(eventData);
		}

		public void OnDrag(PointerEventData data) =>
			window.localPosition = UpdateDraggedPosition(data) - _dragStartPos + _windowDragStartPos;

		private Vector3 UpdateDraggedPosition(PointerEventData data)
		{
			if (data.pointerEnter == null || data.pointerEnter.transform as RectTransform == null) return _drag;
			if (RectTransformUtility.ScreenPointToWorldPointInRectangle(_mDraggingPlane, data.position, data.pressEventCamera, out Vector3 globalMousePos)) 
				_drag = globalMousePos;
			return _drag;
		}
	}
}