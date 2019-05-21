using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	[SerializeField] GameObject m_target;
	[SerializeField] float m_followDistance = 0.0f;

    void Update()
    {
		Vector3 tarPos = m_target.transform.position;
		Vector3 thisPos = transform.position;

		tarPos.z = 0.0f;
		thisPos.z = 0.0f;

		var follow = tarPos - thisPos;

		if (follow.magnitude >= m_followDistance)
		{
			transform.position = Vector3.Lerp(transform.position, transform.position + follow, Time.deltaTime);
		}
    }
}
