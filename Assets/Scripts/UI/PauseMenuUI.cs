using UnityEngine;
using UnityEngine.UI;

public class PauseMenuUI : MonoBehaviour
{
	[SerializeField] private GameObject pauseMenuObjs;
	[SerializeField] private GameObject gameUI;
	[SerializeField] private Button pauseButton;
	[SerializeField] private Button closePauseMenuButton;

	[SerializeField] private PauseMenu pauseMenuManager;

	private void Awake()
	{
		pauseButton.onClick.AddListener(pauseMenuManager.Pause);
		closePauseMenuButton.onClick.AddListener(pauseMenuManager.Resume);
	}

	private void OnEnable()
	{
		pauseMenuManager.OnPause += OnPause;
		pauseMenuManager.OnResume += OnResume;
	}

	private void OnDisable()
	{
		pauseMenuManager.OnPause -= OnPause;
		pauseMenuManager.OnResume -= OnResume;
	}

	private void OnPause()
	{
		pauseMenuObjs.SetActive(true);
		gameUI.SetActive(false);
	}

	private void OnResume()
	{
		pauseMenuObjs.SetActive(false);
		gameUI.SetActive(true);
	}
}