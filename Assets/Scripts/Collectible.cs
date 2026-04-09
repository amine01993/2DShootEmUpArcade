using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Collectible : MonoBehaviour
{
	private int innerBoundsLayer;
	private int outerBoundsLayer;
	private float speed;

	public CollectibleType type = CollectibleType.PowerUp;
	public float minSpeed = 3.0f;
	public float maxSpeed = 6.0f;
	public int maxBounceCount = 3;
	public int bounceCount;
	public Vector2 randomDirection;
	public GameObject OnCollectSfx;

	void Start()
	{
		speed = Random.Range(minSpeed, maxSpeed);
		bounceCount = maxBounceCount;
		randomDirection = new(Random.value, Random.value);
		randomDirection.Normalize();

		var rb = GetComponent<Rigidbody2D>();
		rb.AddForce(randomDirection * speed, ForceMode2D.Impulse);

		innerBoundsLayer = LayerMask.NameToLayer("InnerBound");
		outerBoundsLayer = LayerMask.NameToLayer("OuterBound");
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.layer == innerBoundsLayer)
		{
			bounceCount--;

			if (bounceCount == 0)
			{
				Destroy(gameObject);
			}
		}
		else if (collision.gameObject.layer == outerBoundsLayer)
		{
			Destroy(gameObject);
		}
	}

	public void Init(CollectibleType collectibleType)
	{
		type = collectibleType;
		GameObject go;
		switch (type)
		{
			case CollectibleType.Health:
				go = transform.GetChild(2).gameObject;
				break;
			case CollectibleType.Shield:
				go = transform.GetChild(1).gameObject;
				break;
			default:
				go = transform.GetChild(0).gameObject;
				break;
		}

		go.SetActive(true);
		var collectibleChild = go.GetComponent<CollectibleChild>();
		collectibleChild.onCollected += OnChildCollected;
	}

	public CollectibleType OnChildCollected()
	{
		Destroy(gameObject);
		Instantiate(OnCollectSfx);
		return type;
	}
}

public enum CollectibleType
{
	PowerUp,
	Health,
	Shield,
}