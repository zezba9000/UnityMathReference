using UnityEngine;
using System.Collections;
using UnityMathReference;

public class RayIntersectAxisDemo : MonoBehaviour
{
	void Update()
	{
		Vec3 point;

		// hit X plane
		var ray = new Ray3(new Vector3(-.5f, -.5f, -.5f), new Vector3(.75f, .2f, .5f).normalized);
		Debug.DrawLine(ray.origin.ToVector3(), (ray.origin + (ray.direction*5)).ToVector3(), Color.green, 0, false);
		if (ray.IntersectPlaneX(.5f, out point)) Debug.DrawLine(ray.origin.ToVector3(), point.ToVector3(), Color.red, 0, false);

		// hit Y plane
		ray = new Ray3(new Vector3(-.5f, -.5f, -.5f), new Vector3(.3f, .75f, .5f).normalized);
		Debug.DrawLine(ray.origin.ToVector3(), (ray.origin + (ray.direction * 5)).ToVector3(), Color.green, 0, false);
		if (ray.IntersectPlaneY(.5f, out point)) Debug.DrawLine(ray.origin.ToVector3(), point.ToVector3(), Color.red, 0, false);

		// hit Z plane
		ray = new Ray3(new Vector3(-.5f, -.5f, -.5f), new Vector3(.3f, .2f, 1).normalized);
		Debug.DrawLine(ray.origin.ToVector3(), (ray.origin + (ray.direction * 5)).ToVector3(), Color.green, 0, false);
		if (ray.IntersectPlaneZ(.5f, out point)) Debug.DrawLine(ray.origin.ToVector3(), point.ToVector3(), Color.red, 0, false);
	}
}
