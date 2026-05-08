using Code.Combat;
using Code.Core.Debug;
using Code.Core.Events;
using Code.Managers;
using GondrLib.ObjectPool.RunTime;
using UnityEngine;

namespace Code.Plant.Crops
{
    public abstract class Crops : MonoBehaviour, IPoolable
    {
        [field: SerializeField] public PoolItemSO PoolItem { get; private set; }

        [SerializeField] private CropsSO _cropsSO;
        [SerializeField] private PoolItemSO flowerBullet;

        [SerializeField] protected bool isRangeAttack;
        [SerializeField] protected PoolManagerSO poolManager;
        [SerializeField] protected Transform firePoint;
        [SerializeField] protected LayerMask enemyLayer;

        public bool IsCanAttack { get; private set; }
        public string CropsName { get; private set; }
        public string CropsVisualName { get; private set; }
        public float AttackRange { get; private set; }
        public Sprite Sprite { get; private set; }
        public StatType EatStatType { get; private set; }
        public float EatStatValue { get; private set; }
        public GameObject GameObject => gameObject;

        public float Damage => _damage + PlayerCombatManager.Instance.attackPower;
        public float AttackSpeed => _attackSpeed - PlayerCombatManager.Instance.attackSpeed;

        public bool CanEat => _level >= MAX_LEVEL - 1;
        
        private float _damage;
        private float _attackSpeed;
        private float _growTimer;
        private int _level;
        private Pool _myPool;
        private const int MAX_LEVEL = 3;

        protected float _attackTimer;

        private void Awake()
        {
            ApplyLevelData();
            EatStatType = _cropsSO.eatStatType;
            EatStatValue = _cropsSO.eatStatValue;
            IsCanAttack = _cropsSO.canAttack;
        }

        public virtual void CorpsUpdate()
        {
            HandleGrow();
            HandleAttack();
        }

        private void HandleGrow()
        {
            if (_level >= MAX_LEVEL)
                return;

            _growTimer += Time.deltaTime;

            if (_growTimer >= _cropsSO.growTimeList[_level])
                Grow();
        }

        private void Grow()
        {
            _growTimer = 0;

            ++_level;
            ApplyLevelData();

            Bus<CropsGrowEvent>.Raise(new CropsGrowEvent(this));
        }

        private void ApplyLevelData()
        {
            CropsVisualName = _cropsSO.cropsVisualNameList[_level];
            CropsName = _cropsSO.cropsNameList[_level];
            Sprite = _cropsSO.spriteList[_level];
            AttackRange = _cropsSO.attackRangeList[_level];
            _damage = _cropsSO.damageList[_level];
            _attackSpeed = _cropsSO.attackSpeedList[_level];
        }

        private void HandleAttack()
        {
            if (!CanAttack() || (!IsCanAttack))
                return;

            _attackTimer += Time.deltaTime;

            if (_attackTimer < AttackSpeed)
                return;

            _attackTimer = 0;

            TryAttack();
        }

        private void TryAttack()
        {
            Transform target = FindClosestEnemy();

            if (target == null)
                return;

            if (_level == 1)
            {
                FlowerAttack(target);
                return;
            }

            if (isRangeAttack)
                DoRangeAttack(target);
            else
                DoMeleeAttack(target);
        }

        protected virtual void DoRangeAttack(Transform target)
        {
        }

        protected virtual void DoMeleeAttack(Transform target)
        {
        }

        protected void FlowerAttack(Transform target)
        {
            UnityLogger.Log("꽃 공격");

            var bullet = poolManager.Pop(flowerBullet) as FlowerBullet;
            Vector2 dir = (target.position - firePoint.transform.position).normalized;

            bullet.Initialize(dir, Damage);
            bullet.transform.position = transform.position;

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }

        private Transform FindClosestEnemy()
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position,
                AttackRange, enemyLayer);

            if (hits.Length == 0)
                return null;

            Transform closest = null;
            float minDist = float.MaxValue;

            foreach (var hit in hits)
            {
                float dist = Vector2.Distance(transform.position, hit.transform.position);

                if (!(dist < minDist))
                    continue;

                minDist = dist;
                closest = hit.transform;
            }

            return closest;
        }

        public bool CanAttack()
        {
            return _level > 0;
        }

        public void SetUpPool(Pool pool)
        {
            _myPool = pool;
        }

        public void AddGrowTimer(float value)
            => _growTimer += value;

        public void ResetItem()
        {
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, AttackRange);
        }
    }
}