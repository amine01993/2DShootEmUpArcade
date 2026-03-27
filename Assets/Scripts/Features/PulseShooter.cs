using UnityEngine;

[RequireComponent(typeof(CanShoot))]
public class PulseShooter : MonoBehaviour
{
	private CanShoot canShoot;

	public int bulletsPerPulse = 8;
	public GameObject projectile;
	public Vector3[] initialBulletPositions;

	void Start()
	{
		canShoot = GetComponent<CanShoot>();
		canShoot.OnShooting += Shoot;
	}

	public void Shoot()
	{
		for (var i = 0; i < bulletsPerPulse; ++i)
		{
			Instantiate(
				projectile,
				transform.TransformPoint(initialBulletPositions[i % initialBulletPositions.Length]),
				projectile.transform.rotation * Quaternion.Euler(0, 0, i * 360f / bulletsPerPulse)
			);
		}
	}
}
