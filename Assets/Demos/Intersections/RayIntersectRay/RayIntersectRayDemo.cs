using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityMathReference;

public class RayIntersectRayDemo : MonoBehaviour
{
	private Ray3 ray1 = new Ray3(new Vec3(-1, -3, 0), new Vec3(2, 3, 0));
	private Ray3 ray2 = new Ray3(new Vec3(-4, 1, 0), new Vec3(4, 2, 2.6f));

	private void Update()
	{
		ray1.direction = ray1.direction.NormalizeSafe();
		ray2.direction = ray2.direction.NormalizeSafe();
		var intersectionLine = ray1.IntersectRay(ray2);
		Debug.DrawRay(ray1.origin.ToVector3(), (ray1.direction * 10).ToVector3(), Color.green);
		Debug.DrawRay(ray2.origin.ToVector3(), (ray2.direction * 10).ToVector3(), Color.red);
		Debug.DrawLine(intersectionLine.point1.ToVector3(), intersectionLine.point2.ToVector3(), Color.blue);
	}
}
