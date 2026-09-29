using UnityEngine;
using System.Collections.Generic;
using System.IO;
using TMPro;


public class RouteSaveSystem : MonoBehaviour
{
    public AdminRouteRecorder recorder;
    public TMP_Text messageText;

    public void SaveRoute()
    {
        RouteData data = new RouteData();

        data.waypoints = recorder.recordedWaypoints;

        string json = JsonUtility.ToJson(data, true);

        string path = Application.persistentDataPath + "/A_Block_Entrance_A203.json";

        File.WriteAllText(path, json);

        if(File.Exists(path))
        {
            messageText.text = "Route Saved Successfully!\nWaypoints: " 
                               + data.waypoints.Count;
        }
        else
        {
            messageText.text = "Save Failed";
        }
    }
}