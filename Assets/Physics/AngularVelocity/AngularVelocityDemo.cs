using UnityEngine;
using UnityMathReference;

public class AngularVelocityDemo : MonoBehaviour
{
	private new Rigidbody rigidbody;

	void Start()
	{
		rigidbody = GetComponent<Rigidbody>();
	}
	
	void FixedUpdate()
	{
		// get dumby current and last rotation values
		var rotationLast = Quat.FromEuler(0, 0, 0);
		var rotationCurrent = Quat.FromEuler(0, 0, 1);

		// generate angular velocity
		var angleVel = rotationLast.Inverse().Multiply(rotationCurrent).AngularVel();

		// apply velocity to rigidbody
		rigidbody.angularVelocity = angleVel.ToVector3();
	}
}
