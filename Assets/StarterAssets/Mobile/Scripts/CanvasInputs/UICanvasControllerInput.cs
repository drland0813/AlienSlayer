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
        
        public void VirtualRotateAndShoot(Vector2 virtualMoveDirection)
        {
            _playerInputAction.ShootInput(virtualMoveDirection);
        }


        public void VirtualSprintInput(bool virtualSprintState)
        {
            _playerInputAction.SprintInput(virtualSprintState);
        }
        
        // public void VirtualShootInput(bool virtualShootState)
        // {
        //     _playerInputAction.ShootInput(virtualShootState);
        // }
        
        public void VirtualChangeGunInput()
        {
            _playerInputAction.ChangeGunInput();
        }
        
    }

}
