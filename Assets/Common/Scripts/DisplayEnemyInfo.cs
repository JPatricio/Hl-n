using UnityEngine;
using System.Collections;

public class DisplayEnemyInfo : MonoBehaviour {

	public string id;
	public Sprite enemySprite;

	void OnMouseUp(){
		//Get this object's info
		string[] lol = new string[4];
		lol[0] = "enemyInfo";
		lol[1] = id;
		lol[2] = "1";
		lol[3] = "" + gameObject.GetComponent<MoveEnemy>().speed;
		// Call info prefab
		jumpIn gameInfoPanel = GameObject.Find("gameInfoPanel").GetComponent<jumpIn>();
		gameInfoPanel.displayObjectInfo(lol, enemySprite);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
