using Code.Core.Events;
using TMPro;
using UnityEngine;

namespace Code.UI
{
    public class SeedCountUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI seedCountText;

        private void OnEnable()
        {
            Bus<ChangeSeedCountEvent>.Subscribe(HandleSeedCount);
        }

        private void OnDisable()
        {
            Bus<ChangeSeedCountEvent>.Unsubscribe(HandleSeedCount);
        }

        private void HandleSeedCount(ChangeSeedCountEvent evt)
        {
            seedCountText.text = $"X {evt.SeedCount}";
        }
    }
}