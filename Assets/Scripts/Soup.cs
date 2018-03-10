using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Soup : MonoBehaviour
{
    public static Soup Instance = null;
    public System.Action OnEmpty;

    [Header("Soup")]
    [SerializeField]
    public float m_maxSoupLevel = 1000.0f;
    public float m_currentSoupLevel = 0.0f;
    public float m_soupPercentage = 1.0f;
    public int m_numSections = 0;
    public List<SoupSection> m_sectionsList = null;
    public float m_swirlSpeed = 1.0f;
    public float m_brothValue = 10.0f;

    [Header("Assets")]
    [SerializeField]
    public SpriteRenderer m_soupSprite = null;
    [SerializeField]
    public SpriteRenderer m_soupWaveSprite = null;
    [SerializeField]
    private SpriteRenderer m_bowlSprite = null;
    [SerializeField]
    private List<Transform> m_itemPrefabs = null;

    [Header("Spawning")]
    [SerializeField]
    private float m_itemFrequencyMin = 0.0f;
    [SerializeField]
    private float m_itemFrequencyMax = 1.0f;
    [SerializeField]
    private float m_maxItemsInSoup = 12;
    [SerializeField]
    private float m_maxItemsInSection = 3;

    [Header("Players")]
    [SerializeField]
    public int m_numPlayers = 4;
    [SerializeField]
    public int m_spacesBetweenPlayers;

    public bool m_gameRunning = true;

    private float m_itemTimer = 0.0f;
    private Vector3 m_originScale;

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

    public void DestroyItem(SoupItem item)
    {
        Destroy(item);
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
        m_originScale = m_soupSprite.transform.localScale;
        SetUpSoupSections();
    }

    private void SetUpSoupSections()
    {
        m_sectionsList = new List<SoupSection>();
        float areaSize = 360.0f / m_numSections;
        float angle = 0.0f;
        float radius = m_soupSprite.bounds.size.x / 2;
        SoupItem.ItemType previousType = SoupItem.ItemType.Broccoli;
        SoupItem.ItemType firstType = previousType;

        for (int i = 0; i < m_numSections; ++i)
        {
            SoupItem.ItemType currentType = (SoupItem.ItemType)Random.Range((int)SoupItem.ItemType.Broccoli, (int)SoupItem.ItemType.Shrimp + 1);

            while (currentType == previousType || (i == m_numSections - 1 && currentType == firstType))
            {
                currentType = (SoupItem.ItemType)Random.Range((int)SoupItem.ItemType.Broccoli, (int)SoupItem.ItemType.Shrimp);
            }

            if (i == 0)
            {
                firstType = currentType;
            }

            previousType = currentType;

            m_sectionsList.Add(new SoupSection(areaSize, i, angle, radius, currentType));
            angle += areaSize;
        }
    }

    public SoupItem InstantiateSoupItem(Vector2 position, Quaternion rotation, int type)
    {
        return Instantiate(m_itemPrefabs[type], position, rotation).GetComponent<SoupItem>();
    }

    public List<SoupItem> GetItemsInSection(int index)
    {
        return m_sectionsList[index].m_items;
    }

	public SoupItem RemoveItem(int index)
	{
		if (m_sectionsList [index].m_items.Count == 0)
        {
            RemoveSoup(m_brothValue);
            return null;
        }

		SoupItem item = m_sectionsList [index].m_items[0];
		m_sectionsList [index].m_items.RemoveAt(0);

        RemoveSoup(item.m_soupValue);

		return item;
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

    public void Swirl()
    {
        List<SoupItem> lastList = m_sectionsList[0].m_items;
        SoupItem.ItemType lastType = m_sectionsList[0].m_itemType;

        for (int i = 0; i < m_numSections - 1; ++i)
        {
            m_sectionsList[i].m_items = m_sectionsList[i + 1].m_items;
            m_sectionsList[i].Swirl(m_sectionsList[i + 1].m_itemType);
        }

        // swirl last
        m_sectionsList[m_numSections - 1].m_items = lastList;
        //m_sectionsList[0].m_itemType = lastType;
        m_sectionsList[m_numSections - 1].Swirl(lastType);

    }

    private void UpdateSoupImage()
    {
        m_soupSprite.transform.localScale = new Vector3(m_originScale.x * m_soupPercentage, m_originScale.y * m_soupPercentage, m_originScale.y * m_soupPercentage);
    }

    private void UpdatePercentage()
    {
        m_soupPercentage = m_currentSoupLevel / m_maxSoupLevel;
        m_soupPercentage = m_soupPercentage < 0 ? 0 : m_soupPercentage;
    }

    private void SpawnItem()
    {
        if (m_currentSoupLevel <= 0)
            return;
        // Pick a random section

        // Populate list of available sections
        List<SoupSection> validSections = new List<SoupSection>();
        int totalAlreadySpawned = 0;
        for (int i = 0; i < m_sectionsList.Count; ++i)
        {
            totalAlreadySpawned += m_sectionsList[i].m_items.Count;
            if (m_sectionsList[i].m_items.Count < m_maxItemsInSection)
            {
                validSections.Add(m_sectionsList[i]);
            }
        }

        if (totalAlreadySpawned < m_maxItemsInSoup && validSections.Count > 0)
        {
            int randIndex = Random.Range(0, validSections.Count);

            validSections[randIndex].SpawnItem();
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		if (Input.GetKeyUp(KeyCode.Space))
        {
            RemoveSoup(50);
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            Swirl();
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

        if (m_sectionsList[0].m_isSwirling)
        {
            float distanceMoved = (m_sectionsList[0].m_endAngle - m_sectionsList[0].m_startAngle) * Time.deltaTime * m_swirlSpeed;
            m_soupWaveSprite.transform.Rotate(Vector3.back, distanceMoved);
        }
    }
}
