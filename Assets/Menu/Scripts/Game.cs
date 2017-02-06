using UnityEngine;
using System.Collections;

[System.Serializable]
public class Game {

	public int[] unlockedLevels;
	public bool soundOn;
	public bool doneTutorial;
	public int playing;

	public Game() {
		// Create defaults
		unlockedLevels = new int[10];
		unlockedLevels[0] = 1;
		soundOn = true;
		doneTutorial = false;
		playing = -1;
	}
}
