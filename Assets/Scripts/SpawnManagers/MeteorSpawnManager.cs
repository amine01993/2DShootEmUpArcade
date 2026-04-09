using UnityEngine;
using UnityEngine.Splines;

public class MeteorSpawnManager : MonoBehaviour
{

	private TimeTracker meteorSpawnTimeTracker;
	private Spline meteorSplineLine;
	private SpawnMeteor handler;

	public float spawnIntervale = 5f;
	public float spawnProbability = .3f;
	public float[] typeProbabilities = { .6f, .4f };

	public GameObject meteorSpawnLine;
	public GameObject[] meteors;

	void Start()
	{
		var meteorSplineContainer = meteorSpawnLine.GetComponent<SplineContainer>();
		meteorSplineLine = meteorSplineContainer.Spline;
		handler = new(spawnProbability, typeProbabilities);

		meteorSpawnTimeTracker = new(spawnIntervale);
	}

	void Update()
	{
		SpawnAMeteor();
	}

	private void SpawnAMeteor()
	{
		if (meteorSpawnTimeTracker.IsZero())
		{
			var meteorSpawn = handler.GetSpawn(meteorSplineLine, meteorSpawnLine);
			if (meteorSpawn.HasValue)
			{
				var meteorGo = Instantiate(meteors[meteorSpawn.Value.index]);
				meteorGo.transform.position = meteorSpawn.Value.position;

				var meteor = meteorGo.GetComponent<Meteor>();
				meteor.Init(meteorSpawn.Value.direction);
			}

			meteorSpawnTimeTracker.Update();
		}
		else
		{
			meteorSpawnTimeTracker.Update();
		}
	}
}
