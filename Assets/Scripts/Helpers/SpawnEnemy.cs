
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnEnemy

{
	public static readonly int nbrOfPts = 9;
	private static Dictionary<float, float> selected = new();

	public static float x;
	public static float y;
	public static float width;

	public static void ResetPosition(float X)
	{
		float nPos = (X - x + width / 2) * (nbrOfPts - 1) / width;
		var pair = selected.FirstOrDefault(pair => pair.Value == X);
		selected.Remove(pair.Key);
	}

	public static (int enemy, Vector3 position)? GetSpawn(EnemySpawnProbs[] spawnProbabilties)
	{
		float? nPos;
		var position = GetRandomPredefinedPosition(out nPos);
		if (!position.HasValue) return null;

		int enemy = GetEnemy(spawnProbabilties, (int)nPos.Value);
		return (enemy, position.Value);
	}

	private static Vector3? GetRandomPredefinedPosition(out float? nPos)
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

	private static int GetEnemy(EnemySpawnProbs[] spawnProbabilties, int positionIndex)
	{
		float rand = Random.value;
		float lowerBound = 0f;
		float upperBound = 0f;

		for (var i = 0; i < spawnProbabilties.Length; i++)
		{
			lowerBound = upperBound;
			upperBound += spawnProbabilties[positionIndex][i];

			if (rand >= lowerBound && rand <= upperBound)
			{
				return i;
			}
		}
		return 0;
	}

	private static float GetAlternativePosition()
	{
		var unselected = new int[nbrOfPts - selected.Count];
		var k = 0;

		for (var p = 0; p < nbrOfPts; p++)
		{
			if (!selected.ContainsKey(p))
			{
				unselected[k++] = p;
			}
		}

		var rand = Random.Range(0, unselected.Length);

		return unselected[rand];
	}
}
