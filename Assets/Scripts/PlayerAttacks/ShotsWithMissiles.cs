using UnityEngine;
using UnityEngine.InputSystem;

public class ShotsWithMissiles : MonoBehaviour
{
	private TimeTracker fireRateTimeTracker;
	private TimeTracker missileFireRateTimeTracker;
	public float fireRate = 5f; // bullets per second
	public float missileFireRate = 2f; // bullets per second
	public GameObject projectile;
	public GameObject missile;
	public Vector3 bulletPosition1 = new(.28f, 0, 0);
	public Vector3 bulletPosition3 = new(.1f, 0, 0);
	public Vector3 bulletPosition2 = new(-.1f, 0, 0);
	public Vector3 bulletPosition4 = new(-.28f, 0, 0);
	public Vector3 missilePosition1 = new(-.24f, 0, 0);
	public Vector3 missilePosition2 = new(.24f, 0, 0);

	InputAction attackAction;

	void Start()
	{
		attackAction = InputSystem.actions.FindAction("Attack");
		fireRateTimeTracker = new TimeTracker(1f / fireRate);
		missileFireRateTimeTracker = new TimeTracker(1f / missileFireRate);
	}

	void Update()
	{
		if (attackAction.phase == InputActionPhase.Performed && fireRateTimeTracker.IsZero())
		{
			Instantiate(projectile, transform.TransformPoint(bulletPosition1), projectile.transform.rotation);
			Instantiate(projectile, transform.TransformPoint(bulletPosition2), projectile.transform.rotation);
			Instantiate(projectile, transform.TransformPoint(bulletPosition3), projectile.transform.rotation);
			Instantiate(projectile, transform.TransformPoint(bulletPosition4), projectile.transform.rotation);
			fireRateTimeTracker.Update();
		}
		else if (!fireRateTimeTracker.IsZero())
		{
			fireRateTimeTracker.Update();
		}

		if (attackAction.phase == InputActionPhase.Performed && missileFireRateTimeTracker.IsZero())
		{
			Instantiate(missile, transform.TransformPoint(missilePosition1), projectile.transform.rotation * Quaternion.Euler(0f, 0f, -90f));
			Instantiate(missile, transform.TransformPoint(missilePosition2), projectile.transform.rotation * Quaternion.Euler(0f, 0f, 90f));
			missileFireRateTimeTracker.Update();
		}
		else if (!missileFireRateTimeTracker.IsZero())
		{
			missileFireRateTimeTracker.Update();
		}
	}
}
