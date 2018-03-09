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
    public RectTransform m_soupRect = null;

    [SerializeField]
    public int m_numPlayers = 4;

    [SerializeField]
    public int m_spacesBetweenPlayers;

    public int m_numSections = 0;

    [SerializeField]
    public Image m_soupImage = null;

    public float m_currentSoupLevel = 0.0f;
    public float m_soupPercentage = 1.0f;
    public List<SoupSection> m_sectionsList = null;

    public void RemoveSoup(float amount)
    {
        m_currentSoupLevel -= amount;
        UpdatePercentage();
        UpdateSoupImage();

        if (m_currentSoupLevel <= 0)
        {
            if (OnEmpty != null)
            {
                OnEmpty();
            }

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
        m_numSections = m_numPlayers * (m_spacesBetweenPlayers + 1);
        m_currentSoupLevel = m_maxSoupLevel;
        m_sectionsList = new List<SoupSection>();
    }

    private void SetUpSoupSections()
    {
        float areaSize = 360.0f / m_numSections;
        for (int i = 0; i < m_numSections; ++i)
        {
            m_sectionsList.Add(new SoupSection(areaSize));
        }
    }

    private void DrawSections()
    {
        Vector2 center = transform.position;
        float radius = m_soupRect.rect.width / 2;

        float angleDiff = 360.0f / m_numSections;
        float angle = 0.0f;
        for (int i = 0; i < m_numSections; ++i)
        {
            float x1 = center.x + radius * Mathf.Cos(angle * (Mathf.PI / 180));
            float y1 = center.y + radius * Mathf.Sin(angle * (Mathf.PI / 180));
            Debug.DrawLine(center, new Vector2(x1, y1), Color.red);
            angle += angleDiff;
        }
    }

    private void UpdateSoupImage()
    {
        m_soupImage.transform.localScale = new Vector3(m_soupPercentage, m_soupPercentage, m_soupPercentage);
    }

    private void UpdatePercentage()
    {
        m_soupPercentage = m_currentSoupLevel / m_maxSoupLevel;
        m_soupPercentage = m_soupPercentage < 0 ? 0 : m_soupPercentage;
    }
	
	// Update is called once per frame
	void Update ()
    {
		if (Input.GetKeyUp(KeyCode.Space))
        {
            RemoveSoup(50);
        }
        DrawSections();

    }
}
