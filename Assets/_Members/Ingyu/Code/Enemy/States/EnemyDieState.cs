using Code.Core.Debug;
using Code.Entities;
using UnityEngine;

namespace Code.Ennemis.States
{
    public class EnemyDieState : EnemyState
    {
        private readonly EnemyMovement _movement;
        private readonly Collider2D _collider;

        public EnemyDieState(Entity entity, int animationHash) : base(entity, animationHash)
        {
            _movement = entity.GetCompo<EnemyMovement>();
            _collider = entity.GetComponent<Collider2D>();
        }
        public override void Enter()
        {
            UnityLogger.Log("EnemyDie");
            base.Enter();

            if (_movement != null)
                _movement.StopImmediately(true);

            if (_collider != null)
                _collider.enabled = false;
        }

        public override void Update()
        {
            base.Update();

            if (_enemy != null)
            {
                _enemy.Die();
            }
        }
    }
}

