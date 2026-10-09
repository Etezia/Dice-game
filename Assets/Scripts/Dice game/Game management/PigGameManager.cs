using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using static VectorExtensions;

public class PigGameManager : MonoBehaviour
{
    [SerializeField] private PigScoreManager scoreManager;
	[SerializeField] private BindingManager bindingManager;

	[SerializeField] private Transform cameraRotator;
    [SerializeField, Range(2, 4)] private int playerCount = 2;
	[SerializeField, Range(1, 5)] private int diceCount = 2;
	[SerializeField] private GamePlace[] gamePlaces = new GamePlace[4];

	[SerializeField] private string throwBindingKey = "throwBinding";
	[SerializeField] private InputActionAsset inputSystem;
	private InputAction throwAction;

	private GamePlace currGamePlace;
	private int currPlayerNum;
	private bool isReadyToThrowing = true;

	private int currDiceScore;
	private int totalScore;

	private void Awake()
	{
		for (var i = 0; i < playerCount; i++)
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

		currGamePlace.ThrowDices();
		isReadyToThrowing = false;
		StartCoroutine(WaitForStopAndCount());
	}

	public void SelectNextPlayer()
	{
		if (!isReadyToThrowing)
			return;

		currPlayerNum++;
		currPlayerNum %= playerCount;
		currGamePlace = gamePlaces[currPlayerNum];

		scoreManager.ResetRoundScore();

		isReadyToThrowing = false;
		StartCoroutine(RotateCamera());
	}

	private void CountScore()
	{
		currDiceScore = 0;
		
	}

	private IEnumerator WaitForStopAndCount()
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

		scoreManager.ResetRoundScore();
		scoreManager.GetScoresByRotation(currGamePlace.DiceRbs);
		yield return new WaitForEndOfFrame();
	}

	private IEnumerator RotateCamera()
	{
		var currAngle = cameraRotator.rotation.eulerAngles.y;
		var targetAngle = currGamePlace.CameraRotationAngle;

		while (Math.Max(currAngle, targetAngle) - Math.Min(currAngle, targetAngle) > 1e-1)
		{
			yield return new WaitForFixedUpdate();
			currAngle = Mathf.Lerp(currAngle, targetAngle, 0.1f);
			cameraRotator.rotation = Quaternion.Euler(0f, currAngle, 0f);
		}

		isReadyToThrowing = true;
	}
}
