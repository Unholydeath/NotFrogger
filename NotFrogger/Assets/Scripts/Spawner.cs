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

	public int Lives { get { return m_lives; } }
	public int testing { get; set; }

	private void Start()
	{
		Interactable.OnDeathCollision += Death;
		Interactable.OnGoalCollision += Respawn;
		Interactable.CollisionBlocker += Block;

		++m_lives;
		Respawn();
	}

	private void OnDestroy()
	{
		Interactable.OnDeathCollision -= Death;
		Interactable.OnGoalCollision -= Respawn;
		Interactable.CollisionBlocker -= Block;
	}

	void Death()
	{
		if (!m_block)
		{
			--m_lives;
			Respawn();
		}
	}

	void Respawn()
	{
		if (m_lives >= 0)
		{
			m_playerRef.transform.position = m_rePoint.position;
			m_playerRef.transform.rotation = m_rePoint.rotation;
		}
		else
		{
			GameOver();
		}
	}

	void GameOver()
	{
		Time.timeScale = 0.0f;
		Debug.Log("GG");
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
