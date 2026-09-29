using UnityEngine;

public class SafetyCheckpoint : MonoBehaviour
{
    [Header("Camera")]
    public Transform cameraTransform;

    [Header("Video")]
    public GameObject safetyVideoScreen;

    [Header("Video Controller")]
    public SafetyVideoController videoController;

    [Header("Joystick")]
    public VirtualCameraJoystick joystick;

    [Header("Checkpoint Collider")]
    public BoxCollider checkpointCollider;

    private bool triggered = false;

    private void Start()
    {
        if (safetyVideoScreen != null)
        {
            safetyVideoScreen.SetActive(false);
        }

        // Automatically find Box Collider if not assigned
        if (checkpointCollider == null)
        {
            checkpointCollider = GetComponent<BoxCollider>();
        }
    }

    private void Update()
    {
        if (triggered)
            return;

        if (cameraTransform == null)
            return;

        if (checkpointCollider == null)
            return;

        // Convert camera position into checkpoint local space
        Vector3 localCameraPosition =
            transform.InverseTransformPoint(cameraTransform.position);

        Vector3 center = checkpointCollider.center;
        Vector3 size = checkpointCollider.size;

        // Check X direction
        bool insideX =
            localCameraPosition.x >= center.x - size.x / 2f &&
            localCameraPosition.x <= center.x + size.x / 2f;

        // Check Z direction
        bool insideZ =
            localCameraPosition.z >= center.z - size.z / 2f &&
            localCameraPosition.z <= center.z + size.z / 2f;

        if (insideX && insideZ)
        {
            TriggerCheckpoint();
        }
    }

    private void TriggerCheckpoint()
    {
        triggered = true;

        Debug.Log("================================");
        Debug.Log("SAFETY CHECKPOINT REACHED");
        Debug.Log("MOVEMENT LOCKED");
        Debug.Log("================================");

        // Lock joystick
        if (joystick != null)
        {
            joystick.LockMovement();
        }

        // Position and show video screen
        if (safetyVideoScreen != null)
        {
            Transform screen = safetyVideoScreen.transform;

            // Put video 2 meters in front of camera
            screen.position =
                cameraTransform.position +
                cameraTransform.forward * 2.0f;

            // Make screen face camera
            screen.rotation =
                Quaternion.LookRotation(
                    screen.position -
                    cameraTransform.position
                );

            safetyVideoScreen.SetActive(true);

            Debug.Log("SAFETY VIDEO SHOWN");
        }
        else
        {
            Debug.LogError("Safety Video Screen is NOT assigned!");
        }

        // Start video controller
        if (videoController != null)
        {
            videoController.StartVideo();

            Debug.Log("SAFETY VIDEO STARTED");
        }
        else
        {
            Debug.LogError("Safety Video Controller is NOT assigned!");
        }
    }
}