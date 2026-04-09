using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameOver : MonoBehaviour
{
	private VisualElement container;
	private Button restartGame;
	private Button exit;

	public UIDocument uiDoc;

	private void OnEnable()
	{
		restartGame = uiDoc.rootVisualElement.Q<Button>("restart");
		exit = uiDoc.rootVisualElement.Q<Button>("exit");

		restartGame.RegisterCallback<ClickEvent>(RestartGame);
		exit.RegisterCallback<ClickEvent>(Exit);

		container = uiDoc.rootVisualElement.Q<VisualElement>("container");
		container.style.opacity = 1;
		container.style.translate = new Translate(0, 0, 0);
	}

	private void OnDisable()
	{
		restartGame.UnregisterCallback<ClickEvent>(RestartGame);
		exit.UnregisterCallback<ClickEvent>(Exit);

		container.style.opacity = 0;
		container.style.translate = new Translate(0, 100, 0);
	}

	private void RestartGame(ClickEvent evt)
	{
		//Time.timeScale = 1;
		//gameObject.SetActive(false);
		SceneManager.LoadScene(0);
	}

	private void Exit(ClickEvent evt)
	{
#if UNITY_STANDALONE
		Application.Quit();
#endif
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#endif
	}
}
