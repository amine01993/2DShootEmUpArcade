using UnityEngine;

public class EnemyLifeTime : MonoBehaviour
{
	private TimeTracker lifeTimeTracker;
	public float lifeTime = 8f;

	void Start()
	{
		lifeTimeTracker = new TimeTracker(lifeTime, false);
	}

	void Update()
	{
		if (OnScreenObjectsTracker.IsTracked(gameObject))
		{
			lifeTimeTracker.Update();
		}
		else
		{
			if (lifeTimeTracker.IsExpired())
			{
				Destroy(gameObject);
			}
		}
	}
}
