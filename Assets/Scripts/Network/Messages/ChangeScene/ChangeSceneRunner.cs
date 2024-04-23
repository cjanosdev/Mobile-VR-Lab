using System;
using System.Threading.Tasks;
using Model.Messages;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Model.ChangeScene
{
    public class ChangeSceneRunner : IMessageRunner
    {
        private readonly UnityMainThreadDispatcher _mainThreadDispatcher;

        public ChangeSceneRunner(UnityMainThreadDispatcher mainThreadDispatcher)
        {
            _mainThreadDispatcher = mainThreadDispatcher ?? throw new ArgumentNullException(nameof(mainThreadDispatcher));
        }

        public async Task Run<T>(Message<T> message) where T : MessageBase
        {
            if (message is Message<ChangeSceneData> changeSceneMessage)
            {
                var data = changeSceneMessage.Data;
                if (data != null)
                {
                    await LoadSceneAsync(data.SceneID);
                }
            }
            else
            {
                Debug.LogError("Message data type mismatch. Expected ChangeSceneData.");
            }
        }

        private async Task LoadSceneAsync(int sceneID)
        {
            var tcs = new TaskCompletionSource<bool>();

            _mainThreadDispatcher.Enqueue(() =>
            {
                SceneManager.LoadScene(sceneID);
            });

            await tcs.Task;
        }
    }
}
