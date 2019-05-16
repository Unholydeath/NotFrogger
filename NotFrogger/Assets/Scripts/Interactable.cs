using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
	public delegate void CollisionAction();
	public static CollisionAction OnCollision;

	[SerializeField] bool m_IsHazard = false;

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			if(OnCollision != null)
			{
				OnCollision();
				if (!m_IsHazard) GoalReaction();
			}
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			if (OnCollision != null)
			{
				OnCollision();
				if (!m_IsHazard) GoalReaction();
			}
		}
	}

	void GoalReaction()
	{
		Debug.Log("GOOOOOOALLL!!!!!!!");
	}
}
