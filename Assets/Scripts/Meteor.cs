using UnityEngine;

[RequireComponent(typeof(CanTakeDamage))]
[RequireComponent(typeof(CanCrash))]
[RequireComponent(typeof(ScorableOnDeath))]
[RequireComponent(typeof(TrackedOnScreen))]
public class Meteor : MonoBehaviour
{
	public float speed = 7f;
	public float rotationSpeed = .7f * 180f;
	public Vector3 direction = Vector3.up;

	void Update()
	{
		transform.position += speed * Time.deltaTime * direction;
		transform.RotateAround(transform.position, Vector3.forward, rotationSpeed * Time.deltaTime);
	}

	public void Init(Vector3 dir)
	{
		direction = dir;
	}
}
