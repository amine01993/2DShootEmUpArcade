using System.Linq;
using UnityEngine;
using UnityEngine.Splines;

public class EnemyPath : MonoBehaviour
{
	private int spawnLineLayer;
	private TimeTracker showTimeTracker;
	private GameObject entrancePathClone;
	private SplineAnimate entranceAnimation;
	private GameObject exitPathClone;
	private SplineAnimate exitAnimation;
	private GameObject lookAt;
	private bool animationStarted = false;
	private Transform cam;

	public GameObject entrancePath;
	public GameObject exitPath;

	public float showTime = 3f;
	public float speed = 5f;
	public int row = 0;
	public bool isLookingAtPlayer = false;
	public float rotationSpeed = 5f;

	void Start()
	{
		cam = Camera.main.transform;
		showTimeTracker = new TimeTracker(showTime, false);

		if (isLookingAtPlayer)
		{
			lookAt = GameObject.FindGameObjectWithTag("Player");
		}

		spawnLineLayer = LayerMask.NameToLayer("SpawnLine");
	}

	void Update()
	{
		if (animationStarted)
		{
			// Check if entrance animation is finished and exit animation hasn't started yet
			if (entranceAnimation.NormalizedTime >= 1f && exitAnimation.NormalizedTime == 0f)
			{
				if (!showTimeTracker.IsExpired())
				{
					showTimeTracker.Update();
				}
				// When show time is over => start exit animation
				if (showTimeTracker.IsExpired())
				{
					exitAnimation.Play();
					showTimeTracker.Reset();
				}
			}
			// When exit animation is over => resets animations
			if (exitAnimation.NormalizedTime >= 1f)
			{
				exitAnimation.Restart(false);
				entranceAnimation.Restart(true);
			}

		}

		if (isLookingAtPlayer)
		{
			// Enemy is looking at the player
			var lookAtOrientation = GetLookAtPlayerAngle();

			transform.rotation = Quaternion.Lerp(
				transform.rotation,
				Quaternion.AngleAxis(lookAtOrientation, Vector3.forward),
				Time.deltaTime * rotationSpeed
			);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.layer == spawnLineLayer)
		{
			StartMoving();
		}
	}

	private void OnDestroy()
	{
		Destroy(entrancePathClone);
		Destroy(exitPathClone);
	}

	public void StartMoving()
	{
		if (!animationStarted)
		{
			(entrancePathClone, entranceAnimation) = AddAndConfigureAnimation(entrancePath);
			(exitPathClone, exitAnimation) = AddAndConfigureAnimation(exitPath);
			AdjustSplines();

			animationStarted = true;
			entranceAnimation.Play();
		}
	}

	private float GetLookAtPlayerAngle()
	{
		if (lookAt == null) return 180f;

		var lookAtVector = new Vector2(lookAt.transform.position.x - transform.position.x, lookAt.transform.position.y - transform.position.y);
		var lookAtOrientation = Mathf.Atan2(lookAtVector.y, lookAtVector.x) * Mathf.Rad2Deg;

		return lookAtOrientation - 90;
	}

	private (GameObject pathClone, SplineAnimate animation) AddAndConfigureAnimation(GameObject path)
	{
		GameObject pathClone;
		if (transform.position.x <= SpawnEnemy.x)
		{
			pathClone = Instantiate(path);
		}
		else
		{
			pathClone = Instantiate(path);
			pathClone.transform.rotation *= Quaternion.Euler(0f, 180f, 0f);
		}

		pathClone.transform.position = cam.InverseTransformPoint(transform.position);

		var splineContainer = pathClone.GetComponent<SplineContainer>();

		var animation = gameObject.AddComponent<SplineAnimate>();

		animation.Container = splineContainer;
		animation.AnimationMethod = SplineAnimate.Method.Speed;
		animation.Easing = SplineAnimate.EasingMode.EaseOut;
		animation.Loop = SplineAnimate.LoopMode.Once;
		animation.MaxSpeed = speed;
		if (isLookingAtPlayer)
		{
			animation.Alignment = SplineAnimate.AlignmentMode.None;
		}
		else
		{
			animation.ObjectUpAxis = SplineComponent.AlignAxis.NegativeZAxis;
			animation.ObjectForwardAxis = SplineComponent.AlignAxis.YAxis;
		}
		animation.PlayOnAwake = false;

		return (pathClone, animation);
	}

	private void AdjustSplines() {
		if(row > 0) {
			var entranceSplineContainer = entrancePathClone.GetComponent<SplineContainer>();
			entranceSplineContainer.Spline.Copy(entranceSplineContainer.Spline);
			var entranceKnots = entranceSplineContainer.Spline.Knots.ToArray();
			entranceKnots[1].Position.y -= row;
			entranceSplineContainer.Spline.SetKnot(1, entranceKnots[1]);
			entranceAnimation.Container = entranceSplineContainer;

			var exitSplineContainer = exitPathClone.GetComponent<SplineContainer>();
			exitSplineContainer.Spline.Copy(exitSplineContainer.Spline);
			var exitKnots = exitSplineContainer.Spline.Knots.ToArray();
			exitKnots[0].Position.y -= row;
			exitSplineContainer.Spline.SetKnot(0, exitKnots[0]);
			exitAnimation.Container = exitSplineContainer;
		}
	}
}
