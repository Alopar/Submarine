using System;
using DG.Tweening;
using Enemy;
using UnityEngine;

namespace Gameplay {
	public class TorpedosController : MonoBehaviour {
		[SerializeField] private Torpedo torpedoPrefab;
		[SerializeField] private Transform torpedosRoot;
		[SerializeField] private Transform toPlayerStartPoint;
		[SerializeField] private Transform toEnemyStartPoint;
		public PlayerSubmarine PlayerSubmarine;
		public EnemySubmarine EnemySubmarine;


		public void LaunchPlayerTorpedo(Torpedo torpedo){ }

		public void LaunchEnemyTorpedo(Torpedo torpedo){ }

		public void PlayerShot(Vector3 targetPoint, bool isHit){
			Vector3 outFirePoint = PlayerSubmarine.GetFirePoint();
			float distance = Vector3.Distance(outFirePoint, targetPoint);

			var newTorpedo1 = Instantiate(torpedoPrefab,
										  outFirePoint,
										  Quaternion.identity,
										  torpedosRoot);
//			newTorpedo1.SetLayer(TorpedoLayer.PlayerOut);
			newTorpedo1.RunTo(PlayerSubmarine.transform.position + PlayerSubmarine.transform.forward * distance, isHit);


			var newTorpedo2 = Instantiate(torpedoPrefab,
										  toEnemyStartPoint.position,
										  Quaternion.identity,
										  torpedosRoot);
//			newTorpedo2.SetLayer(TorpedoLayer.EnemyIn);
			newTorpedo2.RunTo(targetPoint, isHit);
		}

		public void EnemyShot(Vector3 targetPoint, bool isHit){
			
			
			Vector3 outFirePoint = EnemySubmarine.GetFirePoint();
			float distance = Vector3.Distance(outFirePoint, targetPoint);

			var newTorpedo1 = Instantiate(torpedoPrefab,
										  outFirePoint,
										  Quaternion.identity,
										  torpedosRoot);
//			newTorpedo1.SetLayer(TorpedoLayer.PlayerOut);
			newTorpedo1.RunTo(EnemySubmarine.transform.position + EnemySubmarine.transform.forward * distance, isHit);


			var newTorpedo2 = Instantiate(torpedoPrefab,
										  toPlayerStartPoint.position,
										  Quaternion.identity,
										  torpedosRoot);
//			newTorpedo2.SetLayer(TorpedoLayer.EnemyIn);
			newTorpedo2.RunTo(targetPoint, isHit);
		}
	}
}