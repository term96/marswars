using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using UnityEngine.AI;

public class ActionsController : MonoBehaviour, InputController.ClickListener
{
	public Button m_moveButton;
	public Button m_cancelButton;
	public Color m_normalColor;
	public Color m_activeColor;
	public float m_moveSpeed;
	public GameObject m_inputControllerObj;
	private InputController m_inputController;
	private IList<GameObject> m_selectedUnits = new List<GameObject>();
	private bool m_moveActionActive;
	private Vector3 m_destination = Vector3.zero;

	private void Start()
	{
		m_moveButton.gameObject.SetActive(false);
		m_cancelButton.gameObject.SetActive(false);
		m_inputController = m_inputControllerObj.GetComponent<InputController>();
		m_inputController.AddClickListener(this);
	}

	private void Update()
	{
		if (m_destination != Vector3.zero)
		{
			foreach (var unit in m_selectedUnits)
			{
				NavMeshAgent agent = unit.GetComponent<NavMeshAgent>();
				if (agent.isStopped)
				{
					continue;
				}
				var dist = Vector3.Distance(unit.transform.position, m_destination);
				Debug.Log(dist);
				if (dist < 0.8f)
				{
					agent.isStopped = true;
				}
			}
		}
		
	}

	public void AddSelectedUnit(GameObject unit)
	{
		if (!m_selectedUnits.Contains(unit))
		{
			m_selectedUnits.Add(unit);
		}
		m_moveButton.gameObject.SetActive(true);
		m_cancelButton.gameObject.SetActive(true);
	}

	public void Cancel()
	{
		if (m_moveActionActive)
		{
			SetMoveActionActive(false);
		}
		else
		{
			m_moveButton.gameObject.SetActive(false);
			m_cancelButton.gameObject.SetActive(false);
			
			foreach (var unit in m_selectedUnits)
			{
				unit.GetComponent<UnitController>().OnDeselect();
			}
			m_selectedUnits.Clear();
		}
	}

	public void Move()
	{
		SetMoveActionActive(true);
	}

	private void SetMoveActionActive(bool active)
	{
		m_moveActionActive = active;
		m_moveButton.GetComponent<Image>().color = active ? m_activeColor : m_normalColor;
	}

	public bool IsSelected(GameObject unit)
	{
		return m_selectedUnits.IndexOf(unit) != -1;
	}

	public void AcceptClickEvent(Vector3 worldPosition, bool uiClick, Transform hitTransform)
	{
		if (m_moveActionActive && !uiClick)
		{
			foreach (var item in m_selectedUnits)
			{
				NavMeshAgent agent = item.GetComponent<NavMeshAgent>();
				worldPosition.z = item.transform.position.z;
				agent.SetDestination(worldPosition);
				agent.isStopped = false;
			}
			m_destination = m_selectedUnits[0].GetComponent<NavMeshAgent>().destination;
			SetMoveActionActive(false);
		}
	}

	public interface DeselectListener
	{
		void OnDeselect();
	}
}
