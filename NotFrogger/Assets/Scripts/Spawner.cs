using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	[SerializeField] Transform m_initPoint;
	[SerializeField] Transform m_rePoint;

	[SerializeField] int m_lives = 1;

	[SerializeField] GameObject m_playerRef;

	bool m_block = false;

	private void Start()
	{
		Interactable.OnCollision += Respawn;
		Interactable.CollisionBlocker += Block;

		++m_lives;
		Respawn();
	}

	private void OnDestroy()
	{
		Interactable.OnCollision -= Respawn;
		Interactable.CollisionBlocker -= Block;

	}

	void Respawn()
	{
		if (!m_block)
		{
			--m_lives;

			if (m_lives >= 0)
			{
				m_playerRef.transform.position = m_rePoint.position;
				m_playerRef.transform.rotation = m_rePoint.rotation;
			}
		}
	}

	void Block(bool toggle)
	{
		m_block = toggle;
	}

	public void GainLife()
	{
		++m_lives;
	}

	public void GainLives(int amount)
	{
		if (amount > 0) m_lives += amount;
	}

	public void ReMapPoint(Transform newPoint)
	{
		if (newPoint) m_rePoint = newPoint;
	}
}
