using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	[SerializeField] Transform m_initPoint;
	[SerializeField] Transform m_rePoint;

	[SerializeField] GameObject m_playerRef;

	public void ReMapPoint(Transform newPoint)
	{
		if (newPoint) m_rePoint = newPoint;
	}
}
