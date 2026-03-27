using UnityEngine;

[RequireComponent(typeof(CanTakeDamage))]
public class CanCrash : MonoBehaviour
{
	private int enemyLayer;
	private int obstacleLayer;
	private TimeTracker collisionCooldownTimeTracker;
	private PlayerShip player;
	private CanTakeDamage playerCtd;
	private GameObject playerShieldGo;
	private Shield shield;

	public int crashDamage = 20;
	public float collisionCooldown = 2f;
	public bool canCrash = true;

	void Start()
	{
		collisionCooldownTimeTracker = new TimeTracker(collisionCooldown);
		player = GetComponent<PlayerShip>();
		shield = GetComponent<Shield>();

		if (player != null)
		{
			playerCtd = player.GetComponent<CanTakeDamage>();
			playerShieldGo = player.transform.GetChild(0).gameObject;
		}

		enemyLayer = LayerMask.NameToLayer("Enemy");
		obstacleLayer = LayerMask.NameToLayer("Obstacle");
	}

	void Update()
	{
		if (!canCrash)
		{
			collisionCooldownTimeTracker.Update();
			if (collisionCooldownTimeTracker.IsZero())
			{
				canCrash = true;
			}
		}
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (shield != null)
		{
			if (collision.gameObject.layer == enemyLayer)
			{
				var enemyCc = collision.gameObject.GetComponent<CanCrash>();
				var enemyCtd = collision.gameObject.GetComponent<CanTakeDamage>();

				if (enemyCc.canCrash)
				{
					shield.TakeDamage(enemyCc.crashDamage);
					enemyCtd.TakeDamage(crashDamage);
					enemyCc.canCrash = false;
				}
			}
			else if (collision.gameObject.layer == obstacleLayer)
			{
				var meteorCc = collision.gameObject.GetComponent<CanCrash>();
				var meteorCtd = collision.gameObject.GetComponent<CanTakeDamage>();
				var shieldCc = GetComponent<CanCrash>();

				if (meteorCc.canCrash)
				{
					shield.TakeDamage(meteorCc.crashDamage);
					meteorCtd.TakeDamage(shieldCc.crashDamage);
					meteorCc.canCrash = false;
				}
			}
		}
		else if (player != null && !playerShieldGo.activeSelf)
		{
			if (collision.gameObject.layer == enemyLayer)
			{
				var enemyCc = collision.gameObject.GetComponent<CanCrash>();
				var enemyCtd = collision.gameObject.GetComponent<CanTakeDamage>();
				if (enemyCc.canCrash)
				{
					playerCtd.TakeDamage(enemyCc.crashDamage);
					enemyCtd.TakeDamage(crashDamage);
					enemyCc.canCrash = false;
				}
			}
			else if (collision.gameObject.layer == obstacleLayer)
			{
				var meteorCc = collision.gameObject.GetComponent<CanCrash>();
				var meteorCtd = collision.gameObject.GetComponent<CanTakeDamage>();
				if (meteorCc.canCrash)
				{
					playerCtd.TakeDamage(meteorCc.crashDamage);
					meteorCtd.TakeDamage(crashDamage);
					meteorCc.canCrash = false;
				}
			}
		}
	}
}
