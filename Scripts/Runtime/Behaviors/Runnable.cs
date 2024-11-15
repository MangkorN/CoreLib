using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoreLib.Behaviors
{
    /// <summary>
    /// This class provides a way to execute coroutines in a globally accessible
    /// manner without having to be attached to a specific game object in the scene.
    /// </summary>
    public class Runnable : Singleton<Runnable>
    {
        protected override void Awake()
        {
            base.Awake();
            if (Instantiated) gameObject.hideFlags = HideFlags.HideAndDontSave;
        }

        public static Coroutine Run(IEnumerator coroutine)
        {
            if (!Instantiated)
            {
                GameObject obj = new("Runnable");
                obj.AddComponent<Runnable>();
            }

            return Instance.StartCoroutine(coroutine);
        }
    }
}
