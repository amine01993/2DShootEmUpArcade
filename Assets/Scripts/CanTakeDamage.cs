using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CanTakeDamage : MonoBehaviour
{
	private TimeTracker hpAnimTimeTracker;
	private float oldPercentage;
	private PlayerShip player;
	private Shield shield;
	private Animator animator;
	public static event Func<int> OnPlayerDeath;

	public int maxHealthPoints = 100;
	public int healthPoints;
	public GameObject healthBarUi;
	public float healthBarTransitionTime = .2f;

	private void OnEnable()
	{
		player = GetComponent<PlayerShip>();
		shield = GetComponent<Shield>();
		animator = GetComponent<Animator>();

		if (healthBarUi != null)
		{
			healthBarUi.SetActive(true);
			hpAnimTimeTracker = new TimeTracker(healthBarTransitionTime, false);
			oldPercentage = healthBarUi.transform.localScale.x;
		}
	}

	void Update()
	{
		if (healthPoints <= 0)
		{
			if (player != null)
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
			else if (shield != null)
			{
				gameObject.SetActive(false);
				healthBarUi.SetActive(false);
			}
			else
			{
				Destroy(gameObject);
			}
		}

		if (healthBarUi != null)
		{
			UpdateHealthBar();
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
				hpAnimTimeTracker.Reset();
				oldPercentage = healthBarUi.transform.localScale.x;
			}

			if (animator != null)
			{
				animator.Play("HitFlash");
			}
		}
	}

	public void AddHealthPoints(int hp)
	{
		healthPoints += hp;
		healthPoints = Math.Min(maxHealthPoints, healthPoints);

		if (healthBarUi != null)
		{
			hpAnimTimeTracker.Reset();
			oldPercentage = healthBarUi.transform.localScale.x;
		}

		if (animator != null)
		{
			if (player != null)
				animator.Play("OnHealth");
		}
	}

	private void UpdateHealthBar()
	{
		if (!hpAnimTimeTracker.IsExpired())
		{
			float percentage = (float)healthPoints / maxHealthPoints;
			healthBarUi.transform.localScale = Vector3.Lerp(
				new Vector3(oldPercentage, 1f, 1f),
				new Vector3(percentage, 1f, 1f),
				hpAnimTimeTracker.GetRatio()
			);
			hpAnimTimeTracker.Update();
		}
	}
}
