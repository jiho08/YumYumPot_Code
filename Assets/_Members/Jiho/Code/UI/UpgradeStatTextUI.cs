using Code.Combat;
using Code.Core.Events;
using TMPro;
using UnityEngine;

namespace Code.UI
{
    public class UpgradeStatTextUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI statText;

        private void OnEnable()
        {
            Bus<EatCropsEvent>.Subscribe(HandleEatCrops);
            SetActive(false);
        }

        private void OnDisable()
        {
            Bus<EatCropsEvent>.Unsubscribe(HandleEatCrops);
        }

        private void HandleEatCrops(EatCropsEvent evt)
        {
            SetActive(true);

            SetText(evt.StatType, evt.Value);
        }

        public async void SetText(StatType statType, float value)
        {
            string statTypeText = "";
            
            switch (statType)
            {
                case StatType.AttackPower:
                    statTypeText = "공격력이";
                    break;
                
                case StatType.AttackSpeed:
                    statTypeText = "공격 속도가";
                    break;
                
                case StatType.Health:
                    statTypeText = "최대 체력이";
                    break;
                
                case StatType.MoveSpeed:
                    statTypeText = "이동 속도가";
                    break;
            }

            statText.text = statTypeText + $" {value} 증가했습니다!";
            await Awaitable.WaitForSecondsAsync(2f);
            
            SetActive(false);
        }

        private void SetActive(bool isActive)
        {
            statText.gameObject.SetActive(isActive);
        }
    }
}