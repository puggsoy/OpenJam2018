﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour {

	public int points = 0;
	public int mouthfuls = 0;
	public int satisfactionLevel = 0;
	public int numPreferredFoodEaten = 0;
	public Spoon spoon;
	public string eatInput;
    public string swirlInput;
	public int section = 0;

    public static bool paused = false;

	public Dictionary<SoupItem.ItemType, int> tally;


	// Use this for initialization
	void Start () {
        paused = false;
		spoon.OnScoop += Eat;

		tally = new Dictionary<SoupItem.ItemType, int> ();

		foreach (SoupItem.ItemType t in Enum.GetValues(typeof(SoupItem.ItemType))) {
			tally [t] = 0;
		}
	}
	
	// Update is called once per frame
	void Update () {
        if (paused)
            return;

		bool eatIsDown = Input.GetButtonDown (eatInput);
		if (eatIsDown) {
			spoon.Scoop (section);
		}
        bool swirlIsDown = Input.GetButtonDown(swirlInput);
        if (swirlIsDown)
        {
            spoon.Swirl();
        }
		/*if (Input.GetKeyDown (KeyCode.Return)) {
			foreach (SoupItem.ItemType t in tally.Keys) {
				Debug.Log ("You ate " + tally [t] + " " + t.ToString() + "s!");
			}

			Debug.Log (mouthfuls + " mouthfuls in total!");
		}*/

	}

	void Eat(SoupItem item) {
		if (item == null) {
			//Debug.Log ("broth");
		} else {
			//Debug.Log (item.m_itemType);
			tally [item.m_itemType]++;
			Destroy (item.gameObject);
		}

		mouthfuls++;

	}
}
