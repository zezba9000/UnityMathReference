using UnityEngine;
using UnityMathReference;

public class AngularVelocityDemo : MonoBehaviour
{
	private new Rigidbody rigidbody;

	private void Start()
	{
		rigidbody = GetComponent<Rigidbody>();
		rigidbody.maxAngularVelocity = float.MaxValue;// make sure there is no cap
	}

	private void FixedUpdate()
	{
		// get dumby current and last rotation values
		var rotationLast = Quat.FromEuler(0, 0, 0);
		var rotationCurrent = Quat.FromEuler(0, 0, 1);

		// generate angular velocity
		//var angularVelocity = rotationLast.Inverse().Multiply(rotationCurrent).AngularVelocity();// NOTE: this common order of operations approch seems to have gimbal-lock issues (maybe someone can explain why?)
		var angularVelocity = rotationCurrent.Delta(rotationLast).AngularVelocity();// USE-THIS: correct order of operations with no gimbal-lock issues

		// apply velocity to rigidbody
		rigidbody.angularVelocity = angularVelocity.ToVector3();
	}
}
