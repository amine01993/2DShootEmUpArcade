using UnityEngine;

[RequireComponent (typeof(CanShoot))]
public class StraightShooter : MonoBehaviour
{
	private CanShoot canShoot;

	public GameObject projectile;
	public Vector3[] initialBulletPositions;

	void Start()
    {
		canShoot = GetComponent<CanShoot>();
		canShoot.OnShooting += Shoot;
	}

	public void Shoot()
	{
		for (int i = 0; i < initialBulletPositions.Length; ++i)
		{
			Instantiate(
				projectile, 
				transform.TransformPoint(initialBulletPositions[i]), 
				projectile.transform.rotation
			);
		}
	}

}
