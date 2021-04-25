using System;
using DG.Tweening;
using Gameplay.Units;
using UnityEngine;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;

namespace Enemy {
	public class EnemySubmarine : MonoBehaviour {
		public event Action OnDestroy;
		public event Action<float> OnHealthUpdate;
		public event Action<bool> OnVisibilityChanged;

		[SerializeField] private Submarine Submarine;
		[SerializeField] private Hider Hider;

		[SerializeField] private EnemySubmarineConfig Config;
		[SerializeField] private EnemySubmarineModel model;

		private void Awake(){
			model=new EnemySubmarineModel(){
				Health = Config.Model.MaxHealth,
				visibilityValue = 1.0f,
				IsVisible = true,
			};
		}
		
		public float GetDamageValue() => Config.Model.Damage;

//		private float GetAccuracy() => Config.Model.RoomsConfig.GetSonarRoomValue(
//			roomsConfigsDictionary[RoomType.Sonar].CrewModels.Count);
//
//		private float GetMobility() => Config.Model.RoomsConfig.GetEngineRoomValue(
//			roomsConfigsDictionary[RoomType.Engine].CrewModels.Count);
		
		public void TakeHullDamage(float damage){
			model.Health = model.Health = Mathf.Max(model.Health - damage, 0.0f);
			OnHealthUpdate?.Invoke(model.Health);
			if (model.Health <= 0.0f){
				OnDestroy?.Invoke();
			}
		}
		
		public void Show(bool isHard = false){
			if (isHard){
				Hider.SetVisibilityValue(1.0f);
			} else{
				DOTween.To(() => model.visibilityValue, value => {
					model.visibilityValue = value;
					Hider.SetVisibilityValue(value);

					if (!model.IsVisible && value >= 0.5f){
						model.IsVisible = true;
						OnVisibilityChanged?.Invoke(true);
					}
				}, 1.0f, 0.5f);
			}
		}

		public void Hide(bool isHard = false){
			if (isHard){
				Hider.SetVisibilityValue(0.0f);
			} else{
				DOTween.To(() => model.visibilityValue, value => {
					model.visibilityValue = value;
					Hider.SetVisibilityValue(value);

					if (model.IsVisible && value <= 0.5f){
						model.IsVisible = false;
						OnVisibilityChanged?.Invoke(false);
					}
				}, 0.0f, 0.5f);
			}
		}

		public void Kill(){
			Object.Destroy(Submarine.gameObject);
		}
	}
}