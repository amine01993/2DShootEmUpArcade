using UnityEngine;
using UnityEngine.UI;

public class PauseMenuHandler : MonoBehaviour
{
	Button pauseBtn;
	Button playBtn;

	void Start()
	{
		pauseBtn = transform.GetChild(0).GetComponent<Button>();
		playBtn = transform.GetChild(1).GetComponent<Button>();

		pauseBtn.onClick.AddListener(OnPauseClick);
		playBtn.onClick.AddListener(OnPlayClick);
	}

	private void Update()
	{
		if(MainController.Instance.uiManager.IsPaused()) {
			pauseBtn.gameObject.SetActive(false);
			playBtn.gameObject.SetActive(true);
		}
		else {
			pauseBtn.gameObject.SetActive(true);
			playBtn.gameObject.SetActive(false);
		}
	}

	private void OnDestroy()
	{
		pauseBtn.onClick.RemoveListener(OnPauseClick);
		playBtn.onClick.RemoveListener(OnPlayClick);
	}

	void OnPauseClick()
	{
		MainController.Instance.uiManager.ShowPauseMenu();
	}

	void OnPlayClick()
	{
		MainController.Instance.uiManager.ContinuePlay();
	}
}
