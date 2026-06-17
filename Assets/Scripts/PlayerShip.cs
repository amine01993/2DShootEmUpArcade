using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CanTakeDamage))]
[RequireComponent(typeof(CanCrash))]
[RequireComponent(typeof(CanShoot))]
[RequireComponent(typeof(SingleShots))]
[RequireComponent(typeof(DoubleShots))]
[RequireComponent(typeof(QuadrupleShots))]
[RequireComponent(typeof(ShotsWithMissiles))]
[RequireComponent(typeof(Animator))]
public class PlayerShip : MonoBehaviour
{
	private BoundCollision boundCollision;
	private SingleShots singleShots;
	private DoubleShots doubleShots;
	private QuadrupleShots quadrupleShots;
	private ShotsWithMissiles shotsWithMissiles;
	private Animator animator;

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
		animator = GetComponent<Animator>();

		boundCollision = new BoundCollision();
	}

	void Update()
	{
		if (MainController.Instance.uiManager.IsPaused()) return;

		Vector2 moveValue = moveAction.ReadValue<Vector2>();
		Debug.DrawLine(transform.position, transform.position + new Vector3(moveValue.x, moveValue.y, 0f));

		float moveX = moveValue.x;
		float moveY = moveValue.y;
		if (moveX > 0 && boundCollision.Has(BoundCollision.Bounds.Right)) moveX = 0f;
		if (moveX < 0 && boundCollision.Has(BoundCollision.Bounds.Left)) moveX = 0f;
		if (moveY > 0 && boundCollision.Has(BoundCollision.Bounds.Top)) moveY = 0f;
		if (moveY < 0 && boundCollision.Has(BoundCollision.Bounds.Bottom)) moveY = 0f;

		HandleMoveAnimation(moveX, moveY);

		transform.position += new Vector3(moveX * hSpeed, moveY * vSpeed, 0f) * Time.deltaTime;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Boundary Top"))
		{
			boundCollision.Add(BoundCollision.Bounds.Top);
		}
		else if (collision.gameObject.CompareTag("Boundary Right"))
		{
			boundCollision.Add(BoundCollision.Bounds.Right);
		}
		else if (collision.gameObject.CompareTag("Boundary Bottom"))
		{
			boundCollision.Add(BoundCollision.Bounds.Bottom);
		}
		else if (collision.gameObject.CompareTag("Boundary Left"))
		{
			boundCollision.Add(BoundCollision.Bounds.Left);
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Boundary Top"))
		{
			boundCollision.Remove(BoundCollision.Bounds.Top);
		}
		else if (collision.gameObject.CompareTag("Boundary Right"))
		{
			boundCollision.Remove(BoundCollision.Bounds.Right);
		}
		else if (collision.gameObject.CompareTag("Boundary Bottom"))
		{
			boundCollision.Remove(BoundCollision.Bounds.Bottom);
		}
		else if (collision.gameObject.CompareTag("Boundary Left"))
		{
			boundCollision.Remove(BoundCollision.Bounds.Left);
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
		animator.Play("PowerUp");
	}

	private void HandleMoveAnimation(float moveX, float moveY) {
		animator.SetInteger("dx", moveX == 0f ? 0 : (moveX > 0 ? 1 : -1));
		animator.SetInteger("dy", moveY == 0f ? 0 : (moveY > 0 ? 1 : -1));
	}
}

public enum Attacks
{
	SingleShots,
	DoubleShots,
	QuadrupleShots,
	ShotsWithMissiles
}