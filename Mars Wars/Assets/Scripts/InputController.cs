using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputController : MonoBehaviour
{
	public TouchPhase? m_latestPhase;
	
	private Vector3 m_startPosition;
	private bool m_moved;
	private bool m_uiClick;

	private IList<MovementListener> m_movementListeners = new List<MovementListener>();
	private IList<ClickListener> m_clickListeners = new List<ClickListener>();

	private bool IsPointerOverUIObject()
	{
		if (EventSystem.current.IsPointerOverGameObject())
		{
			return true;
		}
		PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
		eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		List<RaycastResult> results = new List<RaycastResult>();
		EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
		return results.Count > 0;
	}
	private void Update()
	{
		if (Input.touchCount < 2 && IsMouseUsed())
		{
			CheckEventsUniversal();
		}
	}
	
	private bool IsMouseUsed()
	{
		return Input.GetMouseButton(0) || Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0);
	}

	private void CheckEvents()
	{
		Touch currentTouch = Input.touches[0];
		TouchPhase currentPhase = currentTouch.phase;

		if (currentPhase == TouchPhase.Began)
		{
			m_startPosition = Input.mousePosition;
			m_moved = false;
			m_uiClick = IsPointerOverUIObject();
		}
		else if (currentPhase == TouchPhase.Ended && !m_moved)
		{
			
		}
		else if (currentPhase == TouchPhase.Moved)
		{
			m_moved = true;
			ThrowMovementEvent(Input.mousePosition);
		}
		m_latestPhase = currentPhase;
	}
	
	private void CheckEventsUniversal()
	{
		if (Input.GetMouseButtonDown(0))
		{
			m_startPosition = Input.mousePosition;
			m_moved = false;
			m_uiClick = IsPointerOverUIObject();
		}
		else if (Input.GetMouseButtonUp(0) && !m_moved)
		{
			ThrowClickEvent();
		}
		else if (Input.mousePosition != m_startPosition)
		{
			m_moved = true;
			ThrowMovementEvent(Input.mousePosition);
			m_startPosition = Input.mousePosition;
		}
	}

	private void ThrowMovementEvent(Vector2 newPosition)
	{
		Vector3 direction = Camera.main.ScreenToWorldPoint(newPosition) - Camera.main.ScreenToWorldPoint(m_startPosition);
    	direction = direction * -1;

		foreach (MovementListener listener in m_movementListeners)
		{
			listener.AcceptMovementEvent(direction);
		}
	}

	private void ThrowClickEvent()
	{
		Transform transform = null;
		Vector3 worldPosition = Camera.main.ScreenToWorldPoint(m_startPosition);
		RaycastHit hit;
		Physics.Raycast(worldPosition, Vector3.forward, out hit, Mathf.Infinity);
		if (hit.collider != null)
		{
			transform = hit.transform;
		}
		foreach (ClickListener listener in m_clickListeners)
		{
			listener.AcceptClickEvent(worldPosition, m_uiClick, transform);
		}
	}

	public void AddMovementListener(MovementListener listener)
	{
		m_movementListeners.Add(listener);
	}

	public void AddClickListener(ClickListener listener)
	{
		m_clickListeners.Add(listener);
	}

	public interface MovementListener
	{
		void AcceptMovementEvent(Vector3 worldOffset);
	}

	public interface ClickListener
	{
		void AcceptClickEvent(Vector3 worldPosition, bool uiClick, Transform hitTransform);
	}
}
