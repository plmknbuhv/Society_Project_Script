using AKH.Scripts.Entities;
using UnityEngine;

namespace Work.SHS.Code.MiniGame.BoxMoveGame
{
    public class BoxPlayerMoveDistanceTracker : MonoBehaviour, IEntityComponent
    {
        [SerializeField] private BoxMoveBalanceUI _balanceUI;

        private Vector3 _startPosition;
        private bool _isStarted;
        
        public float MoveDistance { get; private set; }
        
        public void Initialize(Entity entity)
        {
            
        }
        
        public void HandleStartMove()
        {
            _isStarted = true;
            _startPosition = transform.position;
        }
        
        private void Update()
        {
            if (_isStarted == false) return;
            if (MoveDistance > 50)
            {
                _isStarted = false;
                return;
            }
            
            MoveDistance = Vector3.Distance(transform.position, _startPosition);
            _balanceUI.SetDistanceText((int)MoveDistance);
        }
    }
}