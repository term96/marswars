using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitController : MonoBehaviour, InputController.ClickListener, ActionsController.DeselectListener, HealthBar.Unit
{
	public GameObject m_healthBarPrefab;
	public GameObject m_actionsControllerObj;
	public GameObject m_inputControllerObj;
	public float m_damage;
	public float m_cooldown;
	private ActionsController m_actionsController;
	private InputController m_inputController;
	private float m_health = 100f;
	GameObject m_healthBar;
	List<EnemyController> m_targets = new List<EnemyController>();
	float m_currentCooldown;
	private NavMeshAgent m_agent;
    BaseController m_baseTarget;

    void Start()
	{
		m_healthBar = Instantiate(m_healthBarPrefab);
		m_healthBar.GetComponent<HealthBar>().SetParent(this);

		m_actionsController = m_actionsControllerObj.GetComponent<ActionsController>();

		m_inputController = m_inputControllerObj.GetComponent<InputController>();
		m_inputController.AddClickListener(this);

		m_currentCooldown = 0;

		m_agent = GetComponent<NavMeshAgent>();
		m_agent.updateRotation = false;

		SetHealthBarActive(false);
	}

	void LateUpdate()
	{
		m_currentCooldown -= Time.deltaTime;
		if (m_currentCooldown <= 0)
		{
			Fire();
		}

		if (!m_agent.isStopped)
		{
			LookAt(m_agent.steeringTarget);
		}
	}

	void SetHealthBarActive(bool active)
	{
		m_healthBar.SetActive(active);
	}

	public void AcceptClickEvent(Vector3 worldPosition, bool uiClick, Transform hitTransform)
	{
        if (hitTransform == transform)
        {
            m_actionsController.GetComponent<ActionsController>().AddSelectedUnit(gameObject);
            SetHealthBarActive(true);
        }
	}

	public void OnDeselect()
	{
		SetHealthBarActive(false);
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

    public void IncreaseHealth(float health)
    {
        if (m_health < 100)
        {
            m_health += health;
        }
        else
        {
            m_health = 100;
        }
    }

    public Transform GetTransform()
	{
		return transform;
	}

	private void OnDestroy()
	{
		Destroy(m_healthBar);
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            AddUnique(m_targets, (EnemyController)other.GetComponent<EnemyController>());
        }

        if (other.CompareTag("EnemyBase"))
        {
            m_baseTarget = (BaseController)other.GetComponent<BaseController>();
        }
    }

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Enemy"))
		{
			m_targets.Remove((EnemyController) other.GetComponent<EnemyController>());
		}

        if (other.CompareTag("EnemyBase"))
        {
            m_baseTarget = (BaseController)other.GetComponent<BaseController>();
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
            EnemyController target = m_targets[random];

            m_agent.isStopped = true;
            LookAt(target.transform.position);
            target.DecreaseHealth(m_damage);

            m_currentCooldown = m_cooldown;
        }
	}

	void LookAt(Vector3 position)
	{
		Vector3 offset = transform.position - position;
    	transform.LookAt(transform.position + offset, transform.up);
	}
}
