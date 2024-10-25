using UnityEditor;
using UnityEngine;

namespace drland.AlienSlayer
{
    public class Trajectory : MonoBehaviour
    {
        [SerializeField] protected LineRenderer _trajectoryLine;
        [SerializeField] protected int _resolution;
        public void EnableTrajectory(bool enable)
        {
            _trajectoryLine.gameObject.SetActive(enable);
        }
    }
}