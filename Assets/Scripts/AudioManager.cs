using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Audio;

public class AudioManager : MonoBehaviour {

	public GameObject ReadySetGoSFX;
	private AudioSource ReadySetGo;

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
		ReadySetGoSFX.GetComponent<AudioSource>();
		playerSwirls = playerSwirlSFX.GetComponentsInChildren<AudioSource>();
		allPlayerSwirls = playerSwirls.Length;
		playerSlurps = playerSlurpSFX.GetComponentsInChildren<AudioSource>();
		allPlayerSlurps = playerSlurps.Length;
		playerEats = playerEatSFX.GetComponentsInChildren<AudioSource>();
		allPlayerEats = playerEats.Length;
		playerHappys = playerHappySFX.GetComponentsInChildren<AudioSource>();
		allPlayerHappys = playerHappys.Length;
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void SFXReadySetGo()
	{
		ReadySetGo.Play();
	}
	public void SFXplayerSwirl()
	{
		int randomPlayerSwirl = Random.Range(0, allPlayerSwirls);
		playerSwirls[randomPlayerSwirl].Play();
	}
	public void SFXplayerSlurp()
	{
		int randomPlayerSlurp = Random.Range(0, allPlayerSlurps);
		playerSlurps[randomPlayerSlurp].Play();
	}
	public void SFXplayerEat()
	{
		int randomPlayerEat = Random.Range(0, allPlayerEats);
		playerEats[randomPlayerEat].Play();
	}
	public void SFXplayerHappy()
	{
		int randomPlayerHappy = Random.Range(0, allPlayerHappys);
		playerHappys[randomPlayerHappy].Play();
	}

}
