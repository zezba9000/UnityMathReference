using UnityEngine;
using System.Collections;
using UnityMathReference;

public class RayIntersectPlaneDemo : MonoBehaviour
{
	public Transform plane;

	void Update()
	{
		Vec3 point;
		
		var ray = new Ray3(new Vector3(1, 3, .5f), new Vector3(.5f, -1, .5f).normalized);
		Debug.DrawLine(ray.origin.ToVector3(), (ray.origin + (ray.direction * 5)).ToVector3(), Color.green, 0, false);
		if (ray.IntersectPlane(plane.up, new Vec3(), out point)) Debug.DrawLine(ray.origin.ToVector3(), point.ToVector3(), Color.red, 0, false);
	}
}
