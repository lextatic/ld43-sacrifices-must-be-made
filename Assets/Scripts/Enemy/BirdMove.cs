using UnityEngine;

public class BirdMove : MonoBehaviour
{
	private Vector2 targetPosition;
	private Vector3 initialPosition;
	private float totalDistance;
	private float initialTime;
	private bool viewMoveUp;
	private Vector2 centerPositon;

	public float Radius;
	public float MovementSpeed;
	public Transform View;
	public float viewMoveRange;
	public float viewMoveVelocity;

	// Start is called before the first frame update
	void Start()
    {
		DrawNewPosition();
		viewMoveUp = true;
		centerPositon = transform.position;
	}

	public void DrawNewPosition()
	{
		initialPosition = transform.position;
		targetPosition = centerPositon + (Random.insideUnitCircle * Radius);
		totalDistance = Vector2.Distance(initialPosition, targetPosition);
		initialTime = Time.time;
	}

	public void Update()
	{
		var transientPosition = transform.position;
		var currentDistance = Vector2.Distance(transientPosition, targetPosition);

		if (currentDistance < 0.2f)
		{
			DrawNewPosition();
			return;
		}

		transientPosition = Vector2.Lerp(initialPosition, targetPosition, (1 - (currentDistance / totalDistance)) + Time.deltaTime * MovementSpeed / totalDistance);

		transform.position = transientPosition;

		if (View.transform.localPosition.y > viewMoveRange)
		{
			viewMoveUp = false;
		}
		else if(View.transform.localPosition.y < -viewMoveRange)
		{
			viewMoveUp = true;
		}

		View.transform.Translate(0, Time.deltaTime * viewMoveVelocity * (viewMoveUp ? 1 : -1), 0);
	}

	public void OnDrawGizmos()
	{
		//
		// Debugs the ground check collision
		//
		Gizmos.color = new Color(0f, 0f, 1f, 0.2f);

		if (UnityEditor.EditorApplication.isPlaying)
		{
			Gizmos.DrawSphere(centerPositon, Radius);
		}
		else
		{
			Gizmos.DrawSphere(transform.position, Radius);
		}
	}
}
