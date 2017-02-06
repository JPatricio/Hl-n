using UnityEngine;
using System.Collections;

public class jumpIn : MonoBehaviour {

	private bool moveUp, moveDown;
	private bool moving;

	IEnumerator sliderFunction(){
		moving = true;
		moveUp = true;
		yield return new WaitForSeconds(0.9f);
		moveUp = false;
		yield return new WaitForSeconds(2.5f);
		moveDown = true;
		yield return new WaitForSeconds(0.9f);
		moveDown = false;
		moving = false;
		foreach(Transform r in transform) {
			r.gameObject.SetActive(false);
		}
	}

	// Use this for initialization
	void Start () {
		moveUp = moveDown = moving = false;
	}

	public void displayObjectInfo(string[] displayInfo, Sprite enemySprite){
		if (!moving){
			foreach(Transform r in transform) {
				if(displayInfo[0] == r.name){
					r.gameObject.SetActive(true);
					TextMesh[] texts = r.GetComponentsInChildren<TextMesh>();
					foreach (TextMesh k in texts){
						if(k.gameObject.name == "damageText"){
							k.text = displayInfo[2];
						}else if (k.gameObject.name == "speedText" || k.gameObject.name == "radiusText"){
							k.text = displayInfo[3];
						}else if (k.gameObject.name == "nameText"){
							k.text = displayInfo[1];
						}
					}
					SpriteRenderer[] sprites = r.GetComponentsInChildren<SpriteRenderer>();
					foreach (SpriteRenderer spriteRenderer in sprites){
						if (spriteRenderer.gameObject.name == "enemySprite" || spriteRenderer.gameObject.name == "towerSprite"){
							spriteRenderer.sprite = enemySprite;
						}
					}
				}
			}
			StartCoroutine(sliderFunction());
		}
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 moveVector = new Vector3(0, 0.06f, 0);
		if (moveUp && transform.position.y < -7){
			Vector3 newPosition = transform.position + moveVector;
			transform.position = newPosition;
		} else if (moveDown && transform.position.y > -9){
			Vector3 newPosition = transform.position - moveVector;
			transform.position = newPosition;
		}
	}
}
