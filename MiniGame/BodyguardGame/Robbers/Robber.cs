using System;
using UnityEngine;
using Work.SHS.Code.ETC;
using Random = UnityEngine.Random;

namespace Work.SHS.Code.MiniGame.BodyguardGame.Robbers
{
    public class Robber : MonoBehaviour
    {
        [SerializeField] private float minSpeed = 0.85f, maxSpeed = 1.2f;
        [SerializeField] private RobberAnimator robberAnimator;
        [SerializeField] private RobberAnimationTrigger robberAnimationTrigger;
        [SerializeField] private float collisionRotateOffset = 35f;
        [SerializeField] private float lifeDuration = 15f;
        
        private SkinnedMeshRenderer _skinnedMeshRenderer;
        private RobberMovement _movement;
        private float _spawnTime;

        public void SetUpEnemy(Material material)
        {
            _skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
            _movement = GetComponent<RobberMovement>();

            _movement.SetMoveSpeed(Random.Range(minSpeed, maxSpeed));
            _spawnTime = Time.time;
            _skinnedMeshRenderer.material = material;
            robberAnimationTrigger.OnAnimationTrigger += HandleReturnRotation;
        }

        private void Update()
        {
            if (_spawnTime + lifeDuration < Time.time)
            {
                Destroy(gameObject);
            }
        }

        private void OnDestroy()
        {
            robberAnimationTrigger.OnAnimationTrigger -= HandleReturnRotation;
        }

        private void HandleReturnRotation()
        {
            _movement.isCanMoveZ = false;
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.CompareTag("Player") == false) return;
            // 날라가는 부분

            CameraManager.Instance.ImpulseCamera(0.2f);
            _movement.isCanMoveZ = true;
            transform.rotation = Quaternion.Euler(
                0, -90 + collisionRotateOffset * (Random.value > 0.5f ? -1 : 1), 0);
            robberAnimator.SetMoveAnimation();
        }
    }
}