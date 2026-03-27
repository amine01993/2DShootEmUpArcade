using System.Collections.Generic;
using UnityEngine;

public class OnScreenObjectsTracker : MonoBehaviour
{
	private static Dictionary<int, GameObject> objects = new ();
	private static int enemyLayer;
	private static int obstacleLayer;

	private void Start()
	{
		enemyLayer = LayerMask.NameToLayer("Enemy");
		obstacleLayer = LayerMask.NameToLayer("Obstacle");
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		Track(collision.gameObject);
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		UnTrack(collision.gameObject);
	}

	private void OnDestroy()
	{
		objects.Clear();
	}

	public static void Track(GameObject go)
	{
		objects[go.GetInstanceID()] = go;
	}

	public static void UnTrack(GameObject go)
	{
		objects.Remove(go.GetInstanceID());
	}

	public static bool IsTracked(GameObject go) {
		return objects.ContainsKey(go.GetInstanceID());
	}

	public static GameObject GetNearestTarget(Vector3 oPos) {
		GameObject nearestEnemy = null;
		float? nearestEnemyDistance2 = null;
		GameObject nearestObstacle = null;
		float? nearestObstacleDistance2 = null;

		foreach (var item in objects)
		{
			if(item.Value.layer == enemyLayer) {
				float distance2 = (item.Value.transform.position - oPos).sqrMagnitude;
				if (nearestEnemyDistance2 == null || nearestEnemyDistance2.Value > distance2) {
					nearestEnemy = item.Value;
					nearestEnemyDistance2 = distance2;
				}
			}
			else if (item.Value.layer == obstacleLayer)
			{
				float distance2 = (item.Value.transform.position - oPos).sqrMagnitude;
				if (nearestObstacleDistance2 == null || nearestObstacleDistance2.Value > distance2)
				{
					nearestObstacle = item.Value;
					nearestObstacleDistance2 = distance2;
				}
			}
		}

		return nearestEnemy ?? (nearestObstacle ?? null);
	}
}
