using UnityEngine;

public class FlipView : MonoBehaviour
{
	private Vector2 previousPosition;

	public SpriteRenderer SpriteRenderer;

	private void Start()
	{
		previousPosition = transform.position;
	}

	private void Update()
    {
		var move = (Vector2)transform.position - previousPosition;
		previousPosition = transform.position;

		if (Vector2.Dot(move, transform.right) > 0)
		{
			SpriteRenderer.flipX = false;
		}
		else if (Vector2.Dot(move, transform.right) < 0)
		{
			SpriteRenderer.flipX = true;
		}
	}
}
