using System;
using System.Collections;
using UnityEngine;

namespace drland.AlienSlayer
{
    public class GunController : MonoBehaviour
    {
        [SerializeField] private LayerMask _targetMask;
        [SerializeField] private float _shootDelay;
        
        [SerializeField] private Gun _currentGun;
        private float _lastShootTime;
        private InputManager _input;

        private void Start()
        {
            _input = Player.Instance.Input;
            Debug.Log("shoot: " + _input.Action.shoot);
        }

        private void Update()
        {
            if (_input.Action.shoot)
            {
                Debug.Log("shoot");
                Shoot();
            }
        }

        public void Shoot()
        {
            if (_lastShootTime + _shootDelay < Time.time)
            {
                _currentGun.GunSO.MuzzleParticle.Play();
                Vector3 direction = GetDirection();

                if (Physics.Raycast(_currentGun.BulletSpawnTransform.position, direction, out RaycastHit raycastHit, float.MaxValue, _targetMask))
                {
                    TrailRenderer trailRenderer = Instantiate(_currentGun.GunSO.BulltetTrail,
                        _currentGun.BulletSpawnTransform.position, Quaternion.identity);

                    StartCoroutine(SpawnTrail(trailRenderer, raycastHit));
                }
            }
        }

        private IEnumerator SpawnTrail(TrailRenderer trailRenderer, RaycastHit raycastHit)
        {
            float time = 0;
            var trailTransform = trailRenderer.transform;
            Vector3 startPosition = trailTransform.position;
            while (time < 1)
            {
                trailTransform.position = Vector3.Lerp(startPosition, raycastHit.point, time);
                time += Time.deltaTime / trailRenderer.time;
                yield return null;
            }

            trailTransform.position = raycastHit.point;
            Instantiate(_currentGun.GunSO.ImpactParticle, raycastHit.point, Quaternion.LookRotation(raycastHit.normal));
            Destroy(trailRenderer.gameObject, trailRenderer.time);
        }

        private Vector3 GetDirection()
        {
            Vector3 direction = transform.forward;
            return direction;
        }
    }
}