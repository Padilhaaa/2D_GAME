using UnityEngine;

public class PlayerController: MonoBehaviour
{
	private Rigidbody2D rb;
	private SpriteRenderer spriteRenderer;
	private Animator animator;

	[SerializeField] private float speed;
	[SerializeField] private float jumpForce;
	private Vector2 movement;

	[SerializeField] private Transform groundCheck;
	[SerializeField] private float groundCheckRadius = 0.2f;
	[SerializeField] private LayerMask groundLayer;
	[SerializeField] private bool isGrounded;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		animator = GetComponent<Animator>();
	}

	void Update()
	{
		HandleInput();
		HandleJump();
		UpdateAnimations();
	}

	void FixedUpdate()
	{
		Movement();
		CheckGround();
	}

	public void HandleInput()
	{
		movement.x = InputControl.Current.Gameplay.Movement.ReadValue<Vector2>().x;
		movement.y = InputControl.Current.Gameplay.Movement.ReadValue<Vector2>().y;
	}

	public void Movement()
	{
		rb.linearVelocity = new Vector2(movement.x * speed, rb.linearVelocity.y);
		FlipSprite(movement);
		
	}

	private void FlipSprite(Vector2 direction)
	{
		if (direction.x != 0)
			spriteRenderer.flipX = direction.x < 0;
	}

	private void HandleJump()
	{
		if (InputControl.Current.Gameplay.Jump.WasPerformedThisFrame() && isGrounded)
		{
			rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);			
		}
	}

	private void CheckGround()
	{
		isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
	}

	private void UpdateAnimations()
	{
		animator.SetBool("isGrounded", isGrounded);
		animator.SetFloat("Move", Mathf.Abs(movement.x));
	}
}