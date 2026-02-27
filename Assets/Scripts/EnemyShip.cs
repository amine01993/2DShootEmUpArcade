using UnityEngine;
using UnityEngine.Splines;
using System.Linq;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyShip : MonoBehaviour
{
	private float fireRateTimeTracker = 0f;
	private float collisionCooldownTimeTracker = 0f;
	private GameObject entrancePathClone;
	private SplineAnimate entranceAnimation;
	private GameObject exitPathClone;
	private SplineAnimate exitAnimation;
	private float showTimeTracker = 0f;
	private float lifeTimeTracker = 0f;

	public Vector3 initialBulletPosition1 = new (-0.205f, 0.127f, 0f);
	public Vector3 initialBulletPosition2 = new (0.205f, 0.127f, 0f);
	public float fireRate = 4f;
	public float speed = 7f;
	public int maxHealthPoints = 20;
	public int healthPoints;
	public int crashDamage = 20;
	public bool crashed = false;
	public float collisionCooldown = 2f;
	public bool startShooting = false;
	public float showTime = 3f;
	public float lifeTime = 8f;

	public GameObject projectile;
	public GameObject entrancePath;
	public GameObject exitPath;

	void Start()
	{
		healthPoints = maxHealthPoints;

		AddAndConfigureEnteringAnimation(transform.position.x);
		AddAndConfigureExitingAnimation(transform.position.x);
	}

	void Update()
	{
		if (healthPoints <= 0)
		{
			SpawnBoxBounds.ResetPosition(transform.position.x);
			Destroy(entrancePathClone);
			Destroy(exitPathClone);
			Destroy(gameObject);
		}

		if (entranceAnimation.NormalizedTime >= 1f && exitAnimation.NormalizedTime == 0f)
		{
			showTimeTracker += Time.deltaTime;
			lifeTimeTracker += Time.deltaTime;
			startShooting = true;
		}

		if (showTimeTracker >= showTime)
		{
			showTimeTracker = 0f;
			exitAnimation.Play();
		}

		if (exitAnimation.NormalizedTime >= 1f)
		{
			startShooting = false;
			if (lifeTimeTracker < lifeTime)
			{
				exitAnimation.Restart(false);
				entranceAnimation.Restart(true);
			}
			else
			{
				SpawnBoxBounds.ResetPosition(transform.position.x);
				Destroy(entrancePathClone);
				Destroy(exitPathClone);
				Destroy(gameObject);
			}
		}

		if (startShooting && fireRateTimeTracker == 0f)
		{
			Instantiate(projectile, transform.TransformPoint(initialBulletPosition1), projectile.transform.rotation);
			Instantiate(projectile, transform.TransformPoint(initialBulletPosition2), projectile.transform.rotation);
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

	private void AddAndConfigureEnteringAnimation(float positionX)
	{

		entrancePathClone = Instantiate(entrancePath);
		entranceAnimation = gameObject.AddComponent<SplineAnimate>();

		var splineContainer = entrancePathClone.GetComponent<SplineContainer>();

		splineContainer.Spline.Copy(splineContainer.Spline);
		var knots = splineContainer.Spline.Knots.ToArray();
		knots[0].Position.x = positionX;
		knots[1].Position.x = positionX;
		splineContainer.Spline.SetKnot(0, knots[0]);
		splineContainer.Spline.SetKnot(1, knots[1]);

		entranceAnimation.Container = splineContainer;

		entranceAnimation.AnimationMethod = SplineAnimate.Method.Speed;
		entranceAnimation.Easing = SplineAnimate.EasingMode.EaseOut;
		entranceAnimation.Loop = SplineAnimate.LoopMode.Once;
		entranceAnimation.MaxSpeed = speed;
		entranceAnimation.ObjectUpAxis = SplineComponent.AlignAxis.NegativeZAxis;
		entranceAnimation.ObjectForwardAxis = SplineComponent.AlignAxis.YAxis;
		entranceAnimation.PlayOnAwake = false;

		entranceAnimation.Play();
	}

	private void AddAndConfigureExitingAnimation(float positionX)
	{
		exitPathClone = Instantiate(exitPath);
		exitAnimation = gameObject.AddComponent<SplineAnimate>();

		var splineContainer = exitPathClone.GetComponent<SplineContainer>();

		splineContainer.Spline.Copy(splineContainer.Spline);
		var knots = splineContainer.Spline.Knots.ToArray();
		knots[0].Position.x = positionX;
		knots[1].Position.x = positionX;
		splineContainer.Spline.SetKnot(0, knots[0]);
		splineContainer.Spline.SetKnot(1, knots[1]);

		exitAnimation.Container = splineContainer;

		exitAnimation.AnimationMethod = SplineAnimate.Method.Speed;
		exitAnimation.Loop = SplineAnimate.LoopMode.Once;
		exitAnimation.MaxSpeed = speed;
		exitAnimation.ObjectUpAxis = SplineComponent.AlignAxis.NegativeZAxis;
		exitAnimation.ObjectForwardAxis = SplineComponent.AlignAxis.YAxis;
		exitAnimation.PlayOnAwake = false;
	}
}
