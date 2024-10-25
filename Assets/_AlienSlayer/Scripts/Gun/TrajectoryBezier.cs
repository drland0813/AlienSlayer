using UnityEngine;

namespace drland.AlienSlayer
{
    public class TrajectoryBezier : Trajectory
    {
        [SerializeField] protected LineRenderer _trajectoryLine;
        
        
        public void Draw(Vector3 startPoint, Vector3 firstControlPoint, Vector3 secondControlPoint, Vector3 endPoint)
        {
            _trajectoryLine.positionCount = _resolution + 1;
            for (var i = 0; i <= _resolution; i++)
            {
                float t = i / (float) _resolution;
                Vector3 point = CalculateHelper.CalculateBezierPoint(t, startPoint, firstControlPoint, secondControlPoint, endPoint);
                _trajectoryLine.SetPosition(i, point);
            }
        }
    }
}