using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Gameplay {
	public class Torpedo : MonoBehaviour {
		public event Action OnHit;

		[SerializeField] private TorpedoLayers TorpedoLayers;
		[SerializeField] private List<GameObject> AllObjects;
		[SerializeField] private float flyTimeToOpponent = 1.0f;
		[SerializeField] private GameObject explodeEffect;


//		public Vector3 FromPosition;
//		public Vector3 ToPosition;

		private Vector3 start;
		private Vector3 target;
		private float runProgress = 0.0f;
		private bool needExplode;
		private bool isRun = false;


		private void Update(){
			if (isRun){
				runProgress += Time.deltaTime / flyTimeToOpponent;
				transform.position = Vector3.LerpUnclamped(start, target, runProgress);
				transform.Translate(Vector3.forward * 10.0f * Time.deltaTime, Space.Self);

				if (needExplode && runProgress > 1.0f){
					OnHit?.Invoke();
					Kill();
				}

				if (runProgress > 3.0f){
					Kill();
				}
			}
		}

		public void SetLayer(TorpedoLayer layer){
			int layerId = LayerMask.NameToLayer(TorpedoLayers.Layers.Find(lay => lay.Layer == layer).Name);
			foreach (GameObject obj in AllObjects){
				obj.layer = layerId;
			}
		}

		public void RunTo(Vector3 targetPos, bool isHit){
			isRun = true;

			start = transform.position;
			target = targetPos;
			needExplode = isHit;
			
			transform.LookAt(targetPos);
		}

//		public void RunToMiss(Vector3 targetPos){
//			isRun = true;
//
//			start = transform.position;
//			target = targetPos;
//			needExplode = false;
//			
//			transform.LookAt(targetPos);
//		}

		private void Kill(){
			Explode();
			Object.Destroy(gameObject);
		}

		private void Explode(){
			if (explodeEffect != null){
				Instantiate(explodeEffect, transform.position, Quaternion.identity);
			}
		}
	}

	public enum TorpedoLayer {
		PlayerOut,
		PlayerIn,
		EnemyOut,
		EnemyIn,
	}
}