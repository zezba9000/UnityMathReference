using UnityEngine;
using Reign;

public class RefractDemo : MonoBehaviour
{
	float rot;

	// NOTE: 1 = no refraction, 0 = full refraction
	const float refractionIndex = .5f;

	void Update()
	{
		var dir = new Vec3(Mathf.Cos(rot), -Mathf.Abs(Mathf.Sin(rot)), 0).Normalize();
		rot += 1 * Time.deltaTime;

		Debug.DrawRay(new Vector3(), Vector3.up, Color.blue);
		Debug.DrawRay(new Vector3(), Vector3.right, Color.blue);
		Debug.DrawRay(new Vector3(), -Vector3.right, Color.blue);
		Debug.DrawRay(new Vector3(), dir.ToVector3(), Color.red);
		Debug.DrawRay(new Vector3(), dir.Refract(Vec3.up, refractionIndex).ToVector3(), Color.green);
	}

	// TODO
	bool IntersectLineCircle(
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
}
