using Code.Core.Events;
using Code.Entities;
using Code.UI;
using UnityEngine;

namespace Code.Combat
{
    public class EntityHealth : MonoBehaviour, IEntityComponent, IDamageable, IAfterInitialize
    {
        [SerializeField] private float maxHealth;
        [SerializeField] private float currentHealth;
        [SerializeField] private TextInfoSO damageText;

        public delegate void HealthChange(float current, float max);
        public event HealthChange OnHealthChange;

        private Entity _entity;

        [ContextMenu("Damage")]
        private void Damage()
        {
            ApplyDamage(10, Vector2.zero);
        }

        public void Initialize(Entity entity)
        {
            _entity = entity;
        }

        public void AfterInitialize()
        {
            currentHealth = maxHealth;
        }

        public void SetMaxHp(float value)
        {
            maxHealth = value;
            currentHealth = value;
        }
        public void ApplyDamage(float damage, Vector2 hitPoint)
        {
            if (_entity.IsDead)
                return;

            var attackLogic = GetComponent<EnemyAttackLogic>();
            if (attackLogic != null && attackLogic.IsAttacking)
                return;

            float prev = currentHealth;
            currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);

            OnHealthChange?.Invoke(currentHealth, maxHealth);

            int typeHash = damageText.nameHash;
            Bus<PopUpTextEvent>.Raise(
                new PopUpTextEvent(
                    Mathf.RoundToInt(damage).ToString(),
                    typeHash,
                    hitPoint,
                    0.5f
                )
            );

            if (currentHealth <= 0)
            {
                _entity.OnDeadEvent?.Invoke();
                return;
            }

            _entity.OnHitEvent?.Invoke();
        }

        public void HandleHPValueChange(float current, float prev)
        {
            float changed = current - prev;
            maxHealth = current;
            currentHealth = Mathf.Clamp(currentHealth + changed, 1, maxHealth);
            OnHealthChange?.Invoke(currentHealth, maxHealth);
        }

        public void AddMaxHealth(float value)
        {
            maxHealth += value;
            currentHealth += value;
        }
    }
}
