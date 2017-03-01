using UnityEngine;
using UnityMathReference;

public class RayIntersectSphereDemo : MonoBehaviour
{
	void Update()
	{
		Ray3 ray = new Ray3(new Vec3(-2, .3f, .25f), new Vec3(1, .25f, -.4f).Normalize()); 
		Debug.DrawLine(ray.origin.ToVector3(), (ray.origin + (ray.direction*5)).ToVector3(), Color.green, 0, false);

		Vec3 p1, p2, n1, n2;
		if (ray.IntersectRaySphere(Vec3.zero, 1, out p1, out p2, out n1, out n2))
		{
			Debug.DrawLine(p1.ToVector3(), p2.ToVector3(), Color.red, 0, false);
		}
	}
}
