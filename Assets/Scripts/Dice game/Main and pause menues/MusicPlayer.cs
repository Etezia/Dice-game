using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
	[SerializeField] private float defaultMusicVolume = 0.5f;
	[SerializeField] private string musicVolumeKey;

	private void Awake()
	{
		if (PlayerPrefs.HasKey(musicVolumeKey))
			SetMusicPlayerParams();
		else
			musicSource.volume = defaultMusicVolume;
	}

	private void Reset()
	{
		musicSource = GetComponent<AudioSource>();
	}

	public void SetMusicPlayerParams()
	{
		musicSource.volume = PlayerPrefs.GetFloat(musicVolumeKey);
	}
}
