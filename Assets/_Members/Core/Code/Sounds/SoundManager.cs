using Code.Core.Events;
using GondrLib.Dependencies;
using GondrLib.ObjectPool.RunTime;
using UnityEngine;

namespace Code.Core.Sounds
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private PoolItemSO soundPlayer;

        [Inject] private PoolManagerMono _poolManager;

        private SoundPlayer _bgmPlayer;
        
        //private Dictionary<int, SoundPlayer> _longSoundDict = new();
        
        private void OnEnable()
        {
            Bus<PlaySFXEvent>.Subscribe(HandlePlaySFX);
            Bus<PlayBGMEvent>.Subscribe(HandlePlayBGM);
            Bus<StopBGMEvent>.Subscribe(HandleStopBGM);
        }

        private void OnDestroy()
        {
            Bus<PlaySFXEvent>.Unsubscribe(HandlePlaySFX);
            Bus<PlayBGMEvent>.Unsubscribe(HandlePlayBGM);
            Bus<StopBGMEvent>.Unsubscribe(HandleStopBGM);
        }

        private void HandlePlaySFX(PlaySFXEvent evt)
        {
            var sfxPlayer = _poolManager.Pop<SoundPlayer>(soundPlayer);
            sfxPlayer.transform.position = evt.Position;
            sfxPlayer.PlaySound(evt.SFXClip);
        }
        
        private void HandlePlayBGM(PlayBGMEvent evt)
        {
            _bgmPlayer = _poolManager.Pop<SoundPlayer>(soundPlayer);
            _bgmPlayer.PlaySound(evt.BGMClip);
        }

        private void HandleStopBGM(StopBGMEvent evt) => _bgmPlayer?.StopAndGoToPool();
    }
}