using Code.Core.Events;
using Code.Plant;
using GondrLib.Dependencies;
using GondrLib.ObjectPool.RunTime;
using UnityEngine;

namespace Code.Managers
{
    public class PlantPotManager : MonoBehaviour
    {
        [SerializeField] private PoolItemSO plantPotItem;
        [Inject] private PoolManagerMono poolManager;
        
        private void Awake()
        {
            Bus<SelectPlantPotEvent>.Subscribe(HandleSelectPlantPot);
        }

        private void Start()
        {
            var plantPot = poolManager.Pop<PlantPot>(plantPotItem);
            Bus<AddPlantPotEvent>.Raise(new AddPlantPotEvent(plantPot));
        }

        private void HandleSelectPlantPot(SelectPlantPotEvent evt)
        {
            var plantPot = poolManager.Pop<PlantPot>(plantPotItem);
            Bus<AddPlantPotEvent>.Raise(new AddPlantPotEvent(plantPot));
        }
    }
}