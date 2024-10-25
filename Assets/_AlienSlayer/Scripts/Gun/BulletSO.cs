using UnityEngine;
using UnityEngine.Serialization;

namespace drland.AlienSlayer
{
    public enum BulletTrajectoryType
    {
        Straight,
        Bezier
    }
    
    public enum BulletName
    {
        Bullet,
        Grenade
    }
    [CreateAssetMenu(fileName = "BulletSO", menuName = "Bullet", order = 0)]
    public class BulletSO : ScriptableObject
    {
        public BulletName Name;
        public BulletTrajectoryType TrajectoryType;
        public float Speed;
        public float LifeTime;
        public GameObject Prefab;
    }
}