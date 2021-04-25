using System;
using System.Collections;
using DG.Tweening;
using Enemy;
using Gameplay;
using Gameplay.Units;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour {
	public event Action OnBattleTimerEnabled;
	public event Action<float> OnBattleTimerUpdated;
	public event Action OnBattleTimerDisabled;
	public event Action<PlayerSubmarine> OnPlayerSubmarineReady;
	public event Action<EnemySubmarine> OnEnemySubmarineReady;
	public event Action<EnemySubmarine> OnEnemySubmarineDie;
	public event Action OnWin;
	public event Action OnFail;

	[SerializeField] private TorpedosController torpedosController;
	[SerializeField] private GameplaySettings gameplaySettings;

	private PlayerSubmarine playerSubmarine;
	private EnemySubmarine enemySubmarine;

	public Transform PlayerPoint;
	public Transform EnemyMoveStartPoint;
	public Transform EnemyMoveEndPoint;

	private Tween enemyMovingTween;

	private BattleModel model;


	public void Awake(){
//		playerSubmarine.

		model = new BattleModel(){
//			BattleState = BattleState.Preparing,
//			CurrentWaveId = -1,
		};
	}

	private void Start(){
		playerSubmarine = SpawnSubmarine(
				gameplaySettings.PlayerSubmarine.gameObject,
				PlayerPoint)
		   .GetComponent<PlayerSubmarine>();


		PrepareToWave(0);


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

//	private IEnumerator BattleCoroutine(){
//		OnBattleTimerEnabled?.Invoke();
//	}

	private IEnumerator PrepareToWave(int waveId){
		EnemySubmarine enemy = CreateNewEnemy(waveId);
		yield return MoveEnemy(enemy);

		EnemyReady(enemy);
	}

	private void EnemyReady(EnemySubmarine enemy){
		enemySubmarine = enemy;
		enemySubmarine.OnDestroy += EnemyDestroyHandler;
		
		
	}

	private void EnemyDestroyHandler(){
		enemySubmarine.OnDestroy -= EnemyDestroyHandler;

		if (model.CurrentWaveId < gameplaySettings.EnemiesWaves.Count - 1){
			StartCoroutine(PrepareToWave(model.CurrentWaveId + 1));
		} else{
			OnWin?.Invoke();
		}
	}

//	private void Update(){
//		switch (model.BattleState){
////			case BattleState.BeginWave:
////				break;
//			case BattleState.Preparing:
//				++model.CurrentWaveId;
//
//				var enemy = CreateNewEnemy(model.CurrentWaveId);
//				StartCoroutine(MoveEnemy(enemy));
//
//
//				model.PreparingTimer -= Time.deltaTime;
//				OnBattleTimerUpdated?.Invoke(model.PreparingTimer);
//				if (model.PreparingTimer <= 0){
////					model.BattleState = BattleState.Battle;
//
//
//					StartCoroutine(TestSpawnEnemies());
//				}
//
//				DOTween.To(() => model.PreparingTimer, value => { model.PreparingTimer = value; })
//
//				break;
//			case BattleState.Battle:
//				break;
//			case BattleState.Win:
//				break;
//			case BattleState.Fail:
//				break;
//			default:
//				throw new ArgumentOutOfRangeException();
//		}
//	}

	[ContextMenu(nameof(CreateNewEnemy))]
	public void CreateNewEnemy(){
		enemyMovingTween?.Kill();

		enemySubmarine = SpawnSubmarine(
				gameplaySettings.Enemies[Random.Range(0, gameplaySettings.Enemies.Count)].gameObject,
				EnemyMoveStartPoint)
		   .GetComponent<EnemySubmarine>();

//		enemyController.SetSubmarine(newEnemy);
	}


	public EnemySubmarine CreateNewEnemy(int waveId){
		return SpawnSubmarine(
				gameplaySettings.EnemiesWaves[waveId].EnemyPrefab.gameObject,
				EnemyMoveStartPoint)
		   .GetComponent<EnemySubmarine>();
	}

	private IEnumerator MoveEnemy(EnemySubmarine enemy){
//			enemy.Hide(true);
//			enemy.Show();
//			yield return new WaitForSeconds(0.5f);
		enemyMovingTween = enemy.transform.DOMove(EnemyMoveEndPoint.position, 1.0f);
		yield return new WaitForSeconds(1.0f);
//			enemy.Hide();
//			yield return new WaitForSeconds(0.5f);
	}

	private GameObject SpawnSubmarine(GameObject prefab, Transform point){
		GameObject newSubmarine = Instantiate(prefab,
											  point.position,
											  point.rotation,
											  point.parent);
		return newSubmarine;
	}
}

public class BattleModel {
	public BattleState BattleState;
	public int CurrentWaveId;
	public float PreparingTimer;
}


public enum BattleState {
//	BeginWave,
	Preparing,
	Battle,
	Win,
	Fail,
}