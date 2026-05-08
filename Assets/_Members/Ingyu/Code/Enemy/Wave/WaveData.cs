using GondrLib.ObjectPool.RunTime;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wave/WaveData")]
public class WaveData : ScriptableObject
{
    public List<WaveUnit> enemies;
}

[System.Serializable]   
public class WaveUnit
{
    public PoolItemSO enemyItem;
    public EnemyStatSO stat;
    public int count;
    public float spawnDelay;
}
