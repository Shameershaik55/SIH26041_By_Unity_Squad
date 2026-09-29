using UnityEngine;

public class FireExtinguisherSpray : MonoBehaviour
{
    [Header("Spray Particle")]
    public ParticleSystem sprayParticle;

    [Header("Fire Detection")]
    public FireExtinguisherAim aimSystem;

    [Header("Extinguishing")]
    public float extinguishRate = 25f;

    [Header("Spray Sound")]
    public AudioClip spraySound;

    [Range(0f, 1f)]
    public float sprayVolume = 1f;

    private AudioSource sprayAudioSource;

    private bool isSpraying = false;

    private void Start()
    {
        isSpraying = false;

        // Create AudioSource
        sprayAudioSource = gameObject.AddComponent<AudioSource>();

        sprayAudioSource.playOnAwake = false;
        sprayAudioSource.loop = true;
        sprayAudioSource.spatialBlend = 0f; // 2D sound
        sprayAudioSource.volume = sprayVolume;

        if (spraySound != null)
        {
            sprayAudioSource.clip = spraySound;
        }

        // Stop particle
        if (sprayParticle != null)
        {
            sprayParticle.Stop(
                true,
                ParticleSystemStopBehavior.StopEmittingAndClear
            );

            sprayParticle.gameObject.SetActive(false);
        }
    }

    public void StartSpraying()
    {
        Debug.Log("SPRAY BUTTON DOWN");

        if (sprayParticle == null)
        {
            Debug.LogError("Spray Particle is NOT assigned!");
            return;
        }

        isSpraying = true;

        // Start particle
        sprayParticle.gameObject.SetActive(true);
        sprayParticle.Play();

        // Start spray sound
        PlaySpraySound();
    }

    public void StopSpraying()
    {
        Debug.Log("SPRAY BUTTON UP");

        isSpraying = false;

        // Stop particle
        if (sprayParticle != null)
        {
            sprayParticle.Stop(
                true,
                ParticleSystemStopBehavior.StopEmittingAndClear
            );

            sprayParticle.gameObject.SetActive(false);
        }

        // Stop sound
        StopSpraySound();
    }

    private void PlaySpraySound()
    {
        if (spraySound == null)
        {
            Debug.LogWarning("Spray Sound is NOT assigned!");
            return;
        }

        if (sprayAudioSource != null &&
            !sprayAudioSource.isPlaying)
        {
            sprayAudioSource.Play();
        }
    }

    private void StopSpraySound()
    {
        if (sprayAudioSource != null &&
            sprayAudioSource.isPlaying)
        {
            sprayAudioSource.Stop();
        }
    }

    private void Update()
    {
        if (!isSpraying)
            return;

        if (aimSystem == null)
            return;

        if (aimSystem.IsHittingFire())
        {
            FireController fire =
                aimSystem.GetHitFire();

            if (fire != null)
            {
                fire.Extinguish(
                    extinguishRate * Time.deltaTime
                );
            }
        }
    }
}