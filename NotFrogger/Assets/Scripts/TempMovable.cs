using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempMovable : MonoBehaviour
{
	[SerializeField] bool m_isPlayer = false;
	[SerializeField] bool m_carry = false;
	[SerializeField] float m_speed = 1.0f;

	GameObject m_child = null;
	bool m_isPaused = false;

	private void Start()
	{
		GameManager.OnPause += Pause;
	}

	private void OnDestroy()
	{
		GameManager.OnPause -= Pause;
	}

	void Update()
    {
		if (!m_isPaused)
		{
			Vector3 velocity = Vector3.zero;
			if (m_isPlayer)
			{
				if (Input.GetKey(KeyCode.W)) velocity.y += m_speed;
				if (Input.GetKey(KeyCode.S)) velocity.y -= m_speed;

				if (Input.GetKey(KeyCode.D)) velocity.x += m_speed;
				if (Input.GetKey(KeyCode.A)) velocity.x -= m_speed;
			}
			else
			{
				velocity += transform.right * Time.deltaTime * m_speed;
			}

			transform.position = transform.position + velocity;
			if (m_child)
			{
				m_child.transform.position = m_child.transform.position + velocity;
			}
		}
	}

	void Pause()
	{
		m_isPaused = !m_isPaused;
	}

	private void OnTriggerEnter(Collider other)
	{
		if(m_carry && other.gameObject.CompareTag("Player"))
		{
			m_child = other.gameObject;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (m_carry && collision.gameObject.CompareTag("Player"))
		{
			m_child = collision.gameObject;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (m_carry && other.gameObject.CompareTag("Player"))
		{
			m_child = null;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (m_carry && collision.gameObject.CompareTag("Player"))
		{
			m_child = null;
		}
	}
}
