using UnityEngine;

public class SpawnCollectible
{
	private float spawnProbabilty;
	private float[] typeProbabilies;

	public SpawnCollectible(float spawnProbabilty, float[] typeProbabilies)
	{
		this.spawnProbabilty = spawnProbabilty;
		this.typeProbabilies = typeProbabilies;
	}

	public CollectibleType? GetSpawn()
	{
		var rand = Random.value;

		if (rand <= spawnProbabilty)
		{
			var type = GetCollectibleType();
			return (CollectibleType)type;
		}

		return null;
	}

	private int GetCollectibleType()
	{
		var randt = Random.value;

		float lowerBound;
		float upperBound = 0f;

		for (var i = 0; i < typeProbabilies.Length; i++)
		{
			lowerBound = upperBound;
			upperBound += typeProbabilies[i];

			if (randt >= lowerBound && randt <= upperBound)
			{
				return i;
			}
		}

		return 0;
	}
}