using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatManager : MonoBehaviour
{
	public delegate void FloatAction();
	public static FloatAction OnDrown;

	static bool m_isOn = false;
	static bool m_isIn = false;
	static bool m_iFrame = false;

	void FixedUpdate()
    {
		if (m_isIn && !m_isOn)
		{
			if (!m_iFrame && OnDrown != null)
			{
				Debug.Log("On: " + m_isOn + ", In: " + m_isIn + ", Frame: " + m_iFrame);
				OnDrown();
			}
		}

		if (m_iFrame) m_iFrame = false;
	}

	public static void PlaceOn()
	{
		m_isOn = true;
	}

	public static void PlaceIn()
	{
		m_isIn = true;
	}

	public static void TakeOff()
	{
		m_iFrame = true;
		m_isOn = false;
	}

	public static void TakeOut()
	{
		m_iFrame = true;
		m_isIn = false;
	}
}
