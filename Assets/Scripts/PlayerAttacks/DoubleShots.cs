using UnityEngine;

[RequireComponent(typeof(CanShoot))]
public class DoubleShots : MonoBehaviour
{
	private CanShoot canShoot;

	public GameObject projectile;
	public Vector3 bulletPosition1 = new(.18f, 0, 0);
	public Vector3 bulletPosition2 = new(-.18f, 0, 0);

	private void Awake()
	{
		canShoot = GetComponent<CanShoot>();
	}

	private void OnEnable()
	{
		canShoot.OnShooting += Shoot;
	}

	private void OnDisable()
	{
		canShoot.OnShooting -= Shoot;
	}

	void Shoot()
	{
		Instantiate(projectile, transform.TransformPoint(bulletPosition1), projectile.transform.rotation);
		Instantiate(projectile, transform.TransformPoint(bulletPosition2), projectile.transform.rotation);
	}
}
