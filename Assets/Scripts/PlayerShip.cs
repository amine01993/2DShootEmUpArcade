using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShip : MonoBehaviour
{
	private float fireRateTimeTracker = 0f;
	private float collisionCooldownTimeTracker = 0f;
	private bool topBoundCollision = false;
	private bool rightBoundCollision = false;
	private bool bottomBoundCollision = false;
	private bool leftBoundCollision = false;

    public Vector3 initialPosition = new Vector3 (0f, -4.5f, 0f);
	public float hSpeed = 10f;
	public float vSpeed = 7f;
	public float fireRate = 5f; // bullets per second
	public int maxHealthPoints = 100;
	public int maxShield = 100;
	public int healthPoints = 100;
	public int shieldPoints = 0;
	public int crashDamage = 50;
	public bool crashed = false;
	public float collisionCooldown = 3f;

	public GameObject projectile;

	InputAction moveAction;
	InputAction attackAction;


	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
		moveAction = InputSystem.actions.FindAction("Move");
		attackAction = InputSystem.actions.FindAction("Attack");
        transform.position = initialPosition;
	}

	// Update is called once per frame
	void Update()
    {
		if (healthPoints <= 0)
		{
			Destroy(gameObject);
		}

		Vector2 moveValue = moveAction.ReadValue<Vector2>();
		Debug.DrawLine(transform.position, transform.position + new Vector3(moveValue.x, moveValue.y, 0f));

		float moveX = moveValue.x;
		float moveY = moveValue.y;
		//Debug.Log($"{moveValue.x}, {moveValue.y}");
		if(moveX > 0 && rightBoundCollision) moveX = 0f;
		if(moveX < 0 && leftBoundCollision) moveX = 0f;
		if(moveY > 0 && topBoundCollision) moveY = 0f;
		if(moveY < 0 && bottomBoundCollision) moveY = 0f;

		transform.position += new Vector3(moveX, moveY, 0f) * vSpeed * Time.deltaTime;

		if (attackAction.phase == InputActionPhase.Performed && fireRateTimeTracker == 0f)
		{
			// launch projectile
			Instantiate(projectile, transform.position, projectile.transform.rotation);
			updateFireRateTimeTracker();
		}
		// follow the firerate constraint
		else if(fireRateTimeTracker > 0f) {
			updateFireRateTimeTracker();
		}

		if (crashed)
		{
			collisionCooldownTimeTracker += Time.deltaTime;
			if (collisionCooldownTimeTracker >= collisionCooldown)
			{
				collisionCooldownTimeTracker = 0f;
				crashed = false;
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		//Debug.Log($"PlayerShip OnTriggerEnter2D {collision.gameObject.tag}");
		if(collision.gameObject.CompareTag("Boundary Top")) {
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
		else if(collision.gameObject.CompareTag("Enemy Bullet")
		|| collision.gameObject.CompareTag("Enemy Bullet 2")
		|| collision.gameObject.CompareTag("Enemy Bullet 3")) {
			Debug.Log("Player received damage.");
			var enemyBullet = collision.gameObject.GetComponent<EnemyBullet>();
			healthPoints -= enemyBullet.damage;
			Destroy(collision.gameObject);
		}
		else if (collision.gameObject.CompareTag("Enemy"))
		{
			var enemy = collision.gameObject.GetComponent<EnemyShip>();
			Debug.Log($"Player crashed with an Enemy ({crashed}, {enemy.crashed}).");
			if (!crashed || !enemy.crashed)
			{
				healthPoints -= enemy.crashDamage;
				crashed = true;
				enemy.healthPoints -= crashDamage;
				enemy.crashed = true;
			}
		}
		else if (collision.gameObject.CompareTag("Enemy 2"))
		{
			var enemy = collision.gameObject.GetComponent<EnemyShip2>();
			Debug.Log($"Player crashed with an Enemy2 ({crashed}, {enemy.crashed}).");
			if (!crashed || !enemy.crashed)
			{
				healthPoints -= enemy.crashDamage;
				crashed = true;
				enemy.healthPoints -= crashDamage;
				enemy.crashed = true;
			}
		}
		else if (collision.gameObject.CompareTag("Enemy 3"))
		{
			var enemy = collision.gameObject.GetComponent<EnemyShip2>();
			Debug.Log($"Player crashed with an Enemy3 ({crashed}, {enemy.crashed}).");
			if (!crashed || !enemy.crashed)
			{
				healthPoints -= enemy.crashDamage;
				crashed = true;
				enemy.healthPoints -= crashDamage;
				enemy.crashed = true;
			}
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		//Debug.Log($"PlayerShip OnTriggerExit2D {collision.gameObject.tag}");
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

	private void updateFireRateTimeTracker()
	{
		fireRateTimeTracker += Time.deltaTime;
		if (fireRateTimeTracker >= 1 / fireRate)
		{
			fireRateTimeTracker = 0f;
		}
	}
}
