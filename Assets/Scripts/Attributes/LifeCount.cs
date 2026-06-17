using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LifeCount : MonoBehaviour
{
	public int maxLifes = 5;
	public int remainingLifes = 5;
	public TextMeshProUGUI lifeCountText;

	void Start()
	{
		UpdateLifeCountUI();
		CanTakeDamage.OnPlayerDeath += DecrementLifeCount;
	}

	private void Update()
	{
		if(remainingLifes < 0) {
			var scoreScript = transform.parent.Find("Score").GetComponent<Score>();
			MainController.Instance.uiManager.SetScores(scoreScript.score);
			SceneManager.LoadScene("GameOver");
			MainController.Instance.uiManager.ShowHighScore();
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
