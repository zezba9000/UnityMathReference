using UnityEngine;
using UnityMathReference;

public class PointIntersectPlaneDemo : MonoBehaviour
{
	public Transform pointTransform, planeTransform;    

    void Update()
    {
        var point = pointTransform.position.ToVec3();
		var planePoint = planeTransform.position.ToVec3();
		var planeDir = planeTransform.up.ToVec3();
		var collisionPoint = point.IntersectPlane(planeDir, planePoint);
		Debug.DrawLine(point.ToVector3(), collisionPoint.ToVector3(), Color.green, 0, false);
	}
}
