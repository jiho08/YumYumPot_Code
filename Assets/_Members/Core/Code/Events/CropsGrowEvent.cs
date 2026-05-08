using Code.Plant.Crops;

namespace Code.Core.Events
{
    public struct CropsGrowEvent : IEvent
    {
        public Crops Crops { get; }

        public CropsGrowEvent(Crops crops)
        {
            Crops = crops;
        }
    }
}