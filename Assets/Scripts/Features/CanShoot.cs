using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CanShoot : MonoBehaviour
{
	private TimeTracker fireRateTimeTracker;
	private PlayerShip player;
	private InputAction attackAction;
	private bool IsPlayer { get { return player != null; } }

	public event Action OnShooting;

	public float fireRate = 4f;
	public bool startShooting = false;

	void Start()
	{
		player = GetComponent<PlayerShip>();
		if (IsPlayer)
		{
			attackAction = InputSystem.actions.FindAction("Attack");
		}
		fireRateTimeTracker = new TimeTracker(1f / fireRate);
	}

	void Update()
	{
		if (MainController.Instance.uiManager.IsPaused()) return;

		if (startShooting && fireRateTimeTracker.IsZero())
		{
			if (!IsPlayer || attackAction.phase == InputActionPhase.Performed)
			{
				OnShooting.Invoke();
				MainController.Instance.soundManager.PlaySound("laserShoot");
				fireRateTimeTracker.Update();
			}
		}
		else if (!fireRateTimeTracker.IsZero())
		{
			fireRateTimeTracker.Update();
		}
	}

}