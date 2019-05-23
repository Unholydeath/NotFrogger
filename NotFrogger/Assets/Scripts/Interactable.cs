using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
	enum eActionType { GOAL, HAZARD, SAFEZONE }

	public delegate void CollisionAction();
	public static CollisionAction OnGoalCollision;
	public static CollisionAction OnDeathCollision;

	public delegate void CollisionToggle(bool toggle);
	public static CollisionToggle CollisionBlocker;

	[SerializeField] eActionType m_type;

	private bool Subscribed { get { return OnGoalCollision != null && OnDeathCollision != null; } }

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			if(Subscribed)
			{
				HandleAction(collision.gameObject, true);
			}
		}
	}
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			if (Subscribed)
			{
				HandleAction(collision.gameObject, true);
			}
		}
	}

	private void OnTriggerStay(Collider other)
	{	
		if (other.gameObject.CompareTag("Player"))
		{
			if (Subscribed)
			{
				HandleAction(other.gameObject, true);
			}
		}
	}
	private void OnTriggerStay2D(Collider2D collision)
	{	
		if (collision.gameObject.CompareTag("Player"))
		{
			if (Subscribed)
			{
				HandleAction(collision.gameObject, true);
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			if (Subscribed)
			{
				HandleAction(other.gameObject, false);
			}
		}
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			if (Subscribed)
			{
				HandleAction(collision.gameObject, false);
			}
		}
	}

	void HandleAction(GameObject go, bool isEnter)
	{
		switch(m_type)
		{
			case eActionType.SAFEZONE:
				CollisionBlocker(isEnter);
				break;
			case eActionType.GOAL:
				OnGoalCollision();
				gameObject.SetActive(false);
				break;
			case eActionType.HAZARD:
				OnDeathCollision();
				break;
		}
	}
}
