using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
	[SerializeField] private GameMenuManager gameMenuManager;
	[SerializeField] private MusicPlayer musicPlayer;

	[SerializeField] private Button startPigGameButton;
	[SerializeField] private Button startYahtzeeGameButton;
	//[SerializeField] private Button startExperimentalGameButton;
	[SerializeField] private Button exitGameButton;

	[SerializeField] private Slider musicVolumeSlider;

	private void Awake()
	{
		startPigGameButton.onClick.AddListener(gameMenuManager.StartPigGame);
		startYahtzeeGameButton.onClick.AddListener(gameMenuManager.StartYahtzeeGame);
		//startExperimentalGameButton.onClick.AddListener(gameMenuManager.StartExperimentalGame);
		exitGameButton.onClick.AddListener(gameMenuManager.Exit);
	}
}