using UnityEngine;
using Reign;

public class BezierSurfaceDemo : MonoBehaviour
{
	public Material surfaceMaterial;

	private BicubicBezierPatch surface;
	private float rot;

	void Start()
	{
		surface = BicubicBezierPatch.CreateIdentity(50, surfaceMaterial);
		Debug.Log("BicubicBezierPatch Vertices: " + surface.vertices.Length);
		Debug.Log("BicubicBezierPatch Indices: " + surface.indices.Length);
	}
	
	void Update()
	{
		if (surface == null) return;
		
		surface.point1.controlPoint1.z = Mathf.Cos(rot);
		surface.point1.controlPoint2.z = Mathf.Sin(rot);
		surface.point2.controlPoint1.z = Mathf.Cos(rot);
		surface.point2.controlPoint2.z = Mathf.Sin(rot);
		surface.point3.controlPoint1.z = Mathf.Cos(rot);
		surface.point3.controlPoint2.z = Mathf.Sin(rot);
		surface.point4.controlPoint1.z = Mathf.Cos(rot);
		surface.point4.controlPoint2.z = Mathf.Sin(rot);
		
		surface.point1.surfaceControlPoint.z = Mathf.Sin(rot);
		surface.point2.surfaceControlPoint.z = Mathf.Sin(rot);
		surface.point3.surfaceControlPoint.z = Mathf.Sin(rot);
		surface.point4.surfaceControlPoint.z = Mathf.Sin(rot);

		rot += 1 * Time.deltaTime;

		surface.UpdateMesh();
		surface.Draw(Vec3.zero);
	}
}
