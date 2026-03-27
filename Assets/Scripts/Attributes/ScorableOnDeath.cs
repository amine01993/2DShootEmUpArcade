using System;
using UnityEngine;

[RequireComponent(typeof(CanTakeDamage))]
public class ScorableOnDeath : MonoBehaviour
{
	private CanTakeDamage canTakeDamage;
	private Score scoreUi;

	public int score;

	void Start()
	{
		canTakeDamage = GetComponent<CanTakeDamage>();

		var scoreObject = GameObject.FindGameObjectWithTag("Score");
		scoreUi = scoreObject.GetComponent<Score>();
	}

	private void OnDestroy()
	{
		if (canTakeDamage.healthPoints <= 0)
		{
			scoreUi.AddScore(score);
		}
	}
}
