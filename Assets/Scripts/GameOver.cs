using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    private Text m_text = null;

    private void Start()
    {
        m_text = GetComponent<Text>();
        Soup.Instance.OnEmpty += OnGameOver;

        m_text.enabled = false;
    }

    private void OnGameOver()
    {
        m_text.enabled = true;
        Soup.Instance.OnEmpty -= OnGameOver;
    }
}
