using System.Collections.Generic;
using UnityEngine;

namespace Work.SHS.Code.MiniGame.BodyguardGame
{
    public class BodyguardPlayerMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5;
        [SerializeField] private float rotateSpeed = 10;
        [SerializeField] private List<float> bodyguardZPos = new List<float>();
        [SerializeField] private BodyguardInputSO inputSO;
        [SerializeField] private BodyguardPlayerAnimator animator;

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            if (Mathf.Abs(transform.position.z - bodyguardZPos[(int)inputSO.InputType]) > 0.05f)
            {
                // 도착지점에서 멀리있을 때
                float moveSign = -Mathf.Sign(transform.position.z - bodyguardZPos[(int)inputSO.InputType]);
                var moveDir = new Vector3(0, 0, moveSign * (Time.deltaTime * moveSpeed));
                
                transform.position += moveDir;
                transform.rotation = Quaternion.Lerp(
                    transform.rotation, Quaternion.Euler(0, moveSign > 0.5f ? -90 : 90, 0), Time.deltaTime * rotateSpeed);
                animator.SetMoveAnimation(true);
            }
            else
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, Time.deltaTime * rotateSpeed);
                animator.SetMoveAnimation(false);
            }
        }
    }
}
