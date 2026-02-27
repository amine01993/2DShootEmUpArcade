using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
	private float spawnTimeTracker = 0f;

	public float spawnIntervale = 5f;

	public GameObject spawnStart;
	public GameObject spawnEnd;

	public GameObject[] enemies;

	void Start()
	{
		var spawnStartRenderer = spawnStart.GetComponent<SpriteRenderer>();

		SpawnBoxBounds.x = spawnStart.transform.position.x;
		SpawnBoxBounds.y = spawnStart.transform.position.y;
		SpawnBoxBounds.width = spawnStartRenderer.bounds.size.x;
	}

	void Update()
	{
		if (spawnTimeTracker == 0f)
		{
			var spawn = SpawnBoxBounds.GetSpawn();
			if (spawn.enemy.HasValue)
			{
				Instantiate(enemies[spawn.enemy.Value], spawn.position.Value, enemies[spawn.enemy.Value].transform.rotation);
			}
			UpdateSpawnTime();
		}
		else if (spawnTimeTracker > 0f)
		{
			UpdateSpawnTime();
		}
	}
	private void UpdateSpawnTime()
	{
		spawnTimeTracker += Time.deltaTime;
		if (spawnTimeTracker >= spawnIntervale)
		{
			spawnTimeTracker = 0f;
		}
	}
}

public class SpawnBoxBounds
{
	private static int nbrOfPts { get { return spawnProbabilties.GetLength(0); } }
	private static Dictionary<float, float> selected = new();

	public static float x;
	public static float y;
	public static float width;
	public static readonly float[,] spawnProbabilties = {
		{ .1f, 0f, .9f },
		{ .3f, 0f, .7f },
		{ .4f, .5f, .1f },
		{ .2f, .8f, .0f },
		{ .7f,.3f,.0f },
		{ .2f, .8f, .0f },
		{ .4f, .5f, .1f },
		{ .3f, 0f, .7f },
		{ .1f,0f,.9f },
	};

	public static Vector3? GetRandomPredefinedPosition(out float? nPos)
	{
		nPos = null;
		if (selected.Count == nbrOfPts) return null;

		var t = nPos = Mathf.Round(Random.Range(0, nbrOfPts - 1 - selected.Count));
		if (selected.ContainsKey(nPos.Value))
		{
			nPos = GetAlternativePosition();
		}

		var A = x - width / 2;
		var sPos = A + width / (nbrOfPts - 1) * nPos;

		selected.Add(nPos.Value, sPos.Value);
		return new Vector3(sPos.Value, y, 0f);
	}

	public static void ResetPosition(float X)
	{
		float nPos = (X - x + width / 2) * (nbrOfPts - 1) / width;
		var pair = selected.FirstOrDefault(pair => pair.Value == X);
		selected.Remove(pair.Key);
	}

	public static (int? enemy, Vector3? position) GetSpawn()
	{
		float? nPos;
		var position = GetRandomPredefinedPosition(out nPos);
		if (!position.HasValue) return (null, null);

		int enemy = GetEnemy((int)nPos.Value);
		return (enemy, position);
	}

	private static int GetEnemy(int positionIndex)
	{
		float rand = Random.value;
		float lowerBound = 0f;
		float upperBound = 0f;

		for (var i = 0; i < spawnProbabilties.GetLength(1); i++)
		{
			lowerBound = upperBound;
			upperBound += spawnProbabilties[positionIndex, i];

			if (rand >= lowerBound && rand <= upperBound)
			{
				return i;
			}
		}
		return 0;
	}

	private static float GetAlternativePosition() {
		var unselected = new int[nbrOfPts - selected.Count];
		var k = 0;

		for (var p = 0; p < nbrOfPts; p++)
		{
			if (!selected.ContainsKey(p)) {
				unselected[k++] = p;
			}
		}

		var rand = Random.Range(0, unselected.Length);

		return unselected[rand];
	}
}
