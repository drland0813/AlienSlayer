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
    }
}