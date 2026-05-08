using GondrLib.ObjectPool.RunTime;
using UnityEngine;

namespace Code.Combat
{
    public abstract class Bullet : MonoBehaviour, IPoolable
    {
        [SerializeField] protected float speed = 8f;
        [SerializeField] protected float lifeTime = 3f;

        [field: SerializeField] public PoolItemSO PoolItem { get; private set; }
        public GameObject GameObject => gameObject;

        protected Vector2 _dir;
        protected float _damage;
        protected float _timer;
        protected Pool _myPool;

        public virtual void Initialize(Vector2 dir, float damage)
        {
            _dir = dir;
            _damage = damage;
            _timer = 0;
        }

        protected virtual void Update()
        {
            transform.position += (Vector3)(_dir * (speed * Time.deltaTime));

            _timer += Time.deltaTime;
            
            if (_timer >= lifeTime)
                PushBullet();
        }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Enemy"))
                return;
            
            other.GetComponent<IDamageable>()?.ApplyDamage(_damage, transform.position);
            PushBullet();
        }

        protected void PushBullet()
        {
            _myPool.Push(this);
        }

        public void SetUpPool(Pool pool)
        {
            _myPool = pool;
        }

        public void ResetItem()
        {
            _dir = Vector2.zero;
        }
    }
}