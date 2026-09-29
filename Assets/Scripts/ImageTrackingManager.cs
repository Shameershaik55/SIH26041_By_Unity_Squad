using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ImageTrackingManager : MonoBehaviour
{
    private ARTrackedImageManager imageManager;

    public GameObject adminPanel;   // Assign your Admin Panel here

    private void Awake()
    {
        imageManager = GetComponent<ARTrackedImageManager>();
    }

    private void OnEnable()
    {
        imageManager.trackablesChanged.AddListener(OnImagesChanged);
    }

    private void OnDisable()
    {
        imageManager.trackablesChanged.RemoveListener(OnImagesChanged);
    }

    private void OnImagesChanged(ARTrackablesChangedEventArgs<ARTrackedImage> args)
    {
        foreach (var trackedImage in args.added)
        {
            DetectImage(trackedImage);
        }

        foreach (var trackedImage in args.updated)
        {
            DetectImage(trackedImage);
        }
    }

    void DetectImage(ARTrackedImage trackedImage)
    {
        if (trackedImage.trackingState ==
            UnityEngine.XR.ARSubsystems.TrackingState.Tracking)
        {
            Debug.Log("Detected : " + trackedImage.referenceImage.name);

            if (trackedImage.referenceImage.name == "A_Block_Entrance")
            {
                Debug.Log("Admin can start creating route.");

                adminPanel.SetActive(true);
            }
        }
    }
}