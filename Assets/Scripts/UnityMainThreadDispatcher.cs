using System;
using System.Collections.Generic;
using UnityEngine;

    public class UnityMainThreadDispatcher: MonoBehaviour
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