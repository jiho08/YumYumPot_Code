using Code.Combat;
using Code.Players;
using UnityEngine;
using System.Collections;

public class MeleeEnemyAttackLogic : EnemyAttackLogic
{
    private Player targetPlayer;

    [SerializeField] private float range = 1.2f;
    [SerializeField] private float attackDelay = 1f;
    [SerializeField] private float cooldown = 1f;

    void Update()
    {
        if (targetPlayer == null)
            targetPlayer = FindAnyObjectByType<Player>();

        if (targetPlayer != null && !IsInRange(targetPlayer, range))
            targetPlayer = null;
    }

    public override void Attack(float damage)
    {
        if (IsAttacking || targetPlayer == null)
            return;

        StartCoroutine(AttackRoutine(damage));
    }

    IEnumerator AttackRoutine(float damage)
    {
        IsAttacking = true;

        yield return new WaitForSeconds(attackDelay);

        if (targetPlayer != null &&
            targetPlayer.TryGetComponent<IDamageable>(out var d))
        {
            d.ApplyDamage(damage, transform.position);
        }

        yield return new WaitForSeconds(cooldown);

        IsAttacking = false;
    }

    private bool IsInRange(Player player, float range)
    {
        return Vector2.Distance(transform.position, player.transform.position) <= range;
    }
}
