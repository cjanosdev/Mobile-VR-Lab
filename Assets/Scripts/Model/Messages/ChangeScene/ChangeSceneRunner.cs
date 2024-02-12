using System.Threading.Tasks;
using Model.Messages;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Model.ChangeScene
{
    public class ChangeSceneRunner : IMessageRunner
    {
        public async Task Run<T>(Message<T> message) where T : MessageBase
        {
            if (message is Message<ChangeSceneData> changeSceneMessage)
            {
                ChangeSceneData data = changeSceneMessage.Data;
                await LoadSceneAsync(data.SceneID);
            }
            else
            {
                Debug.LogError("Message data type mismatch. Expected ChangeSceneData.");
            }
        }

        private Task LoadSceneAsync(int sceneID)
        {
            // LoadSceneAsync can take either the scene's build index or name as a parameter
            SceneManager.LoadSceneAsync(sceneID, LoadSceneMode.Single);
            return Task.CompletedTask; // LoadSceneAsync is already an async operation
        }
    }
}