using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(GroundedComponent))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(InputComponent))]
public class AnimatorController : MonoBehaviour
{
	private Rigidbody2D myRigidbody;
	private GroundedComponent grounded;
	private Animator animator;

	public SpriteRenderer CatSprite;

	public void Start()
	{
		myRigidbody = GetComponent<Rigidbody2D>();
		grounded = GetComponent<GroundedComponent>();
		animator = GetComponent<Animator>();
	}

	public void Update()
	{
		animator.SetBool("Grounded", grounded.isGrounded);
		animator.SetFloat("HorizontalVelocity", myRigidbody.velocity.x);
		animator.SetFloat("VerticalVelocity", myRigidbody.velocity.y);

		if (myRigidbody.velocity.x < 0)
		{
			CatSprite.flipX = true;
		}
		else if (myRigidbody.velocity.x > 0)
		{
			CatSprite.flipX = false;
		}
	}
}
