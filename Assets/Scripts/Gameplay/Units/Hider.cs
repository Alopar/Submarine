using UnityEngine;

namespace Gameplay.Units {
	public class Hider : MonoBehaviour {
		public Renderer Renderer;
		public CanvasGroup CanvasGroup;

		public void SetVisibilityValue(float value){
			Color newColor = Renderer.material.color;
			newColor.a = value;
			Renderer.material.color = newColor;

			CanvasGroup.alpha = value;
		}
	}
}