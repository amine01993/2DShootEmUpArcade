using UnityEngine;

public enum EnemyShootingStyle
{
	Straight,
	Pulse,
	TowardPlayer,
}

[RequireComponent(typeof(ScorableOnDeath))]
[RequireComponent(typeof(DropCollectibleOnDeath))]
[RequireComponent(typeof(CanCrash))]
[RequireComponent(typeof(TrackedOnScreen))]
[RequireComponent(typeof(EnemyLifeTime))]
[RequireComponent(typeof(EnemyPath))]
[RequireComponent(typeof(CanShoot))]
[RequireComponent(typeof(OnlyShootOnScreen))]
public class Enemy : MonoBehaviour
{
	private void OnDestroy()
	{
		SpawnEnemy.ResetPosition(transform.position.x);
	}
}


