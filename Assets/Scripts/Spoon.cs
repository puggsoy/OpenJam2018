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

    public float maxAngle = 50;
    private Quaternion maxRotation;
    private Quaternion originalRotation;
    public bool isSwirling = false;
    public float angleSpeed = 1;
    public bool goingLeft = false;

	public System.Action<SoupItem> OnScoop;

	// Use this for initialization
	void Start () {
		originalPosition = (Vector2)transform.localPosition;
		maxPosition = originalPosition + ((Vector2)Vector3.up * maxDistance);

        originalRotation = transform.rotation;
        maxRotation = Quaternion.Euler(0, 0, originalRotation.eulerAngles.z + maxAngle);
	}
	
	// Update is called once per frame
	void Update () {
		AnimateScoop();
        AnimateSwirl();
	}

	public void Scoop(int index) {
		if (isScooping || isSwirling)
			return;
		goingUp = true;
		isScooping = true;
		SoupItem item = Soup.Instance.RemoveItem (index);

		OnScoop (item);
	}

    public void Swirl()
    {
        if (isSwirling || isScooping)
            return;
        goingLeft = true;
        isSwirling = true;

        Soup.Instance.Swirl();
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

	void AnimateSwirl() {
		if (!isSwirling)
			return;
		
        if (goingLeft)
        {
            transform.Rotate(0, 0, angleSpeed);

            if (transform.rotation.eulerAngles.z > maxRotation.eulerAngles.z)
            {
                transform.rotation = maxRotation;
                goingLeft = false;
            }
        }
        else
        {
            transform.Rotate(0, 0, -angleSpeed);

            if (transform.rotation.eulerAngles.z < originalRotation.eulerAngles.z)
            {
                transform.rotation = originalRotation;
                isSwirling = false;
            }
        }
	}
}
