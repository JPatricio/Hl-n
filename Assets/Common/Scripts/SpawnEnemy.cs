using UnityEngine;
using System.Collections;

[System.Serializable]
public class Wave {
	public GameObject enemyPrefab;
	public float spawnInterval = 2;
	public int maxEnemies = 20;
}

public class SpawnEnemy : MonoBehaviour {

	public GameObject[] waypoints;
	public GameObject testEnemyPrefab;
	public GameObject callNextWave;

	public Wave[] waves;
	public int timeBetweenWaves = 5;

	private GameManagerBehavior gameManager;

	private float lastSpawnTime;
	private int enemiesSpawned = 0;

	// Use this for initialization
	void Start () {
		lastSpawnTime = Time.time;
		gameManager =
			GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
		// Instantiate the call next wave button
		GameObject firstWaypoint = GameObject.Find("Waypoint0");
		Vector3 callNextWavePos = callNextWave.transform.position;
		callNextWavePos.y = firstWaypoint.transform.position.y;
		callNextWave.transform.position = callNextWavePos;
		Instantiate(callNextWave);
	}

	// TODO: Fix this later..
	private bool once = false;

	// Update is called once per frame
	void Update () {
		// Waiting for next wave call?
		if (!gameManager.holdWave){
			int currentWave = gameManager.Wave;
			if (currentWave < waves.Length) {
				// Spawn enemies
				float timeInterval = Time.time - lastSpawnTime;
				float spawnInterval = waves[currentWave].spawnInterval;
				if (((enemiesSpawned == 0 && timeInterval > timeBetweenWaves) ||
					timeInterval > spawnInterval) && 
					enemiesSpawned < waves[currentWave].maxEnemies) {
					// 
					lastSpawnTime = Time.time;
					GameObject newEnemy = (GameObject)
						Instantiate(waves[currentWave].enemyPrefab);
					newEnemy.GetComponent<MoveEnemy>().waypoints = waypoints;
					enemiesSpawned++;
				}
				// 4 
				if (enemiesSpawned == waves[currentWave].maxEnemies && GameObject.FindGameObjectWithTag("Enemy") == null) {
					gameManager.Gold = Mathf.RoundToInt(gameManager.Gold * 1.1f);
					enemiesSpawned = 0;
					lastSpawnTime = Time.time;
					if (currentWave < waves.Length - 1){
						gameManager.holdWave = true;
						// Instantiate call next wave button
						Instantiate(callNextWave);
					} else {
						gameManager.gameOver = true;
						gameManager.Wave++;
					}
				}
				// 5 
			} else if (!once){
				// TODO: Fix this later..
				once = true;
				// Congratulations. You won!
				gameManager.gameOver = true;
				GameObject gameOverText = GameObject.FindGameObjectWithTag ("GameWon");
				gameOverText.GetComponent<Animator>().SetBool("gameOver", true);
				// Unlock next level
				GameManager globalSettings = GameObject.Find("globalSettings").GetComponent<GameManager>();
				int nextLevel = globalSettings.gameInstance.playing + 1;
				Debug.Log(nextLevel);
				globalSettings.gameInstance.unlockedLevels[nextLevel] = 1;
			}
		}
	}
}
