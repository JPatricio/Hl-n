using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManagerBehavior : MonoBehaviour {

	public static int maxHealth = 10;
	public int startGold = 300;

	public bool gameOver = false;
	public bool holdWave = true;
	public bool inTutorial = true;

	public Text healthLabel;
	public GameObject castle;

	private GameManager globalSettingsManager;

	private int health;
	public int Health {
		get { return health; }
		set {
			// Do camera shake animation. Health lost!
			if (value < health) {
				Camera.main.GetComponent<CameraShake>().Shake();
			}
			// Change castle visualisation if needed
			float previousHealthPercentage = (float)health / (float)maxHealth;
			float newHealthPercentage = (float)value / (float)maxHealth;
			if (newHealthPercentage < 0.8 && previousHealthPercentage >= 0.8) {
				// Careful now..
				castle.GetComponent<CastleData>().increaseDamage();
			} else if (newHealthPercentage < 0.3 && previousHealthPercentage >= 0.3){
				// God help us..
				castle.GetComponent<CastleData>().increaseDamage();
			}
			// Update Health label
			health = value;
			healthLabel.text = health + "x";
			// Did player just lose?
			if (health <= 0 && !gameOver) {
				gameOver = true;
				// Don't allow user to go to next level because he lost
				Transform[] transforms = GameObject.Find("Canvas").GetComponentsInChildren<Transform>(true);
				foreach (Transform child in transforms){
					if (child.gameObject.name == "gameOverPanel") {
						Transform[] grandChilds = child.gameObject.GetComponentsInChildren<Transform>(true);
						foreach (Transform granChild in grandChilds){
							if (granChild.gameObject.name == "nextLevel") {
								granChild.gameObject.SetActive(false);
							}
						}
					}
				}
				GameObject gameOverText = GameObject.FindGameObjectWithTag ("GameOver");
				gameOverText.GetComponent<Animator>().SetBool("gameOver", true);
			}
		}
	}

	public Text goldLabel;

	private int gold;
	public int Gold {
		get { return gold; }
		set {
			gold = value;
			goldLabel.GetComponent<Text>().text = "" + gold;
		}
	}

	public Text manaLabel;

	public static int maxMana = 200;
	private int mana;
	public int Mana {
		get { return mana; }
		set {
			if (value > maxMana) return;
			mana = value;
			manaLabel.GetComponent<Text>().text = "" + mana;
		}
	}

	private void manaTick() {
		if (!holdWave){
			Mana += 1;
		}
	}

	public Text waveLabel;
	public GameObject[] nextWaveLabels;

	private int wave;
	public int Wave {
		get { return wave; }
		set {
			wave = value;
			if (value >= 0) {
				if (!gameOver) {
					for (int i = 0; i < nextWaveLabels.Length; i++) {
						nextWaveLabels[i].GetComponent<Animator>().SetTrigger("nextWave");
					}
				}
				waveLabel.text = "WAVE: " + (wave + 1) + "/" + GameObject.Find("Road").GetComponent<SpawnEnemy>().waves.Length;
			}
		}
	}

	public float globalSpeed;

	// Use this for initialization
	void Start () {
		globalSettingsManager = GameObject.Find("globalSettings").GetComponent<GameManager>();

		Gold = startGold;
		Wave = -1;
		Health = maxHealth;
		globalSpeed = 1;
		mana = 120;

		InvokeRepeating("manaTick", 1.0f, 1.0f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void loadLevel (string scene) {
		SceneManager.LoadScene(scene);
		if (scene != "Menu"){
			globalSettingsManager.gameInstance.playing = System.Convert.ToInt32(scene.Substring(scene.Length - 1));
		}else {
			globalSettingsManager.gameInstance.playing = -1;
		}
	}
}
