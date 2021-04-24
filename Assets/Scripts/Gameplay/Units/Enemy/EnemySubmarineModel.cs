using System;
using UnityEngine;

namespace Enemy {
	[Serializable]
	public class EnemySubmarineModel {
		public float Health{ get; set; }
		
		public bool IsVisible;
		public float visibilityValue;

//		public EnemySubmarineModel(float health){
//			Health = health;
//		}
	}
}

//namespace Enemy {
//	[Serializable]
//	public class EnemySubmarineModel {
//		public float Health{ get; set; }
//		[SerializeField] private float MaxHealth;
//		[SerializeField] private float Damage;
//
//		public EnemySubmarineModel Clone(){
//			return new EnemySubmarineModel(){
//				Health = MaxHealth,
//				MaxHealth = MaxHealth,
//				Damage = Damage,
//			};
//		}
//	}
//}