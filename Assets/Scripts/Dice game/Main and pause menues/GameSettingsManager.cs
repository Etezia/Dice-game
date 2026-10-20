using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static PlayerPrefsKeyManager;

public class GameSettingsManager : MonoBehaviour
{

    [SerializeField] private Slider soundVolumeSlider;

	[SerializeField] private TMP_Dropdown screenResolution;
	[SerializeField] private TMP_Dropdown graphicsQuality;

	[SerializeField] private bool isFullscreen = true;
	[SerializeField] private Toggle fullscreenModeActivator;

	private void Awake()
    {
        if (!PlayerPrefs.HasKey(MusicVolumeKey))
        {
			PlayerPrefs.SetFloat(MusicVolumeKey, 0.5f);
            soundVolumeSlider.value = 0.5f;
		}
        else
        {
            soundVolumeSlider.value = PlayerPrefs.GetFloat(MusicVolumeKey);
        }

		if (!PlayerPrefs.HasKey(ResolutionKey))
		{
			PlayerPrefs.SetInt(ResolutionKey, 2);
			screenResolution.value = 2;
			//SaveScreenResolutionSet();
		}
		else
		{
			screenResolution.value = PlayerPrefs.GetInt(ResolutionKey);
		}

		if (!PlayerPrefs.HasKey(GraphicsKey))
		{
			PlayerPrefs.SetInt(GraphicsKey, 1);
			graphicsQuality.value = 1;
			//SaveGraphicsQuakitySet();
		}
		else
		{
			graphicsQuality.value = PlayerPrefs.GetInt(GraphicsKey);
		}

		if (!PlayerPrefs.HasKey(FullscreenKey))
		{
			PlayerPrefs.SetString(FullscreenKey, "true");
			fullscreenModeActivator.isOn = true;
			isFullscreen = true;
		}
		else
		{
			fullscreenModeActivator.isOn = bool.Parse(PlayerPrefs.GetString(FullscreenKey)); 
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

	public void SaveSoundsSet()
    {
        PlayerPrefs.SetFloat(MusicVolumeKey, soundVolumeSlider.value);
    }

	public void SaveScreenResolutionSet()
	{
		switch (screenResolution.value)
		{
			case 0:
				Screen.SetResolution(1024, 768, isFullscreen);
				break;
			case 1:
				Screen.SetResolution(1280, 720, isFullscreen);
				break;
			case 2:
				Screen.SetResolution(1366, 768, isFullscreen);
				break;
			case 3:
				Screen.SetResolution(2560, 1440, isFullscreen);
				break;
			case 4:
				Screen.SetResolution(3840, 2160, isFullscreen);
				break;
		}

		PlayerPrefs.SetInt(ResolutionKey, screenResolution.value);
		//	Debug.Log("Saved");
	}

	public void SaveGraphicsQualitySet()
	{
		switch (graphicsQuality.value)
		{
			case 0:
				QualitySettings.SetQualityLevel(0);
				PlayerPrefs.SetInt(GraphicsKey, graphicsQuality.value);
				break;
			case 1:
				QualitySettings.SetQualityLevel(1);
				PlayerPrefs.SetInt(GraphicsKey, graphicsQuality.value);
				break;
			case 2:
				QualitySettings.SetQualityLevel(2);
				PlayerPrefs.SetInt(GraphicsKey, graphicsQuality.value);
				break;
		}
		//Debug.Log("Saved");
	}

	public void FullscreenSet()
	{
		PlayerPrefs.SetString(FullscreenKey, Convert.ToString(fullscreenModeActivator.isOn));
		isFullscreen = fullscreenModeActivator.isOn;
		SaveScreenResolutionSet();
	}

	public void ResetAllSettings()
    {
        //PlayerPrefs.DeleteAll();
        PlayerPrefs.SetFloat(MusicVolumeKey, 0.5f);
		PlayerPrefs.SetInt(ResolutionKey, 2);
		PlayerPrefs.SetInt(GraphicsKey, 1);
		PlayerPrefs.SetString(FullscreenKey, "true");

		soundVolumeSlider.value = 0.5f;
		screenResolution.value = 2;
		graphicsQuality.value = 1;
		fullscreenModeActivator.isOn = true;

		Screen.SetResolution(1366, 768, isFullscreen);
    }
}
