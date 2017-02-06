using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class MonsterLevel {
	public int cost;
	public GameObject visualization;
	public GameObject bullet;
	public float fireRate;
}

public class MonsterData : MonoBehaviour {

	public List<MonsterLevel> levels;
	private MonsterLevel currentLevel;

	public string towerName;

	//1
	public MonsterLevel CurrentLevel {
		//2
		get {
			return currentLevel;
		}
		//3
		set {
			currentLevel = value;
			int currentLevelIndex = levels.IndexOf(currentLevel);

			GameObject levelVisualization = levels[currentLevelIndex].visualization;
			for (int i = 0; i < levels.Count; i++) {
				if (levelVisualization != null) {
					if (i == currentLevelIndex) {
						levels[i].visualization.SetActive(true);
					} else {
						levels[i].visualization.SetActive(false);
					}
				}
			}
		}
	}

	public MonsterLevel getNextLevel() {
		int currentLevelIndex = levels.IndexOf (currentLevel);
		int maxLevelIndex = levels.Count - 1;
		if (currentLevelIndex < maxLevelIndex) {
			return levels[currentLevelIndex+1];
		} else {
			return null;
		}
	}

	public void increaseLevel() {
		int currentLevelIndex = levels.IndexOf(currentLevel);
		if (currentLevelIndex < levels.Count - 1) {
			CurrentLevel = levels[currentLevelIndex + 1];
		}
		// Display appropriate tower info
		string[] lol = new string[4];
		lol[0] = "towerInfo";
		lol[1] = towerName;
		lol[2] = "" + CurrentLevel.bullet.GetComponent<BulletBehavior>().damage;
		lol[3] = "" + gameObject.GetComponent<CircleCollider2D>().radius;
		// Call info prefab
		jumpIn gameInfoPanel = GameObject.Find("gameInfoPanel").GetComponent<jumpIn>();
		gameInfoPanel.displayObjectInfo(lol, CurrentLevel.visualization.GetComponent<SpriteRenderer>().sprite);
	}

	void OnEnable() {
		CurrentLevel = levels[0];
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
