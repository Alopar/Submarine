using System;
using DG.Tweening;
using Gameplay.Units;
using UnityEngine;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Enemy {
	public class EnemySubmarine : MonoBehaviour {
		public event Action OnDestroy;
		public event Action OnShot;
		public event Action<float> OnHealthUpdate;
		public event Action<bool> OnVisibilityChanged;

		[SerializeField] private Submarine Submarine;
		[SerializeField] private Hider Hider;

		[SerializeField] private EnemySubmarineConfig Config;
		[SerializeField] private EnemySubmarineModel model;

		private void Awake(){
			model = new EnemySubmarineModel(){
				Health = Config.Model.MaxHealth,
				WeaponsReloadProgress = 1.0f,
				visibilityValue = 1.0f,
				IsVisible = true,
			};
		}

		public float GetDamageValue() => Config.Model.Damage;

		private void Update(){
			WeaponsTick(Time.deltaTime);
		}


		public void WeaponsTick(float deltaTime){
			if (model.WeaponsReloadProgress < 1.0f){
				float appendReload = Config.Model.FireFate * deltaTime;
				model.WeaponsReloadProgress = Mathf.Min(model.WeaponsReloadProgress + appendReload, 1.0f);
			}

			if (model.WeaponsReloadProgress == 1.0f){
				model.WeaponsReloadProgress = 0.0f;
				OnShot?.Invoke();
			}
		}


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
				Kill();
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

		#region Tests

		[ContextMenu(nameof(Test_GetHullDamage))]
		public void Test_GetHullDamage() => TakeHullDamage(50);

		#endregion

		
		public Vector3 GetFirePoint()=>Submarine.GetFirePoint();
		public Vector3 GetHitPoint()=>Submarine.GetHitPoint();
		public Vector3 GetMissPoint()=>Submarine.GetMissPoint();
	}
}