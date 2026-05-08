using GondrLib.ObjectPool.RunTime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private List<WaveData> waves;
    [SerializeField] private Spawner spawner;
    [SerializeField] private TextMeshProUGUI timerText;

    public static System.Action<int> OnWaveChanged;

    Dictionary<PoolItemSO, WaveUnit> unitByItem = new();
    Dictionary<PoolItemSO, int> aliveCount = new();

    int currentWaveIndex;
    float currentTime;
    const float WAVE_TIME = 60f;
    bool waveRunning;

    void OnEnable()
    {
        Enemy.OnEnemyDeadForWave += HandleEnemyDead;
    }

    void OnDisable()
    {
        Enemy.OnEnemyDeadForWave -= HandleEnemyDead;
    }

    void Start()
    {
        StartWave();
    }

    void Update()
    {
        if (!waveRunning)
            return;

        currentTime -= Time.deltaTime;
        UpdateTimerUI();

        if (currentTime <= 0f || Input.GetKeyDown(KeyCode.Y))
        {
            NextWave();
        }
    }

    void StartWave()
    {
        if (currentWaveIndex >= waves.Count)
            return;

        StopAllCoroutines();

        currentTime = WAVE_TIME;
        waveRunning = true;

        OnWaveChanged?.Invoke(currentWaveIndex + 1);

        WaveData wave = waves[currentWaveIndex];

        foreach (var unit in wave.enemies)
        {
            if (!unitByItem.ContainsKey(unit.enemyItem))
            {
                unitByItem.Add(unit.enemyItem, unit);
                aliveCount.Add(unit.enemyItem, 0);
            }

            for (int i = 0; i < unit.count; i++)
            {
                SpawnUnit(unit);
            }
        }
    }

    void SpawnUnit(WaveUnit unit)
    {
        Enemy enemy = spawner.Spawn(
            unit.enemyItem,
            unit.stat,
            GetStatMultiplier()
        );

        if (enemy == null)
            return;

        aliveCount[unit.enemyItem]++;
    }

    void HandleEnemyDead(Enemy enemy)
    {
        if (!waveRunning)
            return;

        PoolItemSO item = enemy.PoolItem;

        if (!aliveCount.ContainsKey(item))
            return;

        aliveCount[item]--;

        WaveUnit unit = unitByItem[item];

        if (aliveCount[item] < unit.count)
        {
            StartCoroutine(RespawnRoutine(unit));
        }
    }

    IEnumerator RespawnRoutine(WaveUnit unit)
    {
        yield return new WaitForSeconds(unit.spawnDelay);

        if (!waveRunning)
            yield break;

        SpawnUnit(unit);
    }

    void NextWave()
    {
        waveRunning = false;
        StopAllCoroutines();

        currentWaveIndex++;
        StartWave();
    }

    float GetStatMultiplier()
    {
        return 1f + (currentWaveIndex * 0.05f);
    }

    void UpdateTimerUI()
    {
        int sec = Mathf.CeilToInt(currentTime);
        timerText.text = $"남은 시간 : {sec}s";
    }
}
