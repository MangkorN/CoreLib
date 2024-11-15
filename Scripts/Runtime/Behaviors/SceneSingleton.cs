using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoreLib.Behaviors
{
    /// <summary>
    /// Scene (generic) singleton class:
    /// If a new scene is loaded that also uses this Singleton type, a new instance will be created.
    /// Will also be destroyed when the scene unloads.
    /// </summary>
    public abstract class SceneSingleton<T> : MonoBehaviour where T : SceneSingleton<T>
    {
        public static event Action OnInstantiated;

        private static T _instance;
        public static T Instance => _instance;

        public static bool Instantiated { get; private set; }

        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = (T)this;
                Instantiated = true;
                OnInstantiated?.Invoke();
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }
        }

        protected virtual void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
                Instantiated = false;
            }
        }
    }
}
