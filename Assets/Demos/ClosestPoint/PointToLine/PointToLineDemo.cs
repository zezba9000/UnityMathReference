using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityMathReference;

public class PointToLineDemo : MonoBehaviour
{
	public Transform pointTransform;

	private void Update()
	{
		var point = pointTransform.position.ToVec3();
		var line = new Line3(new Vec3(-1, 1, 0), new Vec3(1, 1, 0));
		var closestPoint = point.ClosestPointToLine(line);

		Debug.DrawLine(line.point1.ToVector3(), line.point2.ToVector3(), Color.green);
		Debug.DrawLine(point.ToVector3(), closestPoint.ToVector3(), Color.red);
	}
}
