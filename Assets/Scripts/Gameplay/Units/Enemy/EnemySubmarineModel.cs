using System;
using UnityEngine;

namespace Enemy {
	[Serializable]
	public class EnemySubmarineModel {
		public float Health{ get; set; }
		
		public bool IsVisible;
		public float visibilityValue;
	}
}