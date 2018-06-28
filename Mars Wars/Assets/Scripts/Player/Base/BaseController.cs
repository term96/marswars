using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour, HealthBar.Unit
{
    public GameObject m_healthBarPrefab;
    GameObject m_healthBar;
    private float m_health = 100f;

    // Use this for initialization
    void Start () {
        m_healthBar = Instantiate(m_healthBarPrefab);
        m_healthBar.GetComponent<HealthBar>().SetParent(this);
        SetHealthBarActive(true);
    }

	void Update() {
		
	}

    public float GetHealth()
    {
        return m_health;
    }

    public void DecreaseHealth(float damage)
    {
        m_health -= damage;
        if (m_health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public Transform GetTransform()
    {
        return transform;
    }

    void SetHealthBarActive(bool active)
    {
        m_healthBar.SetActive(active);
    }
}
