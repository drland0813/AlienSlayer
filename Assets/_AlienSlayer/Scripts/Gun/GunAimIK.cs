using System;
using UnityEngine;

namespace drland.AlienSlayer
{
    public class GunAimIK : MonoBehaviour
    {
        [SerializeField] private Transform _targetTransform;
        [SerializeField] private Transform _aimTransform;
        [SerializeField] private Transform _bone;

        [SerializeField] private int _iterations = 10;
        private void LateUpdate()
        {
            Vector3 targetPosition = _targetTransform.position;
            for (int i = 0; i < _iterations; i++)
            {
                AimAtTarget(_bone, targetPosition);
            }
            AimAtTarget(_bone, targetPosition);
        }

        private void AimAtTarget(Transform bone, Vector3 targetPosition)
        {
            Vector3 aimDirection = _aimTransform.forward;
            Vector3 targetDirection = targetPosition - _aimTransform.position;
            Quaternion aimToward = Quaternion.FromToRotation(aimDirection, targetDirection);
            bone.rotation = aimToward * bone.rotation;
        }
    }
}