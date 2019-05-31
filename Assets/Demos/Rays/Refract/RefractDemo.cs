using UnityEngine;
using UnityMathReference;

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
}
