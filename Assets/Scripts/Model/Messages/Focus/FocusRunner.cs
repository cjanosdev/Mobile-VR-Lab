using System;
using System.Threading.Tasks;
using Model.Messages;
using Model.Messages.Query;
using UnityEngine;

namespace Model.Focus
{
    
    public class FocusRunner
    {
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
            var cube = GameObject.FindGameObjectWithTag(gameObjID);

            if (cube != null)
            {
                HighlightScript highlightScript = cube.GetComponent<HighlightScript>();
                if (highlightScript != null)
                {
                    highlightScript.HighlightFrontSide();
                }
                else
                {
                    Debug.LogError("HighlightObject script not found on the cube GameObject.");
                }
            }
            else
            {
                Debug.LogError("Cube GameObject not found in the scene.");
            }

            return Task.CompletedTask;
        }
    }
}