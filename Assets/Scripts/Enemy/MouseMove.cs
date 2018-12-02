using System.Collections;
using UnityEngine;

public class MouseMove : MonoBehaviour
{
	private Vector2 targetPosition;
	private Vector2 initialPosition;
	private Vector2 originalPosition;
	private float totalDistance;
	private bool walking;
	private bool run;
	private float timeArrivedHome;
	private bool atHome;
	private bool safe;

	public float WalkSpeed;
	public float RunSpeed;
	public Transform Mousehole;
	public float WaitTime;

	public bool isSafe
	{
		set
		{
			if(value)
			{
				timeArrivedHome = Time.time;
			}

			safe = value;
		}
	}
	
	void Start()
    {
		walking = false;
		run = false;
		atHome = false;
		originalPosition = transform.position;
	}

	public void RunAway()
	{
		initialPosition = transform.position;
		targetPosition = Mousehole.transform.position;
		totalDistance = Vector2.Distance(initialPosition, targetPosition);
		run = true;
		walking = true;
		safe = false;
	}

	public void GoToOriginalPosition()
	{
		initialPosition = transform.position;
		targetPosition = originalPosition;
		totalDistance = Vector2.Distance(initialPosition, targetPosition);
		run = false;
		walking = true;
	}

	public void Update()
	{
		if(atHome && safe && Time.time - timeArrivedHome > WaitTime)
		{
			GoToOriginalPosition();
		}

		if (!walking) return;
		var transientPosition = transform.position;
		var currentDistance = Vector2.Distance(transientPosition, targetPosition);

		if (currentDistance < 0.01f)
		{
			walking = false;
			atHome = true;
			timeArrivedHome = Time.time;
			return;
		}

		transientPosition = Vector2.Lerp(initialPosition, targetPosition, (1 - (currentDistance / totalDistance)) + Time.deltaTime * (run ? RunSpeed : WalkSpeed) / totalDistance);

		transform.position = transientPosition;
	}

	public void OnDrawGizmos()
	{
		if (!Mousehole) return;

		//
		// Debugs the ground check collision
		//
		Gizmos.color = new Color(0f, 0f, 1f, 0.2f);

		Gizmos.DrawLine(transform.position, Mousehole.transform.position);
	}
}
