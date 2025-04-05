using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
	public int maxHealth = 100;
	private int currentHealth;

	public Slider healthBar;

	private Animator animator;

	void Start()
	{
		animator = GetComponent<Animator>();
		currentHealth = maxHealth;
		UpdateHealthBar();
	}

	public void TakeDamage(int damage)
	{
		currentHealth -= damage;
		currentHealth = Mathf.Max(currentHealth, 0);
		UpdateHealthBar();

		if (currentHealth <= 0)
		{
			Die();
		}
	}

	public void Heal(int amount)
	{
		currentHealth += amount;
		currentHealth = Mathf.Min(currentHealth, maxHealth);
		UpdateHealthBar();
	}

	private void Die()
	{
		Debug.Log(gameObject.name + " has died!");
		animator.SetTrigger("Death");
	}

	private void UpdateHealthBar()
	{
		if (healthBar != null)
		{
			healthBar.value = (float)currentHealth / maxHealth;
		}
	}
}
