using UnityEngine;
using UnityEngine.InputSystem;

public class PPEInteraction1: MonoBehaviour
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

            TapObject(touchPosition);
        }
    }

    private void TapObject(Vector2 screenPosition)
    {
        if (mainCamera == null)
            return;

        Ray ray = mainCamera.ScreenPointToRay(screenPosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            // PPE
            PPEEquipment equipment =
                hit.collider.GetComponent<PPEEquipment>();

            if (equipment != null)
            {
                equipment.SelectEquipment();
                return;
            }

            // Door
            TrainingDoor door =
                hit.collider.GetComponent<TrainingDoor>();

            if (door != null)
            {
                door.OpenDoor();
            }
        }
    }
}