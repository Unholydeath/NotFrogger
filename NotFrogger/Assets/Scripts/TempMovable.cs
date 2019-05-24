using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempMovable : MonoBehaviour
{
	[SerializeField] bool m_isPlayer = false;
	[SerializeField] bool m_carry = false;
	[SerializeField] float m_speed = 1.0f;

	private void Start()
	{
		Interactable.CollisionBlocker += CarryOrNot;
	}

	private void OnDestroy()
	{
		Interactable.CollisionBlocker -= CarryOrNot;
	}

	void Update()
    {
		Vector3 velocity = transform.position;
        if(m_isPlayer)
		{
			if (Input.GetKeyDown(KeyCode.W)) velocity.y += m_speed;
			if (Input.GetKeyDown(KeyCode.S)) velocity.y -= m_speed;

			if (Input.GetKeyDown(KeyCode.D)) velocity.x += m_speed;
			if (Input.GetKeyDown(KeyCode.A)) velocity.x -= m_speed;
		}
		else
		{
			velocity += transform.right * Time.deltaTime * m_speed;
		}

		transform.position = velocity;
    }

	void CarryOrNot(GameObject obj, bool enter)
	{
		if (m_carry)
		{
			if (enter)
			{
				obj.transform.parent = transform;
			}
			else
			{
				obj.transform.parent = null;
			}
		}
	}
}
