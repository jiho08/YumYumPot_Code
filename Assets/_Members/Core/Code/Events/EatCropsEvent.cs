using Code.Combat;

namespace Code.Core.Events
{
    public struct EatCropsEvent : IEvent
    {
        public StatType StatType { get; }
        public float Value { get; }

        public EatCropsEvent(StatType statType, float value)
        {
            StatType = statType;
            Value = value;
        }
    }
}