using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Drland.Common.Utils;
using Drland.Ultils;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace drland.AlienSlayer
{
    public class GunController : MonoBehaviour
    {
        [SerializeField] private LayerMask _targetMask;
        [SerializeField] private List<Gun> _gunsList;
        [SerializeField] private Gun _currentGun;
        [SerializeField] private ObjectHolder _bulletSample;
        [SerializeField] private Transform _bulletHolder;

        private ObjectPool<ObjectHolder> _bulletPool;
        private float _lastShootTime;
        private InputManager _input;

        private bool _inAimMode;
        private Crosshair _crosshair;
        private Transform _crosshairTargetPosition;
        private bool _hasGun => _currentGun != null;

        [SerializeField] private int _gunIndex = 1;



        private void Awake()
        {
            _input = Player.Instance.Input;
            _bulletPool = new ObjectPool<ObjectHolder>(_bulletSample);
            _input.Action.OnChangGunAction = SwapGun;
            _crosshair = GetComponent<Crosshair>();
        }

        private void Start()
        {
            SwapGun();
        }

        private void SwapGun()
        {
            _gunIndex = (_gunIndex + 1) % _gunsList.Count;
            var newGun = _gunsList[_gunIndex];
            _currentGun?.gameObject.SetActive(false);
            newGun.gameObject.SetActive(true);
            _currentGun = newGun;
            _crosshairTargetPosition = _currentGun.BulletSpawnTransform;

            Player.Instance.RigManager.SetUpHandTarget(true, _currentGun.HandIKData.LeftHand, _currentGun.HandIKData.RightHand);
        }

        private void Update()
        {
            if (_input.Action.ChangeGun)
            {
                SwapGun();
            }

            if (!_hasGun) return;

            CrosshairHandler();
            if (_input.Action.Shoot)
            {
                GunInputHandler();
            }
            else
            {
                if (_inAimMode)
                {
                    _inAimMode = false;
                    _currentGun.EnableTrajectory(false);
                }
            }
        }

        private void CrosshairHandler()
        {
            var bulletEndPosition = _crosshairTargetPosition.position;
            if (_currentGun.GunSO.BulletSO.TrajectoryType == BulletTrajectoryType.Bezier)
            {
                bulletEndPosition.y = 0;
            }
            _crosshairTargetPosition.position = bulletEndPosition;
            var target3DPosition =_currentGun.BulletSpawnTransform.position +
                                  (_crosshairTargetPosition.forward * _currentGun.GunSO.Range);
            _crosshair.UpdateData(target3DPosition);
        }

        private void GunInputHandler()
        {
            if (!_hasGun) return;
            
            if (_currentGun.GunSO.ShootControlType == ShootControlType.QuickShoot)
            {
                Shoot();
            }
            else
            {
                if (_inAimMode) return;
                _inAimMode = true;
                StartCoroutine(AimToShootCoroutine());
            }
        }

        private IEnumerator AimToShootCoroutine()
        {
            while (_inAimMode)
            {
                if (_currentGun.GunSO.BulletSO.TrajectoryType == BulletTrajectoryType.Bezier)
                {
                    _currentGun.EnableTrajectory(true);
                }
                yield return null;
            }

            Shoot();
        }

        private void Shoot()
        {
            if (_lastShootTime + _currentGun.GunSO.ShootSpeed < Time.time)
            {
                var direction = _currentGun.BulletSpawnTransform.forward;
                direction += new Vector3(
                    Random.Range(-_currentGun.GunSO.Spread.x, _currentGun.GunSO.Spread.x),
                    Random.Range(-_currentGun.GunSO.Spread.y, _currentGun.GunSO.Spread.y),
                    Random.Range(-_currentGun.GunSO.Spread.z, _currentGun.GunSO.Spread.z));
                
                if (Physics.Raycast(_currentGun.BulletSpawnTransform.position, direction, out RaycastHit raycastHit, _currentGun.GunSO.Range, _targetMask))
                {
                    StartCoroutine(SpawnBullet(_currentGun.BulletSpawnTransform.position, raycastHit.point, raycastHit));

                }
                else
                {
                    StartCoroutine(SpawnBullet(_currentGun.BulletSpawnTransform.position,_currentGun.BulletSpawnTransform.position +  direction * _currentGun.GunSO.Range, new RaycastHit()));
                }
                _lastShootTime = Time.time;
            }
        }

        private IEnumerator SpawnBullet(Vector3 startPosition, Vector3 endPosition, RaycastHit raycastHit)
        {
            _currentGun.PlayShootEffect();
            
            
            var bullet = _bulletPool.Get(_bulletHolder);
            bullet.Init(_currentGun.GunSO.BulletSO.Prefab);
            bullet.transform.position = startPosition;
            var bulletConfig = _currentGun.GunSO.BulletSO;
            var distance = 0f;
            var remainingDistance = 0f;
            var bulletTrajectoryType = _currentGun.GunSO.BulletSO.TrajectoryType;
            if (bulletTrajectoryType == BulletTrajectoryType.Bezier)
            {
                var startControlPoint = startPosition + new Vector3(0, 1, 0);
                var endControlPoint = endPosition + new Vector3(0, 1f, 0);

                distance = CalculateHelper.CalculateBezierLength(startPosition, endPosition, startControlPoint,
                    endControlPoint);
                remainingDistance = distance;
                endPosition.y = 0;
                while (remainingDistance > 0)
                {
                    bullet.transform.position = DOCurve.CubicBezier.GetPointOnSegment(
                        startPosition,
                        startControlPoint,
                        endPosition,
                        endControlPoint,
                        Mathf.Clamp01(1 - (remainingDistance / distance))
                    );

                    remainingDistance -= bulletConfig.Speed * Time.deltaTime;
                    yield return null;
                }
            }
            else
            {
                distance = Vector3.Distance(startPosition, endPosition);
                remainingDistance = distance;
                while (remainingDistance > 0)
                {
                    bullet.transform.position = Vector3.Lerp(startPosition, endPosition,
                        Mathf.Clamp01(1 - (remainingDistance / distance)));
                    remainingDistance -= bulletConfig.Speed * Time.deltaTime;
                    yield return null;
                }
            }

            bullet.transform.position = endPosition;
            if (raycastHit.collider != null)
            {
                if (raycastHit.collider.TryGetComponent(out HealthComponent healthComponent))
                {
                    Debug.Log($"helth {healthComponent.gameObject.name}");
                    healthComponent?.TakeDamage(_currentGun.GunSO.Damage);
                }

                HitImpactManager.Instance.CreateHitImpact(bulletConfig.Name, raycastHit, endPosition);
            }
            else
            {
                if (bulletTrajectoryType == BulletTrajectoryType.Bezier)
                {
                    HitImpactManager.Instance.CreateHitImpact(bulletConfig.Name, raycastHit, endPosition);
                }
            }

            StartCoroutine(WaitUtils.WaitToDo(bulletConfig.LifeTime, () =>
            {
                bullet.ClearData();
                _bulletPool.Store(bullet);
            }));
        }
    }
}