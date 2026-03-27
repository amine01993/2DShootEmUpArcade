using System;
using UnityEngine;

public class CanShoot : MonoBehaviour
{
	private TimeTracker fireRateTimeTracker;

	public event Action OnShooting;

	public float fireRate = 4f;
	public bool startShooting = false;

	void Start()
	{
		fireRateTimeTracker = new TimeTracker(1f / fireRate);
	}

	void Update()
	{
		if (startShooting && fireRateTimeTracker.IsZero())
		{
			OnShooting.Invoke();
			fireRateTimeTracker.Update();
		}
		else if (!fireRateTimeTracker.IsZero())
		{
			fireRateTimeTracker.Update();
		}
	}

}