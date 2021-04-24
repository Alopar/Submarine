using System;
using DG.Tweening;
using Gameplay.Units;
using UnityEngine;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;

namespace Enemy {
	public class EnemySubmarine : MonoBehaviour {
		public Submarine Submarine;
		public Hider Hider;

		public bool IsVisible;
		private float visibilityValue;

//		public void SetSubmarine(Submarine submarine){
////			if (submarine != null){
////				KillCurrent();
////			}
//
//			this.Submarine = submarine;
//		}

//		public void KillCurrent(){
//			if (Submarine != null){
//				Object.Destroy(Submarine.gameObject);
//			}
//		}

		public void Show(bool isHard = false){
			if (isHard){
				Hider.SetVisibilityValue(1.0f);
			} else{
				DOTween.To(() => visibilityValue, value => {
					visibilityValue = value;
					Hider.SetVisibilityValue(value);
				}, 1.0f, 0.5f);
			}
		}

		public void Hide(bool isHard = false){
			if (isHard){
				Hider.SetVisibilityValue(0.0f);
			} else{
				DOTween.To(() => visibilityValue, value => {
					visibilityValue = value;
					Hider.SetVisibilityValue(value);
				}, 0.0f, 0.5f);
			}
		}

		public void Kill(){
			Object.Destroy(Submarine.gameObject);
		}
	}
}