using Code.Core.Sounds;

namespace Code.Core.Events
{
    public struct PlayBGMEvent : IEvent
    {
        public SoundSO BGMClip { get; }

        public PlayBGMEvent(SoundSO bgmClip)
        {
            BGMClip = bgmClip;
        }
    }
}