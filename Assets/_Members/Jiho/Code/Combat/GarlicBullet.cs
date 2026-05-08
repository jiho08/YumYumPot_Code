using UnityEngine;

namespace Code.Combat
{
    public class GarlicBullet : Bullet
    {
        [SerializeField] private float slowRatio = 0.5f;
        [SerializeField] private float slowDuration = 2f;
        
        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Enemy"))
                return;
            
            other.GetComponent<IDamageable>()?.ApplyDamage(_damage, transform.position);
            other.GetComponent<ISlowable>()?.ApplySlow(slowRatio, slowDuration);
            PushBullet();
        }
    }
}