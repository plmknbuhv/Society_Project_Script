using AKH.Scripts.Entities;
using UnityEngine;
using UnityEngine.Events;

namespace Work.SHS.Code.MiniGame.BoxMoveGame
{
    public class BoxMovePlayerAnimator : MonoBehaviour, IEntityComponent
    {
        private BoxMovePlayer _player;
        private Animator _animator;
        
        public UnityEvent<Vector3, Quaternion> OnAnimatorMoveEvent;
        
        private readonly int _moveXHash = Animator.StringToHash("MOVE_X");
        private readonly int _moveYHash = Animator.StringToHash("MOVE_Y");
        
        public bool ApplyRootMotion
        {
            get => _animator.applyRootMotion;
            set => _animator.applyRootMotion = value;
        }
        
        public void Initialize(Entity entity)
        {
            _player = entity as BoxMovePlayer;
            _animator = GetComponent<Animator>();
        }

        public void SetMoveParam(Vector2 moveParam, bool isDamping)
        {
            if (isDamping)
            {
                _animator.SetFloat(_moveXHash, moveParam.x, 0.2f, Time.deltaTime);
                _animator.SetFloat(_moveYHash, moveParam.y, 0.2f, Time.deltaTime);
            }
            else
            {
                _animator.SetFloat(_moveXHash, moveParam.x);
                _animator.SetFloat(_moveYHash, moveParam.y);
            }
        }
        
        private void OnAnimatorMove()
        {
            OnAnimatorMoveEvent?.Invoke(_animator.deltaPosition, _animator.deltaRotation);
        }
        
        public void OffAnimator()
        {
            _animator.enabled = false;
        }
    }
}