using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static UnityEngine.Timeline.DirectorControlPlayable;

public class MainController : MonoBehaviour
{
	public SoundManager soundManager;
	public UIManager uiManager;
	InputAction pauseAction;

	public static MainController Instance;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
			soundManager = new SoundManager(gameObject);
			uiManager = new UIManager(gameObject);
			pauseAction = InputSystem.actions.FindAction("Pause");

			soundManager.PlayMusic("Main Music");
		}
		else
		{
			Destroy(gameObject);
		}
	}

	private void Update()
	{
		if (pauseAction.WasPressedThisFrame())
		{
			if (Instance.uiManager.IsPaused())
			{
				Instance.uiManager.ContinuePlay();
			}
			else
			{
				if (SceneManager.GetActiveScene().name == "Game")
				{
					Instance.uiManager.ShowPauseMenu();
				}
				else if (SceneManager.GetActiveScene().name == "Title")
				{
					Instance.uiManager.ShowOptionsMenu();
				}
			}
		}
	}

	public void StartGame()
	{
		uiManager.StartGame();
	}

	public void ShowOptionsMenu()
	{
		uiManager.ShowOptionsMenu();
	}
}
