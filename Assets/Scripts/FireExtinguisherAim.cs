using UnityEngine;

public class FireExtinguisherAim : MonoBehaviour
{
    [Header("Spray Point")]
    public Transform sprayPoint;

    [Header("Detection")]
    public float sprayRange = 10f;

    [Header("Debug")]
    public bool showRay = true;

    public bool IsHittingFire()
    {
        if (sprayPoint == null)
            return false;

        Ray ray = new Ray(
            sprayPoint.position,
            sprayPoint.forward
        );

        if (Physics.Raycast(
            ray,
            out RaycastHit hit,
            sprayRange))
        {
            if (hit.collider.CompareTag("Fire"))
            {
                return true;
            }
        }

        return false;
    }

    public FireController GetHitFire()
    {
        if (sprayPoint == null)
            return null;

        Ray ray = new Ray(
            sprayPoint.position,
            sprayPoint.forward
        );

        if (Physics.Raycast(
            ray,
            out RaycastHit hit,
            sprayRange))
        {
            if (hit.collider.CompareTag("Fire"))
            {
                return hit.collider.GetComponentInParent<FireController>();
            }
        }

        return null;
    }

    private void Update()
    {
        if (showRay && sprayPoint != null)
        {
            Debug.DrawRay(
                sprayPoint.position,
                sprayPoint.forward * sprayRange,
                Color.red
            );
        }
    }
}