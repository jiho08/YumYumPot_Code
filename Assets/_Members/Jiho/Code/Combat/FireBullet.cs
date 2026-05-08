using UnityEngine;

namespace Code.Combat
{
    public class FireBullet : Bullet
    {
        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Enemy"))
                return;
            
            other.GetComponent<IDamageable>()?.ApplyDamage(_damage, transform.position);
        }
    }
}