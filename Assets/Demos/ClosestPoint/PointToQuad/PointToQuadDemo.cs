using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityMathReference;

public class PointToQuadDemo : MonoBehaviour
{
	public Transform point;
	private float rot;

	private void Update()
	{
		point.position = new Vector3(Mathf.Cos(rot), Mathf.Sin(rot), 0) * 1.25f;
		var closestPoint = point.position.ToVec3().ClosestPointToQuad(transform.position, transform.rotation, new Vec2(transform.localScale.x, transform.localScale.y) / 2);
		Debug.DrawLine(point.position, closestPoint.ToVector3(), Color.blue);
		rot += Time.deltaTime * 2;
	}
}
