using System;
using System.Threading.Tasks;
using Model.Messages;
using Model.Messages.Query;
using UnityEngine;

namespace Model.Focus
{
    
    public class FocusRunner
    {
        private Material outlineMaterial;
        public Task Run(Message<FocusData> message)
        {
            UnityMainThreadDispatcher.Instance.Enqueue(() => InternalRun(message));
            return Task.CompletedTask;
        }

        private async Task InternalRun(Message<FocusData> message)
        {
            var data = message.Data;
            if (data != null)
            {
                await HighlightObjectAsync(data.ObjectID);
            }
            else
            {
                Debug.LogError("Message data is null. Cannot focus on object.");
            }
        }

        private Task HighlightObjectAsync(string gameObjID)
        {
            var objToHighlight = GameObject.FindGameObjectWithTag(gameObjID);

            if (objToHighlight != null)
            {
                // Dynamically attach HighlightObject script
                HighlightObject highlightScript = objToHighlight.GetComponent<HighlightObject>();
                if (highlightScript == null)
                {
                    // If HighlightObject script doesn't exist, instantiate and attach it
                    highlightScript = objToHighlight.AddComponent<HighlightObject>();
                }
                
                // Load a material from the Resources folder
                outlineMaterial = Resources.Load<Material>("OutlineMaterial");
            
                if (outlineMaterial != null)
                {
                    // Log the name of the loaded material
                    Debug.Log("Loaded material: " + outlineMaterial.name);
                }
                else
                {
                    Debug.LogError("Failed to load material: OutlineMaterial");
                }
                
                // Set properties of the HighlightObject script (e.g., outline material)
                if (highlightScript != null)
                {
                    // Assuming outlineMaterial is a public field in FocusRunner class
                    highlightScript.outlineMaterial = outlineMaterial;
                    
                    // Apply the outline effect
                    highlightScript.ApplyOutlineEffect();
                }
                else
                {
                    Debug.LogError($"Failed to attach HighlightObject script to the {objToHighlight.name} GameObject.");
                }
            }
            else
            {
                Debug.LogError("GameObject not found in the scene.");
            }

            return Task.CompletedTask;
        }
    }
}