using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AdminRouteRecorder : MonoBehaviour
{
    [Header("AR Camera")]
    public Transform arCamera;

    [Header("Recording Settings")]
    public float recordDistance = 0.5f; // Record every 0.5 meters

    [Header("UI")]
    public TMP_Text statusText;

    private bool isRecording = false;
    private Vector3 lastRecordedPosition;

    public List<Vector3> recordedWaypoints = new List<Vector3>();

    void Start()
    {
        if (statusText != null)
            statusText.text = "Waiting to Start...";
    }

    void Update()
    {
        if (!isRecording)
            return;

        if (Vector3.Distance(arCamera.position, lastRecordedPosition) >= recordDistance)
        {
            recordedWaypoints.Add(arCamera.position);
            lastRecordedPosition = arCamera.position;

            if (statusText != null)
            {
                statusText.text = "Recording...\nWaypoints : " + recordedWaypoints.Count;
            }
        }
    }

    // Called by Start Recording Button
    public void StartRecording()
    {
        recordedWaypoints.Clear();

        isRecording = true;

        lastRecordedPosition = arCamera.position;
        recordedWaypoints.Add(lastRecordedPosition);

        if (statusText != null)
        {
            statusText.text = "Recording Started\nWaypoints : 1";
        }
    }

    // Called by Stop Recording Button
    public void StopRecording()
    {
        isRecording = false;

        if (statusText != null)
        {
            statusText.text = "Recording Stopped\nTotal Waypoints : " + recordedWaypoints.Count;
        }
    }
}