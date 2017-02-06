using UnityEngine;
using System.Collections;

public class CastSpellManager : MonoBehaviour {

	public int cost;
	protected GameManagerBehavior gameManager;

	public bool canCastSpell(){
		return gameManager.Mana > cost;
	}

	// Use this for initialization
	protected virtual void Start () {
		cost = 100;
		gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
	}
}
