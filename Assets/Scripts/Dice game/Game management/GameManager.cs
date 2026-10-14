using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static VectorExtensions;

public class GameManager : MonoBehaviour
{
	[SerializeField] private BindingManager bindingManager;

	[SerializeField] private Transform cameraRotator;
    [SerializeField, Range(2, 4)] private int playersCount = 2;
	[SerializeField, Range(1, 5)] private int diceCount = 2;
	[SerializeField] private GamePlace[] gamePlaces = new GamePlace[4];
	[SerializeField] private GameMode gameMode = GameMode.Pig;

	[SerializeField] private string throwBindingKey = "throwBinding";
	[SerializeField] private string playersCountKey = "playersCount";
	[SerializeField] private string diceCountKey = "diceCount";
	[SerializeField] private InputActionAsset inputSystem;
	private InputAction throwAction;

	public List<Rigidbody> CurrDiceRbs => currGamePlace.DiceRbs;
	public int CurrPlayerScore => currGamePlace.OverallScore;

	private GamePlace currGamePlace;
	private int currPlayerNum;
	private bool isReadyToThrowing = true;

	public event Action OnDiceStop;
	public event Action OnRoundEnd;
	public event Action OnRoundStart;

	private void Awake()
	{
		if (gameMode == GameMode.Yahtzee)
			diceCount = 5;
		else
			if (PlayerPrefs.HasKey(diceCountKey))
			diceCount = PlayerPrefs.GetInt(diceCountKey);

		if (PlayerPrefs.HasKey(playersCountKey))
			playersCount = PlayerPrefs.GetInt(playersCountKey);

		for (var i = 0; i < playersCount; i++)
		{
			gamePlaces[i].gameObject.SetActive(true);
			gamePlaces[i].SpawnDice(diceCount);
		}

		currGamePlace = gamePlaces[0];
		currPlayerNum = 0;

		throwAction = inputSystem.FindAction("Throw Dice");

		var throwRebinding = PlayerPrefs.GetString(throwBindingKey);
		if (!string.IsNullOrEmpty(throwRebinding)) 
			inputSystem.LoadBindingOverridesFromJson(throwRebinding);
		
		bindingManager.SetBindingLabelText(
				$"Press {throwAction.bindings[0].effectivePath.Split('/')[1].ToUpper()} to throw");

		Subscribe();
		bindingManager.OnRebinding += Unsubscribe;
		bindingManager.OnEndRebinding += Subscribe;
	}

	private void OnEnable()
	{
		bindingManager.OnRebinding += Unsubscribe;
		bindingManager.OnEndRebinding += Subscribe;
	}

	private void OnDisable()
	{
		bindingManager.OnRebinding -= Unsubscribe;
		bindingManager.OnEndRebinding -= Subscribe;
	}

	private void Subscribe() => throwAction.performed += ThrowDice;

	private void Unsubscribe() => throwAction.performed -= ThrowDice;

	public void ThrowDice() => ThrowDice(new InputAction.CallbackContext());

	private void ThrowDice(InputAction.CallbackContext callback)
	{
		if (!isReadyToThrowing)
			return;

		currGamePlace.ThrowDice();
		isReadyToThrowing = false;
		StartCoroutine(WaitForStop());
	}

	public void EndRound()
	{
		OnRoundEnd?.Invoke();
		if (!isReadyToThrowing)
			return;

		currPlayerNum++;
		currPlayerNum %= playersCount;
		currGamePlace = gamePlaces[currPlayerNum];

		isReadyToThrowing = false;
		StartCoroutine(SelectNextPlayer());
	}

	public void FinishGame()
	{
		isReadyToThrowing = false;
	}

	private IEnumerator WaitForStop()
	{
		do
		{
			isReadyToThrowing = true;
			foreach (var diceRb in currGamePlace.DiceRbs)
				if (!diceRb.IsSleeping())
					isReadyToThrowing = false;
			yield return new WaitForSeconds(0.2f);
		}
		while (!isReadyToThrowing);

		OnDiceStop?.Invoke();
		yield return new WaitForEndOfFrame();
	}

	private IEnumerator SelectNextPlayer()
	{
		yield return new WaitForSeconds(0.3f);

		var currAngle = cameraRotator.rotation.eulerAngles.y;
		var targetAngle = currGamePlace.CameraRotationAngle;

		while (Math.Max(currAngle, targetAngle) - Math.Min(currAngle, targetAngle) > 1e-1)
		{
			yield return new WaitForFixedUpdate();
			currAngle = Mathf.Lerp(currAngle, targetAngle, 0.1f);
			cameraRotator.rotation = Quaternion.Euler(0f, currAngle, 0f);
		}

		OnRoundStart?.Invoke();
		isReadyToThrowing = true;
	}

	public void AddPointsToPlayer(int score) => currGamePlace.OverallScore += score;
}

public enum GameMode
{
	Pig, 
	Yahtzee
}