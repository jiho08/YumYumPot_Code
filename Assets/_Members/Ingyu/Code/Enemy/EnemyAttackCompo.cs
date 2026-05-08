using Code.Entities;
using UnityEngine;

public class EnemyAttackCompo : MonoBehaviour, IEntityComponent
{
    [SerializeField] protected int damage;
    [SerializeField] private EnemyAttackLogic attackLogic;
    private Entity _entity;

    public EnemyAttackLogic AttackLogic => attackLogic;

    public void Initialize(Entity entity)
    {
        _entity = entity;
    }

    public void Attack()
    {
        if (attackLogic == null)
            return;

        attackLogic.Attack(damage);
    }
    public void SetDamage(int value)
    {
        damage = value;
    }
}
