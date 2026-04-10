using System;
using UnityEngine;

[RequireComponent(typeof(CanCrash))]
[RequireComponent(typeof(CanTakeDamage))]
public class Shield : MonoBehaviour
{
	public CanTakeDamage canTakeDamage;

	public void AddShield(int sp)
	{
		if (!gameObject.activeSelf)
		{
			gameObject.SetActive(true);
		}

		canTakeDamage.AddHealthPoints(sp);
	}

	public int TakeDamage(int damage)
	{
		var remainingDamage = Math.Max(0, damage - canTakeDamage.healthPoints);

		if (damage >= 0)
		{
			canTakeDamage.TakeDamage(damage);
		}

		return remainingDamage;
	}
}
