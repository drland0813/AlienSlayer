using System;
using DG.Tweening;
using Drland.Common.Utils;
using UnityEngine;
using NaughtyAttributes;
namespace drland.AlienSlayer
{
    public class RagdollManager : MonoBehaviour
    {
        [SerializeField] private GameObject _root;
        [SerializeField] private RagdollChildPart[] _childParts;
        
        private Animator _animator;
        private HealthComponent _healthComponent;
        private Collider _controller;
        [SerializeField] private Rigidbody _mainRigid;

        public void Init(Animator animator)
        {
            _healthComponent = GetComponent<HealthComponent>();
            _childParts = GetComponentsInChildren<RagdollChildPart>();
            _animator = animator;
            
            DeactivateRagdoll();
        }

        public void DeactivateRagdoll()
        {
            foreach (var child in _childParts)
            {
                child.Rigid.isKinematic = true;
                child.Collider.isTrigger = true;
                child.Collider.providesContacts = true;
            }
            _animator.enabled = true;
        }
        
        public void ActivateRagdoll()
        {
            foreach (var child in _childParts)
            {
                child.Rigid.isKinematic = false;
                child.Collider.isTrigger = false;
                child.Collider.providesContacts = false;
            }
            _animator.enabled = false;
            StartCoroutine(WaitUtils.WaitToDo(3, () =>
            {
                Destroy(transform.gameObject);
            }));
        }

        public void ApplyForce(Vector3 direction, float force)
        {
            _mainRigid.AddForce(direction * force, ForceMode.Impulse);
        }
    }
}