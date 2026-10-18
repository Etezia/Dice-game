using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePresetMenuUI : MonoBehaviour
{
	[SerializeField] private GameMenuManager gameMenuManager;

	[SerializeField] private Slider diceCountSlider;
	[SerializeField] private Slider playersCountSlider;
	[SerializeField] private Slider maxScoreSlider;

	[SerializeField] private TMP_InputField diceCountField;
	[SerializeField] private TMP_InputField playersCountField;
	[SerializeField] private TMP_InputField maxScoreField;

	private void Awake()
	{
		diceCountSlider?.onValueChanged.AddListener(gameMenuManager.RefreshDiceCountValue);
		playersCountSlider?.onValueChanged.AddListener(gameMenuManager.RefreshPlayersCountValue);
		maxScoreSlider?.onValueChanged.AddListener(gameMenuManager.RefreshMaxScoreValue);

		if (diceCountField != null)
		{
			RefreshDiceCountField(gameMenuManager.DiceCount);
			diceCountSlider.onValueChanged.AddListener(RefreshDiceCountField); 
		}
		if (playersCountField != null)
		{
			RefreshPlayersCountField(gameMenuManager.PlayersCount);
			playersCountSlider.onValueChanged.AddListener(RefreshPlayersCountField);
		}
		if (maxScoreField != null)
		{
			RefreshMaxScoreField(gameMenuManager.MaxScoreValue);
			maxScoreSlider.onValueChanged.AddListener(RefreshMaxScoreField);
		}

		if (diceCountSlider != null)
			diceCountSlider.value = gameMenuManager.DiceCount;
		if (playersCountSlider != null)
			playersCountSlider.value = gameMenuManager.PlayersCount;
		if (maxScoreSlider != null)
			maxScoreSlider.value = gameMenuManager.MaxScoreValue;
	}

	private void RefreshDiceCountField(float value) => diceCountField.text = value.ToString();

	private void RefreshPlayersCountField(float value) => playersCountField.text = value.ToString();

	private void RefreshMaxScoreField(float value) => maxScoreField.text = value.ToString();
}
