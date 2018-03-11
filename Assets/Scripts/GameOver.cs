using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject resultScreensParent;
    public ResultScreen[] resultScreens;
    public Player[] players;

    private bool restartable = false;
    
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

        for(int i = 0; i < resultScreens.Length; i++)
        {
            var t = players[i].tally;
            resultScreens[i].SetStats(t[SoupItem.ItemType.Broccoli], t[SoupItem.ItemType.Egg], t[SoupItem.ItemType.Mushroom],
                                    t[SoupItem.ItemType.Pinapple], t[SoupItem.ItemType.Shrimp], players[i].mouthfuls);
        }

        restartable = true;
    }

    void Update()
    {
        if (!restartable)
            return;

        bool spaceDown = Input.GetButtonDown("Restart");
        if (spaceDown)
        {
            restartable = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
