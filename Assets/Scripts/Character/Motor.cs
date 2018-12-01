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
	public float TimeToPeak = 0.35f;
	public float JumpHeight = 5f;

	private float _jumpVelocity = 5f;

	

	public void Start()
	{
		input = GetComponent<InputComponent>();
		myRigidbody = GetComponent<Rigidbody2D>();
		grounded = GetComponent<GroundedComponent>();
		animator = GetComponent<Animator>();

		//
		// Defines the jumpVecolity and gravity to achieve the desired jump height and time spent on air.
		//
		_jumpVelocity = (2f * JumpHeight) / TimeToPeak;
		var gravity = -(2f * JumpHeight) / (TimeToPeak * TimeToPeak);
		myRigidbody.gravityScale = (gravity) / Physics2D.gravity.y;
	}

	public void FixedUpdate()
    {
		var velocity = myRigidbody.velocity;

		velocity.x = input.Horizontal * (input.Run ? RunSpeed : WalkSpeed);

		if (input.Jump)
		{
			input.Jump = false;

			if (grounded.isGrounded)
			{
				velocity.y = _jumpVelocity;
			}
		}

		myRigidbody.velocity = velocity;
	}
}
