using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CoreLib.Behaviors;

namespace CoreLib.Utilities
{
    public static class ImageUtilities
    {
        public const float SPEED_DEFAULT = 0.5f;

        public static void FadeIn(Image image)
        {
            image.gameObject.SetActive(true);
            Runnable.Run(AlphaCoroutine(image, 1, SPEED_DEFAULT, true));
        }

        public static void FadeOut(Image image)
        {
            image.gameObject.SetActive(true);
            Runnable.Run(AlphaCoroutine(image, 0, SPEED_DEFAULT, false));
        }

        public static void AnimateAlpha(Image image, float targetAlpha, float speed, bool setActiveOnFinish)
        {
            image.gameObject.SetActive(true);
            Runnable.Run(AlphaCoroutine(image, targetAlpha, speed, setActiveOnFinish));
        }

        private static IEnumerator AlphaCoroutine(Image image, float targetAlpha, float speed, bool setActiveOnFinish)
        {
            if (targetAlpha < 0) targetAlpha = 0;
            if (targetAlpha > 1) targetAlpha = 1;

            float startAlpha = image.color.a;
            float endAlpha = targetAlpha;
            float rate = 1.0f / speed;
            float progress = 0.0f;

            while (progress < 1.0)
            {
                progress += rate * Time.deltaTime;
                Color color = image.color;
                color.a = Mathf.Lerp(startAlpha, endAlpha, progress);
                image.color = color;

                yield return null;
            }

            image.gameObject.SetActive(setActiveOnFinish);
        }
    }
}
