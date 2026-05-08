using System;
using Code.Core.Debug;
using Code.Core.Events;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    public class UIExpBar : MonoBehaviour
    {
        [SerializeField] private Image fillImage;
        [SerializeField] private float tweenDuration = 0.25f;
        [SerializeField] private Ease ease = Ease.OutQuad;

        private Tween _fillTween;

        private void OnEnable()
        {
            fillImage.fillAmount = 0f;

            // if (PlayerExp.Instance != null)
            //     PlayerExp.Instance.OnExpRatioChanged += HandlePlayerExp;
            
            Bus<PlayerExpEvent>.Subscribe(HandlePlayerExp);
        }

        private void OnDisable()
        {
            // if (PlayerExp.Instance != null)
            //     PlayerExp.Instance.OnExpRatioChanged -= HandlePlayerExp;

            _fillTween?.Kill();
            Bus<PlayerExpEvent>.Unsubscribe(HandlePlayerExp);
        }

        private void HandlePlayerExp(PlayerExpEvent evt)
        {
            UnityLogger.Log("경험치 바");
            _fillTween?.Kill();
            _fillTween = fillImage.DOFillAmount(evt.CurrentExp / evt.MaxExp, tweenDuration)
                .SetEase(ease);
        }
    }
}
