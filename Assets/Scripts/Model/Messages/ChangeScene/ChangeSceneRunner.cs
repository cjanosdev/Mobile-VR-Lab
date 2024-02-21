using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Model.Messages;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Model.ChangeScene
{
    public class ChangeSceneRunner : IMessageRunner
    {
        public ChangeSceneRunner()
        {
            // make sure the UnityMainThreadDispatcher instance is initialized.
            if (UnityMainThreadDispatcher.Instance == null)
            {
                new GameObject("UnityMainThreadDispatcher")
                    .AddComponent<UnityMainThreadDispatcher>();
                
                //Debug.LogError("UnityMainThreadDispatcher instance was not found. Make sure it's added to the scene and initialized.");
            }
        }

        public async Task Run<T>(Message<T> message) where T : MessageBase
        {
            if (message is Message<ChangeSceneData> changeSceneMessage)
            {
                ChangeSceneData data = changeSceneMessage.Data as ChangeSceneData;
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

            UnityMainThreadDispatcher.Instance.Enqueue(() =>
            {
                var asyncOperation = SceneManager.LoadSceneAsync(sceneID, LoadSceneMode.Single);
                if (asyncOperation == null)
                {
                    Debug.LogError($"Failed to load the scene with ID: {sceneID}");
                    tcs.SetResult(false); // Indicate that the scene loading failed
                }
                else
                {
                    asyncOperation.completed += _ => tcs.SetResult(true);
                }
            });

            await tcs.Task;
        }
    }

    public class UnityMainThreadDispatcher : MonoBehaviour
    {
        private static readonly Queue<Action> _executionQueue = new Queue<Action>();
        private static UnityMainThreadDispatcher _instance;

        public static UnityMainThreadDispatcher Instance
        {
            get
            {
                if (_instance == null)
                {
                    var gameObject = new GameObject("UnityMainThreadDispatcher");
                    _instance = gameObject.AddComponent<UnityMainThreadDispatcher>();
                    DontDestroyOnLoad(gameObject);
                }
                return _instance;
            }
        }

        public void Update()
        {
            while (_executionQueue.Count > 0)
            {
                _executionQueue.Dequeue().Invoke();
            }
        }

        public void Enqueue(Action action)
        {
            _executionQueue.Enqueue(action);
        }

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
}
