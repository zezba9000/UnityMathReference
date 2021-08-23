using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityMathReference;

public class PointToTriangleDemo : MonoBehaviour
{
	public Transform pointTransform;

	private void Update()
	{
		var point = pointTransform.position.ToVec3();
		var triangle = new Triangle3(new Vec3(-1, 1, 0), new Vec3(0, 1, 1), new Vec3(1, 1, 0));
		var closestPoint = point.ClosestPointToTriangle(triangle);

		Debug.DrawLine(triangle.point1.ToVector3(), triangle.point2.ToVector3(), Color.green);
		Debug.DrawLine(triangle.point2.ToVector3(), triangle.point3.ToVector3(), Color.green);
		Debug.DrawLine(triangle.point3.ToVector3(), triangle.point1.ToVector3(), Color.green);

		Debug.DrawLine(point.ToVector3(), closestPoint.ToVector3(), Color.red);
	}
}
