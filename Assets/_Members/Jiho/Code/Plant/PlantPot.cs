using System.Collections.Generic;
using Code.Core.Events;
using Code.Managers;
using GondrLib.ObjectPool.RunTime;
using UnityEngine;

namespace Code.Plant
{
    public class PlantPot : MonoBehaviour, IPoolable
    {
        [field : SerializeField] public PoolItemSO PoolItem { get; private set; }
        [field: SerializeField] public Crops.Crops Crops { get; private set; }
        public GameObject GameObject => gameObject;

        [SerializeField] private PoolManagerSO poolManager;

        private string _cropsName;
        private Sprite _defaultSprite;
        private Dictionary<string, GameObject> _plantDict = new();

        private void Update()
        {
            Crops?.CorpsUpdate();
        }

        private void OnEnable()
        {
            Bus<CropsGrowEvent>.Subscribe(HandleCropsGrow);
            
            _plantDict.Clear();

            var animators = GetComponentsInChildren<Animator>(true);

            foreach (var animator in animators)
            {
                var obj = animator.gameObject;

                if (!_plantDict.TryAdd(obj.name, obj))
                    continue;
            }
        }

        private void OnDisable()
        {
            Bus<CropsGrowEvent>.Unsubscribe(HandleCropsGrow);
        }

        private void HandleCropsGrow(CropsGrowEvent evt)
        {
            if (evt.Crops != Crops)
                return;
            
            SetPlantVisual(evt.Crops.CropsName);
            
            Bus<ChangePlantPotEvent>.Raise(new ChangePlantPotEvent(this));
        }

        public void SetCrops(PoolItemSO cropsItem)
        {
            var crops = poolManager.Pop(cropsItem) as Crops.Crops;
            
            crops.transform.SetParent(transform);
            crops.transform.localPosition = Vector3.zero;
            Crops = crops;
            
            SetPlantVisual(Crops.CropsName);
        }

        public void EatCrops()
        {
            // 능력치 올려주는 이벤트 발행하기
            // UI도
            StatManager.Instance.ApplyStat(Crops.EatStatType, Crops.EatStatValue);
            Bus<EatCropsEvent>.Raise(new EatCropsEvent(Crops.EatStatType, Crops.EatStatValue));
            Crops = null;
            SetPlantVisual(null);
        }

        private void SetPlantVisual(string key)
        {
            foreach (var kv in _plantDict)
                kv.Value.SetActive(false);

            if (key == null)
                return;
            
            if (_plantDict.TryGetValue(key, out var obj))
                obj.SetActive(true);
        }

        public void ReduceGrowTimer(float value)
        {
            if (Crops == null)
                return;
            
            Crops.AddGrowTimer(value);
        }
        
        public void SetUpPool(Pool pool)
        {
        }

        public void ResetItem()
        {
        }
    }
}