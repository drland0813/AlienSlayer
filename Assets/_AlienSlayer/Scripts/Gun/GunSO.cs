using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace drland.AlienSlayer
{
    [Serializable]
    public enum GunType
    {
        AssaultRifle,
        GrenadeLauncher
    }

    [Serializable]
    public class GunStats
    {
        public float Damage;
        public float ShootDelay;
    }

    [CreateAssetMenu(fileName = "Gun SO", menuName = "Gun/Gun SO", order = 0)]
    public class GunSO : ScriptableObject
    {
        public GunType Type;
        public String Name;
        public GunStats Stats;
        public ParticleSystem MuzzleParticle;
        public ParticleSystem ImpactParticle;
        public TrailRenderer BulltetTrail;
    }
}
