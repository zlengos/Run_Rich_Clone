using System;
using DG.Tweening;
using UnityEngine;

namespace Tools
{
    public static class CanvasHandler
    {
        public static void ShowCanvas(CanvasGroup canvasGroup, Action onComplete = null, float duration = 0.5f)
        {
            if (canvasGroup != null)
            {
                canvasGroup.DOFade(1f, duration).OnComplete(() =>
                {
                    canvasGroup.interactable = true;
                    canvasGroup.blocksRaycasts = true;
                    
                    onComplete?.Invoke();
                });
            }
        }

        public static void HideCanvas(CanvasGroup canvasGroup, Action onComplete = null, float duration = 0.5f)
        {
            if (canvasGroup != null)
            {
                canvasGroup.DOFade(0f, duration).OnComplete(() =>
                {
                    canvasGroup.interactable = false;
                    canvasGroup.blocksRaycasts = false;

                    onComplete?.Invoke();
                });
            }
        }

        public static void ToggleCanvas(CanvasGroup canvasGroup, bool isActive, float duration = 0.5f)
        {
            if (canvasGroup != null)
            {
                var endValue = isActive ? 1f : 0f;

                canvasGroup.DOFade(endValue, duration).OnComplete(() =>
                {
                    canvasGroup.interactable = isActive;
                    canvasGroup.blocksRaycasts = isActive;
                });
            }
        }
    }
}