using Code.Combat;
using Code.Core;
using Code.Core.Events;
using Code.Entities;
using Code.FSM;
using GondrLib.Dependencies;
using UnityEngine;

namespace Code.Players
{
    public class Player : Entity, IDependencyProvider
    {
        [field: SerializeField] public InputReader PlayerInput { get; private set; }
        [SerializeField] private StateSO[] states;

        [Provide]
        public Player GetPlayer() => this;
        
        private PlayerMovement _movement;
        private EntityHealth _healthCompo;
        private EntityStateMachine _stateMachine;

        protected override void InitializeComponents()
        {
            base.InitializeComponents();

            _movement = GetCompo<PlayerMovement>();
            _healthCompo = GetCompo<EntityHealth>();

            _stateMachine = new EntityStateMachine(this, states);
        }

        protected override void AfterInitializeComponents()
        {
            base.AfterInitializeComponents();

            OnHitEvent.AddListener(HandleHitEvent);
            OnDeadEvent.AddListener(HandleDeadEvent);

            _healthCompo.OnHealthChange += HandleHealthChange;
        }

        private void OnDestroy()
        {
            OnHitEvent.RemoveListener(HandleHitEvent);
            OnDeadEvent.RemoveListener(HandleDeadEvent);

            _healthCompo.OnHealthChange -= HandleHealthChange;
        }

        private void Start()
        {
            ChangeState("IDLE");
        }

        private void Update()
        {
            _stateMachine.UpdateStateMachine();
        }

        public void ChangeState(string newStateName, bool forced = false)
            => _stateMachine.ChangeState(newStateName, forced);

        private void HandleHitEvent()
        {
            if (IsDead)
                return;
            
            ChangeState("HIT", true);
        }

        private void HandleDeadEvent()
        {
            if (IsDead)
                return;

            IsDead = true;
            Bus<PlayerDeadEvent>.Raise(new PlayerDeadEvent());
            ChangeState("DEAD", true);
        }

        private void HandleHealthChange(float current, float max)
        {
            Bus<PlayerHealthEvent>.Raise(new PlayerHealthEvent(current, max));
        }
    }
}