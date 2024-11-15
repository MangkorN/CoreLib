using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CoreLib.Behaviors;

namespace CoreLib.Components
{
    public class SystemLoader : Singleton<SystemLoader>
    {
        public static event Action OnSystemLoad;
        public static event Action OnSystemUnload;

        [Header("Systems")]
        [SerializeField] private bool _systemsLoaded = false;
        [SerializeField] private bool _loadSystemOnStart = true;
        [SerializeField] private List<GameObject> _systemManagerObjects;
        private readonly List<ISystemManager> _systemManagers = new();

        [Header("Settings")]
        [SerializeField] private bool _showDebugLogs;

        public bool SystemsLoaded => _systemsLoaded;

        protected override void Awake()
        {
            base.Awake();

            // Retrieve system manager component from all the system manager prefabs referenced
            foreach (var go in _systemManagerObjects)
            {
                if (go.TryGetComponent<ISystemManager>(out var systemManager))
                {
                    _systemManagers.Add(systemManager);
                }
                else
                {
                    Debug.LogWarning($"GameObject {go.name} does not have a component implementing ISystemManager");
                }
            }
        }

        public void Start()
        {
            if (_loadSystemOnStart)
                LoadSystems();
        }

        public void LoadSystems(Action callback = null) => StartCoroutine(LoadSystemsRoutine(callback));
        public void UnloadSystems(Action callback = null) => StartCoroutine(UnloadSystemsRoutine(callback));

        private IEnumerator LoadSystemsRoutine(Action callback = null)
        {
            DebugLog("Begin loading systems...");

            foreach (var manager in _systemManagers)
            {
                yield return manager.Initialize();

                if (manager.IsInitialized)
                    DebugLog($"> {manager.Name} initialized.");
                else
                    DebugLog($"> {manager.Name} failed to initialize.");
            }
            _systemsLoaded = true;

            DebugLog("... all systems loaded.");
            callback?.Invoke();
            OnSystemLoad?.Invoke();
        }

        private IEnumerator UnloadSystemsRoutine(Action callback = null)
        {
            DebugLog("Begin unloading systems...");

            foreach (var manager in _systemManagers)
            {
                yield return manager.Deinitialize();

                if (!manager.IsInitialized)
                    DebugLog($"> {manager.Name} deinitialized.");
                else
                    DebugLog($"> {manager.Name} failed to deinitialize.");
            }
            _systemsLoaded = false;

            DebugLog("... all systems unloaded.");
            callback?.Invoke();
            OnSystemUnload?.Invoke();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            if (_systemsLoaded)
                DebugLog("System Loader was destroyed before unloading all systems!");
        }

        private void DebugLog(string log)
        {
            if (_showDebugLogs)
                Debug.Log(log);
        }
    }
}
