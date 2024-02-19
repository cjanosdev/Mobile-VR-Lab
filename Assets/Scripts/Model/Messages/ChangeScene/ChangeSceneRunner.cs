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
            UnityMainThreadDispatcher._instance = GameObject.FindObjectOfType(typeof(UnityMainThreadDispatcher)) as UnityMainThreadDispatcher;
        }
        public async Task Run<T>(Message<T> message) where T : MessageBase
        {
            if (message is Message<ChangeSceneData> changeSceneMessage)
            {
                var data = changeSceneMessage.Data;
                await LoadSceneAsync(data.SceneID);
            }
            else
            {
                Debug.LogError("Message data type mismatch. Expected ChangeSceneData.");
            }
        }

        private async Task LoadSceneAsync(int sceneID)
        {
            // We use a TaskCompletionSource to wait for the scene to load
            var tcs = new TaskCompletionSource<bool>();

            // Run the scene loading on the main thread using Unity's dispatcher
            UnityMainThreadDispatcher.Instance().Enqueue(() =>
            {
                // Start loading the scene and subscribe to the completed event
                // Note: Unity requires that all scene loading and most other interactions with the Unity API occur on the main thread, so
                // we can't just call SceneManager.LoadSceneAsync(sceneId) directly here since this is a non-main thread. 
                // When we call .LoadSceneAsync() on the thread dispatcher, it enqueues the scene loading action to be run on the main thread and uses a
                // TaskCompletionSource to await the async operation to complete, allowing Run() to await the completion of the
                // scene load while still being an async method itself.
                var asyncOperation = SceneManager.LoadSceneAsync(sceneID, LoadSceneMode.Single);
                asyncOperation.completed += _ => tcs.SetResult(true);
            });

            // Wait for the scene to finish loading
            await tcs.Task;
        }
    }
    
    // `UnityMainThreadDispatcher` is a MonoBehaviour that we have to attach to a GameObject in our scenes.
    // It has a queue of actions to execute (_executionQueue). Enqueue is used to add actions to this queue, and
    // Update method dequeues and executes them on the main thread.
    public class UnityMainThreadDispatcher : MonoBehaviour
    {
        private static readonly Queue<Action> _executionQueue = new();

        public static UnityMainThreadDispatcher _instance;

        // public UnityMainThreadDispatcher(UnityMainThreadDispatcher instance)
        // {
        //     if (UnityMainThreadDispatcher._instance == null)
        //     {
        //         UnityMainThreadDispatcher._instance = instance;
        //     } 
        // }

        public void Update()
        {
            while (_executionQueue.Count > 0) _executionQueue.Dequeue().Invoke();
        }

        public void Enqueue(Action action)
        {
            _executionQueue.Enqueue(action);
        }

        public static UnityMainThreadDispatcher Instance()
        {
            if (_instance == null)
            {
                // _instance = FindObjectOfType(typeof(UnityMainThreadDispatcher)) as UnityMainThreadDispatcher;
                if (_instance == null)
                {
                    var gameObject = new GameObject("UnityMainThreadDispatcher");
                    _instance = gameObject.AddComponent<UnityMainThreadDispatcher>();
                    DontDestroyOnLoad(gameObject);
                }
            }
            return _instance;
        }
    }
}