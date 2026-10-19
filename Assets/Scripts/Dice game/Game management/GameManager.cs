using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static VectorExtensions;
using static PlayerPrefsKeyManager;
using TMPro;

public class GameManager : MonoBehaviour
{
	[SerializeField] private BindingManager bindingManager;

	[SerializeField] private Transform cameraRotator;
    [SerializeField, Range(2, 4)] private int playersCount = 2;
	[SerializeField, Range(1, 5)] private int diceCount = 2;
	[SerializeField] private PlayerPlace[] gamePlaces = new PlayerPlace[4];
	[SerializeField] private GameMode gameMode = GameMode.Pig;

	[SerializeField] private InputActionAsset inputSystem;
	private InputAction throwAction;

	private PlayerPlace currGamePlace;
	private int currPlayerNum;
	private bool isReadyToThrowing = true;

	public List<Rigidbody> CurrDiceRbs => currGamePlace.DiceRbs;
	public int CurrPlayerScore => currGamePlace.OverallScore;
	public string CurrPlayernNme => currGamePlace.PlayerName;

	public event Action OnDiceStop;
	public event Action OnRoundEnd;
	public event Action OnRoundStart;
	public event Action OnGameStart;
	public event Action OnGameEnd;

	private void Awake()
	{
		if (PlayerPrefs.HasKey(GameModeKey))
			gameMode = (GameMode)PlayerPrefs.GetInt(GameModeKey);

		if (PlayerPrefs.HasKey(DiceCountKey))
			diceCount = PlayerPrefs.GetInt(DiceCountKey);

		if (PlayerPrefs.HasKey(PlayersCountKey))
			playersCount = PlayerPrefs.GetInt(PlayersCountKey);

		for (var i = 0; i < playersCount; i++)
		{
			gamePlaces[i].gameObject.SetActive(true);
			gamePlaces[i].SpawnDice(diceCount);
		}

		currGamePlace = gamePlaces[0];
		currPlayerNum = 0;

		throwAction = inputSystem.FindAction("Throw Dice");
	}

	private void OnEnable()
	{
		Subscribe();

		bindingManager.OnRebinding += Unsubscribe;
		bindingManager.OnEndRebinding += Subscribe;

		OnGameStart?.Invoke();
	}

	private void OnDisable()
	{
		Unsubscribe();

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
		OnGameEnd?.Invoke();
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