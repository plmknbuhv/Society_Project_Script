using UnityEngine;
using UnityEngine.InputSystem;

namespace Work.SHS.Code.MiniGame.BodyguardGame
{
    [CreateAssetMenu(menuName = "SO/Input/BodyguardInputSO")]
    public class BodyguardInputSO : ScriptableObject, Controls.IBodyguardPlayerActions
    {
        [SerializeField] private float inputSpaceTime = 0.25f;
        
        private Controls _controls;
        private float _lastInputTime;
        
        public BodyguardInputType InputType { get; private set; } = BodyguardInputType.None;
        
        private void OnEnable()
        {
            if (_controls == null)
            {
                _controls = new Controls();
                _controls.BodyguardPlayer.SetCallbacks(this);
            }
            _controls.Enable();
            InputType = BodyguardInputType.None;
            _lastInputTime = Time.time;
        }
        
        private void OnDisable() => _controls.Disable();
        
        public void OnQ(InputAction.CallbackContext context) => ChangeInputType(BodyguardInputType.Q);
        public void OnW(InputAction.CallbackContext context) => ChangeInputType(BodyguardInputType.W);
        public void OnE(InputAction.CallbackContext context) => ChangeInputType(BodyguardInputType.E);
        public void OnR(InputAction.CallbackContext context) => ChangeInputType(BodyguardInputType.R);

        private void ChangeInputType(BodyguardInputType inputType)
        {
            if (_lastInputTime + inputSpaceTime > Time.time) return;
            
            _lastInputTime = Time.time;
            InputType = inputType;
        }
    }
}