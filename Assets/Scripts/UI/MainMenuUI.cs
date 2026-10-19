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
	[SerializeField] private Button nextTrackButton;

	[SerializeField] private Slider musicVolumeSlider;

	private void Awake()
	{
		startPigGameButton.onClick.AddListener(gameMenuManager.StartPigGame);
		startYahtzeeGameButton.onClick.AddListener(gameMenuManager.StartYahtzeeGame);
		//startExperimentalGameButton.onClick.AddListener(gameMenuManager.StartExperimentalGame);
		exitGameButton.onClick.AddListener(gameMenuManager.Exit);
	}

	private void OnEnable()
	{
		musicPlayer = MusicPlayer.Instance;
		nextTrackButton.onClick.AddListener(musicPlayer.PlayNextTrack);
	}

	private void OnDisable()
	{
		nextTrackButton.onClick.RemoveListener(musicPlayer.PlayNextTrack);
	}
}