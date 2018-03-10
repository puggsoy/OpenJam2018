using UnityEngine;

public class SoupItem : MonoBehaviour
{
    public enum ItemType
    {
        Broccoli,
        Egg,
        Mushroom,
        Pinapple,
        Shrimp
    }

    [SerializeField]
    private float m_minRotationSpeed = -15.0f;
    [SerializeField]
    private float m_maxRotationSPeed = 15.0f;

    [SerializeField]
    private float m_minAlpha = -50;
    [SerializeField]
    private float m_alphaTime = 5.0f;

    public float m_rotationSpeed = 0.0f;

	// Use this for initialization
	void Awake ()
    {
        m_rotationSpeed = Random.Range(m_minRotationSpeed, m_maxRotationSPeed);
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.Rotate(Vector3.forward, m_rotationSpeed * Time.deltaTime);
    }
}
