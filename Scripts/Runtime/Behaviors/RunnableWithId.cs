using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoreLib.Behaviors
{
    public struct RunnableCoroutineId
    {
        public int Id { get; private set; }

        public RunnableCoroutineId(int id)
        {
            this.Id = id;
        }

        public readonly void StopCoroutine() => RunnableWithId.StopCoroutineById(Id);
        public readonly bool CoroutineExists() => RunnableWithId.CoroutineWithIdExists(Id);
    }

    /// <summary>
    /// Extends the <see cref="Runnable"/> singleton to enable ID-based control over coroutines.
    /// Coroutines initiated via this class are assigned unique IDs and tracked in a dictionary.
    /// The class provides methods to specifically stop these coroutines using their IDs.
    /// </summary>
    public class RunnableWithId : Singleton<RunnableWithId>
    {
        private static readonly Dictionary<int, Coroutine> _coroutines = new();
        private static int _nextCoroutineId = 0;

        protected override void Awake()
        {
            base.Awake();
            if (Instantiated) gameObject.hideFlags = HideFlags.HideAndDontSave;
        }

        public static RunnableCoroutineId Run(IEnumerator routine)
        {
            if (!Instantiated)
            {
                GameObject obj = new("RunnableWithId");
                obj.AddComponent<RunnableWithId>();
            }

            int newId = _nextCoroutineId++;
            Coroutine newCoroutine = Instance.StartCoroutine(routine);
            _coroutines.Add(newId, newCoroutine);

            return new RunnableCoroutineId(newId);
        }

        public static bool CoroutineWithIdExists(int id) => _coroutines.TryGetValue(id, out _);

        public static void StopCoroutineById(int id)
        {
            if (_coroutines.TryGetValue(id, out Coroutine toBeStopped))
            {
                Instance.StopCoroutine(toBeStopped);
                _coroutines.Remove(id);
            }
            else
            {
                Debug.LogWarning($"No coroutine with ID {id} exists");
            }
        }

        public static void StopAllCoroutinesWithIds()
        {
            Instance.StopAllCoroutines();
            _coroutines.Clear();
        }
    }
}
