using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityMathReference;

public class LineToTriangleDemo : MonoBehaviour
{
	public Transform lineP1, lineP2;
	public Transform triP1, triP2, triP3;

	private void Update()
	{
		var line = new Line3(lineP1.position, lineP2.position);
		var triangle = new Triangle3(triP1.position, triP2.position, triP3.position);
		var closestLine = line.ClosestLineToTriangle(triangle);

		Debug.DrawLine(triangle.point1.ToVector3(), triangle.point2.ToVector3(), Color.green);
		Debug.DrawLine(triangle.point2.ToVector3(), triangle.point3.ToVector3(), Color.green);
		Debug.DrawLine(triangle.point3.ToVector3(), triangle.point1.ToVector3(), Color.green);

		Debug.DrawLine(line.point1.ToVector3(), line.point2.ToVector3(), Color.red);

		Debug.DrawLine(closestLine.point1.ToVector3(), closestLine.point2.ToVector3(), Color.blue);
	}
}
