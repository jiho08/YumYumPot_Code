using System.Collections.Generic;
using Code.Core;
using Code.Core.Events;
using Code.Players;
using GondrLib.Dependencies;
using GondrLib.ObjectPool.RunTime;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Managers
{
    public class SeedManager : MonoSingleton<SeedManager>
    {
        [SerializeField] private List<PoolItemSO> cropsList;
        [Inject] private Player player;

        public PlayerData PlayerData { get; private set; }

        private void Start()
        {
            PlayerData = player.GetCompo<PlayerData>();
            
            Bus<SelectSeedEvent>.Subscribe(HandleAddSeed);
        }

        private void OnDestroy()
        {
            Bus<SelectSeedEvent>.Unsubscribe(HandleAddSeed);
        }

        public PoolItemSO GetRandomSeed()
        {
            if (cropsList == null || cropsList.Count == 0)
                return null;
            
            return cropsList[Random.Range(0, cropsList.Count)];
        }

        private void HandleAddSeed(SelectSeedEvent evt)
        {
            ++PlayerData.Seed;
            Bus<ChangeSeedCountEvent>.Raise(new ChangeSeedCountEvent(PlayerData.Seed));
        }
    }
}