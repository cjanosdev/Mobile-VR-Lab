using UnityEngine;

public class EnablePassthrough : MonoBehaviour
{
    private bool isOn = false;
    public OVRManager ovrManager; // Reference to the OVRManager component
    
    void Start()
    {
        // Find the OVRManager component in the scene
        ovrManager = GetComponent<OVRManager>();
        
        // Ensure the OVRManager component is assigned
        if (ovrManager == null)
        {
            Debug.LogError("OVRManager component not found in the scene.");
            return;
        }
    }
    
    // This method is called when the GameObject is selected using the Interaction SDK
    public void OnSelect()
    {
        // Toggle the passthrough mode
        isOn = !isOn;
        SetPassthroughMode(isOn);
    }

    void SetPassthroughMode(bool isEnabled)
    {
        if (ovrManager != null)
        {
            ovrManager.isInsightPassthroughEnabled = isEnabled;
        }
    }
}