using UnityEngine;

public class VirtualCameraJoystick : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 1.5f;

    [Header("Joystick")]
    public FixedJoystick joystick;

    [Header("Camera")]
    public Transform cameraTransform;

    [Header("Collision")]
    public float playerRadius = 0.3f;
    public float collisionDistance = 0.2f;
    public LayerMask collisionLayers;

    [Header("Movement Sound")]
    public AudioClip movementSound;
    [Range(0f, 1f)]
    public float movementVolume = 1f;

    private AudioSource movementAudioSource;

    [HideInInspector]
    public bool movementLocked = false;

    private void Start()
    {
        // Create AudioSource automatically
        movementAudioSource = gameObject.AddComponent<AudioSource>();

        movementAudioSource.playOnAwake = false;
        movementAudioSource.loop = true;
        movementAudioSource.spatialBlend = 0f; // 2D sound
        movementAudioSource.volume = movementVolume;

        if (movementSound != null)
        {
            movementAudioSource.clip = movementSound;
        }
    }

    private void Update()
    {
        if (movementLocked)
        {
            StopMovementSound();
            return;
        }

        if (joystick == null || cameraTransform == null)
        {
            StopMovementSound();
            return;
        }

        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        // Keep movement horizontal
        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 movement =
            forward * vertical +
            right * horizontal;

        if (movement.magnitude < 0.01f)
        {
            StopMovementSound();
            return;
        }

        movement.Normalize();

        float moveAmount =
            Mathf.Clamp01(
                new Vector2(horizontal, vertical).magnitude
            )
            * moveSpeed
            * Time.deltaTime;

        // Check collision
        if (IsBlocked(movement, moveAmount))
        {
            StopMovementSound();
            return;
        }

        // Move virtual environment
        transform.position -= movement * moveAmount;

        // Play movement sound
        PlayMovementSound();
    }

    private bool IsBlocked(Vector3 direction, float distance)
    {
        return Physics.SphereCast(
            cameraTransform.position,
            playerRadius,
            direction,
            out RaycastHit hit,
            distance + collisionDistance,
            collisionLayers,
            QueryTriggerInteraction.Ignore
        );
    }

    private void PlayMovementSound()
    {
        if (movementSound == null)
            return;

        if (!movementAudioSource.isPlaying)
        {
            movementAudioSource.Play();
        }
    }

    private void StopMovementSound()
    {
        if (movementAudioSource != null &&
            movementAudioSource.isPlaying)
        {
            movementAudioSource.Stop();
        }
    }

    // Lock joystick movement
    public void LockMovement()
    {
        movementLocked = true;

        StopMovementSound();

        Debug.Log("MOVEMENT LOCKED");
    }

    // Unlock joystick movement
    public void UnlockMovement()
    {
        movementLocked = false;

        Debug.Log("MOVEMENT UNLOCKED");
    }
}