using Code.Entities;

namespace Code.Ennemis.States
{
    public class EnemyAttackState : EnemyState
    {
        private EnemyAttackCompo _attackCompo;
        private EnemyMovement _movement;

        public EnemyAttackState(Entity entity, int hash)
            : base(entity, hash)
        {
            _attackCompo = entity.GetCompo<EnemyAttackCompo>();
            _movement = entity.GetCompo<EnemyMovement>();
        }

        public override void Enter()
        {
            base.Enter();
            _movement.StopImmediately(true);
            _attackCompo.Attack();
        }

        public override void Update()
        {
            base.Update();

            if (!_attackCompo.AttackLogic.IsAttacking)
            { 
                _enemy.ChangeState("EnemyMove");
            }
        }

        public override void Exit()
        {
            _movement.StopImmediately(false);

            base.Exit();
        }
    }
}
