using UnityEngine;

namespace Code.Combat
{
    public interface IDamageable
    {
        void ApplyDamage(float damage, Vector2 hitPoint);
    }
}
