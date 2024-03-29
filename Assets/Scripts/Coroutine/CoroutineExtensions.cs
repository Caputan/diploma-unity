﻿using System.Collections;
using UnityEngine;

namespace Coroutine
{
    public static class CoroutineExtensions
    {
        private static AsyncOperationBehavior _asyncOperationBehavior = null;
        
        public static UnityEngine.Coroutine StartCoroutine(this IEnumerator task, 
            out IEnumerator back,
            out CoroutineController coroutineController)
        {
            Initialize();
            if (task == null)
            {
                throw new System.ArgumentNullException(nameof(task));
            }
            
            coroutineController = new CoroutineController(task);
            back = task;
            return _asyncOperationBehavior.StartCoroutine(coroutineController.Start());
        }

        public static void StopCoroutine(this IEnumerator task)
        {
            if (task == null)
            {
                throw new System.ArgumentNullException(nameof(task));
            }
            _asyncOperationBehavior.StopCoroutine(task);
            
        } 

        public static void Initialize()
        {
            if (_asyncOperationBehavior != null)
            {
                return;
            }

            GameObject go = new GameObject();
            Object.DontDestroyOnLoad(go);
            go.name = "AsyncOperationExtensionCoroutine";
            go.hideFlags = HideFlags.HideAndDontSave;

            _asyncOperationBehavior = go.AddComponent<AsyncOperationBehavior>();
        }
    }
}