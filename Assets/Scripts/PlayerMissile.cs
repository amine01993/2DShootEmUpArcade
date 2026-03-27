using UnityEngine;

[RequireComponent(typeof(TrackedOnScreen))]
public class PlayerMissile : MonoBehaviour
{
	private int outerBoundLayer;
	private int enemyLayer;
	private int obstacleLayer;

	public float speed = 2.75f;
	public float rotationSpeed = 2.47f;
	public int damage = 70;

	private GameObject target = null;

	private void Start()
	{
		outerBoundLayer = LayerMask.NameToLayer("OuterBound");
		enemyLayer = LayerMask.NameToLayer("Enemy");
		obstacleLayer = LayerMask.NameToLayer("Obstacle");
	}

	void Update()
	{
		if (!OnScreenObjectsTracker.IsTracked(gameObject)) return;

		if (target == null || !OnScreenObjectsTracker.IsTracked(target))
		{
			target = OnScreenObjectsTracker.GetNearestTarget(transform.position);
		}
		else
		{
			var lookAtOrientation = GetLookAtTargetAngle(target);

			transform.rotation = Quaternion.Lerp(
				transform.rotation,
				Quaternion.AngleAxis(lookAtOrientation, Vector3.forward),
				Time.deltaTime * rotationSpeed
			);
		}

		transform.Translate(speed * Time.deltaTime * Vector3.up);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.layer == enemyLayer)
		{
			var enemyCtd = collision.gameObject.GetComponent<CanTakeDamage>();
			enemyCtd.TakeDamage(damage);
			Destroy(gameObject);
		}
		if (collision.gameObject.layer == obstacleLayer)
		{
			var meteorCtd = collision.gameObject.GetComponent<CanTakeDamage>();
			meteorCtd.TakeDamage(damage);
			Destroy(gameObject);
		}
		else if (collision.gameObject.layer == outerBoundLayer)
		{
			Destroy(gameObject);
		}
	}

	private float GetLookAtTargetAngle(GameObject target)
	{
		var lookAtVector = new Vector2(target.transform.position.x - transform.position.x, target.transform.position.y - transform.position.y);
		var lookAtOrientation = Mathf.Atan2(lookAtVector.y, lookAtVector.x) * Mathf.Rad2Deg;

		return lookAtOrientation - 90;
	}
}
