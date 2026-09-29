using UnityEngine;

public class PPEEquipment : MonoBehaviour
{
    [Header("Equipment")]
    public string equipmentName;

    [Header("Sound")]
    public AudioClip pickupSound;

    [Range(0f, 1f)]
    public float pickupVolume = 1f;

    public void SelectEquipment()
    {
        PPETrainingManager manager =
            FindFirstObjectByType<PPETrainingManager>();

        if (manager == null)
            return;

        // Play pickup sound as 2D audio
        if (pickupSound != null)
        {
            GameObject soundObject = new GameObject("PPE_Pickup_Sound");

            AudioSource soundSource =
                soundObject.AddComponent<AudioSource>();

            soundSource.clip = pickupSound;
            soundSource.volume = pickupVolume;
            soundSource.spatialBlend = 0f; // 2D audio
            soundSource.playOnAwake = false;

            soundSource.Play();

            Destroy(
                soundObject,
                pickupSound.length + 0.1f
            );
        }

        // Tell manager PPE was collected
        manager.CollectPPE();

        // Hide PPE
        gameObject.SetActive(false);
    }
}