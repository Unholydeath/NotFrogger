using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
	public delegate void CollisionAction();
	public static CollisionAction OnGoalCollision;
	public static CollisionAction OnDeathCollision;

	public delegate void CollisionToggle(bool toggle);
	public static CollisionToggle CollisionBlocker;

	public delegate void CarryAlert(GameObject obj, bool toggle);
	public static CarryAlert OnCarry;

	public delegate void TriggerAlert(bool toggle);
	public static TriggerAlert OnZoneEnter;

	enum eActionType { GOAL, HAZARD, FLOAT, SAFE }

	[SerializeField] eActionType m_action;

	bool m_triggerOn = false;
	bool m_triggerIn = false;

	bool isPlayer(GameObject obj)
	{
		return obj.CompareTag("Player");
	}

	//For Hazards and Goals
	private void OnCollisionEnter(Collision collision)
	{
		if (isPlayer(collision.gameObject)) HandleAction();
	}
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (isPlayer(collision.gameObject)) HandleAction();
	}
	//--

	//For Safes
	private void OnTriggerEnter(Collider other)
	{
		if (isPlayer(other.gameObject))
		{
			m_triggerOn = true;
			HandleAction(other.gameObject);
		}
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (isPlayer(collision.gameObject))
		{
			m_triggerOn = true;
			HandleAction(collision.gameObject);
		}
	}
	//--

	//For Floats
	private void OnTriggerStay(Collider other)
	{
		if (isPlayer(other.gameObject))
		{
			m_triggerIn = true;
			HandleAction();
		}
	}
	private void OnTriggerStay2D(Collider2D collision)
	{
		if (isPlayer(collision.gameObject))
		{
			m_triggerIn = true;
			HandleAction();
		}
	}
	//--

	//For Safes and Floats
	private void OnTriggerExit(Collider other)
	{
		if (isPlayer(other.gameObject))
		{
			m_triggerOn = false;
			m_triggerIn = false;
			HandleAction(other.gameObject);
		}
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (isPlayer(collision.gameObject))
		{
			m_triggerOn = false;
			m_triggerIn = false;
			HandleAction(collision.gameObject);
		}
	}
	//--

	void HandleAction(GameObject obj = null)
	{
		switch(m_action)
		{
			case eActionType.GOAL:
				if (OnGoalCollision != null) OnGoalCollision();
				gameObject.SetActive(false);
				break;
			case eActionType.HAZARD:
				if (OnDeathCollision != null) OnDeathCollision();
				break;
			case eActionType.SAFE:
				if (CollisionBlocker != null) CollisionBlocker(m_triggerOn);
				if (OnCarry != null) OnCarry(obj, m_triggerOn);
				break;
			case eActionType.FLOAT:
				if (OnZoneEnter != null) OnZoneEnter(m_triggerIn);
				break;
		}
	}
}
