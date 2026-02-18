using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyShip3 : MonoBehaviour
{
	private float fireRateTimeTracker = 0f;
	private float collisionCooldownTimeTracker = 0f;
	private int bulletsFired = 0;
	private GameObject lookAt;

	public Vector3 initialBulletPosition1 = new Vector3(-0.065f, 0.35f, 0f);
	public Vector3 initialBulletPosition2 = new Vector3(0.065f, 0.35f, 0f);
	public float fireRate = 2f;
	public float vSpeed = 10f;
	public float hSpeed = 7f;
	public float rotationSpeed = 5f;
	public int maxHealthPoints = 120;
	public int healthPoints;
	public int crashDamage = 60;
	public bool crashed = false;
	public float collisionCooldown = 2f;

	public GameObject projectile;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		healthPoints = maxHealthPoints;
		//initialBulletPosition1 = transform.TransformPoint(initialBulletPosition1);
		//initialBulletPosition2 = transform.TransformPoint(initialBulletPosition2);
		lookAt = GameObject.FindGameObjectWithTag("Player");
	}

	// Update is called once per frame
	void Update()
	{
		if (healthPoints <= 0)
		{
			Destroy(gameObject);
		}

		// Enemy is looking at the player
		var vectorOrientation = getLookAtPlayerAngle();

		transform.rotation = Quaternion.Lerp(
			transform.rotation, 
			Quaternion.AngleAxis(vectorOrientation, Vector3.forward), 
			Time.deltaTime * rotationSpeed
		);
		

		if (fireRateTimeTracker == 0f)
		{
			if (bulletsFired % 2 == 0)
			{
				Instantiate(
					projectile,
					transform.TransformPoint(initialBulletPosition1),
					projectile.transform.rotation * Quaternion.Euler(0, 0, vectorOrientation)
				);
			}
			else
			{
				Instantiate(
					projectile, 
					transform.TransformPoint(initialBulletPosition2), 
					projectile.transform.rotation * Quaternion.Euler(0, 0, vectorOrientation)
				);
			}
			++bulletsFired;
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

	private float getLookAtPlayerAngle() {
		if (lookAt.IsDestroyed()) return 180f;

		var lookAtVector = new Vector2(lookAt.transform.position.x - transform.position.x, lookAt.transform.position.y - transform.position.y);
		var vectorOrientation = Mathf.Atan2(lookAtVector.y, lookAtVector.x) * Mathf.Rad2Deg;
		//Debug.Log($"orientation between enemy and player is {vectorOrientation - 90} deg");

		return vectorOrientation - 90;
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
