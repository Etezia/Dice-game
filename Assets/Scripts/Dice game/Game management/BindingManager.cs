using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static PlayerPrefsKeyManager;

public class BindingManager : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputSystem;
    private InputActionRebindingExtensions.RebindingOperation rebindingOperation;
	public InputAction ThrowAction { get; private set; }

	public event Action OnRebinding;
	public event Action OnEndRebinding;

	private void Awake() => ThrowAction = inputSystem.FindAction("Throw Dice");

	public void Rebind()
    {
		OnRebinding?.Invoke();
		ThrowAction.Disable();
		rebindingOperation = ThrowAction.PerformInteractiveRebinding()
			.WithCancelingThrough("<Keyboard>/escape")
			.OnComplete(operation => EndRebind())
			.OnCancel(operation => CancelRebinding());
		rebindingOperation.Start();
    }

	private void EndRebind()
	{
		rebindingOperation.Dispose();
		ThrowAction.Enable();
		OnEndRebinding?.Invoke();

		PlayerPrefs.SetString(ThrowBindingKey, inputSystem.SaveBindingOverridesAsJson());
	}

	private void CancelRebinding()
	{
		rebindingOperation.Dispose();
		ThrowAction.Enable();
		OnEndRebinding?.Invoke();
	}
}
