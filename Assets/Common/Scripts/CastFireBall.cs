using UnityEngine;
using System.Collections;

public class CastFireBall : CastSpellManager {

	bool selecting = false;
	Transform fireRadius;

	void OnMouseUp () {
		// Check if have enough mana
		if (selecting){
			stopCast();
		}else if(canCastSpell()){
			selecting = true;
		}
	}

	void cast() {
		gameManager.Mana-=cost;
		Collider2D[] hitColliders = Physics2D.OverlapCircleAll(fireRadius.position, 3);
		Debug.Log(hitColliders.Length);
		foreach (Collider2D collider in hitColliders) {
			if (collider.gameObject.CompareTag("Enemy")){
				Transform healthBarTransform = collider.gameObject.transform.FindChild("HealthBar");
				HealthBar healthBar = healthBarTransform.gameObject.GetComponent<HealthBar>();
				healthBar.currentHealth -= 60;
				if (healthBar.currentHealth <= 0) {
					Destroy(collider.gameObject);
					AudioSource audioSource = collider.gameObject.GetComponent<AudioSource>();
					AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);

					gameManager.Gold += 50;
				}
			}
		}
		stopCast();
	}

	void stopCast(){
		selecting = false;
		fireRadius.position = new Vector3(-20, -20, 0);
	}

	// Use this for initialization
	protected override void Start () {
		// Call parent start
		base.Start();
		fireRadius = transform.FindChild("fireRadius");
		fireRadius.position = new Vector3(-20, -20, 0);
	}

	void Update(){
		// do it!
		if (selecting) {
			var mousePos = Input.mousePosition;
			mousePos.z = 1.0f;
			fireRadius.position = Camera.main.ScreenToWorldPoint(mousePos);
			if (Input.GetMouseButtonDown(0)) {
				cast();
			}
		}
	}
}
