using System.Collections.Generic;
using System.Linq;
using AKH.Scripts.Entities;
using UnityEngine;

namespace Work.SHS.Code.ETC
{
    public class RagDollCompo : MonoBehaviour, IEntityComponent
    {
        [SerializeField] private Transform ragDollParentTrm;
        [SerializeField] private LayerMask whatIsBody;

        private List<RagDollPart> _partList;
        private Collider[] _results;
        private RagDollPart _defaultPart;
        
        public void Initialize(Entity entity)
        {
            _results = new Collider[1];
            _partList = ragDollParentTrm.GetComponentsInChildren<RagDollPart>().ToList();
            foreach (RagDollPart part in _partList)
            {
                part.Initialize();
            }
            Debug.Assert(_partList.Count > 0, $"No ragdoll part found in {gameObject.name}");
            _defaultPart = _partList[0]; //첫번째 파츠를 디폴트 값으로 넣어준다.
            
            SetRagDollActive(false);
            SetColliderActive(false);
        }

        public void HandleDeadEvent()
        {
            SetRagDollActive(true);
            SetColliderActive(true);
        }

        private void SetRagDollActive(bool isActive)
        {
            foreach (RagDollPart part in _partList)
            {
                part.SetRagDollActive(isActive);
            }
        }

        private void SetColliderActive(bool isActive)
        {
            foreach (RagDollPart part in _partList)
            {
                part.SetCollider(isActive);
            }
        }

        public void AddForceToRagDoll(Vector3 force, Vector3 point)
        {
            HandleDeadEvent(); // 레그돌 켜주기

            foreach (var part in _partList)
            {
                part.KnockBack(force, point);
            }
        }
    }
}