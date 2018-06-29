using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour, HealthBar.Unit
{
	public GameObject m_healthBarPrefab;
	public float m_damage;
	public float m_cooldown;

	private float m_health = 100f;
	GameObject m_healthBar;
	List<UnitController> m_targets = new List<UnitController>();
    BaseController m_baseTarget;
	float m_currentCooldown;

    private NavMeshAgent m_agent;
    Vector3 m_playerBasePosition;

    void Start()
	{
		m_healthBar = Instantiate(m_healthBarPrefab);
		m_healthBar.GetComponent<HealthBar>().SetParent(this);

		m_currentCooldown = m_cooldown;
        m_playerBasePosition = GameObject.FindGameObjectWithTag("PlayerBase").transform.position;

        m_agent = GetComponent<NavMeshAgent>();
        m_agent.SetDestination(m_playerBasePosition);

        SetHealthBarActive(true);
	}

    private void Update()
    {
        var dist = Vector3.Distance(m_agent.transform.position, m_playerBasePosition);
        if (dist < 3f)
        {
            m_agent.isStopped = true;
        }
    }

    void LateUpdate()
	{
		m_currentCooldown -= Time.deltaTime;
		if (m_currentCooldown <= 0)
		{
			Fire();
			m_currentCooldown = m_cooldown;
		}
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
			Destroy(gameObject);
		}
	}

	private void OnDestroy() {
		Destroy(m_healthBar);
	}

	public Transform GetTransform()
	{
		return transform;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Unit"))
		{
			AddUnique(m_targets, (UnitController) other.GetComponent<UnitController>());
		}

        if(other.CompareTag("PlayerBase"))
        {
            m_baseTarget = (BaseController)other.GetComponent<BaseController>();
        }
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Unit"))
		{
			m_targets.Remove((UnitController) other.GetComponent<UnitController>());
		}

        if(other.CompareTag("PlayerBase"))
        {
            m_baseTarget = null;
        }
    }

	private void AddUnique<T>(List<T> list, T item)
	{
		if (!list.Contains(item))
		{
			list.Add(item);
		}
	}

	private void Fire()
	{
        if (m_baseTarget != null)
        {
            m_baseTarget.DecreaseHealth(m_damage);
        }
        else
        {
            m_targets.RemoveAll((item) => { return item == null || item.gameObject == null; });
            if (m_targets.Count == 0)
            {
                return;
            }
            int random = (int)Mathf.Floor(Random.value * m_targets.Count);
            UnitController target = m_targets[random];
            transform.LookAt(target.transform);
            target.DecreaseHealth(m_damage);
        }
	}
}
