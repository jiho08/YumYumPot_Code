using Code.Core.Events;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Code.UI
{
    public class RewardSelectUI : MonoBehaviour
    {
        [SerializeField] private Button plantPotCard;
        [SerializeField] private Button fertilizerCard;
        [SerializeField] private Button seedCard;
        [SerializeField] private float hoverScale = 1.1f;
        [SerializeField] private float tweenDuration = 0.25f;

        [SerializeField] private int potCount;
        
        private void OnEnable()
        {
            plantPotCard.onClick.AddListener(HandlePlantPot);
            fertilizerCard.onClick.AddListener(HandleFertilizer);
            seedCard.onClick.AddListener(HandleSeed);
            
            AddHoverEffect(plantPotCard);
            AddHoverEffect(fertilizerCard);
            AddHoverEffect(seedCard);
            
            if (potCount > 5)
                plantPotCard.gameObject.SetActive(false);

            SetTime(true);
        }

        private void OnDisable()
        {
            plantPotCard.onClick.RemoveListener(HandlePlantPot);
            fertilizerCard.onClick.RemoveListener(HandleFertilizer);
            seedCard.onClick.RemoveListener(HandleSeed);

            ResetButton(plantPotCard);
            ResetButton(fertilizerCard);
            ResetButton(seedCard);
        }

        private void ResetButton(Button button)
        {
            var t = button.transform;
            t.DOKill();
            t.localScale = Vector3.one;
        }
        
        private void AddHoverEffect(Button button)
        {
            var trigger = button.gameObject.GetComponent<EventTrigger>();

            if (trigger == null)
                trigger = button.gameObject.AddComponent<EventTrigger>();
            
            trigger.triggers.Clear();
            
            var enter = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerEnter
            };
            
            enter.callback.AddListener(_ => OnHoverEnter(button.transform));
            trigger.triggers.Add(enter);

            var exit = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerExit
            };
            
            exit.callback.AddListener(_ => OnHoverExit(button.transform));
            trigger.triggers.Add(exit);
        }
        
        private void OnHoverEnter(Transform target)
        {
            target.DOKill();
            target.DOScale(hoverScale, tweenDuration)
                .SetEase(Ease.OutBack)
                .SetUpdate(true);
        }

        private void OnHoverExit(Transform target)
        {
            target.DOKill();
            target.DOScale(1f, tweenDuration)
                .SetEase(Ease.OutQuad)
                .SetUpdate(true);
        }

        private void HandlePlantPot()
        {
            ++potCount;
            Bus<SelectPlantPotEvent>.Raise(new SelectPlantPotEvent());
            SetTime(false);
            gameObject.SetActive(false);
        }

        private void HandleFertilizer()
        {
            Bus<SelectFertilizerEvent>.Raise(new SelectFertilizerEvent());
            SetTime(false);
            gameObject.SetActive(false);
        }

        private void HandleSeed()
        {
            Bus<SelectSeedEvent>.Raise(new SelectSeedEvent());
            SetTime(false);
            gameObject.SetActive(false);
        }
        
        private void SetTime(bool isStop)
        => Time.timeScale = isStop ? 0 : 1;
    }
}