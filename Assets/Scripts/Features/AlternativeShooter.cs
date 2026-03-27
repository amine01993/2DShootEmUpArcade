using UnityEngine;

[RequireComponent(typeof(CanShoot))]
public class AlternativeShooter : MonoBehaviour
{
	private int bulletsFired = 0;
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
		Instantiate(
			projectile,
			transform.TransformPoint(initialBulletPositions[bulletsFired % initialBulletPositions.Length]),
			transform.rotation * Quaternion.Euler(0f, 0f, 180f)
		);

		++bulletsFired;
	}
}
