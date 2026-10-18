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

	[SerializeField] private Button rebindButton;
	[SerializeField] private TMP_Text bindingText;

	[SerializeField] private InputActionAsset inputSystem;

	[SerializeField] private TMP_Text winScoreText;
	[SerializeField] private TMP_Text currPlayerScoreText;

	private void Awake()
	{
		rebindButton.onClick.AddListener(bindingManager.Rebind);

		bindingManager.OnRebinding += OnRebinding;
		bindingManager.OnEndRebinding += OnEndrebinding;

		var throwAction = inputSystem.FindAction("Throw Dice");

		bindingText.text =
			$"Press {throwAction.bindings[0].effectivePath.Split('/')[1].ToUpper()} to throw";

		if (PlayerPrefs.HasKey(MaxScoreKey))
			winScoreText.text = PlayerPrefs.GetInt(MaxScoreKey).ToString();
		else
			winScoreText.text = scoreManager.WinScore.ToString();

		scoreManager.OnScoreChange += RefreshScore;
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
}