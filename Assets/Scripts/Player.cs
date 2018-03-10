using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public int points = 0;
	public int mouthfuls = 0;
	public int satisfactionLevel = 0;
	public int numPreferredFoodEaten = 0;
	public Spoon spoon;


	// Use this for initialization
	void Start () {
		spoon.OnScoop += Eat;
	}
	
	// Update is called once per frame
	void Update () {
		
		bool eatIsDown = Input.GetButtonDown ("Eat");
		if (eatIsDown) {
			spoon.Scoop ();
		}
	}

	void Eat() {
		Debug.Log ("yummy!!!");
	}
}
