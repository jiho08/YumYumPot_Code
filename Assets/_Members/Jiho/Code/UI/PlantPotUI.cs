using Code.Core.Events;
using Code.Managers;
using Code.Plant;
using GondrLib.ObjectPool.RunTime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    public class PlantPotUI : MonoBehaviour, IPoolable
    {
        [field: SerializeField] public PoolItemSO PoolItem { get; private set; }
        [SerializeField] private Button plantButton;
        [SerializeField] private Button eatButton;
        [SerializeField] private Image plantImage;
        [SerializeField] private Sprite defaultImage;
        [SerializeField] private TextMeshProUGUI plantNameText;
            
        public GameObject GameObject => gameObject;

        private PlantPot _plantPot;
        private Pool _myPool;

        [ContextMenu("Init")]
        public void Initialize()
        {
            plantButton.onClick.AddListener(HandlePlant);
            eatButton.onClick.AddListener(HandleEat);
            
            Bus<ChangePlantPotEvent>.Subscribe(HandleChangePlantPot);
        }

        private void OnDisable()
        {
            plantButton.onClick.RemoveListener(HandlePlant);
            eatButton.onClick.RemoveListener(HandleEat);
            
            Bus<ChangePlantPotEvent>.Unsubscribe(HandleChangePlantPot);
        }

        private void HandleChangePlantPot(ChangePlantPotEvent evt)
        {
            if (evt.PlantPot != _plantPot)
                return;
            
            plantImage.sprite = _plantPot.Crops.Sprite;
            plantNameText.text = _plantPot.Crops.CropsVisualName;
        }

        public void SetPlantPot(PlantPot plantPot)
        {
            _plantPot = plantPot;
        }
        
        public void SetUpPool(Pool pool)
        {
            _myPool = pool;
        }

        public void ResetItem()
        {
        }

        private void HandlePlant()
        {
            // 여기서 화분에 심어지게 하기
            if (_plantPot.Crops != null || SeedManager.Instance.PlayerData.Seed <= 0)
                return;
            
            Bus<ChangeSeedCountEvent>.Raise(new ChangeSeedCountEvent(--SeedManager.Instance.PlayerData.Seed));
            
            var seed = SeedManager.Instance.GetRandomSeed();
            
            _plantPot.SetCrops(seed);
            plantImage.sprite = _plantPot.Crops.Sprite;
            plantNameText.text = _plantPot.Crops.CropsVisualName;
        }

        private void HandleEat()
        {
            // 화분에 있던 농작물 섭취
            if (_plantPot.Crops == null || !_plantPot.Crops.CanEat)
                return;
            
            _plantPot.EatCrops();
            plantImage.sprite = defaultImage;
            plantNameText.text = "비어 있음";
        }
    }
}