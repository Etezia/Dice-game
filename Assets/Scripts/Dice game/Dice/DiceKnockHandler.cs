using UnityEngine;
using static PlayerPrefsKeyManager;

[RequireComponent(typeof(AudioSource), typeof(Rigidbody), typeof(Collider))]
public class DiceKnockHandler : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip knockSound;

	private const float MaxVolume = 0.7f;
	private const float VolumeCoeff = 0.012f;

	private float soundVolume;

	private void Awake()
	{
		soundVolume = PlayerPrefs.GetFloat(MusicVolumeKey);

		audioSource = GetComponent<AudioSource>();
		audioSource.clip = knockSound;
		audioSource.playOnAwake = false;
		audioSource.loop = false;
		audioSource.spatialBlend = 1.0f;
	}

	private void OnCollisionEnter(Collision collision)
	{
		audioSource.volume = collision.impulse.magnitude * VolumeCoeff * MaxVolume * soundVolume;
		audioSource.Play();
	}
}
