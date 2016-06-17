using UnityEngine;
using Reign;

public class BezierCurveDemo : MonoBehaviour
{
	public Material curveMaterial;
	internal new Transform transform;

	private CubicBezierCurve animatingCurve, circleCurve1, circleCurve2, circleCurve3, circleCurve4;
	private float rot;

	void Start()
	{
		this.transform = GetComponent<Transform>();	

		// animation
		animatingCurve = CubicBezierCurve.CreateIdentity(transform.position, 50, curveMaterial);
		Debug.Log("CubicBezierCurve Vertices: " + animatingCurve.vertices.Length);
		Debug.Log("CubicBezierCurve Indices: " + animatingCurve.indices.Length);

		// circle
		const float size = 1;
		float controlPointDis = CubicBezierCurvePoint.CalculateControlPointForCircle(4);

		var point1 = new CubicBezierCurvePoint(new Vec3(-size, 0, 0), new Vec3(-size, controlPointDis, 0));
		var point2 = new CubicBezierCurvePoint(new Vec3(0, size, 0), new Vec3(-controlPointDis, size, 0));
		circleCurve1 = new CubicBezierCurve(point1, point2, transform.position, 50, curveMaterial);

		point1 = new CubicBezierCurvePoint(new Vec3(0, size, 0), new Vec3(controlPointDis, size, 0));
		point2 = new CubicBezierCurvePoint(new Vec3(size, 0, 0), new Vec3(size, controlPointDis, 0));
		circleCurve2 = new CubicBezierCurve(point1, point2, transform.position, 50, curveMaterial);

		point1 = new CubicBezierCurvePoint(new Vec3(size, 0, 0), new Vec3(size, -controlPointDis, 0));
		point2 = new CubicBezierCurvePoint(new Vec3(0, -size, 0), new Vec3(controlPointDis, -size, 0));
		circleCurve3 = new CubicBezierCurve(point1, point2, transform.position, 50, curveMaterial);

		point1 = new CubicBezierCurvePoint(new Vec3(0, -size, 0), new Vec3(-controlPointDis, -size, 0));
		point2 = new CubicBezierCurvePoint(new Vec3(-size, 0, 0), new Vec3(-size, -controlPointDis, 0));
		circleCurve4 = new CubicBezierCurve(point1, point2, transform.position, 50, curveMaterial);
	}
	
	void Update()
	{
		if (animatingCurve == null) return;

		// animation
		animatingCurve.point1.controlPoint.y = Mathf.Cos(rot);
		animatingCurve.point2.controlPoint.y = Mathf.Sin(rot);
		rot += 1 * Time.deltaTime;

		animatingCurve.UpdateMesh(transform.position);
		animatingCurve.Draw(Vec3.zero);

		// circle
		circleCurve1.Draw(Vec3.zero);
		circleCurve2.Draw(Vec3.zero);
		circleCurve3.Draw(Vec3.zero);
		circleCurve4.Draw(Vec3.zero);
	}
}
