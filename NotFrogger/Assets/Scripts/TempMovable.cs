using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempMovable : MonoBehaviour
{
	[SerializeField] bool m_isPlayer = false;
	[SerializeField] float m_speed = 1.0f;
	[SerializeField] 

    void Update()
    {
		Vector3 velocity = transform.position;
        if(m_isPlayer)
		{
			var up = Input.GetAxis("Vertical");
			var left = Input.GetAxis("Horizontal");

			if (up != 0.0f && Input.anyKeyDown) velocity += transform.up * m_speed * up;
			if (left != 0.0f && Input.anyKeyDown) velocity += transform.right * m_speed * left;
		}
		else
		{
			velocity += transform.right * Time.deltaTime * m_speed;
		}

		transform.position = velocity;
    }
}
