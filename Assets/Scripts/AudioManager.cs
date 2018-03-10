using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Audio;

public class AudioManager : MonoBehaviour {

	public GameObject ReadySetGoSFX;

	public GameObject playerSwirlSFX;
	public GameObject playerSlurpSFX;
	public GameObject playerEatSFX;
	public GameObject playerHappySFX;
	private AudioSource[] playerSwirls;
	private AudioSource[] playerSlurps;
	private AudioSource[] playerEats;
	private AudioSource[] playerHappys;
	private int allPlayerSwirls;
	private int allPlayerSlurps;
	private int allPlayerEats;
	private int allPlayerHappys;


	// Use this for initialization
	void Start () {
		playerSwirls = playerSwirlSFX.GetComponentsInChildren<AudioSource>();
		allPlayerSwirls = playerSwirls.Length;

	}

	// Update is called once per frame
	void Update () {
		
	}
}
