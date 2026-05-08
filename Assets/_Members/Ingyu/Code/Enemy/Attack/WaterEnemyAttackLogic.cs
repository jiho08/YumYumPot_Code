using Code.Players;
using System.Collections;
using UnityEngine;

public class WaterEnemyAttackLogic : EnemyAttackLogic
{
    [SerializeField] private Transform sprayPoint;
    [SerializeField] private WaterBullet waterBulletPrefab;

    [SerializeField] private float attackRange = 5f;
    [SerializeField] private int sprayCount = 6;
    [SerializeField] private float fireInterval = 0.12f;
    [SerializeField] private float bulletSpeed = 8f;
    [SerializeField] private float bulletLifeTime = 1f;

    private Transform player;
    private Coroutine routine;

    public override bool IsLockingMovement => true;

    public override void Attack(float damage)
    {
        if (IsAttacking)
            return;

        if (player == null)
            player = FindAnyObjectByType<Player>()?.transform;

        if (player == null)
            return;

        float dist = Vector2.Distance(transform.position, player.position);
        if (dist > attackRange)
            return;

        routine = StartCoroutine(SprayRoutine(damage));
    }

    private IEnumerator SprayRoutine(float damage)
    {
        IsAttacking = true;

        Vector2 dir = (player.position - sprayPoint.position);
        if (dir.sqrMagnitude < 0.0001f)
            dir = Vector2.right;

        dir.Normalize();

        for (int i = 0; i < sprayCount; i++)
        {
            Fire(dir, damage);
            yield return new WaitForSeconds(fireInterval);
        }

        IsAttacking = false;
        routine = null;
    }

    private void Fire(Vector2 dir, float damage)
    {
        WaterBullet bullet = Instantiate(
            waterBulletPrefab,
            sprayPoint.position,
            Quaternion.identity
        );

        bullet.Launch(
            dir,
            bulletSpeed,
            damage,
            bulletLifeTime,
            gameObject
        );
    }
}
    