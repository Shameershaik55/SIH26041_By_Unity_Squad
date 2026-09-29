using UnityEngine;

public class FireController : MonoBehaviour
{
    [Header("Fire Settings")]
    public float maxFireHealth = 100f;
    public float currentFireHealth = 100f;

    [Header("Extinguishing")]
    public float extinguishRate = 25f;

    [Header("Fire Effects")]
    public ParticleSystem fireParticle;
    public ParticleSystem smokeParticle;

    private bool extinguished = false;

    private FireTrainingManager trainingManager;

    private void Start()
    {
        currentFireHealth = maxFireHealth;
        extinguished = false;

        // Find the fire training manager
        trainingManager =
            FindFirstObjectByType<FireTrainingManager>();

        // Start fire
        if (fireParticle != null)
        {
            fireParticle.gameObject.SetActive(true);
            fireParticle.Play();
        }

        // Hide smoke initially
        if (smokeParticle != null)
        {
            smokeParticle.gameObject.SetActive(false);
        }
    }

    public void Extinguish(float amount)
    {
        if (extinguished)
            return;

        currentFireHealth -= amount;

        currentFireHealth =
            Mathf.Clamp(
                currentFireHealth,
                0f,
                maxFireHealth
            );

        Debug.Log(
            gameObject.name +
            " Fire Health: " +
            currentFireHealth
        );

        if (currentFireHealth <= 0f)
        {
            ExtinguishFire();
        }
    }

    private void ExtinguishFire()
    {
        if (extinguished)
            return;

        extinguished = true;

        // Stop fire
        if (fireParticle != null)
        {
            fireParticle.Stop(
                true,
                ParticleSystemStopBehavior.StopEmittingAndClear
            );

            fireParticle.gameObject.SetActive(false);
        }

        // Start smoke
        if (smokeParticle != null)
        {
            smokeParticle.gameObject.SetActive(true);
            smokeParticle.Play();
        }

        Debug.Log(
            "🔥 " +
            gameObject.name +
            " EXTINGUISHED!"
        );

        // Tell manager to check all fires
        if (trainingManager != null)
        {
            trainingManager.CheckAllFires();
        }
    }

    public bool IsExtinguished()
    {
        return extinguished;
    }
}