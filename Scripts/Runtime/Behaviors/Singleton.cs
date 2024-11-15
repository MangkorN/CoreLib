using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoreLib.Behaviors
{
    /// <summary>
    /// Generic singleton class.
    /// </summary>
    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
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

                if (transform.parent == null)
                {
                    DontDestroyOnLoad(this.gameObject);
                }
                else
                {
                    if (transform.root.gameObject.scene.buildIndex != -1)
                    {
                        transform.SetParent(null);
                        DontDestroyOnLoad(this.gameObject);
                    }
                }

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
