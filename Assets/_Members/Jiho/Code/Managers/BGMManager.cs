using Code.Core.Events;
using Code.Core.Sounds;
using UnityEngine;

namespace Code.Managers
{
    public class BGMManager : MonoBehaviour
    {
        [SerializeField] private SoundSO bgm;
        
        private void Start()
        {
            Bus<PlayBGMEvent>.Raise(new PlayBGMEvent(bgm));
        }
    }
}