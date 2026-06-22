using System.Collections;
using UnityEngine;

namespace Compose.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class BasePanel : MonoDul
    {
        [SerializeField] private bool showOnStart = true;
        [SerializeField] private float fadeDuration = 0.2f;

        private CanvasGroup canvasGroup;
        private Coroutine fadeCoroutine;

        public bool IsVisible { get; private set; }
        public bool ShowOnStart => showOnStart;

        public void Show(bool immediately = false)
        {
            EnsureCanvasGroup();
            gameObject.SetActive(true);
            IsVisible = true;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            StartFade(1f, immediately, true);
        }

        public void Hide(bool immediately = false)
        {
            EnsureCanvasGroup();
            IsVisible = false;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            StartFade(0f, immediately, false);
        }

        private void StartFade(float target, bool immediately, bool activeAfterFade)
        {
            if (fadeCoroutine != null)
                StopCoroutine(fadeCoroutine);

            if (immediately || fadeDuration <= 0f)
            {
                SetFadeResult(target, activeAfterFade);
                return;
            }

            fadeCoroutine = StartCoroutine(Fade(target, activeAfterFade));
        }

        private IEnumerator Fade(float target, bool activeAfterFade)
        {
            var start = canvasGroup.alpha;
            var time = 0f;

            while (time < fadeDuration)
            {
                time += Time.unscaledDeltaTime;
                canvasGroup.alpha = Mathf.Lerp(start, target, time / fadeDuration);
                yield return null;
            }

            SetFadeResult(target, activeAfterFade);
        }

        private void SetFadeResult(float alpha, bool active)
        {
            fadeCoroutine = null;
            canvasGroup.alpha = alpha;
            canvasGroup.interactable = active;
            canvasGroup.blocksRaycasts = active;

            if (!active)
                gameObject.SetActive(false);
        }

        private void EnsureCanvasGroup()
        {
            if (canvasGroup == null)
                canvasGroup = GetComponent<CanvasGroup>();
        }
    }
}
