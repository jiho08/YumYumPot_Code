using Code.Combat;
using Code.Players;
using UnityEngine;
using System.Collections;

public class PesticideEnemyAttackLogic : EnemyAttackLogic
{
    [SerializeField] private Transform sprayPoint;
    [SerializeField] private WaterBullet waterBulletPrefab;

    [SerializeField] private float attackRange = 5f;
    [SerializeField] private int sprayCount = 6;
    [SerializeField] private float fireInterval = 0.12f;
    [SerializeField] private float bulletSpeed = 8f;
    [SerializeField] private float bulletLifeTime = 1f;

    [SerializeField] private float spreadAngle = 15f;

    private Transform player;
    private Coroutine routine;

    public override bool IsLockingMovement => true;


    void Awake()
    {
        if (sprayPoint == null)
            sprayPoint = transform;
    }

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

        Vector2 baseDir = (player.position - sprayPoint.position);
        if (baseDir.sqrMagnitude < 0.0001f)
            baseDir = Vector2.right;

        baseDir.Normalize();

        for (int i = 0; i < sprayCount; i++)
        {
            FireSpread(baseDir, damage);
            yield return new WaitForSeconds(fireInterval);
        }

        IsAttacking = false;
        routine = null;
    }

    private void FireSpread(Vector2 baseDir, float damage)
    {
        Fire(Rotate(baseDir, -spreadAngle), damage);
        Fire(baseDir, damage);
        Fire(Rotate(baseDir, spreadAngle), damage);
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

    private Vector2 Rotate(Vector2 dir, float angle)
    {
        float rad = angle * Mathf.Deg2Rad;
        float cos = Mathf.Cos(rad);
        float sin = Mathf.Sin(rad);

        return new Vector2(
            dir.x * cos - dir.y * sin,
            dir.x * sin + dir.y * cos
        ).normalized;
    }
}