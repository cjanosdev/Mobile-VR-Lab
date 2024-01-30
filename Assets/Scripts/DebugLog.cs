using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugLog : MonoBehaviour
{
    private Text debugText;
    private string starterText = "Welcome to the Debug Log!\n";


    // Event to signal when the debug canvas is loaded
    public delegate void DebugCanvasLoadedEventHandler();
    public static event DebugCanvasLoadedEventHandler OnDebugCanvasLoaded;

    void Start()
    {
        // Find the Text component
        debugText = GetComponent<Text>();
        if (debugText == null)
        {
            Debug.LogError("DebugText script requires a Text component.");
            enabled = false;
        }

        // Set the starter text
        debugText.text = starterText;

        // Notify subscribers that the debug canvas is loaded
        OnDebugCanvasLoaded?.Invoke();
        NetworkManager.InitializeSockets();
    }

    public void Log(string message)
    {
        // Append new log message to the existing text
        debugText.text += message + "\n";
    }

    public void Clear()
    {
        // Clear the text
        debugText.text = "";
    }
}
