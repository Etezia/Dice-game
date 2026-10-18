using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static PlayerPrefsKeyManager;

public class GameMenuManager : MonoBehaviour
{
    [SerializeField] private string gameScene;

	public int DiceCount { get; private set; } = 5;
	public int PlayersCount { get; private set; } = 4;
	public int MaxScoreValue { get; private set; } = 100;

	private void Awake()
	{
		DiceCount = PlayerPrefs.GetInt(DiceCountKey);
		PlayersCount = PlayerPrefs.GetInt(PlayersCountKey);
		MaxScoreValue = PlayerPrefs.GetInt(MaxScoreKey);
	}

	public void StartPigGame()
	{
		PlayerPrefs.SetInt(GameModeKey, (int)GameMode.Pig);
		SceneManager.LoadScene(gameScene);
	}

	public void StartYahtzeeGame()
	{
		PlayerPrefs.SetInt(DiceCountKey, 5);
		PlayerPrefs.SetInt(GameModeKey, (int)GameMode.Yahtzee);
		SceneManager.LoadScene(gameScene);
	}

	public void StartExperimentalGame()
	{
		PlayerPrefs.SetInt(GameModeKey, (int)GameMode.Experimental);
		SceneManager.LoadScene(gameScene);
	}

	public void RefreshDiceCountValue(float value)
	{
		DiceCount = Mathf.RoundToInt(value);
		SaveDiceCountValue();
	}

	public void RefreshPlayersCountValue(float value)
	{
		PlayersCount = Mathf.RoundToInt(value);
		SavePlayersCountValue();
	}

	public void RefreshMaxScoreValue(float value)
	{
		MaxScoreValue = Mathf.RoundToInt(value);
		SaveMaxScoreValue();
		print($"score: {MaxScoreValue}");
	}

	private void SaveDiceCountValue() => PlayerPrefs.SetInt(DiceCountKey, DiceCount);

	private void SavePlayersCountValue() => PlayerPrefs.SetInt(PlayersCountKey, PlayersCount);

	private void SaveMaxScoreValue() => PlayerPrefs.SetInt(MaxScoreKey, MaxScoreValue);

	public void Exit() => Application.Quit();
}
