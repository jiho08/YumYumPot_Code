namespace Code.Core.Events
{
    public struct PlayerHealthEvent : IEvent
    {
        public float Health { get; }
        public float MaxHealth { get; }

        public PlayerHealthEvent(float health, float maxHealth)
        {
            Health = health;
            MaxHealth = maxHealth;
        }
    }
}