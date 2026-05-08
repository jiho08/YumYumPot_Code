using Code.Combat;
using Code.Players;
using UnityEngine;
using System.Collections;

public class GrassEnemyAttackLogic : EnemyAttackLogic
{
    private Player targetPlayer;

    public override bool IsLockingMovement => IsAttacking;

    [SerializeField] private float range = 6f;
    [SerializeField] private float chargeTime = 2f;
    [SerializeField] private float dashPower = 6f;

    [SerializeField] private LineRenderer warningLine;

    private Rigidbody2D rigid;
    private Vector2 lockedDir;

    private bool wasInRange;
    private bool canHit;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (targetPlayer == null)
            targetPlayer = Object.FindAnyObjectByType<Player>();

        if (targetPlayer == null || IsAttacking)
            return;

        float dist = Vector2.Distance(transform.position, targetPlayer.transform.position);
        bool isInRange = dist <= range;

        if (isInRange && !wasInRange)
            StartCoroutine(ChargeAndDash());

        wasInRange = isInRange;
    }

    public override void Attack(float damage)
    {
    }

    private IEnumerator ChargeAndDash()
    {
        IsAttacking = true;

        lockedDir = (targetPlayer.transform.position - transform.position).normalized;

        yield return StartCoroutine(WarningOnce(lockedDir));
        yield return new WaitForSeconds(chargeTime);

        canHit = true;
        rigid.linearVelocity = lockedDir * dashPower;

        yield return new WaitForSeconds(0.25f);

        rigid.linearVelocity = Vector2.zero;
        canHit = false;

        IsAttacking = false;
    }

    private IEnumerator WarningOnce(Vector2 dir)
    {
        Vector3 start = transform.position;
        Vector3 end = start + (Vector3)(dir * range);

        warningLine.SetPosition(0, start);
        warningLine.SetPosition(1, end);

        warningLine.enabled = true;
        yield return new WaitForSeconds(0.5f);
        warningLine.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!canHit)
            return;

        if (collision.collider.TryGetComponent<IDamageable>(out var d))
        {
            d.ApplyDamage(5f, transform.position);
            canHit = false;
            rigid.linearVelocity = Vector2.zero;
        }
    }
}
