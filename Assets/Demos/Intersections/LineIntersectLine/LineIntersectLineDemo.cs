using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityMathReference;

public class LineIntersectLineDemo : MonoBehaviour
{
	public Line3 line1 = new Line3(new Vec3(-1, -3, 0), new Vec3(2, 3, 0));
	public Line3 line2 = new Line3(new Vec3(-4, 1, 0), new Vec3(4, 2, 2.6f));

	private void Update()
	{
		var intersectionLine = line1.IntersectLine(line2);
		Debug.DrawLine(line1.point1.ToVector3(), line1.point2.ToVector3(), Color.green);
		Debug.DrawLine(line2.point1.ToVector3(), line2.point2.ToVector3(), Color.red);
		Debug.DrawLine(intersectionLine.point1.ToVector3(), intersectionLine.point2.ToVector3(), Color.blue);
	}
}
