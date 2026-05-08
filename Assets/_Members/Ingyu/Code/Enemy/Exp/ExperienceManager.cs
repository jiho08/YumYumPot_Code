using UnityEngine;

public class ExperienceManager : MonoBehaviour
{
    private void OnEnable()
    {
        Enemy.OnEnemyDead += HandleEnemyDead;
    }

    private void OnDisable()
    {
        Enemy.OnEnemyDead -= HandleEnemyDead;
    }

    void HandleEnemyDead(int amount)
    {
        PlayerExp.Instance.AddExp(amount);
    }
}
