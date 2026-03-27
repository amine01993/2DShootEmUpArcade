using UnityEngine;
using UnityEngine.Splines;

public class SpawnMeteor
{
	private float spawnProbability;
	private float[] spawnProbabilities;

	public SpawnMeteor(float spawnProbability, float[] spawnProbabilities)
	{
		this.spawnProbability = spawnProbability;
		this.spawnProbabilities = spawnProbabilities;
	}

	public (int index, Vector3 position, Vector3 direction)? GetSpawn(Spline splineLine, GameObject spawnLine)
	{
		var rand = Random.value;

		if (rand <= spawnProbability)
		{
			var index = GetIndex();
			var position = GetInitialPosition(splineLine, spawnLine);
			var direction = GetDirection(position);

			return (index, position, direction);
		}

		return null;
	}

	private Vector3 GetInitialPosition(Spline splineLine, GameObject spawnLine)
	{
		var initialPositionF = SplineUtility.EvaluatePosition(splineLine, Random.value);
		var initialPosition = new Vector3(initialPositionF.x, initialPositionF.y, initialPositionF.z);
		initialPosition = spawnLine.transform.TransformPoint(initialPosition);
		return initialPosition;
	}

	private Vector3 GetDirection(Vector3 initialPosition)
	{
		var direction = Vector3.zero - initialPosition;
		var angle = Random.Range(-20, 20);
		direction = Quaternion.AngleAxis(angle, Vector3.forward) * direction;
		return direction.normalized;
	}

	private int GetIndex()
	{
		float rand = Random.value;
		float lowerBound;
		float upperBound = 0f;

		for (var i = 0; i < spawnProbabilities.Length; ++i)
		{
			lowerBound = upperBound;
			upperBound += spawnProbabilities[i];

			if (rand >= lowerBound && rand <= upperBound)
			{
				return i;
			}
		}
		return 0;
	}
}

