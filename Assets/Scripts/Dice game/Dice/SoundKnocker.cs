using UnityEngine;

[RequireComponent(typeof(AudioSource), typeof(Rigidbody), typeof(Collider))]
public class SoundKnocker : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip knockSound;

	private float maxVolume = 0.7f;
	[SerializeField] private float volumeCoeff = 0.012f;

	private void Awake()
	{
		audioSource = GetComponent<AudioSource>();
		audioSource.clip = knockSound;
		audioSource.playOnAwake = false;
		audioSource.loop = false;
		audioSource.volume = 0.4f;
		audioSource.spatialBlend = 1.0f;
	}

	private void OnCollisionEnter(Collision collision)
	{
		audioSource.volume = collision.impulse.magnitude * volumeCoeff * maxVolume;
		audioSource.Play();
	}
}
