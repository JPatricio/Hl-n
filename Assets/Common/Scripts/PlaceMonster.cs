using UnityEngine;
using System.Collections;

public class PlaceMonster : MonoBehaviour {

	// The Tower this sport is using
	private GameObject monsterPrefab;

	private GameObject monster;
	private GameManagerBehavior gameManager;
	private bool displayingTowerWidget;
	private GameObject towerWidget;
	public GameObject[] towerPrefabs;

	private bool canAffordTower() {
		int cost = monsterPrefab.GetComponent<MonsterData> ().levels[0].cost;
		return gameManager.Gold >= cost;
	}

	private bool canPlaceTower() {
		return monster == null;
	}

	private bool canUpgradeMonster() {
		if (monster != null && !gameManager.inTutorial) {
			MonsterData monsterData = monster.GetComponent<MonsterData> ();
			MonsterLevel nextLevel = monsterData.getNextLevel();
			return nextLevel != null && gameManager.Gold >= nextLevel.cost;
		}
		return false;
	}

	public void buildTower(string towerName){
		foreach (GameObject towerPrefab in towerPrefabs){
			if (towerPrefab.name == towerName){
				monsterPrefab = towerPrefab;
				break;
			}
		}
		if (canAffordTower()){
			GetComponent<SpriteRenderer>().enabled = false;
			monster = (GameObject) 
				Instantiate(monsterPrefab, transform.position, Quaternion.identity);
			AudioSource audioSource = gameObject.GetComponent<AudioSource>();
			audioSource.PlayOneShot(audioSource.clip);
			
			//TODO: Remove this ugly biatch
			if (gameManager.inTutorial){
				GameObject dialogPanel = GameObject.Find("DialogPanel");
				if (dialogPanel != null){
					ScrollDialog scrollDialogPanel = dialogPanel.GetComponent<ScrollDialog>();
					scrollDialogPanel.objective = false;
					scrollDialogPanel.OnMouseUp();
				}
			}
			// Deduct gold
			gameManager.Gold -= monster.GetComponent<MonsterData>().CurrentLevel.cost;
			
			// Remove the building Widget
			towerWidget.SetActive(false);
			displayingTowerWidget = false;
		}else{
			// Show gold lack animation
			FlashingText goldLabel = GameObject.Find("GoldLabel").GetComponent<FlashingText>();
			goldLabel.activateBlinking();
		}
	}

	public void displayTowerStats(string towerName){
		GameObject desiredTowerPrefab = null;
		foreach (GameObject towerPrefab in towerPrefabs){
			if (towerPrefab.name == towerName){
				desiredTowerPrefab = towerPrefab;
				break;
			}
		}
		// Show tower range
		var childComponents = GetComponentsInChildren<Renderer>(true);
		foreach(var r in childComponents) {
			if (r.name == "towerRadius") {
				r.gameObject.transform.localScale = new Vector3(1,1,1);
				r.gameObject.transform.localScale = r.gameObject.transform.localScale * desiredTowerPrefab.GetComponent<CircleCollider2D>().radius;
				r.gameObject.SetActive(true);
			}
		}
		// Show tower Cost
		var textChildComponents = GetComponentsInChildren<TextMesh>(true);
		foreach(var r in textChildComponents) {
			if (r.name == "textCost") {
				r.gameObject.SetActive(true);
				int cost = desiredTowerPrefab.GetComponent<MonsterData>().levels[0].cost;
				if (gameManager.Gold < cost){
					r.color = Color.red;
				}else {
					r.color = Color.green;
				}
				r.text = "Cost: " + desiredTowerPrefab.GetComponent<MonsterData>().levels[0].cost;
			}
		}
	}

	public void hideTowerStats(){
		// Hide tower range
		var childComponents = GetComponentsInChildren<Renderer>(true);
		foreach(var r in childComponents) {
			if (r.name == "towerRadius") {
				r.gameObject.SetActive(false);
			}
		}
		// Hide tower Cost
		var textChildComponents = GetComponentsInChildren<TextMesh>(true);
		foreach(var r in textChildComponents) {
			if (r.name == "textCost") {
				r.gameObject.SetActive(false);
			}
		}
	}

	void OnMouseUp () {
		if (canPlaceTower ()) {
			// Tower doesn't exist, can be placed
			// Display tower choices
			if (!displayingTowerWidget){
				// Hide all other tower's TowerWidgets
				GameObject[] openSpots = GameObject.FindGameObjectsWithTag("OpenSpot");
				foreach (GameObject openSpot in openSpots ){
					if (openSpot != gameObject) {
						PlaceMonster otherPlaceMonster = openSpot.GetComponent<PlaceMonster>();
						otherPlaceMonster.hideTowerWidget();
					}
				}
				// This widget is just being opened, so no stats should be on display.
				foreach (Transform child in transform) {
					if (child.name == "upgradeWidget"){
						foreach (Transform grandChild in child.transform) {
							if (grandChild.gameObject.CompareTag("MiniTowerPicker")) {
								grandChild.GetComponent<chooseTower>().closeDisplayingTowerStats();
							}
						}
					}
				}
				towerWidget.SetActive(true);
				displayingTowerWidget = true;
			}else{
				hideTowerWidget();
			}
		} else if (canUpgradeMonster()) {
			// Monster exists but can be upgraded
			monster.GetComponent<MonsterData>().increaseLevel();
			AudioSource audioSource = gameObject.GetComponent<AudioSource>();
			audioSource.PlayOneShot(audioSource.clip);
			// TODO: Deduct gold
			gameManager.Gold -= monster.GetComponent<MonsterData>().CurrentLevel.cost;
		}
	}

	public void hideTowerWidget(){
		hideTowerStats();
		towerWidget.SetActive(false);
		displayingTowerWidget = false;
	}

	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
		var childComponents = GetComponentsInChildren<Renderer>(true);
		foreach(var r in childComponents) {
			if (r.name == "upgradeWidget") {
				towerWidget = r.gameObject;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
