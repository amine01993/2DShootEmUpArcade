using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameOver : MonoBehaviour
{
	private Button restartGame;
	private Button exit;

	public UIDocument uiDoc;

	private void OnEnable()
	{
		restartGame = uiDoc.rootVisualElement.Q<Button>("restart");
		exit = uiDoc.rootVisualElement.Q<Button>("exit");

		restartGame.RegisterCallback<ClickEvent>(RestartGame);
		exit.RegisterCallback<ClickEvent>(Exit);
	}

	private void OnDisable()
	{
		restartGame.UnregisterCallback<ClickEvent>(RestartGame);
		exit.UnregisterCallback<ClickEvent>(Exit);
	}

	private void RestartGame(ClickEvent evt)
	{
		Debug.Log("RestartGame");
		SceneManager.LoadScene(0);
		gameObject.SetActive(false);
		Time.timeScale = 1;
	}

	private void Exit(ClickEvent evt)
	{
		Debug.Log("Exit");
#if UNITY_STANDALONE
		Application.Quit();
#endif
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#endif
	}
}
