using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public Transform Target;
	public Transform LeftLimit;
	public Transform RightLimit;

	public float LerpSpeed = 1f;

	// Update is called once per frame
	void LateUpdate()
    {
		var transientPosition = transform.position;

		var xPosition = Target.transform.position.x;

		if(Target.transform.position.x < LeftLimit.transform.position.x)
		{
			xPosition = LeftLimit.transform.position.x;
		}
		else if (Target.transform.position.x > RightLimit.transform.position.x)
		{
			xPosition = RightLimit.transform.position.x;
		}

		transientPosition.x = Mathf.Lerp(transform.position.x, xPosition, (Mathf.Abs(transform.position.x - xPosition)) * Time.deltaTime * LerpSpeed);

		transform.position = transientPosition;

	}
}
