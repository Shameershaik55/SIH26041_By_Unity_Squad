using UnityEngine;

public class FireExtinguisherPickup : MonoBehaviour
{
    [Header("Hold Point")]
    public Transform holdPoint;

    [Header("Spray Button")]
    public GameObject sprayButton;

    private bool isPickedUp = false;

    private void Start()
    {
        // Hide spray button when training starts
        if (sprayButton != null)
            sprayButton.SetActive(false);
    }

    public void Pickup()
    {
        if (isPickedUp)
            return;

        if (holdPoint == null)
        {
            Debug.LogError("Extinguisher Hold Point is not assigned.");
            return;
        }

        isPickedUp = true;

        // Attach extinguisher to camera
        transform.SetParent(holdPoint);

        // Reset position and rotation
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        // Show spray button
        if (sprayButton != null)
            sprayButton.SetActive(true);

        Debug.Log("Fire extinguisher picked up.");
    }
}