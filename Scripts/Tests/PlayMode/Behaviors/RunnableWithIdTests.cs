using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using CoreLib.Behaviors;

namespace CoreLib.UnitTest.Behaviors
{
    public class RunnableWithIdTests
    {
        private bool coroutineRan = false;
        private bool coroutineCompleted = false;

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
            var objs = Resources.FindObjectsOfTypeAll<RunnableWithId>();
            foreach (var obj in objs)
            {
                GameObject.DestroyImmediate(obj.gameObject);
            }
            coroutineRan = false;
            coroutineCompleted = false;
        }

        private IEnumerator TestCoroutine()
        {
            coroutineRan = true;
            yield return new WaitForFixedUpdate();
            yield return new WaitForFixedUpdate();
            coroutineCompleted = true;
        }

        [UnityTest]
        public IEnumerator InstantiatesFromRun()
        {
            yield return new WaitForFixedUpdate();
            Assert.IsFalse(RunnableWithId.Instantiated);

            RunnableWithId.Run(TestCoroutine());

            yield return new WaitForFixedUpdate();
            Assert.IsTrue(RunnableWithId.Instantiated);
        }

        [UnityTest]
        public IEnumerator StartsCoroutine()
        {
            yield return new WaitForFixedUpdate();

            RunnableWithId.Run(TestCoroutine());

            yield return new WaitForFixedUpdate();
            Assert.IsTrue(coroutineRan);
        }

        [UnityTest]
        public IEnumerator HasHideAndDontSaveFlag()
        {
            yield return new WaitForFixedUpdate();

            RunnableWithId.Run(TestCoroutine());

            yield return new WaitForFixedUpdate();
            Assert.AreEqual(RunnableWithId.Instance.gameObject.hideFlags, HideFlags.HideAndDontSave);
        }

        [UnityTest]
        public IEnumerator DirectSingletonReference_StopCoroutineById()
        {
            yield return new WaitForFixedUpdate();

            int coroutineId = RunnableWithId.Run(TestCoroutine()).Id;

            yield return new WaitForFixedUpdate();  // Let the coroutine set 'coroutineRan' to true
            RunnableWithId.StopCoroutineById(coroutineId);

            Assert.IsTrue(coroutineRan);
            Assert.IsFalse(coroutineCompleted);  // Assert the coroutine was actually stopped
        }

        [UnityTest]
        public IEnumerator RunnableCoroutineIdStruct_StopCoroutine()
        {
            yield return new WaitForFixedUpdate();

            RunnableCoroutineId coroutineId = RunnableWithId.Run(TestCoroutine());

            yield return new WaitForFixedUpdate();  // Let the coroutine set 'coroutineRan' to true
            coroutineId.StopCoroutine();

            Assert.IsTrue(coroutineRan);
            Assert.IsFalse(coroutineCompleted);  // Assert the coroutine was actually stopped
        }

        [Test]
        public void Singleton_InstantiatesCorrectly()
        {
            Assert.IsNull(RunnableWithId.Instance);
            new GameObject().AddComponent<RunnableWithId>();
            Assert.IsNotNull(RunnableWithId.Instance);
            Assert.IsTrue(RunnableWithId.Instantiated);
        }

        [Test]
        public void Singleton_DestroysCorrectly()
        {
            var singleton = new GameObject().AddComponent<RunnableWithId>();
            GameObject.DestroyImmediate(singleton.gameObject);
            Assert.IsNull(RunnableWithId.Instance);
            Assert.IsFalse(RunnableWithId.Instantiated);
        }

        [Test]
        public void Singleton_HandlesMultipleInstances()
        {
            new GameObject().AddComponent<RunnableWithId>();
            var secondInstance = new GameObject().AddComponent<RunnableWithId>();
            Assert.AreNotEqual(secondInstance, RunnableWithId.Instance);
        }


        [UnityTest]
        public IEnumerator CoroutineWithIdExists_ReturnsFalseWhenNoCoroutine()
        {
            var nonExistentCoroutineId = -1;  // Assuming -1 is an ID that would never be assigned
            yield return null;
            Assert.IsFalse(RunnableWithId.CoroutineWithIdExists(nonExistentCoroutineId));
        }

        [UnityTest]
        public IEnumerator DirectSingletonReference_CoroutineWithIdExists_ReturnsTrueAfterRun()
        {
            int id = RunnableWithId.Run(TestCoroutine()).Id;
            yield return null;  // Ensuring the coroutine has started
            Assert.IsTrue(RunnableWithId.CoroutineWithIdExists(id));
        }

        [UnityTest]
        public IEnumerator RunnableCoroutineIdStruct_CoroutineExists_ReturnsTrueAfterRun()
        {
            RunnableCoroutineId coroutineId = RunnableWithId.Run(TestCoroutine());
            yield return null;  // Ensuring the coroutine has started
            Assert.IsTrue(coroutineId.CoroutineExists());
        }

        [UnityTest]
        public IEnumerator DirectSingletonReference_CoroutineWithIdExists_ReturnsFalseWhenStopped()
        {
            int id = RunnableWithId.Run(TestCoroutine()).Id;
            yield return null;  // Ensuring the coroutine has started
            Assert.IsTrue(RunnableWithId.CoroutineWithIdExists(id));

            RunnableWithId.StopCoroutineById(id);
            yield return null;  // Ensuring the coroutine has had a chance to stop
            Assert.IsFalse(RunnableWithId.CoroutineWithIdExists(id));
        }

        [UnityTest]
        public IEnumerator RunnableCoroutineIdStruct_CoroutineExists_ReturnsFalseWhenStopped()
        {
            RunnableCoroutineId coroutineId = RunnableWithId.Run(TestCoroutine());
            yield return null;  // Ensuring the coroutine has started
            Assert.IsTrue(coroutineId.CoroutineExists());

            coroutineId.StopCoroutine();
            yield return null;  // Ensuring the coroutine has had a chance to stop
            Assert.IsFalse(coroutineId.CoroutineExists());
        }

    }
}
