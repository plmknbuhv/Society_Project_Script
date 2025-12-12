using UnityEngine;
using UnityEngine.Events;

namespace Work.SHS.Code.MiniGame.BodyguardGame.Robbers
{
    public class RobberAnimator : MonoBehaviour
    {
        [SerializeField] private Transform parent;
        
        private Animator _animator;

        public UnityEvent<Vector3, Quaternion> OnAnimatorMoveEvent;
        
        private readonly int _moveHash = Animator.StringToHash("MOVE");

        public bool ApplyRootMotion
        {
            get => _animator.applyRootMotion;
            set => _animator.applyRootMotion = value;
        }
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void SetMoveAnimation()
        {
            _animator.SetTrigger(_moveHash);
        }
        
        private void OnAnimatorMove()
        {
            OnAnimatorMoveEvent?.Invoke(_animator.deltaPosition, _animator.deltaRotation);
        }
    }
}