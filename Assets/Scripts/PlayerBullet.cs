using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    private int outerBoundLayer;
    private int enemyLayer;
    private int obstacleLayer;

    public float speed = 3f;
    public int damage = 25;

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

}
