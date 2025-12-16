using UnityEngine;

namespace Work.SHS.Code.MiniGame.BodyguardGame.Robbers
{
    public class RobberMovement : MonoBehaviour
    {
        public bool isCanMoveZ;
        private float _moveSpeed;
        
        public void SetMoveSpeed(float moveSpeed) => _moveSpeed = moveSpeed;
        
        public void HandleAnimMove(Vector3 deltaPosition, Quaternion deltaRotation)
        {
            transform.position += new Vector3(
                deltaPosition.x * _moveSpeed, deltaPosition.y, isCanMoveZ ? deltaPosition.z : 0);
            transform.rotation = deltaRotation * transform.rotation;
        }
    }
}