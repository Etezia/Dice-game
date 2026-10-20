using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
using static PlayerPrefsKeyManager;

public class GameUI : MonoBehaviour
{
	[SerializeField] private BindingManager bindingManager;
	[SerializeField] private ScoreManager scoreManager;
	[SerializeField] private GameManager gameManager;

	[SerializeField] private Button rebindButton;
	[SerializeField] private TMP_Text bindingText;

	[SerializeField] private Button throwButton;
	[SerializeField] private Button nextPlayerButton;
	[SerializeField] private Button scorePointsButton;

	[SerializeField] private InputActionAsset inputSystem;

	[SerializeField] private TMP_Text winScoreText;
	[SerializeField] private TMP_Text currPlayerScoreText;
	[SerializeField] private TMP_Text diceRollsLeft;

	[SerializeField] private TMP_Text playerName;
	[SerializeField] private TMP_Text winnerLabel;

	private void Awake()
	{
		rebindButton.onClick.AddListener(bindingManager.Rebind);
		throwButton.onClick.AddListener(gameManager.ThrowDice);
		nextPlayerButton.onClick.AddListener(gameManager.EndRound);
		scorePointsButton.onClick.AddListener(scoreManager.ScoreCurrPoints);
		scorePointsButton.onClick.AddListener(gameManager.EndRound);

		if (PlayerPrefs.HasKey(ThrowBindingKey))
			inputSystem.LoadBindingOverridesFromJson(PlayerPrefs.GetString(ThrowBindingKey));
		var throwAction = inputSystem.FindAction("Throw Dice");

		bindingText.text =
			$"Press {throwAction.bindings[0].effectivePath.Split('/')[1].ToUpper()} to throw";

		if (PlayerPrefs.HasKey(MaxScoreKey))
			winScoreText.text = PlayerPrefs.GetInt(MaxScoreKey).ToString();
		else
			winScoreText.text = scoreManager.WinScore.ToString();
	}

	private void OnEnable()
	{
		bindingManager.OnRebinding += OnRebinding;
		bindingManager.OnEndRebinding += OnEndrebinding;

		scoreManager.OnScoreChange += RefreshScore;

		gameManager.OnGameStart += ChangeCurrPlayer;
		gameManager.OnGameStart += RefreshDiceRolls;
		gameManager.OnRoundStart += ChangeCurrPlayer;
		gameManager.OnRoundStart += RefreshDiceRolls;
		gameManager.OnGameEnd += AnnounceWinners;
		gameManager.OnDiceRoll += RefreshDiceRolls;
	}

	private void OnDisable()
	{
		bindingManager.OnRebinding -= OnRebinding;
		bindingManager.OnEndRebinding -= OnEndrebinding;

		scoreManager.OnScoreChange -= RefreshScore;

		gameManager.OnGameStart -= ChangeCurrPlayer;
		gameManager.OnGameStart -= RefreshDiceRolls;
		gameManager.OnRoundStart -= ChangeCurrPlayer;
		gameManager.OnRoundStart -= RefreshDiceRolls;
		gameManager.OnGameEnd -= AnnounceWinners;
		gameManager.OnDiceRoll -= RefreshDiceRolls;
	}

	private void OnRebinding()
	{
		bindingText.text = "Press any button to rebind";
		rebindButton.interactable = false;
	}

	private void OnEndrebinding()
	{
		bindingText.text = 
			$"Press {bindingManager.ThrowAction.bindings[0].effectivePath.Split('/')[1].ToUpper()} to throw";
		rebindButton.interactable = true;
	}

	private void RefreshScore() => 
		currPlayerScoreText.text = (scoreManager.CurrPlayerScore + scoreManager.CurrRoundScore).ToString();

	private void ChangeCurrPlayer() => playerName.text = gameManager.CurrPlayernNme;

	private void AnnounceWinners()
	{
		winnerLabel.gameObject.SetActive(true);
		winnerLabel.text = $"CONGRATULATIONS! {gameManager.CurrPlayernNme} IS WINNER!";
	}

	private void RefreshDiceRolls() => diceRollsLeft.text = gameManager.DiceRollsLeft.ToString();
}