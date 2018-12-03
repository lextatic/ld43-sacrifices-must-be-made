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
		Jump = Jump || Input.GetButtonDown("Jump");
		// Run = Input.GetKey(KeyCode.LeftControl);
	}
}
