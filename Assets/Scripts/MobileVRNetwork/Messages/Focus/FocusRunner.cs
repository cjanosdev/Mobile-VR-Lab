using System.Threading.Tasks;
using UnityEngine;

namespace MobileVRNetwork.Messages.Focus
{
    
    public class FocusRunner
    {
        public Task Run(Message<FocusData> message)
        {
            UnityMainThreadDispatcher.instance.Enqueue(() => InternalRun(message));
            return Task.CompletedTask;
        }

        private async Task InternalRun(Message<FocusData> message)
        {
            var data = message.Data;
            if (data != null)
            {
                await PointToObjectAsync(data.ObjectID);
            }
            else
            {
                Debug.LogError("Message data is null. Cannot focus on object.");
            }
        }

        private Task PointToObjectAsync(string gameObjID)
        {
            var objToPointTo = GameObject.FindGameObjectWithTag(gameObjID);

            if (objToPointTo != null)
            {
                // Dynamically attach HighlightObject script
                ObjectVisibilityController pointToScript = objToPointTo.GetComponent<ObjectVisibilityController>();
                if (pointToScript == null)
                {
                    // If HighlightObject script doesn't exist, instantiate and attach it
                    pointToScript = objToPointTo.AddComponent<ObjectVisibilityController>();
                }
                
                
                // Set properties of the HighlightObject script (e.g., outline material)
                if (pointToScript != null)
                {
                    
                    // Apply the outline effect
                    pointToScript.InitializeController();
                }
                else
                {
                    Debug.LogError($"Failed to attach HighlightObject script to the {objToPointTo.name} GameObject.");
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