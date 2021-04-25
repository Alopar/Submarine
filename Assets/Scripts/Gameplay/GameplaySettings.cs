using System.Collections.Generic;
using Enemy;
using Gameplay.Units;
using UnityEngine;

namespace Gameplay {
	[CreateAssetMenu(fileName = nameof(GameplaySettings), menuName = nameof(GameplaySettings))]
	public class GameplaySettings : ScriptableObject {
		public PlayerSubmarine PlayerSubmarine;
		public List<EnemySubmarine> Enemies;
		public List<BattleWave> EnemiesWaves;
		
		
	}

	public class BattleWave {
		public float PreparingTime;
		public EnemySubmarine EnemyPrefab;
	}
}