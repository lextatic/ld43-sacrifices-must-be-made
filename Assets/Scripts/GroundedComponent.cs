using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedComponent : MonoBehaviour
{
	[HideInInspector]
	public bool isGrounded;

	public Vector2 CenterPosion;
	public Vector2 BoxSize;
    
    void FixedUpdate()
    {
		isGrounded = false;

		//
		// Check for ground collisions and set variables acordingly
		//
		Collider2D[] colliders = Physics2D.OverlapBoxAll((Vector2)transform.position + CenterPosion, BoxSize, 0f, LayerMask.NameToLayer("Floor"));

		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				isGrounded = true;
			}
		}
	}

	public void OnDrawGizmos()
	{
		//
		// Debugs the ground check collision
		//
		if (isGrounded)
		{
			Gizmos.color = new Color(0f, 1f, 0f, 0.2f);
		}
		else
		{
			Gizmos.color = new Color(1f, 0f, 0f, 0.2f);
		}

		Gizmos.DrawCube((Vector2)transform.position + CenterPosion, BoxSize);
	}
}
