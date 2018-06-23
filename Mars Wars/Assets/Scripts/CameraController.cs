using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour, InputController.MovementListener
{
	public Vector2 m_cameraMinPosition;
	public Vector2 m_cameraMaxPosition;
	public GameObject m_inputControllerObj;
	private InputController m_inputController;

	void Start()
	{
		m_inputController = m_inputControllerObj.GetComponent<InputController>();
		m_inputController.AddMovementListener(this);
	}

	public void AcceptMovementEvent(Vector3 worldOffset)
	{
		Vector3 newPos = transform.position + worldOffset;

		transform.position = new Vector3(
			x: Mathf.Clamp(newPos.x, m_cameraMinPosition.x, m_cameraMaxPosition.x),
			y: Mathf.Clamp(newPos.y, m_cameraMinPosition.y, m_cameraMaxPosition.y),
			z: newPos.z
		);
	}
}
