namespace Code.Core.Events
{
    public struct AddExpEvent : IEvent
    {
        public int Amount { get; }

        public AddExpEvent(int amount)
        {
            Amount = amount;
        }
    }
}