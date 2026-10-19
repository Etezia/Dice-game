using System;
using UnityEngine;
using static PlayerPrefsKeyManager;

[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour
{
	public static MusicPlayer Instance { get; private set; }

    [SerializeField] private AudioSource musicSource;
	[SerializeField] private AudioClip[] musicClips;
	[SerializeField,
	Range(0f, 1f)] private float defaultMusicVolume = 0.5f;

	[SerializeField] private string badAppleTrackName;

	private int currClipId = 0;

	public float Time => musicSource.time;

	public event Action OnBadAppleStartPlaying;
	public event Action OnBadAppleStopPlaying;

	private void Awake()
	{
		if (Instance == null)
			Instance = this;
		else
			Destroy(this.gameObject);

		if (PlayerPrefs.HasKey(MusicVolumeKey))
			SetMusicPlayerParams();
		else
			musicSource.volume = defaultMusicVolume;
		
		musicSource.loop = false;
	}

	private void Start()
	{
		if (musicClips != null && musicClips.Length > 0)
			PlayNextTrack();
	}

	private void Update()
	{
		if (!musicSource.isPlaying && musicClips != null && musicClips.Length > 0)
			PlayNextTrack();
	}

	private void Reset() => musicSource = GetComponent<AudioSource>();

	public void SetMusicPlayerParams()
	{
		musicSource.volume = PlayerPrefs.GetFloat(MusicVolumeKey);
	}

	public void PlayNextTrack()
	{
		var currTrack = musicSource.clip?.name;
		if (currTrack != null && currTrack == badAppleTrackName)
		{
			print("stop mus playing");
			OnBadAppleStopPlaying?.Invoke();
		}

		musicSource.clip = musicClips[currClipId];

		currTrack = musicSource.clip.name;
		if (currTrack == badAppleTrackName)
		{
			print("mus playing");
			OnBadAppleStartPlaying?.Invoke();
		}

		musicSource.Play();
		currClipId = (currClipId + 1) % musicClips.Length;
	}
}