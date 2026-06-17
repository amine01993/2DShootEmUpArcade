using System;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    private int outerBoundLayer;
    private int enemyLayer;
    private int obstacleLayer;

    public float speed = 3f;
    public int damage = 25;
	public GameObject OnHitVfx;

	private void Start()
	{
		outerBoundLayer = LayerMask.NameToLayer("OuterBound");
		enemyLayer = LayerMask.NameToLayer("Enemy");
		obstacleLayer = LayerMask.NameToLayer("Obstacle");
	}

	void Update()
    {
        transform.position += new Vector3(0, speed, 0) * Time.deltaTime;
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.layer == enemyLayer)
		{
			var enemyCtd = collision.gameObject.GetComponent<CanTakeDamage>();
			enemyCtd.TakeDamage(damage);
			MainController.Instance.soundManager.PlaySound("hitHurt");
			CreateVfx(collision);
			Destroy(gameObject);
		}
		if (collision.gameObject.layer == obstacleLayer)
		{
			var meteorCtd = collision.gameObject.GetComponent<CanTakeDamage>();
			meteorCtd.TakeDamage(damage);
			MainController.Instance.soundManager.PlaySound("hitHurt");
			CreateVfx(collision);
			Destroy(gameObject);
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
