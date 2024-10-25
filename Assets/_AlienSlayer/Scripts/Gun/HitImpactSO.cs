using UnityEngine;

namespace drland.AlienSlayer
{
    [CreateAssetMenu(fileName = "HitImpactSO", menuName = "HitImpact", order = 0)]
    public class HitImpactSO : ScriptableObject
    {
        public string Name;
        public float LifeTime;
        public GameObject Prefab;
    }
}