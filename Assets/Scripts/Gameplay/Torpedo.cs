using System;
using UnityEngine;

namespace Gameplay {
	public class Torpedo : MonoBehaviour {
//		public Vector3 FromPosition;
//		public Vector3 ToPosition;

		private void Update(){
			transform.Translate(Vector3.forward * 10.0f * Time.deltaTime, Space.Self);
		}
	}
}