using GondrLib.ObjectPool.RunTime;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private PoolManagerMono poolManager;
    [SerializeField] private Transform[] spawnPoints;

    public Enemy Spawn(PoolItemSO item, EnemyStatSO stat, float multiplier)
    {
        if (spawnPoints == null || spawnPoints.Length == 0)
            return null;

        Transform point = spawnPoints[Random.Range(0, spawnPoints.Length)];

        Enemy enemy = poolManager.Pop<Enemy>(item);
        enemy.gameObject.SetActive(true);
        if (enemy == null)
            return null;

        enemy.gameObject.SetActive(true);
        enemy.transform.position = point.position;

        enemy.ApplyStat(stat, multiplier);

        return enemy;
    }
}
