using UnityEngine;

namespace drland.AlienSlayer
{
    [CreateAssetMenu(fileName = "BulletSO", menuName = "Bullet", order = 0)]
    public class BulletSO : ScriptableObject
    {
        public float Speed;
        public float TimeLife;
        public BulletTrajectoryType TrajectoryType;
    }
}