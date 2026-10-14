using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static ScoreCombinationsCounter;

public class PigScoreManager : MonoBehaviour
{
	[SerializeField] private GameMode scoresCountMode;

    [SerializeField] private GameManager gameManager;

    [SerializeField] private AxisToScoreAccordance scoresAccordance;
    [SerializeField] private int winScore;

	[SerializeField] private string winScoreKey = "winScore";

	[SerializeField] private TMP_Text winScoreText;
	[SerializeField] private TMP_Text currPlayerScoreText;

	private int currPlayerScore;
	private int currRoundScore;

	private void Awake()
	{
		if (PlayerPrefs.HasKey(winScoreKey))
			winScore = PlayerPrefs.GetInt(winScoreKey);
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
		currPlayerScore = gameManager.CurrPlayerScore;
		RefreshScore();
	}

	public void ScoreCurrPoints()
	{
		gameManager.AddPointsToPlayer(currRoundScore);
		RefreshPlayerData();
	}

	private void ResetRoundScore()
    {
		currRoundScore = 0;
        RefreshScore();
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
			currRoundScore = GetPigGameScore(diceScores);
		else
			currRoundScore = GetYahtzeeGameScore(diceScores);
		RefreshScore();
    }

    private float DotWithUpVector(Vector3 vector) => Vector3.Dot(Vector3.up, vector);

    private void RefreshScore() => currPlayerScoreText.text = (currPlayerScore + currRoundScore).ToString();
}
