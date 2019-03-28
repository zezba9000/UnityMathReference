using UnityEngine;
using UnityMathReference;

public class PointIntersectRectangleDemo : MonoBehaviour
{
	public Transform pointTransform, rectTransform;

	void Update()
	{
		var point = pointTransform.position.ToVec3();
		var rectCenter = rectTransform.position.ToVec3();
		var rectRot = rectTransform.rotation.ToQuat();
		var rectScale = rectTransform.localScale.ToVec3().ToVec2();
		Vec3 collisionPoint;
		if (point.IntersectRectangle(rectCenter, rectRot, rectScale, out collisionPoint))
		{
			Debug.DrawLine(point.ToVector3(), collisionPoint.ToVector3(), Color.green, 0, false);
		}
	}
}
