using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CanTakeDamage))]
[RequireComponent(typeof(CanCrash))]
[RequireComponent(typeof(SingleShots))]
[RequireComponent(typeof(DoubleShots))]
[RequireComponent(typeof(QuadrupleShots))]
[RequireComponent(typeof(ShotsWithMissiles))]
public class PlayerShip : MonoBehaviour
{
	private bool topBoundCollision = false;
	private bool rightBoundCollision = false;
	private bool bottomBoundCollision = false;
	private bool leftBoundCollision = false;
	private SingleShots singleShots;
	private DoubleShots doubleShots;
	private QuadrupleShots quadrupleShots;
	private ShotsWithMissiles shotsWithMissiles;

	public Vector3 initialPosition = new(0f, -4.5f, 0f);
	public float hSpeed = 10f;
	public float vSpeed = 7f;

	public Attacks attack = Attacks.SingleShots;

	InputAction moveAction;

	void Start()
	{
		moveAction = InputSystem.actions.FindAction("Move");
		transform.position = initialPosition;

		singleShots = GetComponent<SingleShots>();
		doubleShots = GetComponent<DoubleShots>();
		quadrupleShots = GetComponent<QuadrupleShots>();
		shotsWithMissiles = GetComponent<ShotsWithMissiles>();
	}

	void Update()
	{
		Vector2 moveValue = moveAction.ReadValue<Vector2>();
		Debug.DrawLine(transform.position, transform.position + new Vector3(moveValue.x, moveValue.y, 0f));

		float moveX = moveValue.x;
		float moveY = moveValue.y;
		if (moveX > 0 && rightBoundCollision) moveX = 0f;
		if (moveX < 0 && leftBoundCollision) moveX = 0f;
		if (moveY > 0 && topBoundCollision) moveY = 0f;
		if (moveY < 0 && bottomBoundCollision) moveY = 0f;

		transform.position += new Vector3(moveX * hSpeed, moveY * vSpeed, 0f) * Time.deltaTime;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Boundary Top"))
		{
			topBoundCollision = true;
		}
		else if (collision.gameObject.CompareTag("Boundary Right"))
		{
			rightBoundCollision = true;
		}
		else if (collision.gameObject.CompareTag("Boundary Bottom"))
		{
			bottomBoundCollision = true;
		}
		else if (collision.gameObject.CompareTag("Boundary Left"))
		{
			leftBoundCollision = true;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Boundary Top"))
		{
			topBoundCollision = false;
		}
		else if (collision.gameObject.CompareTag("Boundary Right"))
		{
			rightBoundCollision = false;
		}
		else if (collision.gameObject.CompareTag("Boundary Bottom"))
		{
			bottomBoundCollision = false;
		}
		else if (collision.gameObject.CompareTag("Boundary Left"))
		{
			leftBoundCollision = false;
		}
	}

	public void PowerUp()
	{
		switch (attack)
		{
			case Attacks.SingleShots:
				singleShots.enabled = false;
				doubleShots.enabled = true;
				attack = Attacks.DoubleShots;
				break;
			case Attacks.DoubleShots:
				doubleShots.enabled = false;
				quadrupleShots.enabled = true;
				attack = Attacks.QuadrupleShots;
				break;
			case Attacks.QuadrupleShots:
				quadrupleShots.enabled = false;
				shotsWithMissiles.enabled = true;
				attack = Attacks.ShotsWithMissiles;
				break;
			case Attacks.ShotsWithMissiles:
				break;
		}
	}
}

public enum Attacks
{
	SingleShots,
	DoubleShots,
	QuadrupleShots,
	ShotsWithMissiles
}