using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public int points = 0;
	public int mouthfuls = 0;
	public int satisfactionLevel = 0;
	public int numPreferredFoodEaten = 0;
	public Spoon spoon;
	public string eatInput;
	public int section = 0;


	// Use this for initialization
	void Start () {
		spoon.OnScoop += Eat;
	}
	
	// Update is called once per frame
	void Update () {
		
		bool eatIsDown = Input.GetButtonDown (eatInput);
		if (eatIsDown) {
			spoon.Scoop (section);
		}
	}

	void Eat(string msg) {
		Debug.Log (msg);
	}
}
