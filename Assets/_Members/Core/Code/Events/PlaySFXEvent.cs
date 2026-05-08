using Code.Core.Sounds;
using UnityEngine;

namespace Code.Core.Events
{
    public struct PlaySFXEvent : IEvent
    {
        public SoundSO SFXClip { get; }
        public Vector2 Position { get; }
        
        public PlaySFXEvent(SoundSO sfxClip, Vector2 position)
        {
            SFXClip = sfxClip;
            Position = position;
        }
    }
}