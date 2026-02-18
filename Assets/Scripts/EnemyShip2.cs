using Unity.VisualScripting;
using UnityEngine;

public class EnemyShip2 : MonoBehaviour
{
	private float fireRateTimeTracker = 0f;
	private float collisionCooldownTimeTracker = 0f;

	public Vector3 initialBulletPosition = Vector3.zero;
	//public Vector3 initialBulletPosition2 = new Vector3(0.205f, 0.127f, 0f);
	public float fireRate = 2f;
	public float vSpeed = 10f;
	public float hSpeed = 7f;
	public int maxHealthPoints = 40;
	public int healthPoints;
	public int crashDamage = 30;
	public bool crashed = false;
	public float collisionCooldown = 2f;
	public int bulletsPerPulse = 8;

	public GameObject projectile;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		healthPoints = maxHealthPoints;
		initialBulletPosition = transform.TransformPoint(initialBulletPosition);
	}

	// Update is called once per frame
	void Update()
	{
		if (healthPoints <= 0)
		{
			Destroy(gameObject);
		}

		if (fireRateTimeTracker == 0f)
		{
			for (var i = 0; i < bulletsPerPulse; ++i)
			{
				Instantiate(projectile, initialBulletPosition, projectile.transform.rotation * Quaternion.Euler(0, 0, i * 360f / bulletsPerPulse));
			}
			updateFireRateTimeTracker();
		}
		else if (fireRateTimeTracker > 0f)
		{
			updateFireRateTimeTracker();
		}

		if (crashed)
		{
			collisionCooldownTimeTracker += Time.deltaTime;
			if (collisionCooldownTimeTracker >= collisionCooldown)
			{
				collisionCooldownTimeTracker = 0f;
				crashed = false;
			}
		}
	}

	private void updateFireRateTimeTracker()
	{
		fireRateTimeTracker += Time.deltaTime;
		if (fireRateTimeTracker >= 1 / fireRate)
		{
			fireRateTimeTracker = 0f;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		//Debug.Log($"EnemyShip OnTriggerEnter2D {collision.gameObject.tag} {collision.gameObject.name}");
		if (collision.gameObject.CompareTag("Player Bullet"))
		{
			Debug.Log("Enemy took damage");
			var playerBullet = collision.gameObject.GetComponent<PlayerBullet>();
			healthPoints -= playerBullet.damage;
			Destroy(collision.gameObject);
		}
	}
}
