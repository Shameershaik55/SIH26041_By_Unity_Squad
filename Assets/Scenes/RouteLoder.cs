using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class RouteLoder : MonoBehaviour
{
    [Header("Route Marker")]
    public GameObject markerPrefab;

    [Header("Loaded Waypoints")]
    public List<Vector3> loadedWaypoints = new List<Vector3>();

    public void LoadRoute()
    {
        string path = Application.persistentDataPath + "/A_Block_Entrance_A203.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);

            RouteData data = JsonUtility.FromJson<RouteData>(json);

            loadedWaypoints = data.waypoints;

            Debug.Log("Route Loaded Successfully!");
            Debug.Log("Waypoints Loaded: " + loadedWaypoints.Count);

            // Create a marker at every waypoint
            foreach (Vector3 point in loadedWaypoints)
            {
                Instantiate(markerPrefab, point, Quaternion.identity);
            }
        }
        else
        {
            Debug.Log("Route file not found!");
        }
    }
}