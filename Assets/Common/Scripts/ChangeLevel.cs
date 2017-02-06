using UnityEngine;
using UnityEngine.SceneManagement; 
using System.Collections;

public class ChangeLevel : MonoBehaviour {

	public void loadLevel (string scene) {
		SceneManager.LoadScene(scene);
	}
		
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
