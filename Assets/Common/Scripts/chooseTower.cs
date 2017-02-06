using UnityEngine;
using System.Collections;

public class chooseTower : MonoBehaviour {

	private bool displayingTowerStats;
	private GameObject grandPa;

	void OnMouseUp () {
		grandPa.GetComponent<PlaceMonster>().hideTowerStats();
		Debug.Log(displayingTowerStats);
		if (displayingTowerStats){
			// Call build tower on grandpa.
			grandPa.GetComponent<PlaceMonster>().buildTower(gameObject.name);
		}else{
			// Display Tower range and cost
			GameObject[] miniTowers = GameObject.FindGameObjectsWithTag("MiniTowerPicker");
			foreach (GameObject miniTower in miniTowers ){
				if (miniTower != gameObject){
					miniTower.GetComponent<chooseTower>().closeDisplayingTowerStats();
				}
			}
			grandPa.GetComponent<PlaceMonster>().displayTowerStats(gameObject.name);
			displayingTowerStats = true;
		}
	}

	public void closeDisplayingTowerStats (){
		displayingTowerStats = false;
	}

	// Use this for initialization
	void Start () {
		displayingTowerStats = false;
		grandPa = transform.parent.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
