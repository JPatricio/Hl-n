using UnityEngine;
using System.Collections;

public class MoveEnemy : MonoBehaviour {

	[HideInInspector]
	public GameObject[] waypoints;
	private int currentWaypoint = 0;
	public float speed = 1.0f;
	private float baseSpeed = 0.015f;
	private bool facingRight = true;
	GameManagerBehavior gameManager;

	private void RotateIntoMoveDirection() {
		//1
		Vector3 newStartPosition = waypoints [currentWaypoint].transform.position;
		Vector3 newEndPosition = waypoints [currentWaypoint + 1].transform.position;
		Vector3 newDirection = (newEndPosition - newStartPosition);
		//2
		float x = newDirection.x;
		float y = newDirection.y;
		float rotationAngle = Mathf.Atan2 (y, x) * 180 / Mathf.PI;
		//3
		GameObject sprite = (GameObject)
			gameObject.transform.FindChild("Sprite").gameObject;
		if(rotationAngle == 180 && facingRight){
			Vector3 newScale = sprite.transform.localScale;
			newScale.x *= -1;
			sprite.transform.localScale = newScale;
			facingRight = false;
		}else if (!facingRight){
			Vector3 newScale = sprite.transform.localScale;
			newScale.x *= -1;
			sprite.transform.localScale = newScale;
			facingRight = true;
		}
		//sprite.transform.rotation = 
		//	Quaternion.AngleAxis(rotationAngle, Vector3.forward);
	}

	public float distanceToGoal() {
		float distance = 0;
		distance += Vector3.Distance(
			gameObject.transform.position, 
			waypoints [currentWaypoint + 1].transform.position);
		for (int i = currentWaypoint + 1; i < waypoints.Length - 1; i++) {
			Vector3 startPosition = waypoints [i].transform.position;
			Vector3 endPosition = waypoints [i + 1].transform.position;
			distance += Vector3.Distance(startPosition, endPosition);
		}
		return distance;
	}

	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
		gameObject.transform.position = waypoints[0].transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		// end position for the current path segment
		Vector3 endPosition = waypoints [currentWaypoint + 1].transform.position;

		float step = speed * baseSpeed * gameManager.globalSpeed;
		transform.position = Vector3.MoveTowards(transform.position, endPosition, step);

		// Has he reached the end of this path?
		if (gameObject.transform.position.Equals(endPosition)) {
			if (currentWaypoint < waypoints.Length - 2) {
				// Yes but there's more ahead
				currentWaypoint++;
				RotateIntoMoveDirection();
			} else {
				// Yes and he has reached the castle
				Destroy(gameObject);

				AudioSource audioSource = gameObject.GetComponent<AudioSource>();
				AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);

				gameManager.Health -= 1;
			}
		}
	}
}
