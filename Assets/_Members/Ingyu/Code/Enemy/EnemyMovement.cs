using System.Collections;
using Code.Combat;
using Code.Entities;
using UnityEngine;

namespace Code.Ennemis
{
    public class EnemyMovement : MonoBehaviour, IEntityComponent, ISlowable
    {
        public float moveSpeed = 1;
        public Entity target;

        Rigidbody2D rigid;
        SpriteRenderer spriter;

        public Vector2 Velocity { get; private set; }
        public bool IsStop { get; set; }

        bool _isFacingRight;
        bool _lockPosition;
        Vector3 _lockedPos;

        Entity _entity;
        float _originSpeed;
        Coroutine _slowRoutine;

        public void Initialize(Entity entity)
        {
            _entity = entity;
            _originSpeed = moveSpeed;
        }

        void Awake()
        {
            rigid = GetComponent<Rigidbody2D>();
            spriter = GetComponentInChildren<SpriteRenderer>();
        }

        void FixedUpdate()
        {
            UpdateFlip();
        }

        bool lockPosition;
        Vector3 lockedWorldPos;

        public void LockPosition()
        {
            lockedWorldPos = transform.position;
            lockPosition = true;
        }

        public void UnlockPosition()
        {
            lockPosition = false;
        }

        void LateUpdate()
        {
            if (lockPosition)
                transform.position = lockedWorldPos;
        }

        public void SetTarget(Entity entity)
        {
            target = entity;
        }

        public void StopImmediately(bool isStop)
        {
            IsStop = isStop;
            if (isStop)
                rigid.linearVelocity = Vector2.zero;
        }

        public void SetPositionLock(bool value)
        {
            _lockPosition = value;
            if (value)
                _lockedPos = transform.position;
        }

        public void ApplyVelocity(Vector2 nextVec)
        {
            if (IsStop || target == null)
            {
                rigid.linearVelocity = Vector2.zero;
                return;
            }

            Vector2 dir = ((Vector2)target.transform.position - rigid.position).normalized;
            rigid.linearVelocity = dir * moveSpeed;
            Velocity = rigid.linearVelocity;
        }

        void UpdateFlip()
        {
            float x = rigid.linearVelocity.x;
            if (Mathf.Abs(x) < 0.01f)
                return;

            bool shouldFaceRight = x > 0f;
            if (_isFacingRight == shouldFaceRight)
                return;

            _isFacingRight = shouldFaceRight;

            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * (_isFacingRight ? -1f : 1f);
            transform.localScale = scale;
        }

        public void ApplySlow(float slowRatio, float duration)
        {
            if (_slowRoutine != null)
                StopCoroutine(_slowRoutine);

            _slowRoutine = StartCoroutine(SlowRoutine(slowRatio, duration));
        }

        IEnumerator SlowRoutine(float ratio, float duration)
        {
            moveSpeed = _originSpeed * (1f - ratio);
            yield return new WaitForSeconds(duration);
            moveSpeed = _originSpeed;
            _slowRoutine = null;
        }
    }
}
