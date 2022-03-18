using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraCommand : ICommands
{
    public string Name { get; set; } = "camera";
    public string Example { get; set; } = "camera 0";
    public string Description { get; set; } = "Shows camera footage";
    public object[] Arguments { get; set; }
    public TerminalResponseBundle Response { get; set; } = new TerminalResponseBundle();

    private List<GameObject> cameras = new List<GameObject>();

    private bool hasCameras = false;
    
    public TerminalResponseBundle Execute()
    {
        Response.Clear();
        Response.ClearCommandLine();

        if (!hasCameras)
            SearchCameras();

        DisplayCamera();
    

        return Response;
    }

    private void SearchCameras()
    {
        GameObject[] gameObjects = Resources.FindObjectsOfTypeAll<GameObject>();
        foreach (GameObject gm in gameObjects)
        {
            if (gm.CompareTag("Camera"))
                cameras.Add(gm);
        }
        hasCameras = true;
    }

    private void DisplayCamera()
    {
        int cameraIndex = int.Parse(Arguments[0].ToString());
        cameras[cameraIndex].SetActive(true);
    }
}

