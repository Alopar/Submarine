using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Units.Player {
//	[Serializable]
//	public class PlayerSubmarineController {
//		public event Action OnDestroy;
//		public event Action<RoomType> OnRoomRepaired;
//		public event Action<int> OnCrewDeath;
//
//		public PlayerSubmarineModel model;
//
//		public void Init(PlayerSubmarineModel model){
//			this.model = model;
//		}
//
//		public void Tick(){
//			HealTick();
//			RepairTick();
//		}
//
//		public void TakeDamage(float damage){
//			model.Health = model.Health = Mathf.Max(model.Health - damage, 0.0f);
//			if (model.Health <= 0.0f){
//				OnDestroy?.Invoke();
//			}
//		}
//
//		private void HealTick(){
//			//todo
//		}
//
//		private void RepairTick(){
//			//todo
////			model.Health = Mathf.Min(model.Health+x,model.MaxHealth);
//		}
//	}
}