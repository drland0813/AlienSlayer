using UnityEngine;

namespace drland.AlienSlayer
{
    public class TrajectoryBezier : Trajectory
    {
        public void Draw(Vector3 startPoint, Vector3 startControlPoint, Vector3 endControlPoint, Vector3 endPoint)
        {
            _trajectoryLine.positionCount = _resolution + 1;
            for (var i = 0; i <= _resolution; i++)
            {
                float t = i / (float) _resolution;
                Vector3 point = CalculateHelper.CalculateBezierPoint(t, startPoint, startControlPoint, endControlPoint, endPoint);
                _trajectoryLine.SetPosition(i, point);
            }
        }
    }
}