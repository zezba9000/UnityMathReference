using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereRigidbodiesDemo : MonoBehaviour
{
	public float wallBounds = 5;
	public float airFriction = .99f;
	public Transform ball1, ball2;
	public float ballMass1 = 1, ballMass2 = 1;
	public Vector3 ballVel1, ballVel2;
	private Vector3 ballAccel1, ballAccel2;

	private Vector3 origPos1, origPos2;

	private void Start()
	{
		origPos1 = ball1.position;
		origPos2 = ball2.position;
	}

	private void OnGUI()
	{
		if (GUI.Button(new Rect(10, Screen.height - 10 - 32, 128, 32), "Reset"))
		{
			ball1.position = origPos1;
			ball2.position = origPos2;
			ballVel1 = Vector3.zero;
			ballVel2 = Vector3.zero;
		}
	}

	private void FixedUpdate()
	{
		float radius1 = ball1.localScale.x * .5f;
		float radius2 = ball2.localScale.x * .5f;

		SimulateStep(ball1, ref ballVel1, ref ballAccel1);
		SimulateStep(ball2, ref ballVel2, ref ballAccel2);

		CollideBalls(ball1, ball2, ref ballVel1, ref ballVel2, radius1, radius2);

		CollideWalls(ball1, ref ballVel1, radius1);
		CollideWalls(ball2, ref ballVel2, radius2);
	}

	private void SimulateStep(Transform ball, ref Vector3 vel, ref Vector3 accel)
	{
		// apply gravity
		var gravity = Physics.gravity * Time.fixedDeltaTime;// calculate with acceleration
		accel += gravity;

		// move balls
		vel += accel;
		vel *= airFriction;
		ball.position += vel * Time.fixedDeltaTime;

		// reset accel
		accel = Vector3.zero;
	}

	private void CollideWalls(Transform ball, ref Vector3 vel, float radius)
	{
		var pos = ball.position;

		// floor
		if (pos.y - radius < 0)
		{
			pos.y = radius;
			vel.y = -vel.y;
		}

		// left
		if (pos.x - radius < -wallBounds)
		{
			pos.x = (-wallBounds) + radius;
			vel.x = -vel.x;
		}

		// right
		if (pos.x + radius > wallBounds)
		{
			pos.x = wallBounds - radius;
			vel.x = -vel.x;
		}

		// back
		if (pos.z - radius < -wallBounds)
		{
			pos.z = (-wallBounds) + radius;
			vel.z = -vel.z;
		}

		// front
		if (pos.z + radius > wallBounds)
		{
			pos.z = wallBounds - radius;
			vel.z = -vel.z;
		}

		ball.position = pos;
	}

	private void CollideBalls(Transform ball1, Transform ball2, ref Vector3 vel1, ref Vector3 vel2, float radius1, float radius2)
	{
		var vec = ball1.position - ball2.position;
		float dis = vec.magnitude;
		if (dis < radius1 + radius2)
		{
			var n = vec.normalized;
			ReflectVelocity(ref vel1, ref vel2, ballMass1, ballMass2, n);

			var c = Vector3.Lerp(ball1.position, ball2.position, radius1 / (radius1 + radius2));
			ball1.position = c + (n * radius1);
			ball2.position = c - (n * radius2);
		}
	}

	public static void ReflectVelocity(ref Vector3 vel1, ref Vector3 vel2, float mass1, float mass2, Vector3 intersectionNormal)
	{
		float velImpact1 = Vector3.Dot(vel1, intersectionNormal);
		float velImpact2 = Vector3.Dot(vel2, intersectionNormal);

		float totalMass = mass1 + mass2;
		float massTransfure1 = mass1 / totalMass;
		float massTransfure2 = mass2 / totalMass;

		vel1 += ((velImpact2 * massTransfure2) - (velImpact1 * massTransfure2)) * intersectionNormal;
		vel2 += ((velImpact1 * massTransfure1) - (velImpact2 * massTransfure1)) * intersectionNormal;
	}
}
