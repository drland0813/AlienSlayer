using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace drland.AlienSlayer
{
    [CreateAssetMenu(fileName = "HitImpactSO", menuName = "HitImpact", order = 0)]
    public class HitImpactSO : ScriptableObject
    {
        public BulletName Name;
        public float LifeTime;
        public LayerMask Target;
        public List<GameObject> HitImpactCollisionPrefab;
        public GameObject GetHitImpactCollisionPrefab(int index)
        {
            return HitImpactCollisionPrefab[index];
        }
    }
    
}