using Code.Entities;

namespace Code.Players.States
{
    public class PlayerHitState : PlayerState
    {
        private PlayerMovement _movement;
        
        public PlayerHitState(Entity entity, int animationHash) : base(entity, animationHash)
        {
            _movement = entity.GetCompo<PlayerMovement>();
        }

        public override void Enter()
        {
            base.Enter();

            _movement.CanMovement = false;
            _movement.StopImmediately();
        }

        public override void Update()
        {
            base.Update();
            
            if (_isTriggerCall)
                _player.ChangeState("IDLE");
        }

        public override void Exit()
        {
            _movement.CanMovement = true;
            
            base.Exit();
        }
    }
}