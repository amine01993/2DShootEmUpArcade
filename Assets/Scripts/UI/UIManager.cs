using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager
{
	GameObject pauseMenu;
	GameObject options;
	GameObject titleCanvas;
	GameObject highScoreCanvas;

	Button continueBtn;
	Button restartBtn;
	Button optionsBtn;
	Button backToTitleBtn;
	Button backToPauseMenuBtn;

	TextMeshProUGUI sfxValue;
	TextMeshProUGUI musicValue;
	TextMeshProUGUI panStereoValue;
	TextMeshProUGUI masterVolumeValue;

	Slider sfxSlider;
	Slider musicSlider;
	Slider panStereoSlider;
	Slider masterVolumeSlider;

	TextMeshProUGUI highScoreValue;
	TextMeshProUGUI currentScoreValue;

	public UIManager(GameObject gameObject)
	{
		titleCanvas = gameObject.transform.GetChild(2).gameObject;
		highScoreCanvas = gameObject.transform.GetChild(3).gameObject;

		pauseMenu = gameObject.transform.GetChild(0).gameObject;

		continueBtn = pauseMenu.transform.GetChild(0).GetChild(1).GetComponent<Button>();
		restartBtn = pauseMenu.transform.GetChild(0).GetChild(2).GetComponent<Button>();
		optionsBtn = pauseMenu.transform.GetChild(0).GetChild(3).GetComponent<Button>();
		backToTitleBtn = pauseMenu.transform.GetChild(0).GetChild(4).GetComponent<Button>();

		options = gameObject.transform.GetChild(1).gameObject;

		sfxValue = options.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>();
		musicValue = options.transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>();
		panStereoValue = options.transform.GetChild(0).GetChild(3).GetChild(1).GetComponent<TextMeshProUGUI>();
		masterVolumeValue = options.transform.GetChild(0).GetChild(4).GetChild(1).GetComponent<TextMeshProUGUI>();

		sfxSlider = options.transform.GetChild(0).GetChild(1).GetChild(2).GetComponent<Slider>();
		musicSlider = options.transform.GetChild(0).GetChild(2).GetChild(2).GetComponent<Slider>();
		panStereoSlider = options.transform.GetChild(0).GetChild(3).GetChild(2).GetComponent<Slider>();
		masterVolumeSlider = options.transform.GetChild(0).GetChild(4).GetChild(2).GetComponent<Slider>();

		backToPauseMenuBtn = options.transform.GetChild(0).GetChild(5).GetComponent<Button>();

		highScoreValue = highScoreCanvas.transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>();
		currentScoreValue = highScoreCanvas.transform.GetChild(0).GetChild(3).GetComponent<TextMeshProUGUI>();

		continueBtn.onClick.AddListener(ContinuePlay);
		restartBtn.onClick.AddListener(RestartGame);
		optionsBtn.onClick.AddListener(ShowOptionsMenu);
		backToTitleBtn.onClick.AddListener(BackToTitle);

		backToPauseMenuBtn.onClick.AddListener(BackToMenu);

		sfxSlider.onValueChanged.AddListener(OnSfxVolumeChange);
		musicSlider.onValueChanged.AddListener(OnMusicVolumeChange);
		panStereoSlider.onValueChanged.AddListener(OnPanStereoVolumeChange);
		masterVolumeSlider.onValueChanged.AddListener(OnMasterVolumeChange);
	}

	public void StartGame()
	{
		Debug.Log("StartGame");
		titleCanvas.SetActive(false);
		SceneManager.LoadScene("Game");
	}

	public void ShowPauseMenu()
	{
		options.SetActive(false);
		pauseMenu.SetActive(true);
		Time.timeScale = 0f;
	}

	public void BackToMenu()
	{
		if (SceneManager.GetActiveScene().name == "Game")
		{
			options.SetActive(false);
			pauseMenu.SetActive(true);
			Time.timeScale = 0f;
		}
		else
		{
			options.SetActive(false);
			Time.timeScale = 1f;
		}
	}

	public void ContinuePlay()
	{
		options.SetActive(false);
		pauseMenu.SetActive(false);
		Time.timeScale = 1.0f;
	}

	public void RestartGame()
	{
		pauseMenu.SetActive(false);
		SceneManager.LoadScene("Game");
		Time.timeScale = 1.0f;
	}

	public void ShowOptionsMenu()
	{
		options.SetActive(true);
		pauseMenu.SetActive(false);
		Time.timeScale = 0.0f;
	}

	public void BackToTitle()
	{
		titleCanvas.SetActive(true);
		pauseMenu.SetActive(false);
		SceneManager.LoadScene("Title");
		Time.timeScale = 1.0f;
	}

	public void OnSfxVolumeChange(float val)
	{
		MainController.Instance.soundManager.SetSoundVolume(val);
		sfxValue.text = val.ToString("F2");
	}

	public void OnMusicVolumeChange(float val)
	{
		MainController.Instance.soundManager.SetMusicVolume(val);
		musicValue.text = val.ToString("F2");
	}

	public void OnPanStereoVolumeChange(float val)
	{
		MainController.Instance.soundManager.SetPanSetero(val);
		panStereoValue.text = val.ToString("F2");
	}

	public void OnMasterVolumeChange(float val)
	{
		MainController.Instance.soundManager.SetMasterVolume(val);
		masterVolumeValue.text = val.ToString("F2");
	}

	public bool IsPaused() { return Time.timeScale == 0f; }

	public void SetScores(int score)
	{
		currentScoreValue.text = score.ToString();

		int highScore = PlayerPrefs.GetInt("HighScore", 0);

		if (highScore > score)
		{
			highScoreValue.text = highScore.ToString();
		}
		else
		{
			highScoreValue.text = score.ToString();
			PlayerPrefs.SetInt("HighScore", score);
		}
	}

	public void ShowHighScore()
	{
		highScoreCanvas.SetActive(true);
	}

	public void HideHighScore()
	{
		highScoreCanvas.SetActive(false);
	}
}
