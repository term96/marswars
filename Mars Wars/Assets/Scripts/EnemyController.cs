using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, HealthBar.Unit
{
	public GameObject m_healthBarPrefab;

	private float m_health = 100f;
	Rigidbody2D m_rigidBody;
	GameObject m_healthBar;

	void Start()
	{
		m_rigidBody = GetComponent<Rigidbody2D>();
		m_healthBar = Instantiate(m_healthBarPrefab);
		m_healthBar.GetComponent<HealthBar>().SetParent(this);

		SetHealthBarActive(false);
	}

	void SetHealthBarActive(bool active)
	{
		m_healthBar.SetActive(active);
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
			Destroy(m_healthBar);
			Destroy(gameObject);
		}
	}

	public Transform GetTransform()
	{
		return transform;
	}

	private void OnTriggerEnter(Collider other)
	{
		Debug.Log("Enter");
		if (other.CompareTag("Unit"))
		{
			Debug.Log("Comp");
			Destroy(other.gameObject);
		}
	}
}
