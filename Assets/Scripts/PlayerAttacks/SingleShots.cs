using UnityEngine;

[RequireComponent(typeof(CanShoot))]
public class SingleShots : MonoBehaviour
{
	private CanShoot canShoot;

	public GameObject projectile;

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
		Instantiate(projectile, transform.position, projectile.transform.rotation);
	}
}
