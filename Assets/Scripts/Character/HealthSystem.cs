using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
	public int maxHealth = 100;
	public int currentHealth;

	public bool hasArmor;
	public int MaxAmor = 3;
	public int currentArmor;

	public Slider healthBar;

	public Image[] armorBar;

	private Animator animator;

	void Start()
	{
		animator = GetComponent<Animator>();
		currentHealth = maxHealth;
		currentArmor = 0;
		UpdateHealthBar();
	}

	public void TakeDamage(int damage)
	{
		if (hasArmor && currentArmor > 0)
		{
			currentArmor--;
		}
		else
		{
			currentHealth -= damage;
			currentHealth = Mathf.Max(currentHealth, 0);
			UpdateHealthBar();

			if (currentHealth <= 0)
			{
				Die();
			}
		}			
	}

	public bool Heal(int amount)
	{
		if (currentHealth == maxHealth) return false;

		currentHealth += amount;
		currentHealth = Mathf.Min(currentHealth, maxHealth);
		UpdateHealthBar();
		return true;
	}

	public bool HealArmor(int amount)
	{
		if (currentArmor == MaxAmor) return false;
		currentArmor += amount;
		currentArmor = Mathf.Min(currentArmor, MaxAmor);
		UpdateArmorBar();
		return true;
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

	private void UpdateArmorBar()
	{
		if (armorBar != null)
		{
			for (int i = 0; i < armorBar.Length; i++)
			{
				armorBar[i].gameObject.SetActive(i < Mathf.Clamp(currentArmor, 0, armorBar.Length));
			}
		}
	}
}
