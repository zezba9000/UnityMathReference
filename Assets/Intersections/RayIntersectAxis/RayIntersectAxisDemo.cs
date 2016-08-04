using UnityEngine;
using System.Collections;

public class RayIntersectAxisDemo : MonoBehaviour
{
	void Update()
	{
		Vector3 p1;

		Vector3 rayOriginX = new Vector3(-.5f, -.5f, -.5f);
		Vector3 rayDirX = new Vector3(.75f, .2f, .5f).normalized;
		Debug.DrawLine(rayOriginX, rayOriginX + (rayDirX*5), Color.green, 0, false);
		if (IntersectRayAxisX3(rayOriginX, rayDirX, .5f, out p1))
		{
			Debug.DrawLine(rayOriginX, p1, Color.red, 0, false);
		}

		Vector3 rayOriginY = new Vector3(-.5f, -.5f, -.5f);
		Vector3 rayDirY = new Vector3(.3f, .75f, .5f).normalized;
		Debug.DrawLine(rayOriginY, rayOriginY + (rayDirY*5), Color.green, 0, false);
		if (IntersectRayAxisY3(rayOriginY, rayDirY, .5f, out p1))
		{
			Debug.DrawLine(rayOriginY, p1, Color.red, 0, false);
		}
		
		Vector3 rayOriginZ = new Vector3(-.5f, -.5f, -.5f);
		Vector3 rayDirZ = new Vector3(.3f, .2f, 1).normalized;
		Debug.DrawLine(rayOriginZ, rayOriginZ + (rayDirZ*5), Color.green, 0, false);
		if (IntersectRayAxisZ3(rayOriginZ, rayDirZ, .5f, out p1))
		{
			Debug.DrawLine(rayOriginZ, p1, Color.red, 0, false);
		}
	}

	// TODO: refactor to use Vec types

	bool IntersectRayAxisX3(Vector3 rayOrigin, Vector3 rayDir, float xOffset, out Vector3 point)
	{
		point = new Vector3();
		if (rayDir.x == 0.0f) return false;

		// slopes
		float slopeZ = rayDir.z / rayDir.x;
		float slopeY = rayDir.y / rayDir.x;

		// apply pos
		float dis = xOffset - rayOrigin.x;
		point.z = (slopeZ * dis) + rayOrigin.z;
		point.y = (slopeY * dis) + rayOrigin.y;
		point.x = xOffset;

		return true;
	}

	bool IntersectRayAxisY3(Vector3 rayOrigin, Vector3 rayDir, float yOffset, out Vector3 point)
	{
		point = new Vector3();
		if (rayDir.y == 0.0f) return false;

		// slopes
		float slopeX = rayDir.x / rayDir.y;
		float slopeZ = rayDir.z / rayDir.y;

		// apply pos
		float dis = yOffset - rayOrigin.y;
		point.x = (slopeX * dis) + rayOrigin.x;
		point.z = (slopeZ * dis) + rayOrigin.z;
		point.y = yOffset;

		return true;
	}

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
