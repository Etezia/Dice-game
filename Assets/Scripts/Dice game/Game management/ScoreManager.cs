using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static ScoreCombinationsCounter;
using static VectorExtensions;
using static PlayerPrefsKeyManager;

public class ScoreManager : MonoBehaviour
{
	[SerializeField] private GameMode scoresCountMode;
    [SerializeField] private GameManager gameManager;

    [SerializeField] private AxisToScoreAccordance scoresAccordance;
    [field: SerializeField] public int WinScore { get; private set; } = 100;

	public int CurrPlayerScore { get; private set; }
	public int CurrRoundScore { get; private set; }

	public event Action OnScoreChange;

	private void Awake()
	{
		if (PlayerPrefs.HasKey(GameModeKey))
			scoresCountMode = (GameMode)PlayerPrefs.GetInt(GameModeKey);

		if (PlayerPrefs.HasKey(MaxScoreKey))
			WinScore = PlayerPrefs.GetInt(MaxScoreKey);
	}

	private void OnEnable()
	{
		gameManager.OnDiceStop += CountDiceScores;
		gameManager.OnRoundStart += RefreshPlayerData;
		gameManager.OnRoundEnd += ResetRoundScore;
	}

	private void OnDisable()
	{
		gameManager.OnDiceStop -= CountDiceScores;
		gameManager.OnRoundStart -= RefreshPlayerData;
		gameManager.OnRoundEnd -= ResetRoundScore;
	}

	private void RefreshPlayerData()
	{
		CurrPlayerScore = gameManager.CurrPlayerScore;
		OnScoreChange?.Invoke();
	}

	public void ScoreCurrPoints() => gameManager.AddPointsToPlayer(CurrRoundScore);

	private void ResetRoundScore()
    {
		CurrRoundScore = 0;
	}

	private void CheckIsMaxScoreGained()
	{
		if (CurrPlayerScore + CurrRoundScore >= WinScore)
		{
			gameManager.FinishGame();
			enabled = false;
		}
	}

	private void CountDiceScores()
    {
		ResetRoundScore();

		var diceRbs = gameManager.CurrDiceRbs;
		var currPlayerScore = gameManager.CurrPlayerScore;
		var diceScores = new List<int>();

		foreach (var rb in diceRbs)
        {
            var rbTransform = rb.transform;

			if (DotWithUpVector(rbTransform.up) > 1f - 1e-2)
				diceScores.Add(scoresAccordance.Up);
			else if (DotWithUpVector(-rbTransform.up) > 1f - 1e-2)
				diceScores.Add(scoresAccordance.Down);
			else if (DotWithUpVector(rbTransform.forward) > 1f - 1e-2)
				diceScores.Add(scoresAccordance.Forward);
			else if (DotWithUpVector(-rbTransform.forward) > 1f - 1e-2)
				diceScores.Add(scoresAccordance.Back);
			else if (DotWithUpVector(rbTransform.right) > 1f - 1e-2)
				diceScores.Add(scoresAccordance.Right);
			else if (DotWithUpVector(-rbTransform.right) > 1f - 1e-2)
				diceScores.Add(scoresAccordance.Left);
		}

		if (scoresCountMode == GameMode.Pig)
			CurrRoundScore = GetPigGameScore(diceScores);
		else
			CurrRoundScore = GetYahtzeeGameScore(diceScores);

		OnScoreChange?.Invoke();
		CheckIsMaxScoreGained();
	}
}