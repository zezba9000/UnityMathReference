using UnityEngine;
using System.Collections;

public class BezierSurfaceDemo : MonoBehaviour
{
	public Material surfaceMaterial;

	private BezierSurface surface;
	private float rot;

	void Start()
	{
		surface = BezierSurface.CreateIdentity(50, surfaceMaterial);
		Debug.Log("DisplacmentMesh Vertices: " + surface.vertices.Length);
		Debug.Log("DisplacmentMesh Indices: " + surface.indices.Length);
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
		
		surface.surface1.z = Mathf.Sin(rot);
		surface.surface2.z = Mathf.Sin(rot);
		surface.surface3.z = Mathf.Sin(rot);
		surface.surface4.z = Mathf.Sin(rot);

		rot += 1 * Time.deltaTime;

		surface.UpdateMesh();
		surface.Draw(Vector3.zero);
	}
}
