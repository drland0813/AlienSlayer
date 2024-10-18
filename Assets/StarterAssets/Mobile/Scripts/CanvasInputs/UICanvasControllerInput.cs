using UnityEngine;
using UnityEngine.Serialization;

namespace StarterAssets
{
    public class UICanvasControllerInput : MonoBehaviour
    {

        [FormerlySerializedAs("starterAssetsInputs")] [Header("Output")]
        public PlayerInputAction _playerInputAction;

        public void VirtualMoveInput(Vector2 virtualMoveDirection)
        {
            _playerInputAction.MoveInput(virtualMoveDirection);
        }

        public void VirtualLookInput(Vector2 virtualLookDirection)
        {
            _playerInputAction.LookInput(virtualLookDirection);
        }

        public void VirtualSprintInput(bool virtualSprintState)
        {
            _playerInputAction.SprintInput(virtualSprintState);
        }
        
        public void VirtualShootInput(bool virtualShootState)
        {
            _playerInputAction.ShootInput(virtualShootState);
        }
        
    }

}
