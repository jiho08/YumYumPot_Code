using UnityEngine;

public abstract class EnemyAttackLogic : MonoBehaviour
{
    public virtual bool IsLockingMovement => false;
    public bool IsAttacking { get; protected set; }
        
    public abstract void Attack(float damage);
}
