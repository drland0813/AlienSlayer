using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace drland.AlienSlayer
{
    public class RigManager : MonoBehaviour
    {
        [SerializeField] private RigBuilder _rigBuilder;
        [SerializeField] private TwoBoneIKConstraint _leftHandIKConstraint;
        [SerializeField] private TwoBoneIKConstraint _rightHandIKConstraint;

        public void SetUpHandTarget(bool active, Transform leftHandTarget, Transform rightHandTarget)
        {
            _rigBuilder.layers[0].active = active;
            _leftHandIKConstraint.data.target = active ? leftHandTarget : null;
            _rightHandIKConstraint.data.target = active ? rightHandTarget : null;
        }
    }
}