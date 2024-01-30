using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class FileLogger
{
    private static string logFilePath;

    static FileLogger()
    {
        InitializeFileLogger();
    }

    private static void InitializeFileLogger()
    {
        // Define the path to the log file
        string logFilePath = Application.persistentDataPath + "/log.txt";
        Debug.Log("Log file path: " + logFilePath);


        // Clear existing log file
        if (File.Exists(logFilePath))
        {
            File.Delete(logFilePath);
        }

        // Subscribe to Unity's log message event
        Application.logMessageReceived += HandleLogMessageReceived;
    }

    private static void HandleLogMessageReceived(string logString, string stackTrace, LogType type)
    {
        // Write log message to file
        string logMessage = $"{DateTime.Now} [{type}] {logString}\n";
        File.AppendAllText(logFilePath, logMessage);

        // If you want to also print the log messages to Unity's console, uncomment the next line
        // Debug.Log(logMessage);
    }

    // Method to log messages from other scripts
    public static void LogMessage(string message)
    {
        // Write custom log message to file
        string logMessage = $"{DateTime.Now} [Custom] {message}\n";
        File.AppendAllText(logFilePath, logMessage);

        // If you want to also print the log messages to Unity's console, uncomment the next line
        // Debug.Log(logMessage);
    }
}
