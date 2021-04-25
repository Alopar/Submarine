using System;
using Enemy;
using UnityEngine;

namespace Gameplay {
	public class TorpedosController : MonoBehaviour {
		[SerializeField] private Torpedo torpedoPrefab;
		[SerializeField] private Transform torpedosRoot;
		public PlayerSubmarine PlayerSubmarine;
		public EnemySubmarine EnemySubmarine;

		public void LaunchPlayerTorpedo(Torpedo torpedo){ }

		public void LaunchEnemyTorpedo(Torpedo torpedo){ }

		public void PlayerShot(){
			var newTorpedo = Instantiate(torpedoPrefab,
										 PlayerSubmarine.transform.position,
										 PlayerSubmarine.transform.rotation,
										 torpedosRoot);
		}

		public void EnemyShot(){
			var newTorpedo = Instantiate(torpedoPrefab,
										 EnemySubmarine.transform.position,
										 EnemySubmarine.transform.rotation,
										 torpedosRoot);
		}
	}
}