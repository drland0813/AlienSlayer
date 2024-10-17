using UnityEngine;

namespace drland.AlienSlayer
{
    public class CameraManager : MonoBehaviour
    {
        private Camera _mainCamera;
        public Camera MainCamera => _mainCamera;
        
        public void Init()
        {
            _mainCamera = Camera.main;
        }
    }
}