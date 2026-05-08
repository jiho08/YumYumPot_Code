using Code.Combat;
using GondrLib.ObjectPool.RunTime;
using UnityEngine;

namespace Code.Plant.Crops
{
    public class Garlic : Crops
    {
        [SerializeField] private PoolItemSO garlicBulletItem;
        
        protected override void DoRangeAttack(Transform target)
        {
            var bullet = poolManager.Pop(garlicBulletItem) as GarlicBullet;
            
            Vector2 dir = (target.position - firePoint.transform.position).normalized;
            
            bullet.Initialize(dir, Damage);
            bullet.transform.position = transform.position;

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }
}