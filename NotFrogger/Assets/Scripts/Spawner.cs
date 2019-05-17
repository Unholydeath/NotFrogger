using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	[SerializeField] Transform m_initPoint;
	[SerializeField] Transform m_rePoint;

	[SerializeField] int m_lives = 1;

	[SerializeField] GameObject m_playerRef;

	private void Start()
	{
		Interactable.OnCollision += Respawn;

		++m_lives;
		Respawn();
	}

	private void OnDestroy()
	{
		Interactable.OnCollision -= Respawn;
	}

	void Respawn()
	{
		--m_lives;

		if (m_lives >= 0)
		{
			m_playerRef.transform.position = m_rePoint.position;
			m_playerRef.transform.rotation = m_rePoint.rotation;
		}
	}

	public void GainLife()
	{
		++m_lives;
	}

	public void GainLives()
	{

	}

	public void ReMapPoint(Transform newPoint)
	{
		if (newPoint) m_rePoint = newPoint;
	}
}
