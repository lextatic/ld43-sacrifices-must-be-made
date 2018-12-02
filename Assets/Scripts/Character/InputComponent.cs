using UnityEngine;

public class InputComponent : MonoBehaviour
{
	[HideInInspector]
	public float Horizontal;

	[HideInInspector]
	public bool Jump;

	[HideInInspector]
	public bool Run = true;

	public void Start()
	{
		Run = true;
	}

	public void Update()
	{
		Horizontal = Input.GetAxisRaw("Horizontal");
		Jump = Jump || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow);
		// Run = Input.GetKey(KeyCode.LeftControl);
	}
}
