using System;
using UnityEngine;
using Code.Combat;

public class PlayerExp : MonoBehaviour
{
    public static PlayerExp Instance { get; private set; }

    public event Action<float> OnExpRatioChanged;

    [SerializeField] private LevelUpTableSO levelTable;

    public int CurrentLevel { get; private set; } = 1;
    public int CurrentExp { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void OnEnable()
    {
        Enemy.OnEnemyDeadExp += AddExp;
    }

    void OnDisable()
    {
        Enemy.OnEnemyDeadExp -= AddExp;
    }
    public void AddExp(int exp)
    {
        if (CurrentLevel >= levelTable.maxLevel)
            return;

        CurrentExp += exp;

        while (true)
        {
            int requireExp =
                levelTable.GetRequireExpForNextLevel(CurrentLevel + 1);

            if (requireExp < 0 || CurrentExp < requireExp)
                break;

            CurrentExp -= requireExp;
            CurrentLevel++;
        }

        InvokeExpRatioChanged();
    }

    void InvokeExpRatioChanged()
    {
        int requireExp =
            levelTable.GetRequireExpForNextLevel(CurrentLevel + 1);

        float ratio = 0f;

        if (requireExp > 0)
            ratio = (float)CurrentExp / requireExp;

        OnExpRatioChanged?.Invoke(ratio);
    }
}
