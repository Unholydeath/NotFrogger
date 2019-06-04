using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
	[SerializeField] GameObject m_objOfFocus;
	[SerializeField] Transform m_spawnPoint;
	[SerializeField] int m_lives = 3;
	[SerializeField] Text Lives;

	bool m_isBlocked = false;
	List<bool> m_blocks;

	public int LivesLeft { get; set; }

	private void Start()
	{
		m_blocks = new List<bool>();

        Lives.text = m_lives.ToString();

		Interactable.OnGoalCollision += Respawn;
		Interactable.CollisionBlocker += BlockDeath;
		Interactable.OnDeathCollision += Death;
		FloatManager.OnDrown += Death;
	}

	private void OnDestroy()
	{
		Interactable.OnGoalCollision -= Respawn;
		Interactable.CollisionBlocker -= BlockDeath;
		Interactable.OnDeathCollision -= Death;
		FloatManager.OnDrown -= Death;
	}

	private void Update()
	{
		Lives.text = "" + m_lives.ToString();
	}

	void Respawn()
	{
		if(m_lives > 0)
		{
			m_objOfFocus.transform.position = m_spawnPoint.position;
			m_objOfFocus.transform.rotation = m_spawnPoint.rotation;
		}
		else
		{
			GameManager.GameOver();
		}
	}

	void Death()
	{
		if(!m_isBlocked)
		{
			--m_lives;
			Respawn();
		}
	}

	void BlockDeath(bool entered)
	{
		m_blocks.Add(entered);

		int up = 0;
		foreach(var b in m_blocks)
		{
			if (b) ++up;
		}

		m_isBlocked = up > m_blocks.Count / 2;
	}
}
