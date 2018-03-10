using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoupSection
{
    public float m_area = 0.0f;
    public int m_index = 0;
    public List<SoupItem> m_items = new List<SoupItem>();
    public SoupItem.ItemType m_itemType = SoupItem.ItemType.Broccoli;

    public float m_startAngle = 0.0f;
    public float m_endAngle = 0.0f;
    public float m_radius = 0.0f;

    public bool m_isSwirling = false;
    private float m_distanceToMove = 0.0f;

    public SoupSection(float area, int index, float start, float radius, SoupItem.ItemType type)
    {
        m_area = area;
        m_index = index;
        m_startAngle = start;
        m_endAngle = start + area;
        m_radius = radius;

        //m_itemType = (SoupItem.ItemType)Random.Range((int)SoupItem.ItemType.Broccoli, (int)SoupItem.ItemType.Shrimp);
        m_itemType = type;
    }

    public bool IsInSoup(SoupItem item)
    {
        float distance = Mathf.Abs(Vector3.Distance(item.transform.position, Soup.Instance.transform.position));
        float soupDistance = (Soup.Instance.m_soupSprite.bounds.size.x / 2) * Soup.Instance.m_soupPercentage;

        soupDistance += 1.0f;

        if (distance < soupDistance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Update()
    {
        for (int i = m_items.Count - 1; i >= 0; --i)
        {
            if (IsInSoup(m_items[i]))
            {
                m_items[i].transform.Rotate(Vector3.forward, m_items[i].m_rotationSpeed * Time.deltaTime);
            }
            else
            {
                m_items[i].Kill();
                m_items.RemoveAt(i);
            }
        }

        if (m_isSwirling)
        {
            float distanceMoved = (m_endAngle - m_startAngle) * Time.deltaTime * Soup.Instance.m_swirlSpeed;

            for (int i = 0; i < m_items.Count; ++i)
            {
                m_items[i].transform.RotateAround(Soup.Instance.transform.position, Vector3.back, distanceMoved);
            }

            m_distanceToMove -= distanceMoved;

            m_isSwirling = m_distanceToMove > 0;
        }
    }

    public void Swirl(SoupItem.ItemType newType)
    {
        m_isSwirling = true;
        m_distanceToMove = m_endAngle - m_startAngle;
        m_itemType = newType;
        //for (int i = 0; i < m_items.Count; ++i)
        //{
        //    m_items[i].transform.RotateAround(Soup.Instance.transform.position, Vector3.forward, m_endAngle - m_startAngle);
        //}
    }

    public void SpawnItem()
    {
        if (m_isSwirling)
            return;
        // this is a non equal distribution of points
        // first pick a random angle
        float randAngle = Random.Range(m_startAngle * 1.1f, m_endAngle * 0.9f);

        // Then a random distance
        float randDistance = Random.Range(m_radius / 3.0f, m_radius * 0.9f);

        // and a random rotation
        float randRotation = Random.Range(0.0f, 360.0f);

        // calculate the final point for the item
        float x = Soup.Instance.transform.position.x + randDistance * Mathf.Cos(randAngle * (Mathf.PI / 180));
        float y = Soup.Instance.transform.position.y + randDistance * Mathf.Sin(randAngle * (Mathf.PI / 180));
        Vector2 spawnPoint = new Vector2(x, y);

        SoupItem spawned = Soup.Instance.InstantiateSoupItem(spawnPoint, new Quaternion(Quaternion.identity.x, Quaternion.identity.y, randRotation, Quaternion.identity.w), (int)m_itemType);
        spawned.transform.SetParent(Soup.Instance.transform);
        spawned.transform.position = spawnPoint;
        spawned.m_rotationSpeed = Random.Range(-15.0f, 15.0f);

        m_items.Add(spawned);
    }
}
