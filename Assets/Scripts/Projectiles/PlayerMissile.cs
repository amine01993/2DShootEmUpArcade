using System;
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
	public GameObject OnExplodeVfx;

	private GameObject target = null;

	private void Start()
	{
		outerBoundLayer = LayerMask.NameToLayer("OuterBound");
		enemyLayer = LayerMask.NameToLayer("Enemy");
		obstacleLayer = LayerMask.NameToLayer("Obstacle");
	}

	void Update()
	{
		if (OnScreenObjectsTracker.IsTracked(gameObject)) {
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
		}

		transform.Translate(speed * Time.deltaTime * Vector3.up);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Vector2 collisionPt = collision.ClosestPoint(new Vector2(transform.position.x, transform.position.y));
		var combinedMask = (1 << enemyLayer) | (1 << obstacleLayer) | (1 << outerBoundLayer);
		Collider2D[] colliders = Physics2D.OverlapCircleAll(collisionPt, 1.2f, combinedMask);
		foreach (var collider in colliders)
		{
			if (collider.gameObject.layer == enemyLayer)
			{
				var enemyCtd = collider.gameObject.GetComponent<CanTakeDamage>();
				enemyCtd.TakeDamage(damage);
				MainController.Instance.soundManager.PlaySound("explosion");
				CreateVfx(collider, collisionPt);
				Destroy(gameObject);
			}
			if (collider.gameObject.layer == obstacleLayer)
			{
				var meteorCtd = collider.gameObject.GetComponent<CanTakeDamage>();
				meteorCtd.TakeDamage(damage);
				MainController.Instance.soundManager.PlaySound("explosion");
				CreateVfx(collider, collisionPt);
				Destroy(gameObject);
			}
			else if (collider.gameObject.layer == outerBoundLayer)
			{
				Destroy(gameObject);
			}
		}
	}

	void CreateVfx(Collider2D collision, Vector2 point)
	{
		Instantiate(OnExplodeVfx, point, transform.rotation);
	}

	private float GetLookAtTargetAngle(GameObject target)
	{
		var lookAtVector = new Vector2(target.transform.position.x - transform.position.x, target.transform.position.y - transform.position.y);
		var lookAtOrientation = Mathf.Atan2(lookAtVector.y, lookAtVector.x) * Mathf.Rad2Deg;

		return lookAtOrientation - 90;
	}
}
