using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityMathReference;

public class LineToLineDemo : MonoBehaviour
{
	public Transform line1_P1, line1_P2, line2_P1, line2_P2;

	private void Update()
	{
		var line1 = new Line3(line1_P1.position, line1_P2.position);
		var line2 = new Line3(line2_P1.position, line2_P2.position);
		var closestLine = line1.ClosestLineToLine(line2);

		Debug.DrawLine(line1_P1.position, line1_P2.position, Color.red);
		Debug.DrawLine(line2_P1.position, line2_P2.position, Color.green);
		Debug.DrawLine(closestLine.point1.ToVector3(), closestLine.point2.ToVector3(), Color.blue);
	}
}
