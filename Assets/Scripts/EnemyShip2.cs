using UnityEngine;
using UnityEngine.Splines;

public class EnemyShip2 : MonoBehaviour
{
	private float fireRateTimeTracker = 0f;
	private float collisionCooldownTimeTracker = 0f;
	private float lifeTimeTracker = 0f;
	private SplineAnimate splineAnimate;
	private GameObject pathClone;
	private float initalXPosition;

	public Vector3 initialBulletPosition = Vector3.zero;
	public float fireRate = 2f;
	public float speed = 7f;
	public int maxHealthPoints = 40;
	public int healthPoints;
	public int crashDamage = 30;
	public bool crashed = false;
	public float collisionCooldown = 2f;
	public int bulletsPerPulse = 8;
	public bool startShooting = false;
	public float lifeTime = 8f;

	public GameObject projectile;
	public GameObject leftPath;
	public GameObject rightPath;

	void Start()
	{
		healthPoints = maxHealthPoints;
		initalXPosition = transform.position.x;

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
			for (var i = 0; i < bulletsPerPulse; ++i)
			{
				Instantiate(projectile, transform.TransformPoint(initialBulletPosition), projectile.transform.rotation * Quaternion.Euler(0, 0, i * 360f / bulletsPerPulse));
			}
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

	private void AddAndConfigureAnimation(float positionX)
	{
		if (positionX <= SpawnBoxBounds.x)
		{
			pathClone = Instantiate(leftPath);
			pathClone.transform.position = transform.position;// new Vector3(transform.position);
		}
		else
		{
			pathClone = Instantiate(rightPath);
			pathClone.transform.position = transform.position;
		}

		splineAnimate = gameObject.AddComponent<SplineAnimate>();

		var splineContainer = pathClone.GetComponent<SplineContainer>();

		splineAnimate.Container = splineContainer;

		splineAnimate.AnimationMethod = SplineAnimate.Method.Speed;
		splineAnimate.Easing = SplineAnimate.EasingMode.EaseOut;
		splineAnimate.Loop = SplineAnimate.LoopMode.Loop;
		splineAnimate.MaxSpeed = speed;
		splineAnimate.ObjectUpAxis = SplineComponent.AlignAxis.NegativeZAxis;
		splineAnimate.ObjectForwardAxis = SplineComponent.AlignAxis.YAxis;
		splineAnimate.PlayOnAwake = true;

		splineAnimate.Play();
	}
}
