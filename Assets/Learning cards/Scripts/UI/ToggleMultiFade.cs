using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Learning_cards.Scripts.UI
{
	[AddComponentMenu("UI/Toggle MultiFade", 31)]
	public class ToggleMultiFade : Toggle
	{
		private Graphic[] targetGraphics = new Graphic[0];

		protected override void Awake()
		{
			targetGraphics = GetComponentsInChildren<Graphic>();

			for (int index = 0; index < targetGraphics.Length; index++) 
				if (!targetGraphics[index].CompareTag("Graphic")) targetGraphics[index] = null;
			targetGraphics = targetGraphics.Where(c => c != null).ToArray();

			base.Awake();
		}

		protected override void DoStateTransition(SelectionState state, bool instant)
		{
			base.DoStateTransition(state, instant);

			if (!gameObject.activeInHierarchy)
                return;

			UnityEngine.Color targetColor =
                state switch {
                    SelectionState.Disabled    => colors.disabledColor,
                    SelectionState.Highlighted => colors.highlightedColor,
                    SelectionState.Normal      => colors.normalColor,
                    SelectionState.Pressed     => colors.pressedColor,
                    SelectionState.Selected    => colors.selectedColor,
                    _                          => UnityEngine.Color.white
                };
			
            StartColorTween(targetColor * colors.colorMultiplier, instant);
		}
        void StartColorTween(UnityEngine.Color targetColor, bool instant)
        {
			if (instant)
				foreach (var graphic in targetGraphics) {
					graphic?.CrossFadeColor(targetColor, 0f, true, true);
				}
			else 
				foreach (var graphic in targetGraphics) 
					graphic?.CrossFadeColor(targetColor, colors.fadeDuration, true, true);
		}
	}
}