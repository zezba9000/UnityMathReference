using UnityEngine;
using System.Collections;

public class BezierCurveDemo : MonoBehaviour
{
	public Material curveMaterial;
	internal new Transform transform;

	private CubicBezierCurve curve;
	private float rot;

	void Start()
	{
		this.transform = GetComponent<Transform>();	

		curve = CubicBezierCurve.CreateIdentity(transform.position, 50, curveMaterial);
		Debug.Log("CubicBezierCurve Vertices: " + curve.vertices.Length);
		Debug.Log("CubicBezierCurve Indices: " + curve.indices.Length);
	}
	
	void Update()
	{
		if (curve == null) return;

		curve.point1.controlPoint.y = Mathf.Cos(rot);
		curve.point2.controlPoint.y = Mathf.Sin(rot);
		rot += 1 * Time.deltaTime;

		curve.UpdateMesh(transform.position);
		curve.Draw(Vector3.zero);
	}
}
