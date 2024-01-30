using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    
 private static bool socketsInitialized = false;
    private static TcpClient client;
    private static NetworkStream stream;
    private static Thread receiveThread;
    private static DebugLog _debugLog;
    private static bool debugCanvasLoaded = false;

   // [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void InitializeSocketsBeforeSceneLoad()
    {
        
        // Wait for the debug canvas to be loaded before initializing sockets
        while (!debugCanvasLoaded)
        {
            Debug.Log("Waiting for debug canvas to load...");
        }
        
        if (!socketsInitialized)
        {
            
            InitializeSockets();
        }
    }

    public static void InitializeSockets()
    {
        
        _debugLog = FindObjectOfType<DebugLog>();
        if (_debugLog == null)
        {
            Debug.LogWarning("DebugText component not found. Debug output will not be displayed.");
        }
        try
        {
           
            client = new TcpClient("127.0.0.1", 15300);
            stream = client.GetStream();
            Debug.Log("Sockets initialized successfully");
            socketsInitialized = true;

            // Start a thread to receive commands from the server
            receiveThread = new Thread(ReceiveCommands);
            receiveThread.Start();
        }
        catch (Exception e)
        {
            if (_debugLog == null)
            {
                Debug.LogWarning("DebugText component not found. Debug output will not be displayed.");
            }
            Debug.Log("Error initializing sockets: ");
           // _debugLog.Log("Error initializing sockets: " + e.Message);
        }
    }

    private static void ReceiveCommands()
    {
        byte[] buffer = new byte[1024];
        int bytesRead;

        while (true)
        {
            try
            {
                bytesRead = stream.Read(buffer, 0, buffer.Length);
                string command = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                Debug.Log("Received command from server: " + command);

                // Process the received command (you can implement your own logic here)
                // For example:
                
                // Process the received command (you can implement your own logic here)
                if (command.Trim() == "CHECK_VR_STATUS")
                {
                    // Send the status of VR headsets to the server
                    SendVRStatus();
                    Debug.Log("Executing action for command: " + command);
                }
            }
            catch (Exception e)
            {
                Debug.Log("Error receiving data: " + e.Message);
                break;
            }
        }
    }
    
    private static void SendVRStatus()
    {
        // Simulate getting the VR headset status (replace with actual code)
        string vrStatus = GetVRHeadsetStatus();

        try
        {
            byte[] data = Encoding.ASCII.GetBytes(vrStatus);
            stream.Write(data, 0, data.Length);
            Debug.Log("Sent VR headset status to server: " + vrStatus);
        }
        catch (Exception e)
        {
            Debug.Log("Error sending VR status to server: " + e.Message);
        }
    }
    
    private static string GetVRHeadsetStatus()
    {
        if (OVRManager.isHmdPresent)
        {
            var batteryLevel = SystemInfo.batteryLevel;
            var batteryStatus = SystemInfo.batteryStatus;
            var uniqueId = SystemInfo.deviceUniqueIdentifier;
            
            
            // You can access more headset data and return it as a formatted string
            string headsetData = $"Headset Connected - Battery Level: {batteryLevel}, Battery Status: {batteryStatus}, Unique ID: {uniqueId}";
            Debug.Log(headsetData);
            
            return headsetData;
        }
        else
        {
            return "VR_HEADSET_DISCONNECTED";
        }
    }

    // Clean up resources when the application exits
    private void OnApplicationQuit()
    {
        if (client != null)
        {
            client.Close();
        }

        if (receiveThread != null && receiveThread.IsAlive)
        {
            receiveThread.Abort();
        }
    }
    
    public static void DebugCanvasLoaded()
    {
        debugCanvasLoaded = true;
        Debug.Log("Debug canvas loaded.");
        // Initialize sockets now that the debug canvas is loaded
        if (!socketsInitialized)
        {
            InitializeSockets();
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
