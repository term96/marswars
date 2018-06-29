using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseController : MonoBehaviour, HealthBar.Unit
{
    public GameObject m_healthBarPrefab;
    GameObject m_healthBar;
    private float m_health;

    // Use this for initialization
    void Start () {
        m_healthBar = Instantiate(m_healthBarPrefab);
        m_healthBar.GetComponent<HealthBar>().SetParent(this);
        SetHealthBarActive(true);
        m_health = 1000f;
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
            if (gameObject.tag == "EnemyBase")
            {
                SceneManager.LoadScene("PlayerWin", LoadSceneMode.Single);
            }
            else
            {
                SceneManager.LoadScene("BotWin", LoadSceneMode.Single);
            }
        }
    }

    public Transform GetTransform()
    {
        if (this == null)
        {
            return null;
        }
        else
        {
            return transform;
        }
    }

    void SetHealthBarActive(bool active)
    {
        m_healthBar.SetActive(active);
    }
}
