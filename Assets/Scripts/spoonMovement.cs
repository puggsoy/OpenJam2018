using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spoonMovement : MonoBehaviour {

	public float maxDistance = 100f;
	private Vector2 maxPosition;
	private Vector2 originalPosition;
	public bool isScooping = false;
	public float speed = 10f;
	public bool goingUp = false;

	// Use this for initialization
	void Start () {
		originalPosition = transform.position;
		maxPosition = originalPosition + (Vector2.up * maxDistance);
	}
	
	// Update is called once per frame
	void Update () {
		bool eatIsDown = Input.GetButtonDown ("Eat");
		if (eatIsDown && !isScooping) {
			isScooping = true;
			goingUp = true;
		}
		scoop ();
	}

	void scoop() {
		if (!isScooping)
			return;

		if (goingUp) {
			transform.Translate (0, speed, 0);
			if (transform.position.y > maxPosition.y) {
				transform.position = maxPosition;
				goingUp = false;
			}
		} else {
			transform.Translate (0, -speed, 0);
			if (transform.position.y < originalPosition.y) {
				transform.position = originalPosition;
				isScooping = false;
			}
		}
	
	}
}
