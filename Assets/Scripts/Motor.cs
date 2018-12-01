using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Input))]
[RequireComponent(typeof(GroundedComponent))]
[RequireComponent(typeof(Animator))]
public class Motor : MonoBehaviour
{
	private InputComponent input;
	private Rigidbody2D myRigidbody;
	private GroundedComponent grounded;
	private Animator animator;

	public float WalkSpeed = 2.5f;
	public float RunSpeed = 4f;
	public float JumpVelocity = 5f;

	public void Start()
	{
		input = GetComponent<InputComponent>();
		myRigidbody = GetComponent<Rigidbody2D>();
		grounded = GetComponent<GroundedComponent>();
		animator = GetComponent<Animator>();
	}

	public void FixedUpdate()
    {
		var velocity = myRigidbody.velocity;

		velocity.x = input.Horizontal * (input.Run ? RunSpeed : WalkSpeed);

		if (input.Jump && grounded.isGrounded)
		{
			input.Jump = false;

			velocity.y += JumpVelocity;
		}

		myRigidbody.velocity = velocity;
	}
}
