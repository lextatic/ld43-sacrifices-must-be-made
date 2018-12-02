using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMouseRun : MonoBehaviour
{
	public MouseMove mouseMovement;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.CompareTag("Player") && mouseMovement != null)
		{
			mouseMovement.RunAway();
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Player") && mouseMovement != null)
		{
			mouseMovement.isSafe = true;
		}
	}
}
