using System;
using TMPro;
using UnityEngine;

public class LifeCount : MonoBehaviour
{
	public int maxLifes = 5;
	public int remainingLifes = 5;
	public TextMeshProUGUI lifeCountText;
	public GameObject gameOverUi;

	void Start()
	{
		UpdateLifeCountUI();
		CanTakeDamage.OnPlayerDeath += DecrementLifeCount;
	}

	private void Update()
	{
		if(remainingLifes < 0) {
			gameOverUi.SetActive(true);
			Time.timeScale = 0;
		}
	}

	private void OnDestroy()
	{
		CanTakeDamage.OnPlayerDeath -= DecrementLifeCount;
	}

	private void UpdateLifeCountUI()
	{
		lifeCountText.SetText($"x{Math.Max(remainingLifes, 0)}");
	}

	private int DecrementLifeCount()
	{
		--remainingLifes;
		UpdateLifeCountUI();
		return remainingLifes;
	}
}
