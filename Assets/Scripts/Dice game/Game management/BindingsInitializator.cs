using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerPrefsKeyManager;

public class BindingsInitializator : MonoBehaviour
{
	[SerializeField] private InputActionAsset inputSystem;

	private void OnEnable()
	{
		if (PlayerPrefs.HasKey(ThrowBindingKey))
			inputSystem.LoadBindingOverridesFromJson(PlayerPrefs.GetString(ThrowBindingKey));
	}
}
