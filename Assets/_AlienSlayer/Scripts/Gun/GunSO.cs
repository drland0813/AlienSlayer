using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace drland.AlienSlayer
{
    [Serializable]
    public enum GunType
    {
        AssaultRifle,
        GrenadeLauncher
    }
    
    public enum ShootControlType
    {
        QuickShoot,
        AimToShoot
    }

    [Serializable]
    public class HandIKData
    {
        public Transform LeftHand;
        public Transform RightHand;
    }


    [CreateAssetMenu(fileName = "Gun SO", menuName = "Gun/Gun SO", order = 0)]
    public class GunSO : ScriptableObject
    {
        public GunType Type;
        public String Name;
        public ShootControlType ShootControlType;
        public float Range;
        public int Damage;
        public Vector3 Spread;
        public float ShootSpeed;
        public BulletSO BulletSO;
        public HitImpactSO HitImpactSO;
        public AudioClip ShootAudioClip;
        public int GetDamage(float distance = 0)
        {
            float multiplier = 1 - (distance / Range);
            return Mathf.CeilToInt(Damage * multiplier);
        }
    }
}
