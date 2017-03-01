using UnityEngine;
using UnityMathReference;

public class Transform3 : MonoBehaviour
{
	void Update()
	{
		var vec = new Vec3(0, 0, 1).Normalize();
		Debug.DrawRay(Vector3.zero, vec.ToVector3(), Color.red);

		var vecTran = vec.Transform(Quat.FromEuler(0, MathUtilities.piHalf * .5f, 0));
		Debug.DrawRay(Vector3.zero, vecTran.ToVector3(), Color.green);
	}
}
