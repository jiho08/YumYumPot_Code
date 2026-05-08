namespace Code.Core.Events
{
    public struct LevelUpEvent : IEvent
    {
        public int Level { get; }

        public LevelUpEvent(int level)
        {
            Level = level;
        }
    }
}