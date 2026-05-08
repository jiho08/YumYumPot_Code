using Code.Entities;
using UnityEngine;

namespace Code.Players
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour, IEntityComponent
    {
        [SerializeField] private float _moveSpeed = 5f;
        
        public Vector2 Velocity { get; private set; }
        public bool CanMovement { get; set; } = true;
        
        private Vector2 _movementDir;
        private Entity _entity;
        private Rigidbody2D _rigid;

        private bool _isFacingRight;
        
        public void Initialize(Entity entity)
        {
            _entity = entity;
            _rigid = GetComponent<Rigidbody2D>();
            _rigid.gravityScale = 0;
        }

        private void FixedUpdate()
        {
            ApplyVelocity();
            UpdateFlip();
        }

        public void SetMovementDirection(Vector2 movementInput)
        {
            _movementDir = movementInput.normalized;
        }

        public void StopImmediately()
        {
            _movementDir = Vector2.zero;
        }

        private void ApplyVelocity()
        {
            Velocity = _movementDir * _moveSpeed;
            _rigid.linearVelocity = Velocity;
        }

        private void UpdateFlip()
        {
            if (Mathf.Abs(Velocity.x) < 0.01f)
                return;

            bool shouldFaceRight = Velocity.x > 0f;

            if (_isFacingRight != shouldFaceRight)
            {
                _isFacingRight = shouldFaceRight;

                Vector3 scale = transform.localScale;
                scale.x = Mathf.Abs(scale.x) * (_isFacingRight ? -1f : 1f);
                transform.localScale = scale;
            }
        }

        public void AddMoveSpeed(float value)
        {
            _moveSpeed += value;
        }
    }
}