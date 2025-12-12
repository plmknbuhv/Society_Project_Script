using UnityEngine;
using UnityEngine.InputSystem;

namespace Work.SHS.Code.MiniGame.BoxMoveGame
{
    [CreateAssetMenu(menuName = "SO/Input/BoxMoveInputSO")]
    public class BoxMoveInputSO : ScriptableObject, Controls.IBoxMovePlayerActions
    {
        public Vector2 moveDir;
        
        private Controls _controls;

        private void OnEnable()
        {
            if (_controls == null)
            {
                _controls = new Controls();
                _controls.BoxMovePlayer.SetCallbacks(this);
            }
            _controls.Enable();
        }

        private void OnDisable() => _controls.Disable();
        
        public void OnMove(InputAction.CallbackContext context)
        {
            moveDir = context.ReadValue<Vector2>();
        }
    }
}