using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
	enum eStyle { WARP, CTURN, CCTURN, FLIP }

	[SerializeField] eStyle m_style;
	[SerializeField] List<string> m_acceptedLayers;
	[SerializeField] Transform m_setPoint;

	private void OnTriggerEnter(Collider other)
	{
		var colltag = other.gameObject.tag;
		if (m_acceptedLayers.Contains(colltag)) HandleAction(other.gameObject);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		var colltag = collision.gameObject.tag;
		if (m_acceptedLayers.Contains(colltag) || m_acceptedLayers.Contains("")) HandleAction(collision.gameObject);
	}

	void HandleAction(GameObject go)
	{
		switch(m_style)
		{
			case eStyle.WARP:
				go.transform.position = m_setPoint.position;
				go.transform.rotation = m_setPoint.rotation;
				break;
			case eStyle.CTURN:
				go.transform.Rotate(0.0f, 0.0f, -90.0f);
				break;
			case eStyle.CCTURN:
				go.transform.Rotate(0.0f, 0.0f, 90.0f);
				break;
			case eStyle.FLIP:
				go.transform.Rotate(0.0f, 0.0f, 180.0f);
				break;
		}
	}
}
