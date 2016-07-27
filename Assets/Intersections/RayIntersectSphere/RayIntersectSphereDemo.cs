using UnityEngine;
using System.Collections;

public class RayIntersectSphereDemo : MonoBehaviour
{
	void Update()
	{
		Vector3 rayOrigin = new Vector3(-2, .3f, .25f);
		Vector3 rayDir = new Vector3(1, .25f, -.4f).normalized;
		Debug.DrawLine(rayOrigin, rayOrigin + (rayDir*5), Color.green, 0, false);

		Vector3 p1, p2;
		if (IntersectRayCircle3(rayOrigin, rayDir, new Vector3(), 1, out p1, out p2))
		{
			Debug.DrawLine(p1, p2, Color.red, 0, false);
		}
	}

	// TODO: refactor these methods and put them in the Vector structs

	bool IntersectRayCircle2(
	Vector2 O,  // Line origin
	Vector2 D,  // Line direction
	Vector2 C,  // Circle center
	float radius,      // Circle radius
	//float[] t,        // Parametric values at intersection points
	//Vector2[] point,   // Intersection points
	//Vector2[] normal) // Normals at intersection points
	out Vector2 point1, out Vector2 point2)
	{
		Vector2 d = O - C;
		float a = Vector2.Dot(D, D);
		float b = Vector2.Dot(d, D);
		float c = Vector2.Dot(d, d) - radius * radius;

		float disc = b * b - a * c;
		if (disc < 0.0f)
		{
			point1 = O;
			point2 = O;
			return false;
		}
	
		float sqrtDisc = Mathf.Sqrt(disc);
		float invA = 1.0f / a;
		float t1 = (-b - sqrtDisc) * invA;
		float t2 = (-b + sqrtDisc) * invA;
	
		float invRadius = 1.0f / radius;
		//for (int i = 0; i < 2; ++i)
		//{
		//	point[i] = O + t[i] * D;
		//	normal[i] = (point[i] - C) * invRadius;
		//}
		point1 = O + t1 * D;
		point2 = O + t2 * D;
		//normal[i] = (point[i] - C) * invRadius;
	
		return true;
	}

	bool IntersectRayCircle3(
	Vector3 O,  // Line origin
	Vector3 D,  // Line direction
	Vector3 C,  // Circle center
	float radius,      // Circle radius
	//float[] t,        // Parametric values at intersection points
	//Vector2[] point,   // Intersection points
	//Vector2[] normal) // Normals at intersection points
	out Vector3 point1, out Vector3 point2)
	{
		Vector3 d = O - C;
		float a = Vector3.Dot(D, D);
		float b = Vector3.Dot(d, D);
		float c = Vector3.Dot(d, d) - radius * radius;

		float disc = b * b - a * c;
		if (disc < 0.0f)
		{
			point1 = O;
			point2 = O;
			return false;
		}
	
		float sqrtDisc = Mathf.Sqrt(disc);
		float invA = 1.0f / a;
		float t1 = (-b - sqrtDisc) * invA;
		float t2 = (-b + sqrtDisc) * invA;
	
		float invRadius = 1.0f / radius;
		//for (int i = 0; i < 2; ++i)
		//{
		//	point[i] = O + t[i] * D;
		//	normal[i] = (point[i] - C) * invRadius;
		//}
		point1 = O + t1 * D;
		point2 = O + t2 * D;
		//normal[i] = (point[i] - C) * invRadius;
	
		return true;
	}
}
