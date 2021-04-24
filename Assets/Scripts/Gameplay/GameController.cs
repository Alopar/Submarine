using System;
using System.Collections;
using DG.Tweening;
using Enemy;
using Gameplay;
using Gameplay.Units;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour {
	public GameplaySettings gameplaySettings;
	private PlayerSubmarine playerSubmarine;
	private EnemySubmarine enemySubmarine;

	public Transform PlayerPoint;
	public Transform EnemyMoveStartPoint;
	public Transform EnemyMoveEndPoint;

	private Tween enemyMovingTween;

	public void Awake(){
//		playerSubmarine.
	}

	private void Start(){
		playerSubmarine = SpawnSubmarine(
				gameplaySettings.PlayerSubmarine.gameObject,
				PlayerPoint)
		   .GetComponent<PlayerSubmarine>();
//		playerSubmarine.SetSubmarine(newSubmarine);

		IEnumerator TestSpawnEnemies(){
			for (int i = 0; i < 10; i++){
				if (enemySubmarine != null){
					enemySubmarine.Kill();
				}

				CreateNewEnemy();

				enemySubmarine.Hide(true);
				enemySubmarine.Show();
				yield return new WaitForSeconds(0.5f);
				enemyMovingTween = enemySubmarine.transform.DOMove(EnemyMoveEndPoint.position, 1.0f);
				yield return new WaitForSeconds(1.0f);
				enemySubmarine.Hide();
				yield return new WaitForSeconds(0.5f);
			}
		}

		StartCoroutine(TestSpawnEnemies());
	}

	[ContextMenu(nameof(CreateNewEnemy))]
	public void CreateNewEnemy(){
		enemyMovingTween?.Kill();

		enemySubmarine = SpawnSubmarine(
				gameplaySettings.Enemies[Random.Range(0, gameplaySettings.Enemies.Count)].gameObject,
				EnemyMoveStartPoint)
		   .GetComponent<EnemySubmarine>();

//		enemyController.SetSubmarine(newEnemy);
	}

	private GameObject SpawnSubmarine(GameObject prefab, Transform point){
		GameObject newSubmarine = Instantiate(prefab,
											  point.position,
											  point.rotation,
											  point.parent);
		return newSubmarine;
	}
}