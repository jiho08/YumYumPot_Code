using Code.Core.Events;
using DG.Tweening;
using GondrLib.ObjectPool.RunTime;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    public class PlantPotContainerUI : MonoBehaviour
    {
        [SerializeField] private Button toggleButton;
        [SerializeField] private RectTransform plantPotPanel;
        [SerializeField] private float moveDistance = 400f;
        [SerializeField] private float duration = 0.25f;
        [SerializeField] private PoolManagerMono poolManager;
        [SerializeField] private PoolItemSO plantPotItem;
        [SerializeField] private Transform content;
        
        private Vector2 _defaultPos;
        private bool _isActive;
        private Tween _moveTween;

        private void Awake()
        {
            _defaultPos = plantPotPanel.anchoredPosition;
        }

        private void OnEnable()
        {
            toggleButton.onClick.AddListener(HandleToggle);
            Bus<AddPlantPotEvent>.Subscribe(HandleAddPlantPot);
        }

        private void OnDisable()
        {
            toggleButton.onClick.RemoveListener(HandleToggle);
            Bus<AddPlantPotEvent>.Unsubscribe(HandleAddPlantPot);
        }

        private void HandleToggle()
        {
            _moveTween?.Kill();

            Vector2 targetPos = _isActive ?
                _defaultPos : _defaultPos + Vector2.right * moveDistance;

            _moveTween = plantPotPanel
                .DOAnchorPos(targetPos, duration)
                .SetEase(Ease.OutCubic);

            _isActive = !_isActive;
        }

        private void HandleAddPlantPot(AddPlantPotEvent evt)
        {
            var plantPotUI = poolManager.Pop<PlantPotUI>(plantPotItem);
            plantPotUI.transform.SetParent(content);
            plantPotUI.SetPlantPot(evt.PlantPot);
            plantPotUI.Initialize();
        }

        [ContextMenu("AddPlantPot")]
        private void TestAddPlantPot()
        {
            var plantPotUI = poolManager.Pop<PlantPotUI>(plantPotItem);
            plantPotUI.transform.SetParent(content);
            plantPotUI.Initialize();
        }
    }
}