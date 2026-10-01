using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private bool isOnPause = false;

    [SerializeField] private string sceneForLoadingPause;

    [SerializeField] private Image _pauseMenu;

    [SerializeField] private GameObject _pauseButton;

    void Start()
    {
        Time.timeScale = 1.0f;
    }

    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.Escape) && isOnPause == false)
        {
            Pause();
		}
        else if(Input.GetKeyDown(KeyCode.Escape) && isOnPause == true)
        {
            NormalTimestep();
        }
        
    }

    public void NormalTimestep()
    {
		Time.timeScale = 1f;
		isOnPause = false;
        _pauseMenu.gameObject.SetActive(false);
        _pauseButton.SetActive(true);
	}

	public void Pause()
	{
		Time.timeScale = 0f;
		isOnPause = true;
		_pauseMenu.gameObject.SetActive(true);
        _pauseButton.SetActive(false);
	}

	public void GoToMainMenu()
    {
        SceneManager.LoadScene(sceneForLoadingPause);
    }
}
