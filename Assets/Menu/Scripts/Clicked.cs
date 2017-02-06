using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.UI;
using System.Collections;

public class Clicked : MonoBehaviour {

	GameObject levelSelector;
	GameManager gameManager;

	public void displayLevelEditor () {
		levelSelector.SetActive(true);
		int i = 0;
		foreach (Transform child in levelSelector.transform){
			if (gameManager.gameInstance.unlockedLevels[i] == 1){
				Button bt = child.gameObject.GetComponent<Button>();
				bt.interactable = true;
			}
			i++;
			// Debug.Log(child.gameObject.name);
		}
	}

	public void loadLevel (string scene) {
		SceneManager.LoadScene(scene);
		if (scene != "Menu"){
			gameManager.gameInstance.playing = System.Convert.ToInt32(scene.Substring(scene.Length - 1));
		}else {
			gameManager.gameInstance.playing = -1;
		}
	}

	void Start () {
		levelSelector = GameObject.Find("levelSelector");
		levelSelector.SetActive(false);

		gameManager = GameObject.Find("globalSettings").GetComponent<GameManager>();

	}
}
