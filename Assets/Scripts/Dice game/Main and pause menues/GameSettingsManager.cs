using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameSettingsManager : MonoBehaviour
{

    [SerializeField] private Slider SoundVolume;
    [SerializeField] private string soundsKey;

	[SerializeField] private TMP_Dropdown ScreenResolution;
	[SerializeField] private TMP_Dropdown GraphicsQuality;

    [SerializeField] private string resolutionKey;
	[SerializeField] private string graphicsKey;

	[SerializeField] private bool IsFullscreen = true;
	[SerializeField] private Toggle FullscreenModeActivator;
	[SerializeField] private string fullscreenKey;

	[SerializeField] private string cubeThrowingBinding;
	[SerializeField] private string cubeThrowingKey;

	private InputSystem_Actions inputActions;

	private void Awake()
    {
		inputActions = new InputSystem_Actions();

        if (!PlayerPrefs.HasKey(soundsKey))
        {
			PlayerPrefs.SetFloat(soundsKey, 0.5f);
            SoundVolume.value = 0.5f;
		}
        else
        {
            SoundVolume.value = PlayerPrefs.GetFloat(soundsKey);
        }

		if (!PlayerPrefs.HasKey(resolutionKey))
		{
			PlayerPrefs.SetInt(resolutionKey, 2);
			ScreenResolution.value = 2;
			//SaveScreenResolutionSet();
		}
		else
		{
			ScreenResolution.value = PlayerPrefs.GetInt(resolutionKey);
		}

		if (!PlayerPrefs.HasKey(graphicsKey))
		{
			PlayerPrefs.SetInt(graphicsKey, 1);
			GraphicsQuality.value = 1;
			//SaveGraphicsQuakitySet();
		}
		else
		{
			GraphicsQuality.value = PlayerPrefs.GetInt(graphicsKey);
		}

		if (!PlayerPrefs.HasKey(fullscreenKey))
		{
			PlayerPrefs.SetString(fullscreenKey, "true");
			FullscreenModeActivator.isOn = true;
			IsFullscreen = true;
		}
		else
		{
			FullscreenModeActivator.isOn = bool.Parse(PlayerPrefs.GetString(fullscreenKey)); 
		}
	}

	private void Update()
	{
		//Debug.Log("Sounds:" + PlayerPrefs.GetFloat(soundsKey));
		//Debug.Log("Resolution:" + PlayerPrefs.GetInt(resolutionKey));
		//Debug.Log("Graphics:" + PlayerPrefs.GetInt(graphicsKey));
		//Debug.Log("Fullscreen:" + PlayerPrefs.GetString(fullscreenKey));
		//Debug.Log(QualitySettings.GetQualityLevel());
	}

	private void OnEnable() => inputActions.InGameActions.Enable();

	private void OnDisable() => inputActions.InGameActions.Disable();

	public void SaveSoundsSet()
    {
        PlayerPrefs.SetFloat(soundsKey, SoundVolume.value);
    }

	public void SaveScreenResolutionSet()
	{
		switch (ScreenResolution.value)
		{
			case 0:
				Screen.SetResolution(1024, 768, IsFullscreen);
				PlayerPrefs.SetInt(resolutionKey, ScreenResolution.value);
				break;
			case 1:
				Screen.SetResolution(1280, 720, IsFullscreen);
				PlayerPrefs.SetInt(resolutionKey, ScreenResolution.value);
				break;
			case 2:
				Screen.SetResolution(1366, 768, IsFullscreen);
				PlayerPrefs.SetInt(resolutionKey, ScreenResolution.value);
				break;
			case 3:
				Screen.SetResolution(2560, 1440, IsFullscreen);
				PlayerPrefs.SetInt(resolutionKey, ScreenResolution.value);
				break;
			case 4:
				Screen.SetResolution(3840, 2160, IsFullscreen);
				PlayerPrefs.SetInt(resolutionKey, ScreenResolution.value);
				break;
		}
		Debug.Log("Saved");
	}

	public void SaveGraphicsQualitySet()
	{
		switch (GraphicsQuality.value)
		{
			case 0:
				QualitySettings.SetQualityLevel(0);
				PlayerPrefs.SetInt(graphicsKey, GraphicsQuality.value);
				break;
			case 1:
				QualitySettings.SetQualityLevel(1);
				PlayerPrefs.SetInt(graphicsKey, GraphicsQuality.value);
				break;
			case 2:
				QualitySettings.SetQualityLevel(2);
				PlayerPrefs.SetInt(graphicsKey, GraphicsQuality.value);
				break;
		}
		Debug.Log("Saved");
	}

	public void FullscreenSet()
	{
		PlayerPrefs.SetString(fullscreenKey, Convert.ToString(FullscreenModeActivator.isOn));
		IsFullscreen = FullscreenModeActivator.isOn;
		SaveScreenResolutionSet();
	}

	public void ResetAllSettings()
    {
        //PlayerPrefs.DeleteAll();
        PlayerPrefs.SetFloat(soundsKey, 0.5f);
		PlayerPrefs.SetInt(resolutionKey, 2);
		PlayerPrefs.SetInt(graphicsKey, 1);
		PlayerPrefs.SetString(fullscreenKey, "true");

		SoundVolume.value = 0.5f;
		ScreenResolution.value = 2;
		GraphicsQuality.value = 1;
		FullscreenModeActivator.isOn = true;

		Screen.SetResolution(1366, 768, IsFullscreen);
    }

	public void RebindCubeThrowing()
	{
		
	}
}
