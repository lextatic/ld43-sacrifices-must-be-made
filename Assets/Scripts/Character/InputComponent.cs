using UnityEngine;

public class InputComponent : MonoBehaviour
{
	[HideInInspector]
	public float Horizontal;

	public bool Jump;

	[HideInInspector]
	public bool Run;

	public void Update()
	{
		Horizontal = Input.GetAxisRaw("Horizontal");
		Jump = Jump || Input.GetKeyDown(KeyCode.Space);
		Run = Input.GetKey(KeyCode.LeftControl);
	}
}
