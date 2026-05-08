using Code.Entities;
using UnityEngine;

namespace Code.Players.States
{
    public class PlayerIdleState : PlayerState
    {
        private readonly PlayerMovement _movement;
        
        public PlayerIdleState(Entity entity, int animationHash) : base(entity, animationHash)
        {
            _movement = entity.GetCompo<PlayerMovement>();
        }

        public override void Update()
        {
            base.Update();

            Vector2 movementKey = _player.PlayerInput.MovementKey;
            _movement.SetMovementDirection(movementKey);
            
            if (movementKey.magnitude > _inputThreshold)
                _player.ChangeState("MOVE");
        }
    }
}