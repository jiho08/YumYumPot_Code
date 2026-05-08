using UnityEngine;

[CreateAssetMenu(menuName = "Data/Enemy/Stat")]
public class EnemyStatSO : ScriptableObject
{
    public int maxHp = 30;
    public int damage = 5;
    public int exp = 50;
}
