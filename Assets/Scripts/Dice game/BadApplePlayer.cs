using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using static PlayerPrefsKeyManager;

[RequireComponent(typeof(VideoPlayer), typeof(AudioSource))]
public class BadApplePlayer : MonoBehaviour
{
	[SerializeField] private string gameSceneName = "Dice game scene";

	[SerializeField] private Material badAppleMaterial;
	[SerializeField] private RenderTexture badAppleTexture;

    [SerializeField] VideoPlayer videoPlayer;
	[SerializeField] MusicPlayer musicPlayer;

	private bool badAppleShouldPlay = false;

	private void Awake()
	{
		videoPlayer.isLooping = false;
		videoPlayer.timeUpdateMode = VideoTimeUpdateMode.GameTime;
		videoPlayer.SetDirectAudioMute(0, true);
		videoPlayer.targetTexture = badAppleTexture;
		badAppleMaterial.mainTexture = null;

		musicPlayer.OnBadAppleStartPlaying += PlayBadApple;
		musicPlayer.OnBadAppleStopPlaying += StopPlayingBadApple;
		SceneManager.sceneLoaded += OnNewSceneLoad;
	}

	private void Reset()
	{
		videoPlayer = GetComponent<VideoPlayer>();
		musicPlayer = GetComponent<MusicPlayer>();
	}

	private void OnNewSceneLoad(Scene scene, LoadSceneMode mode)
	{
		if (scene.name != gameSceneName && videoPlayer.isPlaying)
			StopPlayer();
		else if(badAppleShouldPlay)
			PlayBadApple();
	}

	private void StopPlayer()
	{
		videoPlayer.Stop();
		badAppleMaterial.mainTexture = null;
	}

	private void PlayBadApple()
	{
		if (SceneManager.GetActiveScene().name == gameSceneName)
		{
			badAppleMaterial.mainTexture = badAppleTexture;
			videoPlayer.time = musicPlayer.Time;
			videoPlayer.Play();
		}

		badAppleShouldPlay = true;
	}

	private void StopPlayingBadApple()
	{
		videoPlayer.Stop();
		badAppleMaterial.mainTexture = null;
		badAppleShouldPlay = false;
	}
}
