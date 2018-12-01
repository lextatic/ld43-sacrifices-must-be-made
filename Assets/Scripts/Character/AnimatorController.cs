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

	public Transform CatView;

	public void Start()
	{
		myRigidbody = GetComponent<Rigidbody2D>();
		grounded = GetComponent<GroundedComponent>();
		animator = GetComponent<Animator>();
	}

	public void Update()
	{
		animator.SetBool("Grounded", grounded.isGrounded);
		animator.SetFloat("HorizontalVelocity", Mathf.Abs(myRigidbody.velocity.x));
		animator.SetFloat("VerticalVelocity", myRigidbody.velocity.y);

		if (myRigidbody.velocity.x < 0)
		{
			CatView.localScale = new Vector3(-1, 1, 1);
		}
		else if (myRigidbody.velocity.x > 0)
		{
			CatView.localScale = Vector3.one;
		}
	}
}
