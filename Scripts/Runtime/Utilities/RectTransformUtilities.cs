using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CoreLib.Behaviors;

namespace CoreLib.Utilities
{
    public static class RectTransformUtilities
    {
        public const float SPEED_DEFAULT = 0.5f;

        public static void Shrink(GameObject gameObject, float speed = SPEED_DEFAULT, Action callback = null)
        {
            if (!gameObject.TryGetComponent(out RectTransform rectTransform))
            {
                Debug.LogError($"GameObject '{gameObject.name}' does not have a RectTransform!");
                return;
            }
            rectTransform.localScale = new Vector3(1, 1, 1);
            gameObject.SetActive(true);
            Shrink(rectTransform, speed, false, callback);
        }

        public static void Expand(GameObject gameObject, float speed = SPEED_DEFAULT, Action callback = null)
        {
            if (!gameObject.TryGetComponent(out RectTransform rectTransform))
            {
                Debug.LogError($"GameObject '{gameObject.name}' does not have a RectTransform!");
                return;
            }
            rectTransform.localScale = new Vector3(0, 0, 0);
            gameObject.SetActive(true);
            Expand(rectTransform, speed, true, callback);
        }

        public static void Shrink(RectTransform rectTransform, float speed, bool setActiveOnFinish, Action callback = null)
        {
            Runnable.Run(ScaleCoroutine(rectTransform, 0, speed, setActiveOnFinish, callback));
        }

        public static void Expand(RectTransform rectTransform, float speed, bool setActiveOnFinish, Action callback = null)
        {
            Runnable.Run(ScaleCoroutine(rectTransform, 1, speed, setActiveOnFinish, callback));
        }

        private static IEnumerator ScaleCoroutine(RectTransform rectTransform, float targetScale, float speed, bool setActiveOnFinish, Action callback = null)
        {
            Vector3 startScale = rectTransform.localScale;
            Vector3 endScale = new Vector3(targetScale, targetScale, targetScale);

            float time = 0;
            while (time < 1)
            {
                time += Time.deltaTime / speed;
                float lerpValue = Mathf.SmoothStep(0, 1, time);

                // Apply squash and stretch effect
                float squashStretchFactor = Mathf.Sin(lerpValue * Mathf.PI);
                float xScale = Mathf.Lerp(startScale.x, endScale.x, lerpValue) + squashStretchFactor * 0.1f;
                float yScale = Mathf.Lerp(startScale.y, endScale.y, lerpValue) - squashStretchFactor * 0.1f;

                rectTransform.localScale = new Vector3(xScale, yScale, 1);
                yield return null;
            }

            rectTransform.localScale = endScale;
            rectTransform.gameObject.SetActive(setActiveOnFinish);
            callback?.Invoke();
        }
    }
}
