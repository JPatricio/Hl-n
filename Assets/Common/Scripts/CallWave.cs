using UnityEngine;
using System.Collections;

public class CallWave : MonoBehaviour {

	private GameManagerBehavior gameManager;

	void OnMouseUp () {
		gameManager.holdWave = false;
		gameManager.Wave++;
		Destroy(gameObject);
	}

	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
