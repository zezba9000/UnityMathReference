using UnityEngine;
using System.Collections;
using Reign;

public class Matrix3FromCrossedVectorsDemo : MonoBehaviour
{
	float rot;

	void Update()
	{
		var forward = new Vec3(Mathf.Cos(rot), Mathf.Sin(rot), 1).Normalize();
		var up = new Vec3(Mathf.Sin(rot), 1, 0).Normalize();
		
		var mat = Mat3.LookAt(forward, up);
		Debug.DrawRay(Vector3.zero, mat.x.ToVector3(), Color.red);
		Debug.DrawRay(Vector3.zero, mat.y.ToVector3(), Color.green);
		Debug.DrawRay(Vector3.zero, mat.z.ToVector3(), Color.blue);

		rot += Time.deltaTime;
	}
}
