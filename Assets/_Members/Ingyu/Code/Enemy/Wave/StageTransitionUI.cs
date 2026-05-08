using UnityEngine;
using TMPro;
using DG.Tweening;

public class StageTransitionUI : MonoBehaviour
{
    [SerializeField] private RectTransform panel;
    [SerializeField] private TMP_Text stageText;

    [SerializeField] private float moveTime = 0.8f;
    [SerializeField] private float stayTime = 2f;

    float hideY = 900f;
    float showY = 0f;

    int cheatWave = 1;

    void Awake()
    {
        panel.anchoredPosition =
            new Vector2(panel.anchoredPosition.x, hideY);
    }

    void OnEnable()
    {
        WaveManager.OnWaveChanged += Play;
    }

    void OnDisable()
    {
        WaveManager.OnWaveChanged -= Play;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            cheatWave++;
            Play(cheatWave);
        }
    }

    void Play(int wave)
    {
        stageText.text = $"¿þÀÌºê {wave}";

        panel.DOKill();

        Sequence seq = DOTween.Sequence();
        seq.Append(panel.DOAnchorPosY(showY, moveTime).SetEase(Ease.OutCubic));
        seq.AppendInterval(stayTime);
        seq.Append(panel.DOAnchorPosY(hideY, moveTime).SetEase(Ease.InCubic));
    }
}
