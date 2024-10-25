using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace drland.AlienSlayer
{
    public class Crosshair : MonoBehaviour
    {
        [SerializeField] private RectTransform _crosshairUI;
        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }
        public void UpdateData(Vector3 target3DPosition)
        {
            UpdateCrosshairPosition(target3DPosition);
        }

        private void UpdateCrosshairPosition(Vector3 _target3DPosition)
        {
            Vector3 screenPosition = _camera.WorldToScreenPoint(_target3DPosition);

            if (screenPosition.z > 0)
            {
                _crosshairUI.position = screenPosition;
            }
            else
            {
                _crosshairUI.gameObject.SetActive(false);
            }
        }
    }
}
