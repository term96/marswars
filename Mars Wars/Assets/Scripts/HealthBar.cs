using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
	public float m_verticalOffset;
	public int m_width;
	public int m_height;
	public Texture2D m_texture;

	Unit m_parent;
	Vector2 m_position;
	public void SetParent(Unit parent)
	{
		m_parent = parent;
	}
	void Start()
	{
		m_position = Vector3.zero;
	}
	void Update()
	{

	}

	void LateUpdate()
	{

	}

	void OnGUI()
	{
		if (m_parent == null)
		{
			return;
		}
		int width = (int) (m_width * m_parent.GetHealth() / 100f);
		Vector3 vec3 = Camera.main.WorldToScreenPoint(m_parent.GetTransform().position);
		m_position.x = vec3.x - width / 2;
		m_position.y = Screen.height - vec3.y - m_verticalOffset;
		GUI.Box(new Rect(m_position.x, m_position.y, width, m_height), m_texture);
	}

	public interface Unit
	{
		float GetHealth();
		void DecreaseHealth(float damage);
		Transform GetTransform();
	}
}
