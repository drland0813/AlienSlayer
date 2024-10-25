using DG.Tweening;
using UnityEngine;

namespace drland.AlienSlayer
{
    public class CalculateHelper
    {
        public static float CalculateBezierLength(Vector3 startPoint, Vector3 startControlPoint, Vector3 endPoint, Vector3 endControlPoint, int resolution = 50)
        {
            float totalLength = 0f;
            Vector3 previousPoint = startPoint;
    
            for (int i = 1; i <= resolution; i++)
            {
                float t = (float)i / resolution;
        
                Vector3 currentPoint = DOCurve.CubicBezier.GetPointOnSegment(
                    startPoint,
                    startControlPoint,
                    endPoint,
                    endControlPoint,
                    t
                );
        
                totalLength += Vector3.Distance(previousPoint, currentPoint);
        
                previousPoint = currentPoint;
            }

            return totalLength;
        }
        
        public static Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
        {
            float u = 1 - t;
            float tt = t * t;
            float uu = u * u;
            float uuu = uu * u;
            float ttt = tt * t;

            Vector3 point = uuu * p0; 
            point += 3 * uu * t * p1; 
            point += 3 * u * tt * p2; 
            point += ttt * p3;     

            return point;
        }
    }
}