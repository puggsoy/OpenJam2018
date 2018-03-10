using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spoon : MonoBehaviour {

	public float maxDistance = 100f;
	private Vector2 maxPosition;
	private Vector2 originalPosition;
	public bool isScooping = false;
	public float speed = 10f;
	public bool goingUp = false;
	public System.Action OnScoop;

	// Use this for initialization
	void Start () {
		originalPosition = transform.localPosition;
		maxPosition = originalPosition + ((Vector2)transform.up * maxDistance);
	}
	
	// Update is called once per frame
	void Update () {
		AnimateScoop ();
	}

	public void Scoop() {
		if (isScooping)
			return;
		goingUp = true;
		isScooping = true;
		OnScoop ();
	}

	void AnimateScoop() {
		if (!isScooping)
			return;

		if (goingUp) {
			transform.Translate (0, speed, 0);
			if (transform.localPosition.y > maxPosition.y) {
				transform.localPosition = maxPosition;
				goingUp = false;
			}
		} else {
			transform.Translate (0, -speed, 0);
			if (transform.localPosition.y < originalPosition.y) {
				transform.localPosition = originalPosition;
				isScooping = false;
			}
		}
	
	}
}
