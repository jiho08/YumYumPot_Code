using Code.Combat;
using Code.Core.Events;
using Code.Entities;
using UnityEngine;

namespace Code.Players
{
    public class PlayerData : MonoBehaviour, IEntityComponent
    {
        private Entity _entity;

        [field: SerializeField] public float Luck { get; private set; }
        [field: SerializeField] public int Seed { get; set; }
        [field: SerializeField] public int Exp { get; private set; }
        [field: SerializeField] public int LevelUpExp { get; private set; }
        [field: SerializeField] public int Level { get; private set; }
        [field: SerializeField] public LevelUpTableSO LevelUpTable { get; private set; }

        public void Initialize(Entity entity)
        {
            _entity = entity;
            Bus<AddExpEvent>.Subscribe(HandleAddExp);
            Bus<ChangeSeedCountEvent>.Subscribe(HandleSeedCount);
            UpdateLevelUpExp();
        }

        private void OnDestroy()
        {
            Bus<AddExpEvent>.Unsubscribe(HandleAddExp);
            Bus<ChangeSeedCountEvent>.Unsubscribe(HandleSeedCount);
        }

        private void Start()
        {
            Bus<PlayerExpEvent>.Raise(new PlayerExpEvent(Exp, LevelUpExp));
            Bus<ChangeSeedCountEvent>.Raise(new ChangeSeedCountEvent(Seed));
        }

        private void HandleAddExp(AddExpEvent evt)
        {
            AddExp(evt.Amount);
        }

        private void HandleSeedCount(ChangeSeedCountEvent evt)
        {
            Seed = evt.SeedCount;
        }

        private void AddExp(int amount)
        {
            Exp += amount;

            if (Exp >= LevelUpExp)
                LevelUpProcess();
            
            Bus<PlayerExpEvent>.Raise(new PlayerExpEvent(Exp, LevelUpExp));
        }

        private void LevelUpProcess()
        {
            Exp -= LevelUpExp;
            ++Level;
            UpdateLevelUpExp();
            Bus<LevelUpEvent>.Raise(new LevelUpEvent(Level));
        }

        private void UpdateLevelUpExp()
        {
            LevelUpExp = LevelUpTable.GetRequireExpForNextLevel(Level + 1);
        }
    }
}