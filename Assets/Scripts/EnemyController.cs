using UnityEngine;

public class EnemyController : MonoBehaviour
{
	private Rigidbody2D rb;
	private Animator animator;
	private SpriteRenderer spriteRenderer;

	[SerializeField] private float walkSpeed = 2f;
	[SerializeField] private float runSpeed = 4f;
	private Transform player;

	[SerializeField] private float detectionRange = 5f;
	[SerializeField] private float attackRange = 1f;
	[SerializeField] private float attackCooldown = 1f;
	private float nextAttackTime;

	private bool isRunning;
	private bool isAttacking;
	private bool isDead;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}

	void Update()
	{
		if (isDead || player == null) return;

		float distanceToPlayer = Vector2.Distance(transform.position, player.position);

		if (distanceToPlayer <= attackRange && Time.time >= nextAttackTime)
		{
			Attack();
		}
		else if (distanceToPlayer <= detectionRange)
		{
			ChasePlayer();
		}
		else
		{
			Patrol();
		}

		UpdateAnimations();
	}

	private void Patrol()
	{
		isRunning = false;
		rb.linearVelocity = new Vector2(walkSpeed, rb.linearVelocity.y);
		FlipSprite(walkSpeed);
	}

	private void ChasePlayer()
	{
		isRunning = true;
		float direction = player.position.x > transform.position.x ? 1f : -1f;
		float distanceToPlayer = Vector2.Distance(transform.position, player.position);

		if (distanceToPlayer > 0.25f)
		rb.linearVelocity = new Vector2(direction * runSpeed, rb.linearVelocity.y);
		FlipSprite(direction);
	}

	private void Attack()
	{
		isAttacking = true;
		animator.SetTrigger("Attack");
		nextAttackTime = Time.time + attackCooldown;
		PerformAttack();
	}

	private void PerformAttack()
	{
		Collider2D playerHit = Physics2D.OverlapCircle(transform.position, attackRange, LayerMask.GetMask("Player"));
		if (playerHit)
		{
			playerHit.GetComponent<HealthSystem>().TakeDamage(10);
		}
	}

	private void FlipSprite(float direction)
	{
		if (direction != 0)
			spriteRenderer.flipX = direction > 0;
	}

	private void UpdateAnimations()
	{
		animator.SetFloat("Move", Mathf.Abs(rb.linearVelocity.x));
	}

	public void Death()
	{
		isDead = true;
		rb.linearVelocity = Vector2.zero;
	}

	//Called in the Death animation event
	private void DestroyEnemy()
	{
		//Spawn loot later
		Destroy(gameObject);
	}
}
