using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liquids : MonoBehaviour
{
	[SerializeField] bool m_isFloat = false;

	bool isPlayer(GameObject obj)
	{
		return obj.CompareTag("Player");
	}

	private void OnTriggerEnter(Collider other)
	{
		if (isPlayer(other.gameObject))
		{
			if (m_isFloat) FloatManager.PlaceOn();
			else FloatManager.PlaceIn();

		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (isPlayer(collision.gameObject))
		{
			if (m_isFloat) FloatManager.PlaceOn();
			else FloatManager.PlaceIn();

		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (isPlayer(other.gameObject))
		{
			if (m_isFloat)	FloatManager.TakeOff();
			else FloatManager.TakeOut();
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (isPlayer(collision.gameObject))
		{
			if (m_isFloat)	FloatManager.TakeOff();
			else FloatManager.TakeOut();
		}
	}
}
