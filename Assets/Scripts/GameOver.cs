using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public GameObject resultScreensParent;
    
    private void Start()
    {
        Soup.Instance.OnEmpty += OnGameOver;
        resultScreensParent.SetActive(false);
    }

    private void OnGameOver()
    {
        resultScreensParent.SetActive(true);
        Soup.Instance.OnEmpty -= OnGameOver;

        Player.paused = true;
    }
}
