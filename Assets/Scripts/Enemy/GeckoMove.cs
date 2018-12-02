using UnityEngine;

public class GeckoMove : MonoBehaviour
{
	private Vector2 targetPosition;
	private Vector2 initialPosition;
	private float totalDistance;
	private bool walking;
	private float timeStopped;
	private float randomStopTime;

	public float WalkSpeed;
	public Transform Point1;
	public Transform Point2;
	
	void Start()
    {
		walking = false;
		timeStopped = Time.time;
		randomStopTime = Random.Range(2f, 5f);
	}

	public void Move()
	{
		initialPosition = transform.position;
		targetPosition = Point1.position + ((Point2.position - Point1.position) * Random.Range(0f, 1f));
		totalDistance = Vector2.Distance(initialPosition, targetPosition);
		walking = true;
	}

	public void Update()
	{
		if (!walking)
		{
			if (Time.time - timeStopped > randomStopTime)
			{
				Move();
			}
			return;
		}
		var transientPosition = transform.position;
		var currentDistance = Vector2.Distance(transientPosition, targetPosition);

		if (currentDistance < 0.1f)
		{
			walking = false;
			timeStopped = Time.time;
			randomStopTime = Random.Range(2f, 5f);
			return;
		}

		transientPosition = Vector2.Lerp(initialPosition, targetPosition, (1 - (currentDistance / totalDistance)) + Time.deltaTime * WalkSpeed / totalDistance);

		transform.position = transientPosition;
	}

	public void OnDrawGizmos()
	{
		if (!Point1 || !Point2) return;

		//
		// Debugs the ground check collision
		//
		Gizmos.color = new Color(0f, 0f, 1f, 0.2f);

		Gizmos.DrawLine(Point1.transform.position, Point2.transform.position);
	}
}
