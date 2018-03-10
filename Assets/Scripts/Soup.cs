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
    public SpriteRenderer m_soupSprite = null;
    [SerializeField]
    public SpriteRenderer m_bowlSprite = null;

    [SerializeField]
    public float m_itemFrequencyMin = 0.0f;

    [SerializeField]
    public float m_itemFrequencyMax = 1.0f;

    [SerializeField]
    public Transform m_itemPrefab = null;

    [SerializeField]
    public int m_numPlayers = 4;

    [SerializeField]
    public int m_spacesBetweenPlayers;

    public int m_numSections = 0;

    public float m_currentSoupLevel = 0.0f;
    public float m_soupPercentage = 1.0f;
    public List<SoupSection> m_sectionsList = null;
    public bool m_gameRunning = true;

    private float m_itemTimer = 0.0f;

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
        m_itemTimer = Random.Range(m_itemFrequencyMin, m_itemFrequencyMax);
        m_numSections = m_numPlayers * (m_spacesBetweenPlayers + 1);
        m_currentSoupLevel = m_maxSoupLevel;
        SetUpSoupSections();
    }

    private void SetUpSoupSections()
    {
        m_sectionsList = new List<SoupSection>();
        float areaSize = 360.0f / m_numSections;
        float angle = 0.0f;
        float radius = m_soupSprite.bounds.size.x / 2;
        for (int i = 0; i < m_numSections; ++i)
        {
            m_sectionsList.Add(new SoupSection(areaSize, i, m_itemPrefab, angle, radius));
            angle += areaSize;
        }
    }

    public SoupItem InstantiateSoupItem(Vector2 position, Quaternion rotation)
    {
        return Instantiate(m_itemPrefab, position, rotation).GetComponent<SoupItem>();
    }

    public List<SoupItem> GetItemsInSection(int index)
    {
        return m_sectionsList[index].m_items;
    }

    private void DrawSections()
    {
        Vector2 center = transform.position;
        float radius = m_soupSprite.bounds.size.x / 2;

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
        m_soupSprite.transform.localScale = new Vector3(m_soupPercentage, m_soupPercentage, m_soupPercentage);
    }

    private void UpdatePercentage()
    {
        m_soupPercentage = m_currentSoupLevel / m_maxSoupLevel;
        m_soupPercentage = m_soupPercentage < 0 ? 0 : m_soupPercentage;
    }

    private void SpawnItem()
    {
        // Pick a random section
        int randIndex = Random.Range(0, m_sectionsList.Count);

        m_sectionsList[randIndex].SpawnItem();
    }
	
	// Update is called once per frame
	void Update ()
    {
		if (Input.GetKeyUp(KeyCode.Space))
        {
            RemoveSoup(50);
        }
        DrawSections();

        m_itemTimer -= Time.deltaTime;

        if (m_itemTimer < 0)
        {
            SpawnItem();
            m_itemTimer = Random.Range(m_itemFrequencyMin, m_itemFrequencyMax);
        }

        for (int i = 0; i < m_sectionsList.Count; ++i)
        {
            m_sectionsList[i].m_radius = m_soupSprite.bounds.size.x / 2;

            m_sectionsList[i].Update();
        }
    }
}
