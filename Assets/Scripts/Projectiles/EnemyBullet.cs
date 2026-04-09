using System;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
	private int outerBoundLayer;
	private int playerLayer;
	private int shieldLayer;

	public float speed = 5f;
	public int damage = 10;
	public GameObject OnHitSfx;
	public GameObject OnHitVfx;

	private void Start()
	{
		outerBoundLayer = LayerMask.NameToLayer("OuterBound");
		playerLayer = LayerMask.NameToLayer("Player");
		shieldLayer = LayerMask.NameToLayer("Shield");
	}

	void Update()
	{
		transform.Translate(Time.deltaTime * speed * Vector3.down);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.layer == playerLayer)
		{
			var playerCtd = collision.gameObject.GetComponent<CanTakeDamage>();
			playerCtd.TakeDamage(damage);
			Instantiate(OnHitSfx);
			CreateVfx(collision);
			Destroy(gameObject);
		}
		else if (collision.gameObject.layer == shieldLayer)
		{
			var shield = collision.gameObject.GetComponent<Shield>();
			var remainingDamage = shield.TakeDamage(damage);
			Instantiate(OnHitSfx);
			CreateVfx(collision);
			if (remainingDamage == 0)
			{
				Destroy(gameObject);
			}
			else
			{
				damage -= remainingDamage;
			}
		}
		else if (collision.gameObject.layer == outerBoundLayer)
		{
			Destroy(gameObject);
		}
	}

	void CreateVfx(Collider2D collision)
	{
		Vector2 collisionPt = collision.ClosestPoint(new Vector2(transform.position.x, transform.position.y));
		var direction = collisionPt - (Vector2)transform.position;
		var orientation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

		Instantiate(OnHitVfx, collisionPt, Quaternion.Euler(orientation, -90f, -90f));
	}
}
