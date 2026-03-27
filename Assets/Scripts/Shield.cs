using System;
using UnityEngine;

[RequireComponent(typeof(CanCrash))]
[RequireComponent(typeof(CanTakeDamage))]
public class Shield : MonoBehaviour
{
	public CanTakeDamage canTakeDamage;

	public void AddShield(int sp)
	{
		canTakeDamage.AddHealthPoints(sp);

		if (!gameObject.activeSelf)
		{
			gameObject.SetActive(true);
		}
	}

	public int TakeDamage(int damage)
	{
		var remainingDamage = Math.Max(0, damage - canTakeDamage.healthPoints);

		if (damage >= 0)
		{
			canTakeDamage.TakeDamage(damage);

			if (canTakeDamage.healthPoints <= 0)
			{
				gameObject.SetActive(false);
			}
		}

		return remainingDamage;
	}
}
