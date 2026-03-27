using UnityEngine;

public class EnemySpawnManager: MonoBehaviour
{
	private TimeTracker spawnTimeTracker;
	private SpawnBox spawnBox;

	public bool isEnabled = false;
	public float spawnIntervale = 5f;
	public GameObject[] enemies;
	public bool isRandom = true;
	public EnemySpawnProbs[] probabilities = new EnemySpawnProbs[SpawnEnemy.nbrOfPts];

	void Start()
	{
		var spawnStart = GameObject.FindGameObjectWithTag("SpawnLine");
		var spawnStartRenderer = spawnStart.GetComponent<SpriteRenderer>();

		SpawnEnemy.x = spawnStart.transform.position.x;
		SpawnEnemy.y = spawnStart.transform.position.y;
		SpawnEnemy.width = spawnStartRenderer.bounds.size.x;

		spawnTimeTracker = new TimeTracker(spawnIntervale);

		spawnBox = transform.GetChild(0).GetComponent<SpawnBox>();
		spawnBox.OnEnable += Enable;
		spawnBox.OnDisable += Disable;
	}

	void Update()
	{
		if (isEnabled)
		{
			if (isRandom)
			{
				SpawnAnEnemy();
			}
		}
	}

	private void OnDestroy()
	{
		spawnBox.OnEnable -= Enable;
		spawnBox.OnDisable -= Disable;
	}

	private void SpawnAnEnemy()
	{
		if (spawnTimeTracker.IsZero())
		{
			var spawn = SpawnEnemy.GetSpawn(probabilities);
			if (spawn.HasValue)
			{
				Instantiate(enemies[spawn.Value.enemy], spawn.Value.position, enemies[spawn.Value.enemy].transform.rotation);
			}
			spawnTimeTracker.Update();
		}
		else
		{
			spawnTimeTracker.Update();
		}
	}

	private void Enable()
	{
		isEnabled = true;
	}

	private void Disable()
	{
		isEnabled = false;
	}
}

