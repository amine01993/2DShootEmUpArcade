using System;
using UnityEngine;

public class CollectibleChild : MonoBehaviour
{
	private int playerLayer;

	public int healthPointsIncrement = 50;
	public int shieldPointsIncrement = 50;

	public event Func<CollectibleType> onCollected;

	private void Start()
	{
		playerLayer = LayerMask.NameToLayer("Player");
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.layer == playerLayer)
		{
			var type = onCollected.Invoke();
			PlayerShip player;

			switch (type)
			{
				case CollectibleType.PowerUp:
					player = collision.gameObject.GetComponent<PlayerShip>();
					player.PowerUp();
					break;
				case CollectibleType.Shield:
					player = collision.gameObject.GetComponent<PlayerShip>();
					var playerShield = player.transform.GetChild(0).GetComponent<Shield>();
					playerShield.AddShield(shieldPointsIncrement);
					break;
				case CollectibleType.Health:
					var playerCtd = collision.gameObject.GetComponent<CanTakeDamage>();
					playerCtd.AddHealthPoints(healthPointsIncrement);
					break;
			}
		}
	}

	private void OnDestroy()
	{
		onCollected = null;
	}
}
