using UnityEngine;

[RequireComponent(typeof(CanTakeDamage))]
[RequireComponent(typeof(CanCrash))]
[RequireComponent(typeof(ScorableOnDeath))]
[RequireComponent(typeof(TrackedOnScreen))]
public class Meteor : MonoBehaviour
{
	private int outerBoundLayer;

	public float speed = 7f;
	public float rotationSpeed = .7f * 180f;
	public Vector3 direction = Vector3.up;

	private void Start()
	{
		outerBoundLayer = LayerMask.NameToLayer("OuterBound");
	}

	void Update()
	{
		transform.position += speed * Time.deltaTime * direction;
		transform.RotateAround(transform.position, Vector3.forward, rotationSpeed * Time.deltaTime);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.layer == outerBoundLayer)
		{
			Destroy(gameObject);
		}
	}

	public void Init(Vector3 dir)
	{
		direction = dir;
	}
}
