using Code.Core.Events;
using UnityEngine;

namespace Code.UI
{
    public class RewardManageUI : MonoBehaviour
    {
        [SerializeField] private GameObject rewardSelectUI;

        private void OnEnable()
        {
            Bus<LevelUpEvent>.Subscribe(HandleLevelUp);
        }

        private void OnDisable()
        {
            Bus<LevelUpEvent>.Unsubscribe(HandleLevelUp);
        }

        private void HandleLevelUp(LevelUpEvent evt)
        {
            rewardSelectUI.SetActive(true);
        }
    }
}