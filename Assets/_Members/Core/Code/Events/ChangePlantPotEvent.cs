using Code.Plant;

namespace Code.Core.Events
{
    public struct ChangePlantPotEvent : IEvent
    {
        public PlantPot PlantPot { get; }

        public ChangePlantPotEvent(PlantPot plantPot)
        {
            PlantPot = plantPot;
        }
    }
}