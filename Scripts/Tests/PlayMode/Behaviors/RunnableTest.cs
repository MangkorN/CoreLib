using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using CoreLib.Behaviors;

namespace CoreLib.UnitTest.Behaviors
{
    public class RunnableTests
    {
        private bool coroutineRan = false;

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
            var objs = Resources.FindObjectsOfTypeAll<Runnable>();
            foreach (var obj in objs)
            {
                GameObject.DestroyImmediate(obj.gameObject);
            }
            coroutineRan = false;
        }

        [UnityTest]
        public IEnumerator InstantiatesFromRun()
        {
            yield return new WaitForFixedUpdate();
            Assert.IsFalse(Runnable.Instantiated);

            Runnable.Run(TestCoroutine());

            yield return new WaitForFixedUpdate();
            Assert.IsTrue(Runnable.Instantiated);
        }

        [UnityTest]
        public IEnumerator StartsCoroutine()
        {
            yield return new WaitForFixedUpdate();

            Runnable.Run(TestCoroutine());

            yield return new WaitForFixedUpdate();
            Assert.IsTrue(coroutineRan);
        }

        [UnityTest]
        public IEnumerator HasHideAndDontSaveFlag()
        {
            yield return new WaitForFixedUpdate();

            Runnable.Run(TestCoroutine());

            yield return new WaitForFixedUpdate();
            Assert.AreEqual(Runnable.Instance.gameObject.hideFlags, HideFlags.HideAndDontSave);
        }

        private IEnumerator TestCoroutine()
        {
            coroutineRan = true;
            yield return new WaitForFixedUpdate();
        }

        [Test]
        public void Singleton_InstantiatesCorrectly()
        {
            Assert.IsNull(Runnable.Instance);
            new GameObject().AddComponent<Runnable>();
            Assert.IsNotNull(Runnable.Instance);
            Assert.IsTrue(Runnable.Instantiated);
        }

        [Test]
        public void Singleton_DestroysCorrectly()
        {
            var singleton = new GameObject().AddComponent<Runnable>();
            GameObject.DestroyImmediate(singleton.gameObject);
            Assert.IsNull(Runnable.Instance);
            Assert.IsFalse(Runnable.Instantiated);
        }

        [Test]
        public void Singleton_HandlesMultipleInstances()
        {
            new GameObject().AddComponent<Runnable>();
            var secondInstance = new GameObject().AddComponent<Runnable>();
            Assert.AreNotEqual(secondInstance, Runnable.Instance);
        }
       
    }
}
