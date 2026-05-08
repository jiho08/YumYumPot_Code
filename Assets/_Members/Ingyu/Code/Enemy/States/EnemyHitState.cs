using Code.Core.Debug;
using Code.Entities;
using UnityEngine;

namespace Code.Ennemis.States
{
    public class EnemyHitState : EnemyState
    {
        private readonly EnemyMovement _movement;

        public EnemyHitState(Entity entity, int animationHash) : base(entity, animationHash)
        {
            _movement = entity.GetCompo<EnemyMovement>();
        }

        public override void Enter()
        {
            // �̹� ��Ʈ ���¸� ������ ���� ����
            if (_enemy != null && _enemy.IsHit)
                return;

            UnityLogger.Log("EnemyHit");
            base.Enter();

            if (_movement != null)
                _movement.StopImmediately(true);

            _enemy?.SetHit(true);
        }

        public override void Exit()
        {
            base.Exit();
            _enemy?.SetHit(false);
        }

        public override void Update()
        {
            base.Update();

            if (_enemy != null)
            {
                _enemy.ChangeState("EnemyMove");
            }
        }
    }
}
