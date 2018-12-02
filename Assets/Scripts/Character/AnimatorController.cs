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
	private bool lastFrameGrounded;

	public Transform CatView;

	public AudioSource AudioSource;
	public SimpleAudioEvent LandAudioEvent;
	public SimpleAudioEvent PurrAudioEvent;

	public void Start()
	{
		myRigidbody = GetComponent<Rigidbody2D>();
		grounded = GetComponent<GroundedComponent>();
		animator = GetComponent<Animator>();
		lastFrameGrounded = true;
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

		if(!lastFrameGrounded && grounded.isGrounded)
		{
			LandAudioEvent.Play(AudioSource);

			if (Random.Range(0f, 1f) < 0.1f)
			{
				PurrAudioEvent.Play(AudioSource);
			}
		}

		lastFrameGrounded = grounded.isGrounded;
	}
}
