using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using CoreLib.Behaviors;

namespace CoreLib.UnitTest.Behaviors
{
    public class SingletonTests
    {
        private class TestSingleton : Singleton<TestSingleton> { }

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
            var objs = GameObject.FindObjectsOfType<TestSingleton>();
            foreach (var obj in objs)
            {
                GameObject.DestroyImmediate(obj.gameObject);
            }
        }

        [Test]
        public void InstantiatesCorrectly()
        {
            Assert.IsNull(TestSingleton.Instance);
            new GameObject().AddComponent<TestSingleton>();
            Assert.IsNotNull(TestSingleton.Instance);
            Assert.IsTrue(TestSingleton.Instantiated);
        }

        [Test]
        public void DestroysCorrectly()
        {
            var singleton = new GameObject().AddComponent<TestSingleton>();
            GameObject.DestroyImmediate(singleton.gameObject);
            Assert.IsNull(TestSingleton.Instance);
            Assert.IsFalse(TestSingleton.Instantiated);
        }

        [Test]
        public void HandlesMultipleInstances()
        {
            new GameObject().AddComponent<TestSingleton>();
            var secondInstance = new GameObject().AddComponent<TestSingleton>();
            Assert.AreNotEqual(secondInstance, TestSingleton.Instance);
        }

        [Test]
        public void OnInstantiatedEventTriggered()
        {
            bool eventCalled = false;
            Singleton<TestSingleton>.OnInstantiated += () => eventCalled = true;

            new GameObject().AddComponent<TestSingleton>();

            Assert.IsTrue(eventCalled);

            Singleton<TestSingleton>.OnInstantiated -= () => eventCalled = true;
        }
    }
}
