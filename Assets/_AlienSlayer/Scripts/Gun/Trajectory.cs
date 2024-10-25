using UnityEngine;

namespace drland.AlienSlayer
{
    public class TrajectoryBezier : TrajectoryBullet
    {
        [SerializeField] protected LineRenderer _trajectoryLine;

        public void EnableTrajectory(bool enable)
        {
            gameObject.SetActive(enable);    
        }
        
        
        private void SetUpPoint(Vector3 startPoint, Vector3 nextStartPoint)
        {
            
        }
    }
    public class TrajectoryBullet : MonoBehaviour
    {
        [SerializeField] protected LineRenderer _trajectoryLine;

        public void EnableTrajectory(bool enable)
        {
            gameObject.SetActive(enable);    
        }
    }
}