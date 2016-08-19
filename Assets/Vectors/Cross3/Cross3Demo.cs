using UnityEngine;
using System.Collections;
using Reign;

public class Cross3Demo : MonoBehaviour
{
	float rot;

	void Update()
	{
		var forward = new Vec3(0, 0, 1).Normalize();
		var up = new Vec3(Mathf.Sin(rot), 1, 0).Normalize();
		var right = up.Cross(forward);
		Debug.DrawRay(Vector3.zero, right.ToVector3(), Color.red);
		Debug.DrawRay(Vector3.zero, up.ToVector3(), Color.green);
		Debug.DrawRay(Vector3.zero, forward.ToVector3(), Color.blue);

		rot += Time.deltaTime;
	}
}
