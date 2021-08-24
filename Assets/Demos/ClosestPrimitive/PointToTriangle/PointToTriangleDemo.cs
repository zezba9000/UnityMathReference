using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityMathReference;

public class PointToTriangleDemo : MonoBehaviour
{
	public Transform p;
	public Transform triP1, triP2, triP3;

	private void Update()
	{
		var point = p.position.ToVec3();
		var triangle = new Triangle3(triP1.position, triP2.position, triP3.position);
		var closestPoint = point.ClosestPointToTriangle(triangle);

		Debug.DrawLine(triangle.point1.ToVector3(), triangle.point2.ToVector3(), Color.green);
		Debug.DrawLine(triangle.point2.ToVector3(), triangle.point3.ToVector3(), Color.green);
		Debug.DrawLine(triangle.point3.ToVector3(), triangle.point1.ToVector3(), Color.green);

		Debug.DrawLine(point.ToVector3(), closestPoint.ToVector3(), Color.red);
	}
}
