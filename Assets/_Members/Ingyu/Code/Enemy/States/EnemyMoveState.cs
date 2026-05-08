using Code.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Code.Ennemis.States {
    public class EnemyMoveState : EnemyState
    {
        private readonly EnemyMovement _movement;

        public EnemyMoveState(Entity entity, int animationHash) : base(entity, animationHash)
        {
            _movement = entity.GetCompo<EnemyMovement>();
        }
        public override void Enter()
        {
            base.Enter();
            _movement.IsStop = false;
        }
        public override void Update()
        {
            base.Update();

            if (_movement == null || _enemy.Target == null)
                return;

            Vector2 dirVec =
                (Vector2)_enemy.Target.transform.position -
                (Vector2)_enemy.transform.position;

            _movement.ApplyVelocity(dirVec.normalized);

            if (_enemy.IsTargetInAttackRange())
            {
                _enemy.ChangeState("EnemyAttack");
            }
        }

    }
}


