using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace drland.AlienSlayer
{
#if ENABLE_INPUT_SYSTEM
    [RequireComponent(typeof(PlayerInput))]
#endif
    public class InputManager : MonoBehaviour
    {
#if ENABLE_INPUT_SYSTEM 
        public PlayerInput Input;
#endif
        [FormerlySerializedAs("InputAction")] public PlayerInputAction Action;

        public void Init()
        {
#if ENABLE_INPUT_SYSTEM 
            Input = GetComponent<PlayerInput>();
#endif
            Action = GetComponent<PlayerInputAction>();
        }
        
        
    }
}