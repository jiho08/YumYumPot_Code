using System;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static Action<int> OnStageChanged;

    int currentStage = 1;

    void StartStage()
    {
        OnStageChanged?.Invoke(currentStage);
    }

    void EndStage()
    {
        currentStage++;
        StartStage();
    }
}
