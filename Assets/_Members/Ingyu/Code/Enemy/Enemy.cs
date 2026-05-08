using Code.Combat;
using Code.Core.Debug;
using Code.Ennemis;
using Code.Entities;
using Code.FSM;
using GondrLib.ObjectPool.RunTime;
using System;
using Code.Core.Events;
using UnityEngine;

public abstract class Enemy : Entity, IPoolable
{
    [Header("FSM")]
    [SerializeField] private StateSO[] states;

    [Header("Target")]
    [SerializeField] private EntityFinderSO playerFinder;

    [Header("Range")]
    [SerializeField] private float attackRange;

    [Header("EXP")]
    [SerializeField] private int expReward = 5;
    public int ExpReward => expReward;

    private EnemyAttackCompo _attackCompo;
    private EnemyMovement _movement;
    private EntityHealth _healthCompo;
    private EntityStateMachine _stateMachine;
    private Pool _myPool;

    public Entity Target { get; private set; }

    [field: SerializeField] public PoolItemSO PoolItem { get; private set; }

    public static Action<int> OnEnemyDead;

    public static Action<int> OnEnemyDeadExp;

    public static Action<Enemy> OnEnemyDeadForWave;

    [SerializeField] private EnemyStatSO stat;


    public GameObject GameObject => gameObject;

    protected bool isDead;
    public bool IsHit { get; private set; }

    public void MarkDead()
    {
        IsDead = true;
    }

    public void SetHit(bool value)
    {
        IsHit = value;
    }
    public virtual void Die()
    {
        if (isDead)
            return;

        isDead = true;

        Bus<AddExpEvent>.Raise(new AddExpEvent(stat.exp));
        OnEnemyDeadForWave?.Invoke(this); 
        //OnEnemyDeadExp?.Invoke(stat.exp);
        
        _myPool.Push(this);
    }
    protected override void InitializeComponents()
    {
        base.InitializeComponents();

        _healthCompo = GetCompo<EntityHealth>();
        _movement = GetCompo<EnemyMovement>();
        _attackCompo = GetCompo<EnemyAttackCompo>();

        _stateMachine = new EntityStateMachine(this, states);
    }

    protected override void AfterInitializeComponents()
    {
        base.AfterInitializeComponents();

        OnHitEvent.AddListener(HandleHitEvent);
        OnDeadEvent.AddListener(HandleDeadEvent);

        _healthCompo.OnHealthChange += HandleHealthChange;

        Target = playerFinder.Target;
        _movement.SetTarget(Target);
    }

    private void Start()
    {
        ChangeState("EnemyMove");
    }

    private void Update()
    {
        _stateMachine.UpdateStateMachine();
    }

    public bool IsTargetInAttackRange()
    {
        if (Target == null)
            return false;

        float distance = Vector3.Distance(transform.position, Target.transform.position);
        return distance <= attackRange;
    }

    public void DoAttack()
    {
        UnityLogger.Log("DoAttack");
        if (IsHit || IsDead)
            return;

        if (_attackCompo == null)
            return;

        _attackCompo.Attack();
    }

    public void ChangeState(string newStateName, bool forced = false)
    {
        _stateMachine.ChangeState(newStateName, forced);
    }

    private void HandleHitEvent()
    {
        if (IsDead)
            return;

        ChangeState("EnemyHit", true);
    }

    private void HandleDeadEvent()
    {
        if (IsDead)
            return;

        IsDead = true;
        ChangeState("EnemyDie", true);
    }

    private void HandleHealthChange(float current, float max)
    {
    }

    public void SetUpPool(Pool pool)
    {
        _myPool = pool;
    }

    public virtual void ResetItem()
    {
        IsDead = false;
        ChangeState("EnemyMove");
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
#endif
    public void ApplyStat(EnemyStatSO baseStat, float multiplier)
    {
        _healthCompo.SetMaxHp(Mathf.RoundToInt(baseStat.maxHp * multiplier));
        _attackCompo.SetDamage(Mathf.RoundToInt(baseStat.damage * multiplier));
        expReward = Mathf.RoundToInt(baseStat.exp * multiplier);
    }

    public void ForceDie()
    {
        gameObject.SetActive(false);
    }

}
