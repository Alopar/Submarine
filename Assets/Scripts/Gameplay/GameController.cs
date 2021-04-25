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

		model = new BattleModel(){ };
	}

	private void Start(){
		playerSubmarine = SpawnSubmarine(
				gameplaySettings.PlayerSubmarine.gameObject,
				PlayerPoint)
		   .GetComponent<PlayerSubmarine>();

		torpedosController.PlayerSubmarine = playerSubmarine;
		playerSubmarine.OnShot += PlayerShot;

		PrepareToWave(0);

//		StartCoroutine(PrepareToWave(0));


//		playerSubmarine.SetSubmarine(newSubmarine);

//		IEnumerator TestSpawnEnemies(){
//			for (int i = 0; i < 10; i++){
//				if (enemySubmarine != null){
//					enemySubmarine.Kill();
//				}
//
//				CreateNewEnemy();
//
//				enemySubmarine.Hide(true);
//				enemySubmarine.Show();
//				yield return new WaitForSeconds(0.5f);
//				enemyMovingTween = enemySubmarine.transform.DOMove(EnemyMoveEndPoint.position, 1.0f);
//				yield return new WaitForSeconds(1.0f);
//				enemySubmarine.Hide();
//				yield return new WaitForSeconds(0.5f);
//			}
//		}
//
//		StartCoroutine(TestSpawnEnemies());
	}

//	private IEnumerator BattleCoroutine(){
//		OnBattleTimerEnabled?.Invoke();
//	}

	private void PrepareToWave(int waveId){
		model.CurrentWaveId = waveId;
		model.PreparingTimer = gameplaySettings.EnemiesWaves[waveId].PreparingTime;
		model.BattleState = BattleState.Preparing;
	}

	private IEnumerator LaunchNewEnemy(int waveId){
		model.CurrentWaveId = waveId;

//		yield return new WaitForSeconds(gameplaySettings.EnemiesWaves[waveId].PreparingTime);
		EnemySubmarine enemy = CreateNewEnemy(waveId);
		yield return MoveEnemy(enemy);

		EnemyReady(enemy);
	}

	private void EnemyReady(EnemySubmarine enemy){
		enemySubmarine = enemy;
		enemySubmarine.OnDestroy += EnemyDestroyHandler;
		enemySubmarine.OnShot += EnemyShot;

		torpedosController.EnemySubmarine = enemySubmarine;

		playerSubmarine.SetFireEnabled(true);

		model.BattleState = BattleState.Battle;
	}

	private void EnemyDestroyHandler(){
		enemySubmarine.OnDestroy -= EnemyDestroyHandler;
		enemySubmarine.OnShot -= EnemyShot;

		playerSubmarine.SetFireEnabled(false);

		if (model.CurrentWaveId < gameplaySettings.EnemiesWaves.Count - 1){
			PrepareToWave(model.CurrentWaveId + 1);
		} else{
			OnWin?.Invoke();
		}
	}

	public void PlayerShot(){
		bool isHit = Random.value <= playerSubmarine.GetAccuracy();
		Debug.Log(isHit);
		Vector3 targetPoint = isHit ? enemySubmarine.GetHitPoint() : enemySubmarine.GetMissPoint();
		torpedosController.PlayerShot(targetPoint,isHit);
	}

	public void EnemyShot(){
		bool isHit = Random.value <= playerSubmarine.GetMobility();
		Vector3 targetPoint = isHit ? playerSubmarine.GetMissPoint() : playerSubmarine.GetHitPoint();
		torpedosController.EnemyShot(targetPoint,isHit);
	}

	private void Update(){
		switch (model.BattleState){
//			case BattleState.BeginNextWave:
//				break;
			case BattleState.Preparing:
				if (model.PreparingTimer > 0.0f){
					model.PreparingTimer = Mathf.Max(model.PreparingTimer - Time.deltaTime, 0.0f);
					OnBattleTimerUpdated?.Invoke(model.PreparingTimer);
					if (model.PreparingTimer == 0.0f){
						OnBattleTimerDisabled?.Invoke();
						StartCoroutine(LaunchNewEnemy(model.CurrentWaveId));
					}
				}

				break;
			case BattleState.Battle:
				break;
			case BattleState.Win:
				Debug.Log("Win");
				Time.timeScale = 0.0f;
				break;
			case BattleState.Fail:
				Debug.Log("Fail");
				Time.timeScale = 0.0f;
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}
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