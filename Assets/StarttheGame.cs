using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StarttheGame : MonoBehaviour {

    public GameObject ReadySetGoSFX;
    private AudioSource ReadySetGo;

    // Use this for initialization
    void Start () {
        ReadySetGo = ReadySetGoSFX.GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        bool start = Input.GetButtonDown("Restart");

        if (start)
        {
            ReadySetGo.Play();
            Invoke("SceneStarter", 2.760431f);
        }
	}

    void SceneStarter()
    {
        SceneManager.LoadScene(1);
    }
}
