using AKH.Scripts.Entities;
using UnityEngine;
using UnityEngine.Events;
using Work.Common.Core;
using Work.Min.Code.EventChannel;

namespace Work.SHS.Code.MiniGame.BoxMoveGame
{
    public class BoxMovePlayer : Entity
    {
        [field: SerializeField] public BoxMoveInputSO InputSO { get; private set; }

        private bool _isMove;

        public UnityEvent OnStartEvent;

        protected override void Start()
        {
            base.Start();
            GameEventBus.AddListener<StartGameEvent>(ActiveMove);
        }

        private void OnDestroy()
        {
            GameEventBus.RemoveListener<StartGameEvent>(ActiveMove);
        }

        public void HandleAnimationMove(Vector3 deltaPosition, Quaternion deltaRotation)
        {
            if (_isMove == false) return;
            
            transform.position += deltaPosition;
            transform.rotation = deltaRotation * transform.rotation;
        }
        
        private void ActiveMove(StartGameEvent evt)
        {
            OnStartEvent?.Invoke();
            _isMove = true;   
        }
    }
}
