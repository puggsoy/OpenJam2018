using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoupSection
{
    public float m_area = 0.0f;
    public int m_index = 0;
    public List<SoupItem> m_items = new List<SoupItem>();
    public Transform m_itemPrefab = null;

    public float m_startAngle = 0.0f;
    public float m_endAngle = 0.0f;
    public float m_radius = 0.0f;

    public SoupSection(float area, int index, Transform prefab, float start, float radius)
    {
        m_area = area;
        m_index = index;
        m_itemPrefab = prefab;
        m_startAngle = start;
        m_endAngle = start + area;
        m_radius = radius;
    }

    public void SpawnItem()
    {
        // this is a non equal distribution of points

        // first pick a random angle
        float randAngle = Random.Range(m_startAngle, m_endAngle);

        // Then a random distance
        float randDistance = Random.Range(0, m_radius);

        // and a random rotation
        float randRotation = Random.Range(0.0f, 360.0f);

        // calculate the final point for the item
        float x = Soup.Instance.transform.position.x + randDistance * Mathf.Cos(randAngle * (Mathf.PI / 180));
        float y = Soup.Instance.transform.position.y + randDistance * Mathf.Sin(randAngle * (Mathf.PI / 180));
        Vector2 spawnPoint = new Vector2(x, y);

        Debug.Log(x + ", " + y);

        SoupItem spawned = Soup.Instance.InstantiateSoupItem(spawnPoint, new Quaternion(Quaternion.identity.x, Quaternion.identity.y, randRotation, Quaternion.identity.w));
        spawned.transform.parent = Soup.Instance.transform;
        spawned.transform.position = spawnPoint;

        m_items.Add(spawned);
    }
}
