using UnityEngine;

[RequireComponent(typeof(CanShoot))]
public class QuadrupleShots : MonoBehaviour
{
	private CanShoot canShoot;

	public GameObject projectile;
	public Vector3 bulletPosition1 = new(.28f, 0, 0);
	public Vector3 bulletPosition3 = new(.1f, 0, 0);
	public Vector3 bulletPosition2 = new(-.1f, 0, 0);
	public Vector3 bulletPosition4 = new(-.28f, 0, 0);

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
		Instantiate(projectile, transform.TransformPoint(bulletPosition3), projectile.transform.rotation);
		Instantiate(projectile, transform.TransformPoint(bulletPosition4), projectile.transform.rotation);
	}
}
