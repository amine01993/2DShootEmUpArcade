using UnityEngine;

[RequireComponent(typeof(CanShoot))]
[RequireComponent(typeof(TrackedOnScreen))]
public class OnlyShootOnScreen : MonoBehaviour
{
	private CanShoot canShoot;

	void Start()
	{
		canShoot = GetComponent<CanShoot>();
	}

	void Update()
	{
		if (OnScreenObjectsTracker.IsTracked(gameObject))
		{
			canShoot.startShooting = true;
		}
		else
		{
			canShoot.startShooting = false;
		}
	}
}
