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
            // Find the first cube GameObject in the scene
            var cube = GameObject.FindWithTag("Cube");

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