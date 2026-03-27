using System;
using UnityEngine;

[RequireComponent(typeof(CanTakeDamage))]
public class DropCollectibleOnDeath : MonoBehaviour
{
	private CanTakeDamage canTakeDamage;

	public static event Action<Vector3> OnDeath;

	void Start()
    {
		canTakeDamage = GetComponent<CanTakeDamage>();
	}

	private void OnDestroy()
	{
		if (canTakeDamage.healthPoints <= 0)
		{
			OnDeath.Invoke(transform.position);
		}
	}
}
