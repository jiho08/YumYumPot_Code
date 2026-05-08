using UnityEngine;

namespace Code.Core.Events
{
    public struct PopUpTextEvent : IEvent
    {
        public string Text { get; }
        public int TextTypeHash { get; }
        public Vector2 Position { get; }
        public float ShowDuration { get; }

        public PopUpTextEvent(string text, int textTypeHash, Vector2 position, float showDuration)
        {
            Text = text;
            TextTypeHash = textTypeHash;
            Position = position;
            ShowDuration = showDuration;
        }
    }
}