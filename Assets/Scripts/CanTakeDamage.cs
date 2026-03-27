using System;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CanTakeDamage : MonoBehaviour
{
	private PlayerShip player;
	private Shield shield;
	public static event Func<int> OnPlayerDeath;

	public int maxHealthPoints = 100;
	public int healthPoints;
	public GameObject healthBarUi;

	void Start()
	{
		player = GetComponent<PlayerShip>();
		shield = GetComponent<Shield>();
	}

	void Update()
	{
		if (healthPoints <= 0)
		{
			if (player == null)
			{
				Destroy(gameObject);
			}
			else
			{
				var remainingLifes = OnPlayerDeath.Invoke();
				if (remainingLifes > -1)
				{
					player.transform.position = player.initialPosition;
					healthPoints = maxHealthPoints;
				}
				else
				{
					Destroy(gameObject);
				}
			}
		}
	}

	public void TakeDamage(int damage)
	{
		if (damage >= 0)
		{
			healthPoints -= damage;
			healthPoints = Math.Max(0, healthPoints);
			if (healthBarUi != null)
			{
				UpdateHealthBar();
			}
		}
	}

	public void AddHealthPoints(int hp)
	{
		healthPoints += hp;
		healthPoints = Math.Min(maxHealthPoints, healthPoints);
		if (healthBarUi != null)
		{
			UpdateHealthBar();
		}
	}

	private void UpdateHealthBar()
	{
		float percentage = (float)healthPoints / maxHealthPoints;
		healthBarUi.transform.localScale = new Vector3(percentage, 1f, 1f);
	}
}
