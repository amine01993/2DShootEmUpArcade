using UnityEngine;


public class CollectibleSpawnManager : MonoBehaviour
{
	private SpawnCollectible handler;

	public float spawnProbabilty = .3f;
	public float[] typeProbabilies = { .5f, .1f, .4f };

	public GameObject collectible;


	private void Start()
	{
		handler = new(spawnProbabilty, typeProbabilies);
		DropCollectibleOnDeath.OnDeath += SpawnACollectible;
	}

	private void OnDestroy()
	{
		DropCollectibleOnDeath.OnDeath -= SpawnACollectible;
	}

	private void SpawnACollectible(Vector3 position)
	{
		var spawn = handler.GetSpawn();

		if (spawn != null)
		{
			var collectibleInstance = Instantiate(collectible);
			collectibleInstance.transform.position = position;

			var collectibleObj = collectibleInstance.GetComponent<Collectible>();
			collectibleObj.Init(spawn.Value);
		}
	}
}
