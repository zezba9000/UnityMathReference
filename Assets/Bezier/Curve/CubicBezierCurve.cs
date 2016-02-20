using UnityEngine;
using System.Collections;

struct CubicBezierCurvePoint
{
	public Vector3 point, controlPoint;

	public CubicBezierCurvePoint(Vector3 point, Vector3 controlPoint)
	{
		this.point = point;
		this.controlPoint = controlPoint;
	}

	public static float CalculateControlPointForCircle(float numberOfPointsOnCircle)
	{
		const float percent = (4f / 3f);
		return percent * Mathf.Tan(Mathf.PI / (numberOfPointsOnCircle * 2f));
	}
}

class CubicBezierCurve
{
	public CubicBezierCurvePoint point1, point2;
	public Mesh mesh;
	public Material material;

	public int density;
	public Vector3[] vertices, normals;
	public Vector2[] uvs;
	public int[] indices;

	private static void getIdentity(out CubicBezierCurvePoint point1, out CubicBezierCurvePoint point2)
	{
		const float size = 1;
		var cpX = new Vector3(size*.6666f, 0, 0);

		var pos = new Vector3(-size, 0, 0);
		point1 = new CubicBezierCurvePoint(pos, pos + cpX);

		pos = new Vector3(size, 0, 0);
		point2 = new CubicBezierCurvePoint(pos, pos - cpX);
	}

	public void SetIdentity()
	{
		getIdentity(out point1, out point2);
	}

	public static CubicBezierCurve CreateIdentity(Vector3 cameraPosition, int density, Material material)
	{
		CubicBezierCurvePoint point1, point2;
		getIdentity(out point1, out point2);
		return new CubicBezierCurve(point1, point2, cameraPosition, density, material);
	}

	public CubicBezierCurve(CubicBezierCurvePoint point1, CubicBezierCurvePoint point2, Vector3 cameraPosition, int density, Material material)
	{
		this.point1 = point1;
		this.point2 = point2;

		this.density = density;
		this.material = material;

		// create mesh
		int vertexCount = density;
		int indexCount = density;
		vertices = new Vector3[vertexCount];
		normals = new Vector3[vertexCount];
		uvs = new Vector2[vertexCount];
		indices = new int[indexCount];

		// compute mesh verts and indices
		float delta = 1.0f / density;
		for (int i = 0; i != density; ++i)
		{
			float u = i / (float)(density - 1);
			vertices[i] = getPoint(u, cameraPosition, delta, out normals[i]);
			uvs[i] = new Vector2(u, .5f);
			indices[i] = i;
		}

		// set buffers
		mesh = new Mesh();
		mesh.MarkDynamic();
		mesh.vertices = vertices;
		mesh.uv = uvs;
		mesh.normals = normals;
		mesh.SetIndices(indices, MeshTopology.LineStrip, 0);
	}

	public void UpdateMesh(Vector3 cameraPosition)
	{
		// compute mesh verts
		float delta = 1.0f / density;
		for (int i = 0; i != density; ++i)
		{
			float u = i / (float)(density - 1);
			vertices[i] = getPoint(u, cameraPosition, delta, out normals[i]);
		}

		// update buffers
		mesh.normals = normals;
		mesh.vertices = vertices;
	}

	public void Draw(Vector3 offset)
	{
		Graphics.DrawMesh(mesh, offset, Quaternion.identity, material, 0);
	}

	private Vector3 lerp(Vector3 p1, Vector3 p2, float value)
	{
		return p1 + ((p2 - p1) * value);
	}

	private Vector3 bezierCurve(Vector3 p1, Vector3 cp1, Vector3 p2, Vector3 cp2, float value)
	{
		var a = lerp(p1, cp1, value);
		var b = lerp(cp2, p2, value);
		var c = lerp(cp1, cp2, value);

		a = lerp(a, c, value);
		b = lerp(c, b, value);
		return lerp(a, b, value);
	}

	private Vector3 cross(Vector3 vector1, Vector3 vector2)
	{
		return new Vector3
		(
			((vector1.y*vector2.z) - (vector1.z*vector2.y)),
			((vector1.z*vector2.x) - (vector1.x*vector2.z)),
			((vector1.x*vector2.y) - (vector1.y*vector2.x))
		);
	}

	public Vector3 getPoint(float value)
	{
		return bezierCurve(point1.point, point1.controlPoint, point2.point, point2.controlPoint, value);
	}

	public Vector3 getPoint(float value, Vector3 cameraPosition, float delta, out Vector3 normal)
	{
		var p = getPoint(value);
		var z = cameraPosition - p;
		z.x = Mathf.Abs(z.x);
		z.y = Mathf.Abs(z.y);
		z.z = Mathf.Abs(z.z);
		
		var dMin = getPoint(value - delta) - p;
		var dMag = getPoint(value + delta) - p;

		var n = cross(dMin, z);
		var n2 = -cross(dMag, z);
		normal = ((n + n2) * .5f);
		normal.Normalize();
		
		return p;
	}
}