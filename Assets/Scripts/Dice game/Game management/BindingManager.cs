using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static PlayerPrefsKeyManager;

public class BindingManager : MonoBehaviour
{
    [SerializeField] private TMP_Text bindingText;
    [SerializeField] private Button rebindButton;

    [SerializeField] private InputActionAsset inputSystem;
    private InputActionRebindingExtensions.RebindingOperation rebindingOperation;
	private InputAction throwAction;

	private void Awake()
	{
		throwAction = inputSystem.FindAction("Throw Dice");
	}

	public void Rebind()
    {
		OnRebinding.Invoke();
		throwAction.Disable();
		bindingText.text = "Press any button to rebind";
		rebindButton.interactable = false;
		rebindingOperation = throwAction.PerformInteractiveRebinding().OnComplete(operation => EndRebind());
		rebindingOperation.Start();
    }

	private void EndRebind()
	{
		rebindingOperation.Dispose();
		throwAction.Enable();
		SetBindingLabelText($"Press {throwAction.bindings[0].effectivePath.Split('/')[1].ToUpper()} to throw");
		rebindButton.interactable = true;
		OnEndRebinding.Invoke();

		PlayerPrefs.SetString(ThrowBindingKey, inputSystem.SaveBindingOverridesAsJson());
	}

	public void SetBindingLabelText(string text) => bindingText.text = text;

	public event Action OnRebinding;
	public event Action OnEndRebinding;
}
