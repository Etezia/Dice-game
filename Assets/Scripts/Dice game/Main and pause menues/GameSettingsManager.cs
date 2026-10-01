using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSettingsManager : MonoBehaviour
{

    public Slider SoundVolume;

    [SerializeField] private string soundsKey;

    public Dropdown ScreenResolution;
    public Dropdown GraphicsQuality;

    [SerializeField] private string resolutionKey;
	[SerializeField] private string graphicsKey;

	public bool IsFullscreen = true;
	public Toggle FullscreenModeActivator;
	[SerializeField] private string fullscreenKey;

	void Start()
    {
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


    public void _saveSoundsSet()
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

	public void SaveGraphicsQuakitySet()
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
		IsFullscreen = bool.Parse(PlayerPrefs.GetString(fullscreenKey));
		SaveScreenResolutionSet();
	}

	public void _ResetAllSettings()
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

	private void Update()
	{
		Debug.Log("Sounds:" + PlayerPrefs.GetFloat(soundsKey));
		Debug.Log("Resolution:" + PlayerPrefs.GetInt(resolutionKey));
		Debug.Log("Graphics:" + PlayerPrefs.GetInt(graphicsKey));
		Debug.Log("Fullscreen:" + PlayerPrefs.GetString(fullscreenKey));
		Debug.Log(QualitySettings.GetQualityLevel());
	}
}
