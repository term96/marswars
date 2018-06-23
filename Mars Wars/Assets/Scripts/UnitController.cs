using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour, InputController.ClickListener, ActionsController.DeselectListener
{
	public GameObject m_healthBarPrefab;
	public GameObject m_actionsControllerObj;
	public GameObject m_inputControllerObj;
	private ActionsController m_actionsController;
	private InputController m_inputController;

	private float m_health = 100f;
	Rigidbody2D m_rigidBody;
	GameObject m_healthBar;

	void Start()
	{
		m_rigidBody = GetComponent<Rigidbody2D>();
		m_healthBar = Instantiate(m_healthBarPrefab);
		m_healthBar.GetComponent<HealthBar>().SetParent(this);

		m_actionsController = m_actionsControllerObj.GetComponent<ActionsController>();

		m_inputController = m_inputControllerObj.GetComponent<InputController>();
		m_inputController.AddClickListener(this);

		SetHealthBarActive(false);
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
			Destroy(m_healthBar);
			Destroy(gameObject);
		}
	}
}
