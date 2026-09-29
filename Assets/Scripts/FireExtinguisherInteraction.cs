using UnityEngine;
using UnityEngine.InputSystem;

public class FireExtinguisherInteraction : MonoBehaviour
{
    [Header("Camera")]
    public Camera mainCamera;

    private void Update()
    {
        if (Touchscreen.current == null)
            return;

        if (Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            Vector2 touchPosition =
                Touchscreen.current.primaryTouch.position.ReadValue();

            TryPickup(touchPosition);
        }
    }

    private void TryPickup(Vector2 screenPosition)
    {
        if (mainCamera == null)
            return;

        Ray ray = mainCamera.ScreenPointToRay(screenPosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            FireExtinguisherPickup extinguisher =
                hit.collider.GetComponentInParent<FireExtinguisherPickup>();

            if (extinguisher != null)
            {
                extinguisher.Pickup();
            }
        }
    }
}