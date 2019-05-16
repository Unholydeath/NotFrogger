using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	[SerializeField] Transform m_initPoint;
	[SerializeField] Transform m_rePoint;

	[SerializeField] GameObject m_playerRef;

	private void Start()
	{
		Interactable.OnCollision += Respawn;

		m_playerRef.transform.position = m_initPoint.position;
		m_playerRef.transform.rotation = m_initPoint.rotation;
	}

	private void OnDestroy()
	{
		Interactable.OnCollision -= Respawn;
	}

	void Respawn()
	{
		m_playerRef.transform.position = m_rePoint.position;
		m_playerRef.transform.rotation = m_rePoint.rotation;
	}

	public void ReMapPoint(Transform newPoint)
	{
		if (newPoint) m_rePoint = newPoint;
	}
}
