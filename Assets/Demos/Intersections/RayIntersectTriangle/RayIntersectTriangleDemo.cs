using UnityEngine;
using UnityMathReference;

public class RayIntersectTriangleDemo : MonoBehaviour
{
	public Material triangleMaterial;
	public Transform rayTransform;
	private Triangle3 triangle;
	private Mesh triangleMesh;

	void Start()
	{
		// triangle verticies
		triangle = new Triangle3
		(
			new Vec3(0, 0, .33f),
			new Vec3(0, 1.5f, 0),
			new Vec3(1, 0, -.12f)
		);

		// create unity mesh
		triangleMesh = new Mesh();
		triangleMesh.vertices = new Vector3[3]
		{
			triangle.point1.ToVector3(),
			triangle.point2.ToVector3(),
			triangle.point3.ToVector3()
		};
		triangleMesh.SetIndices(new int[3] { 0, 1, 2 }, MeshTopology.Triangles, 0);
	}

	void Update()
	{
		var ray = new Ray3(rayTransform.position.ToVec3(), rayTransform.forward.ToVec3());
		Vec3 collisionPoint;
		if (ray.IntersectTriangle(triangle, out collisionPoint))
		{
			Debug.DrawLine(ray.origin.ToVector3(), (ray.origin + (ray.direction * 5)).ToVector3(), Color.red, 0, false);
			Debug.DrawLine(ray.origin.ToVector3(), collisionPoint.ToVector3(), Color.green, 0, false);
		}

		Graphics.DrawMesh(triangleMesh, Matrix4x4.identity, triangleMaterial, 0);
	}
}
