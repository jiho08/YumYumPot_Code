using Code.Entities;

namespace Code.Players.States
{
    public class PlayerDeadState : PlayerState
    {
        private PlayerMovement _movement;
        
        public PlayerDeadState(Entity entity, int animationHash) : base(entity, animationHash)
        {
            _movement = entity.GetCompo<PlayerMovement>();
        }

        public override void Enter()
        {
            base.Enter();

            _movement.CanMovement = false;
            _movement.StopImmediately();
        }
    }
}