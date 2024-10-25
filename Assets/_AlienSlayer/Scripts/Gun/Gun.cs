using System;
using Drland.Ultils;
using UnityEngine;
using UnityEngine.Serialization;

namespace drland.AlienSlayer
{
    public class Gun : MonoBehaviour
    {
        public GunSO GunSO;
        public Transform BulletSpawnTransform;
        public Vector3 BulletStartPosition;
        public ParticleSystem MuzzleParticle;
        public HandIKData HandIKData;
        public TrajectoryBezier TrajectoryBezier;
        public AudioSource AudioSource;
        private void Update()
        {
            if (BulletSpawnTransform.localPosition != BulletStartPosition)
            {
                BulletSpawnTransform.localPosition = BulletStartPosition;
            }
        }

        public void PlayShootEffect()
        {
            MuzzleParticle.Play();
            AudioSource.PlayOneShot(GunSO.ShootAudioClip);
        }

        public void EnableTrajectory(bool enable)
        {
            TrajectoryBezier.EnableTrajectory(enable);
            if (!enable) return;

            var direction = BulletSpawnTransform.forward;
            var startPoint = BulletSpawnTransform.position;
            var endPoint = startPoint + direction * GunSO.Range;
            endPoint.y = 0;
            var startControlPoint = startPoint + new Vector3(0, 1, 0);
            var endControlPoint = endPoint + new Vector3(0, 1f, 0);
            TrajectoryBezier.Draw(startPoint, startControlPoint, endControlPoint, endPoint);
        }
    }
}