using System;
using UnityEngine;

namespace Enemy {
	[Serializable]
	public class EnemySubmarineModel {
		public float Health;
		
		public float WeaponsReloadProgress;
		
		public bool IsVisible;
		public float visibilityValue;
	}
}