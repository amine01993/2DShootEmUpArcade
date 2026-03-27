using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class Score : MonoBehaviour
{
	private TextMeshProUGUI textElem;
	public int score = 0;
	public int maxScoreLength = 6;

	void Start()
	{
		textElem = GetComponent<TextMeshProUGUI>();
		textElem.SetText(GetScore());
	}

	public void AddScore(int scored)
	{
		score += scored;
		textElem.SetText(GetScore());
	}

	private string GetScore()
	{
		return score.ToString().PadLeft(maxScoreLength, '0');
	}
}
