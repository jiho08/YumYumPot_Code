namespace Code.Core.Events
{
    public struct PlayerExpEvent : IEvent
    {
        public float CurrentExp { get; }
        public float MaxExp { get; }

        public PlayerExpEvent(float current, float max)
        {
            CurrentExp = current;
            MaxExp = max;
        }
    }
}