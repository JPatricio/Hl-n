using UnityEngine;
using System.Collections;

public class CastBlizzard : CastSpellManager {

	void OnMouseUp () {
		// Check if have enough mana
		if(canCastSpell()){
			StartCoroutine(cast());
		}
	}

	IEnumerator cast() {
		gameManager.Mana-=cost;
		gameManager.globalSpeed = 0.5f;
		// Make enemies blue
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		foreach (GameObject enemy in enemies){
			enemy.transform.FindChild("Sprite").gameObject.GetComponent<SpriteRenderer>().color = new Color(102.0f/255, 163.0f/255, 210.0f/255);
		}
		yield return new WaitForSeconds(5);
		gameManager.globalSpeed = 1;
		foreach (GameObject enemy in enemies){
			enemy.transform.FindChild("Sprite").gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);
		}
	}

	// Use this for initialization
	protected override void Start () {
		// Call parent start
		base.Start();
	}

}
