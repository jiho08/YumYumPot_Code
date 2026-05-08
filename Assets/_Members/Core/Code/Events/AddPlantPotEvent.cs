using Code.Plant;

namespace Code.Core.Events
{
    public struct AddPlantPotEvent : IEvent
    {
        public PlantPot PlantPot { get; }

        public AddPlantPotEvent(PlantPot plantPot)
        {
            PlantPot = plantPot;
        }
    }
}