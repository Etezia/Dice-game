using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private bool isOnPause = false;

    [SerializeField] private string sceneForLoadingPause;

    [SerializeField] private InputActionAsset inputSystem;
    private InputAction pauseButtonPerforming;

    public event Action OnPause;
    public event Action OnResume;

    private void Awake()
    {
        pauseButtonPerforming = inputSystem.FindAction("Switch Pause");
        Time.timeScale = 1.0f;
    }

    private void OnEnable()
    {
		pauseButtonPerforming.performed += ChangeGameState;
		pauseButtonPerforming.Enable();
	}

	private void OnDisable()
    {
        pauseButtonPerforming.performed -= ChangeGameState;
		pauseButtonPerforming.Disable();
	}

    private void ChangeGameState(InputAction.CallbackContext context)
    {
        print("callback");
        if (isOnPause)
            Resume();
        else
            Pause();
    }

	public void Resume()
    {
		Time.timeScale = 1f;
		isOnPause = false;
        OnResume?.Invoke();
	}

	public void Pause()
	{
		Time.timeScale = 0f;
		isOnPause = true;
		OnPause?.Invoke();
	}

	public void GoToMainMenu() => SceneManager.LoadScene(sceneForLoadingPause);
}