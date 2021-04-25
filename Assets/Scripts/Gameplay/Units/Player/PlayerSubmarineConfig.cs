using UnityEngine;

namespace Gameplay.Units.Player {
	[CreateAssetMenu(fileName = nameof(PlayerSubmarineConfig), menuName = nameof(PlayerSubmarineConfig), order = 0)]
	public class PlayerSubmarineConfig : ScriptableObject {
		public PlayerSubmarineConfigModel Model;
	}
}