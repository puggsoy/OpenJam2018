using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Soup : MonoBehaviour
{
    public static Soup Instance = null;
    public System.Action OnEmpty;

    [SerializeField]
    public float m_maxSoupLevel = 1000.0f;

    [SerializeField]
    public Image m_soupImage = null;

    public float m_currentSoupLevel = 0.0f;

    public float m_soupPercentage = 1.0f;

    public void RemoveSoup(float amount)
    {
        m_currentSoupLevel -= amount;
        UpdatePercentage();
        UpdateSoupImage();

        if (m_currentSoupLevel <= 0 && OnEmpty != null)
        {
            OnEmpty();
            m_currentSoupLevel = 0;
        }
    }

	// Use this for initialization
	void Awake ()
    {
        if (Instance == null)
        {
            Instance = this;
            Initialise();
        }
        else
        {
            Destroy(this);
        }
	}

    public void Initialise()
    {
        m_currentSoupLevel = m_maxSoupLevel;
    }

    private void UpdateSoupImage()
    {
        m_soupImage.transform.localScale = new Vector3(m_soupPercentage, m_soupPercentage, m_soupPercentage);
    }

    private void UpdatePercentage()
    {
        m_soupPercentage = m_currentSoupLevel / m_maxSoupLevel;
    }
	
	// Update is called once per frame
	void Update ()
    {
		if (Input.GetKeyUp(KeyCode.Space))
        {
            RemoveSoup(10);
        }
	}
}
