using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using CoreLib.Behaviors;

namespace CoreLib.UnitTest.Behaviors
{
    public class SceneSingletonTests
    {
        private class TestSceneSingleton : SceneSingleton<TestSceneSingleton> { }

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
            var objs = GameObject.FindObjectsOfType<TestSceneSingleton>();
            foreach (var obj in objs)
            {
                GameObject.DestroyImmediate(obj.gameObject);
            }
        }

        [Test]
        public void InstantiatesCorrectly()
        {
            Assert.IsNull(TestSceneSingleton.Instance);
            new GameObject().AddComponent<TestSceneSingleton>();
            Assert.IsNotNull(TestSceneSingleton.Instance);
            Assert.IsTrue(TestSceneSingleton.Instantiated);
        }

        [Test]
        public void DestroysCorrectly()
        {
            var singleton = new GameObject().AddComponent<TestSceneSingleton>();
            GameObject.DestroyImmediate(singleton.gameObject);
            Assert.IsNull(TestSceneSingleton.Instance);
            Assert.IsFalse(TestSceneSingleton.Instantiated);
        }

        [Test]
        public void HandlesMultipleInstances()
        {
            new GameObject().AddComponent<TestSceneSingleton>();
            var secondInstance = new GameObject().AddComponent<TestSceneSingleton>();
            Assert.AreNotEqual(secondInstance, TestSceneSingleton.Instance);
        }

        [Test]
        public void OnInstantiatedEventTriggered()
        {
            bool eventCalled = false;
            SceneSingleton<TestSceneSingleton>.OnInstantiated += () => eventCalled = true;

            new GameObject().AddComponent<TestSceneSingleton>();

            Assert.IsTrue(eventCalled);

            SceneSingleton<TestSceneSingleton>.OnInstantiated -= () => eventCalled = true;
        }
    }
}
