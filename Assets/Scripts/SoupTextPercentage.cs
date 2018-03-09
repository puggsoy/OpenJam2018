using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class SoupTextPercentage : MonoBehaviour
{
    private Text m_textPercentage = null;
    private Soup m_soup = null;

    private void Start()
    {
        m_textPercentage = GetComponent<Text>();
        m_soup = Soup.Instance;
    }

    private void Update()
    {
        m_textPercentage.text = "SOUP LEFT: " + (m_soup.m_soupPercentage * 100) + "%";
    }
}
