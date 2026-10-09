using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
	[SerializeField] private AudioClip[] musicClips;
	[SerializeField] private float defaultMusicVolume = 0.5f;
	[SerializeField] private string musicVolumeKey;

	private int currClipId = 0;

	private void Awake()
	{
		if (PlayerPrefs.HasKey(musicVolumeKey))
			SetMusicPlayerParams();
		else
			musicSource.volume = defaultMusicVolume;

		musicSource.loop = false;
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
		musicSource.volume = PlayerPrefs.GetFloat(musicVolumeKey);
	}

	public void PlayNextTrack()
	{
		musicSource.clip = musicClips[currClipId];
		musicSource.Play();
		currClipId = (currClipId + 1) % musicClips.Length;
	}
}
