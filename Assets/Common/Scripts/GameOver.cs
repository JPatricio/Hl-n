using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {

	// Use this for initialization
	void RestartLevel () {
		
		Transform[] transforms = GameObject.Find("Canvas").GetComponentsInChildren<Transform>(true);
		foreach (Transform child in transforms){
			if (child.gameObject.name == "gameOverPanel") {
				child.gameObject.SetActive(true);
			}
		}

		// Add level after this one to unlocked levels 
	}

}
