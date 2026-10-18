using UnityEngine;
using static PlayerPrefsKeyManager;

[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour
{
	private static MusicPlayer instance;

    [SerializeField] private AudioSource musicSource;
	[SerializeField] private AudioClip[] musicClips;
	[SerializeField,
	Range(0f, 1f)] private float defaultMusicVolume = 0.5f;

	private int currClipId = 0;

	private void Awake()
	{
		if (instance == null)
			instance = this;
		else
			Destroy(this.gameObject);

		if (PlayerPrefs.HasKey(MusicVolumeKey))
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
		musicSource.volume = PlayerPrefs.GetFloat(MusicVolumeKey);
	}

	public void PlayNextTrack()
	{
		musicSource.clip = musicClips[currClipId];
		musicSource.Play();
		currClipId = (currClipId + 1) % musicClips.Length;
	}
}
