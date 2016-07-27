using UnityEngine;
using System.Collections;

public class RayIntersectAxisDemo : MonoBehaviour
{
	void Update()
	{
		Vector3 rayOrigin = new Vector3(-.2f, -.1f, -.45f);
		Vector3 rayDir = new Vector3(.3f, .2f, 1).normalized;
		Debug.DrawLine(rayOrigin, rayOrigin + (rayDir*5), Color.green, 0, false);

		Vector3 p1;
		if (IntersectRayAxisZ3(rayOrigin, rayDir, 0, out p1))
		{
			Debug.DrawLine(rayOrigin, p1, Color.red, 0, false);
		}
	}

	// TODO: refactor to use Vec types

	bool IntersectRayAxisZ3(Vector3 rayOrigin, Vector3 rayDir, float zOffset, out Vector3 point)
	{
		point = new Vector3();
		if (rayDir.z == 0) return false;

		// slopes
		float slopeX = rayDir.x / rayDir.z;
		float slopeY = rayDir.y / rayDir.z;

		// apply pos
		float dis = zOffset - rayOrigin.z;
		point.x = (slopeX * dis) + rayOrigin.x;
		point.y = (slopeY * dis) + rayOrigin.y;
		point.z = zOffset;

		return true;
	}
}
