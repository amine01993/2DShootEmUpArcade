using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
	private int outerBoundLayer;
	private int playerLayer;
	private int shieldLayer;

	public float speed = 5f;
	public int damage = 10;

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
			Destroy(gameObject);
		}
		else if (collision.gameObject.layer == shieldLayer)
		{
			var shield = collision.gameObject.GetComponent<Shield>();
			var remainingDamage = shield.TakeDamage(damage);
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
}
