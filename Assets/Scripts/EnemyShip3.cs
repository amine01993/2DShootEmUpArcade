using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;

public class EnemyShip3 : MonoBehaviour
{
	private float fireRateTimeTracker = 0f;
	private float collisionCooldownTimeTracker = 0f;
	private float lifeTimeTracker = 0f;
	private int bulletsFired = 0;
	private float initalXPosition;
	private GameObject lookAt;
	private GameObject pathClone;
	private SplineAnimate splineAnimate;

	public Vector3 initialBulletPosition1 = new (-0.065f, 0.35f, 0f);
	public Vector3 initialBulletPosition2 = new (0.065f, 0.35f, 0f);
	public float fireRate = 2f;
	public float speed = 7f;
	public float rotationSpeed = 5f;
	public int maxHealthPoints = 120;
	public int healthPoints;
	public int crashDamage = 60;
	public bool crashed = false;
	public float collisionCooldown = 2f;
	public bool startShooting = false;
	public float lifeTime = 8f;

	public GameObject projectile;
	public GameObject path;

	void Start()
	{
		healthPoints = maxHealthPoints;
		initalXPosition = transform.position.x;
		lookAt = GameObject.FindGameObjectWithTag("Player");

		AddAndConfigureAnimation(transform.position.x);
	}

	void Update()
	{
		if (healthPoints <= 0)
		{
			SpawnBoxBounds.ResetPosition(initalXPosition);
			Destroy(pathClone);
			Destroy(gameObject);
		}

		// Enemy is looking at the player
		var vectorOrientation = GetLookAtPlayerAngle();

		transform.rotation = Quaternion.Lerp(
			transform.rotation, 
			Quaternion.AngleAxis(vectorOrientation, Vector3.forward), 
			Time.deltaTime * rotationSpeed
		);

		var viewportPoint = Camera.main.WorldToViewportPoint(transform.position);
		if (viewportPoint.x >= 0 && viewportPoint.x <= 1 && viewportPoint.y >= 0 && viewportPoint.y <= 1)
		{
			lifeTimeTracker += Time.deltaTime;
			startShooting = true;
		}
		else
		{
			startShooting = false;
			if (lifeTimeTracker >= lifeTime)
			{
				SpawnBoxBounds.ResetPosition(initalXPosition);
				Destroy(pathClone);
				Destroy(gameObject);
			}
		}


		if (startShooting && fireRateTimeTracker == 0f)
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
			UpdateFireRateTimeTracker();
		}
		else if (fireRateTimeTracker > 0f)
		{
			UpdateFireRateTimeTracker();
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

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player Bullet"))
		{
			var playerBullet = collision.gameObject.GetComponent<PlayerBullet>();
			healthPoints -= playerBullet.damage;
			Destroy(collision.gameObject);
		}
	}

	private void UpdateFireRateTimeTracker()
	{
		fireRateTimeTracker += Time.deltaTime;
		if (fireRateTimeTracker >= 1 / fireRate)
		{
			fireRateTimeTracker = 0f;
		}
	}

	private float GetLookAtPlayerAngle() {
		if (lookAt == null || lookAt.IsDestroyed()) return 180f;

		var lookAtVector = new Vector2(lookAt.transform.position.x - transform.position.x, lookAt.transform.position.y - transform.position.y);
		var vectorOrientation = Mathf.Atan2(lookAtVector.y, lookAtVector.x) * Mathf.Rad2Deg;

		return vectorOrientation - 90;
	}

	private void AddAndConfigureAnimation(float positionX)
	{
		pathClone = Instantiate(path);
		pathClone.transform.position = transform.position;

		splineAnimate = gameObject.AddComponent<SplineAnimate>();

		var splineContainer = pathClone.GetComponent<SplineContainer>();

		splineAnimate.Container = splineContainer;

		splineAnimate.AnimationMethod = SplineAnimate.Method.Speed;
		splineAnimate.Easing = SplineAnimate.EasingMode.None;
		splineAnimate.Loop = SplineAnimate.LoopMode.Loop;
		splineAnimate.MaxSpeed = speed;
		splineAnimate.Alignment = SplineAnimate.AlignmentMode.None;
		splineAnimate.PlayOnAwake = true;

		splineAnimate.Play();
	}
}
