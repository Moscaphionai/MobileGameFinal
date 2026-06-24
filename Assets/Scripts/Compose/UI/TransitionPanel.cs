using System;
using System.Collections;
using UnityEngine;

namespace Compose.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public sealed class TransitionPanel : MonoDul
    {
        [SerializeField] private float fadeDuration = 0.4f;

        private CanvasGroup canvasGroup;
        private Coroutine fadeCoroutine;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void FadeOut(Action onComplete)
        {
            gameObject.SetActive(true);
            StartFade(1f, true, onComplete);
        }

        public void FadeIn(Action onComplete = null)
        {
            gameObject.SetActive(true);
            canvasGroup.alpha = 1f;
            StartFade(0f, false, onComplete);
        }

        private void StartFade(float target, bool blocksAfterFade, Action onComplete)
        {
            if (fadeCoroutine != null)
                StopCoroutine(fadeCoroutine);

            if (fadeDuration <= 0f)
            {
                SetFadeResult(target, blocksAfterFade);
                onComplete?.Invoke();
                return;
            }

            fadeCoroutine = StartCoroutine(Fade(target, blocksAfterFade, onComplete));
        }

        private IEnumerator Fade(float target, bool blocksAfterFade, Action onComplete)
        {
            var start = canvasGroup.alpha;
            var time = 0f;

            canvasGroup.blocksRaycasts = true;
            canvasGroup.interactable = false;

            while (time < fadeDuration)
            {
                time += Time.unscaledDeltaTime;
                canvasGroup.alpha = Mathf.Lerp(start, target, time / fadeDuration);
                yield return null;
            }

            SetFadeResult(target, blocksAfterFade);
            onComplete?.Invoke();
        }

        private void SetFadeResult(float alpha, bool blocksRaycasts)
        {
            fadeCoroutine = null;
            canvasGroup.alpha = alpha;
            canvasGroup.blocksRaycasts = blocksRaycasts;
            canvasGroup.interactable = false;
        }
    }
}
