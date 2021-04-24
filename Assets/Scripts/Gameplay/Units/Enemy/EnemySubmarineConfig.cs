using UnityEngine;

namespace Enemy {
	[CreateAssetMenu(fileName = nameof(EnemySubmarineConfig), menuName = nameof(EnemySubmarineConfig), order = 0)]
	public class EnemySubmarineConfig : ScriptableObject {
		public EnemySubmarineConfigModel Model;
	}
}