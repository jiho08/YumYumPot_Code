using Code.Core.Events;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Image fillImage;
        [SerializeField] private float tweenDuration = 0.25f;
        [SerializeField] private Ease ease = Ease.OutQuad;

        private Tween _fillTween;
        
        private void OnEnable()
        {
            Bus<PlayerHealthEvent>.Subscribe(HandlePlayerHealth);
        }

        private void OnDisable()
        {
            Bus<PlayerHealthEvent>.Unsubscribe(HandlePlayerHealth);
            _fillTween?.Kill();
        }

        private void HandlePlayerHealth(PlayerHealthEvent evt)
        {
            float targetFill = evt.Health / evt.MaxHealth;
            
            _fillTween?.Kill();
            _fillTween = fillImage.DOFillAmount(targetFill, tweenDuration)
                .SetEase(ease);
        }
    }
}