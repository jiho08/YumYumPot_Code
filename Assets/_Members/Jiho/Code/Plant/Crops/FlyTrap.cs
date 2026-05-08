using Code.Combat;
using Code.Effects;
using GondrLib.ObjectPool.RunTime;
using UnityEngine;

namespace Code.Plant.Crops
{
    public class FlyTrap : Crops
    {
        [SerializeField] private PoolItemSO biteEffectItem;
        
        protected override void DoMeleeAttack(Transform target)
        {
            var damageable = target.GetComponent<IDamageable>();
            damageable?.ApplyDamage(Damage, transform.position);

            SpawnBiteEffect(target.position);
        }

        private void SpawnBiteEffect(Vector3 targetPos)
        {
            var effect = poolManager.Pop(biteEffectItem) as Effect;
            
            effect?.Play(targetPos);
        }
    }
}