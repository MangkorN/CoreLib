using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoreLib.Behaviors
{
    public interface ISystemManager
    {
        IEnumerator Initialize();
        IEnumerator Deinitialize();
        bool IsInitialized { get; }
        string Name { get; }
    }

    public abstract class SystemManager<T> : Singleton<T>, ISystemManager where T : SystemManager<T>
    {
        public static event Action OnInitialized;

        private bool _isInitialized = false;
        
        public bool IsInitialized => _isInitialized;
        public string Name => name;

        public IEnumerator Initialize()
        {
            if (!_isInitialized)
                yield return InitializationRoutine(true);
            else
                Debug.LogWarning("Attempted to initialize a system manager that has already been initialized.");
        }

        public IEnumerator Deinitialize()
        {
            if (_isInitialized)
                yield return InitializationRoutine(false);
            else
                Debug.LogWarning("Attempted to deinitialize a system manager that has not been initialized.");
        }

        private IEnumerator InitializationRoutine(bool initialize)
        {
            if (initialize)
                yield return InitializeSystem();
            else
                yield return DeinitializeSystem();
            
            yield return new WaitForEndOfFrame();
            
            _isInitialized = initialize;
            
            if (initialize)
                OnInitialized?.Invoke();
        }

        protected abstract IEnumerator InitializeSystem();
        protected abstract IEnumerator DeinitializeSystem();
    }
}
