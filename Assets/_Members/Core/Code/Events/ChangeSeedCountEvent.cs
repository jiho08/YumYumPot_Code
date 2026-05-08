namespace Code.Core.Events
{
    public struct ChangeSeedCountEvent : IEvent
    {
        public int SeedCount { get; }

        public ChangeSeedCountEvent(int seedCount)
        {
            SeedCount = seedCount;
        }
    }
}