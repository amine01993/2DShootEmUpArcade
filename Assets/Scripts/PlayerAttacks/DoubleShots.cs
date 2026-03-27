using UnityEngine;
using UnityEngine.InputSystem;

public class DoubleShots : MonoBehaviour
{
	private TimeTracker fireRateTimeTracker;
	public float fireRate = 5f; // bullets per second
	public GameObject projectile;
	public Vector3 bulletPosition1 = new(.18f, 0, 0);
	public Vector3 bulletPosition2 = new(-.18f, 0, 0);

	InputAction attackAction;

	void Start()
	{
		attackAction = InputSystem.actions.FindAction("Attack");
		fireRateTimeTracker = new TimeTracker(1f / fireRate);
	}

	void Update()
	{
		if (attackAction.phase == InputActionPhase.Performed && fireRateTimeTracker.IsZero())
		{
			Instantiate(projectile, transform.TransformPoint(bulletPosition1), projectile.transform.rotation);
			Instantiate(projectile, transform.TransformPoint(bulletPosition2), projectile.transform.rotation);
			fireRateTimeTracker.Update();
		}
		else if (!fireRateTimeTracker.IsZero())
		{
			fireRateTimeTracker.Update();
		}
	}
}
