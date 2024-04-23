using System.Threading.Tasks;
using Model;
using UnityEngine;

namespace Network.Messages.Transparency
{
    public class TransparencyRunner
    {
        public Task Run(Message<TransparencyData> message)
        {
            UnityMainThreadDispatcher.instance.Enqueue(() => InternalRun(message));
            return Task.CompletedTask;
        }

        private Task InternalRun(Message<TransparencyData> message)
        {
            EnablePassthrough passthroughScript = GameObject.FindObjectOfType<EnablePassthrough>();

            if (passthroughScript != null)
            {
                var data = message.Data;
                if (data != null)
                {
                    passthroughScript.SetTransparencyMode(data.EnableTransparencyMode);
                }
                else
                {
                    Debug.LogError("Message data is null. Cannot focus on object.");
                }
                
            }
            else
            {
                Debug.LogError("Unable to enable passthrough mode");
            }

            return Task.CompletedTask;
        }
    }
}