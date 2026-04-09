using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CanShoot))]
public class ShotsWithMissiles : MonoBehaviour
{
	private CanShoot canShoot;
	private CanShoot canLaunch;

	public float missileFireRate = 2f; // bullets per second
	public GameObject projectile;
	public GameObject missile;
	public Vector3 bulletPosition1 = new(.28f, 0, 0);
	public Vector3 bulletPosition3 = new(.1f, 0, 0);
	public Vector3 bulletPosition2 = new(-.1f, 0, 0);
	public Vector3 bulletPosition4 = new(-.28f, 0, 0);
	public Vector3 missilePosition1 = new(-.24f, 0, 0);
	public Vector3 missilePosition2 = new(.24f, 0, 0);

	private void Awake()
	{
		var canShoots = GetComponents<CanShoot>();
		canShoot = canShoots[0];
		canLaunch = canShoots[1];
	}

	private void OnEnable()
	{
		canShoot.OnShooting += Shoot;
		canLaunch.enabled = true;
		canLaunch.OnShooting += Launch;
	}

	private void OnDisable()
	{
		canShoot.OnShooting -= Shoot;
		canLaunch.OnShooting -= Launch;
		canLaunch.enabled = false;
	}

	void Shoot()
	{
		Instantiate(projectile, transform.TransformPoint(bulletPosition1), projectile.transform.rotation);
		Instantiate(projectile, transform.TransformPoint(bulletPosition2), projectile.transform.rotation);
		Instantiate(projectile, transform.TransformPoint(bulletPosition3), projectile.transform.rotation);
		Instantiate(projectile, transform.TransformPoint(bulletPosition4), projectile.transform.rotation);
	}

	void Launch()
	{
		Instantiate(missile, transform.TransformPoint(missilePosition1), projectile.transform.rotation * Quaternion.Euler(0f, 0f, -90f));
		Instantiate(missile, transform.TransformPoint(missilePosition2), projectile.transform.rotation * Quaternion.Euler(0f, 0f, 90f));
	}
}
