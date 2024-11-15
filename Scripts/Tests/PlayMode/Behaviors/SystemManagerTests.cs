using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using CoreLib.Behaviors;

namespace CoreLib.UnitTest.Behaviors
{
    public class SystemManagerTests
    {
        private class TestSystemManager : SystemManager<TestSystemManager>
        {
            protected override IEnumerator InitializeSystem()
            {
                yield return null; // Simulate initialization
            }

            protected override IEnumerator DeinitializeSystem()
            {
                yield return null; // Simulate deinitialization
            }
        }

        [SetUp]
        public void SetUp()
        {
            Cleanup();
        }

        [TearDown]
        public void TearDown()
        {
            Cleanup();
        }

        private void Cleanup()
        {
            var objs = GameObject.FindObjectsOfType<TestSystemManager>();
            foreach (var obj in objs)
            {
                GameObject.DestroyImmediate(obj.gameObject);
            }
        }

        [UnityTest]
        public IEnumerator InitializesCorrectly()
        {
            var managerObject = new GameObject().AddComponent<TestSystemManager>();
            yield return managerObject.Initialize();
            Assert.IsTrue(managerObject.IsInitialized);
        }

        [UnityTest]
        public IEnumerator DoesNotInitializeTwice()
        {
            var managerObject = new GameObject().AddComponent<TestSystemManager>();
            yield return managerObject.Initialize();
            LogAssert.Expect(LogType.Warning, "Attempted to initialize a system manager that has already been initialized.");
            yield return managerObject.Initialize(); // This should log a warning and not reinitialize
            Assert.IsTrue(managerObject.IsInitialized);
        }

        [UnityTest]
        public IEnumerator OnInitializedEventInvoked()
        {
            var managerObject = new GameObject().AddComponent<TestSystemManager>();
            bool eventInvoked = false;
            TestSystemManager.OnInitialized += () => eventInvoked = true;
            yield return managerObject.Initialize();
            Assert.IsTrue(eventInvoked);
        }
    }
}
