using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TestTools;
using CoreLib.Utilities;

namespace CoreLib.UnitTest.Utilities
{
    public class ImageUtilitiesTests
    {
        private GameObject _testGameObject;
        private Image _testImage;

        [SetUp]
        public void SetUp()
        {
            _testGameObject = new GameObject("TestImageObject");
            _testImage = _testGameObject.AddComponent<Image>();
            _testImage.color = new Color(1, 1, 1, 0.5f);  // RGBA
        }

        [TearDown]
        public void TearDown()
        {
            GameObject.Destroy(_testGameObject);
        }

        [UnityTest]
        public IEnumerator ImageFadesInAndStaysActive()
        {
            Assert.AreEqual(0.5f, _testImage.color.a);

            ImageUtilities.FadeIn(_testImage);
            yield return new WaitForSeconds(0.6f);  // Wait for a bit more than the default speed to ensure completion

            Assert.AreEqual(1f, _testImage.color.a);
            Assert.IsTrue(_testImage.gameObject.activeSelf);
        }

        [UnityTest]
        public IEnumerator ImageFadesOutAndBecomesInactive()
        {
            Assert.AreEqual(0.5f, _testImage.color.a);

            ImageUtilities.FadeOut(_testImage);
            yield return new WaitForSeconds(0.6f);  // Wait for a bit more than the default speed to ensure completion

            Assert.AreEqual(0f, _testImage.color.a);
            Assert.IsFalse(_testImage.gameObject.activeSelf);
        }
    }
}
