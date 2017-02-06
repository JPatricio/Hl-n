using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class CastleState {
	public GameObject visualization;
}

public class CastleData : MonoBehaviour {

	public List<CastleState> states;
	private CastleState currentState;
	//1
	public CastleState CurrentState {
		//2
		get {
			return currentState;
		}
		//3
		set {
			currentState = value;
			int currentStateIndex = states.IndexOf(currentState);

			GameObject stateVisualization = states[currentStateIndex].visualization;
			for (int i = 0; i < states.Count; i++) {
				if (stateVisualization != null) {
					if (i == currentStateIndex) {
						states[i].visualization.SetActive(true);
					} else {
						states[i].visualization.SetActive(false);
					}
				}
			}
		}
	}

	public void increaseDamage() {
		int currentStateIndex = states.IndexOf(currentState);
		if (currentStateIndex < states.Count - 1) {
			CurrentState = states[currentStateIndex + 1];
		}
	}

	void OnEnable() {
		CurrentState = states[0];
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
