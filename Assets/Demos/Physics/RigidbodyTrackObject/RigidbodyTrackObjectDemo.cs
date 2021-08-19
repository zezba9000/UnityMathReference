using UnityEngine;
using UnityMathReference;

public class RigidbodyTrackObjectDemo : MonoBehaviour
{
	public Transform srcObject;
	public Rigidbody dstRigidbody_UMR, dstRigidbody_U;
	[Range(0, 1)] public float followSpeed = .25f;

	private Vector3 startPos_UMR, startPos_U;

	private void Start()
	{
		// make sure there is no cap
		dstRigidbody_UMR.maxAngularVelocity = float.MaxValue;
		dstRigidbody_U.maxAngularVelocity = float.MaxValue;

		// capture start positions
		startPos_UMR = dstRigidbody_UMR.position;
		startPos_U = dstRigidbody_U.position;
	}

	private void FixedUpdate()
	{
		Run_UnityMathReferencePrimitives();
		Run_UnityPrimitives();
	}

	private void Run_UnityMathReferencePrimitives()
	{
		// apply angular-velocity to rigidbody
		var rotationDelta = dstRigidbody_UMR.rotation.ToQuat().Delta(srcObject.transform.rotation.ToQuat());
		var angularVelocity = rotationDelta.AngularVelocity();
		dstRigidbody_UMR.angularVelocity = ((angularVelocity * followSpeed) / Time.fixedDeltaTime).ToVector3();

		// apply linear-velocity to rigidbody
		var linearVelocity = startPos_UMR.ToVec3() - dstRigidbody_UMR.position.ToVec3();
		dstRigidbody_UMR.velocity = ((linearVelocity * followSpeed) / Time.fixedDeltaTime).ToVector3();
	}

	private void Run_UnityPrimitives()
	{
		// apply angular-velocity to rigidbody
		var rotationDelta = srcObject.transform.rotation * Quaternion.Inverse(dstRigidbody_U.rotation);

		rotationDelta.ToAngleAxis(out float angle, out var axis);
		var angularVelocity = axis * angle * Mathf.Deg2Rad;

		dstRigidbody_U.angularVelocity = (angularVelocity * followSpeed) / Time.fixedDeltaTime;

		// apply linear-velocity to rigidbody
		var linearVelocity = startPos_U - dstRigidbody_U.position;
		dstRigidbody_U.velocity = (linearVelocity * followSpeed) / Time.fixedDeltaTime;
	}
}
