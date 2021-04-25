using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Units {
	public class Submarine : MonoBehaviour {
		[SerializeField] private Transform Body;
		[SerializeField] private List<Transform> HitPoints = new List<Transform>();
		[SerializeField] private List<Transform> MissPoints = new List<Transform>();
		[SerializeField] private Transform firePoint;

		public Vector3 GetFirePoint() => firePoint.position;

		public Vector3 GetHitPoint() => HitPoints[Random.Range(0, HitPoints.Count)].position;

		public Vector3 GetMissPoint() => MissPoints[Random.Range(0, MissPoints.Count)].position;
	}
}