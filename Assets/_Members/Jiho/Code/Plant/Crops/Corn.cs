using Code.Combat;
using Code.Core.Debug;
using GondrLib.ObjectPool.RunTime;
using UnityEngine;

namespace Code.Plant.Crops
{
    public class Corn : Crops
    {
        [SerializeField] private PoolItemSO cornBulletItem;

        protected override void DoRangeAttack(Transform target)
        {
            UnityLogger.Log("옥수수 공격");
            var bullet = poolManager.Pop(cornBulletItem) as CornBullet;
            
            Vector2 dir = (target.position - firePoint.transform.position).normalized;
            
            bullet.Initialize(dir, Damage);
            bullet.transform.position = transform.position;

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }
}