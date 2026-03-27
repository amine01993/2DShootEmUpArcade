using UnityEngine;
using UnityEngine.InputSystem;

public class SingleShots : MonoBehaviour
{
	private TimeTracker fireRateTimeTracker;
	public float fireRate = 5f; // bullets per second
	public GameObject projectile;

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
			Instantiate(projectile, transform.position, projectile.transform.rotation);
			fireRateTimeTracker.Update();
		}
		else if (!fireRateTimeTracker.IsZero())
		{
			fireRateTimeTracker.Update();
		}
	}
}
