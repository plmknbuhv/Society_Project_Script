using UnityEngine;

namespace Work.SHS.Code.MiniGame.BodyguardGame
{
    public class BodyguardPlayerAnimator : MonoBehaviour
    {
        [SerializeField] private Transform parent;
        
        private Animator _animator;
        
        private readonly int _moveHash = Animator.StringToHash("MOVE");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void SetMoveAnimation(bool isMove)
        {
            _animator.SetBool(_moveHash, isMove);
        }
    }
}