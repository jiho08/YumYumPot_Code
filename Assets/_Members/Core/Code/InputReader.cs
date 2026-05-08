using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Core
{
    [CreateAssetMenu(menuName = "SO/Input", fileName = "InputReader", order = 0)]
    public class InputReader : ScriptableObject, Controls.IPlayerActions
    {
        public event Action OnInteractPressed;
        public Vector2 MovementKey { get; private set; }
        
        private Controls _controls;
        
        private void OnEnable()
        {
            if (_controls == null)
            {
                _controls = new Controls();
                _controls.Player.SetCallbacks(this);
            }
            
            _controls.Player.Enable();
        }

        private void OnDisable()
        {
            _controls?.Player.Disable();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            MovementKey = context.ReadValue<Vector2>();
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnInteractPressed?.Invoke();
        }
    }
}