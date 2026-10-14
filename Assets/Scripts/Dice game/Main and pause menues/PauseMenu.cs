using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private bool isOnPause = false;

    [SerializeField] private string sceneForLoadingPause;

    [SerializeField] private GameObject pauseMenu;

    [SerializeField] private GameObject pauseButton;

    [SerializeField] private InputActionAsset inputSystem;

    private InputAction pauseButtonPerforming;

    private void Awake()
    {
        pauseButtonPerforming = inputSystem.FindAction("Switch Pause");
        Time.timeScale = 1.0f;
    }

    void Update()
    {
        var isPauseButtonPressed = pauseButtonPerforming.WasPerformedThisFrame();
		if (isPauseButtonPressed && isOnPause == false)
            Pause();
        else if(isPauseButtonPressed && isOnPause == true)
            NormalTimestep();
    }

    private void OnEnable() => pauseButtonPerforming.Enable();

	private void OnDisable() => pauseButtonPerforming.Disable();

	public void NormalTimestep()
    {
		Time.timeScale = 1f;
		isOnPause = false;
        pauseMenu.SetActive(false);
        pauseButton.SetActive(true);
	}

	public void Pause()
	{
		Time.timeScale = 0f;
		isOnPause = true;
		pauseMenu.SetActive(true);
        pauseButton.SetActive(false);
	}

	public void GoToMainMenu() => SceneManager.LoadScene(sceneForLoadingPause);
}
