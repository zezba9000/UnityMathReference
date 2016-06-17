using UnityEngine;
using Reign;

struct CubicBezierCurvePoint
{
	public Vec3 point, controlPoint;

	public CubicBezierCurvePoint(Vec3 point, Vec3 controlPoint)
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
	public Vec3[] vertices, normals;
	public Vec2[] uvs;
	public int[] indices;

	private static void getIdentity(out CubicBezierCurvePoint point1, out CubicBezierCurvePoint point2)
	{
		const float size = 1;
		var cpX = new Vec3(size*.6666f, 0, 0);

		var pos = new Vec3(-size, 0, 0);
		point1 = new CubicBezierCurvePoint(pos, pos + cpX);

		pos = new Vec3(size, 0, 0);
		point2 = new CubicBezierCurvePoint(pos, pos - cpX);
	}

	public void SetIdentity()
	{
		getIdentity(out point1, out point2);
	}

	public static CubicBezierCurve CreateIdentity(Vec3 cameraPosition, int density, Material material)
	{
		CubicBezierCurvePoint point1, point2;
		getIdentity(out point1, out point2);
		return new CubicBezierCurve(point1, point2, cameraPosition, density, material);
	}

	public CubicBezierCurve(CubicBezierCurvePoint point1, CubicBezierCurvePoint point2, Vec3 cameraPosition, int density, Material material)
	{
		this.point1 = point1;
		this.point2 = point2;

		this.density = density;
		this.material = material;

		// create mesh
		int vertexCount = density;
		int indexCount = density;
		vertices = new Vec3[vertexCount];
		normals = new Vec3[vertexCount];
		uvs = new Vec2[vertexCount];
		indices = new int[indexCount];

		// compute mesh verts and indices
		float delta = 1.0f / density;
		for (int i = 0; i != density; ++i)
		{
			float u = i / (float)(density - 1);
			vertices[i] = getPoint(u, cameraPosition, delta, out normals[i]);
			uvs[i] = new Vec2(u, .5f);
			indices[i] = i;
		}

		// set buffers
		mesh = new Mesh();
		mesh.MarkDynamic();
		mesh.vertices = vertices.ToVector3();
		mesh.uv = uvs.ToVector2();
		mesh.normals = normals.ToVector3();
		mesh.SetIndices(indices, MeshTopology.LineStrip, 0);
	}

	public void UpdateMesh(Vec3 cameraPosition)
	{
		// compute mesh verts
		float delta = 1.0f / density;
		for (int i = 0; i != density; ++i)
		{
			float u = i / (float)(density - 1);
			vertices[i] = getPoint(u, cameraPosition, delta, out normals[i]);
		}

		// update buffers
		mesh.normals = normals.ToVector3();
		mesh.vertices = vertices.ToVector3();
	}

	public void Draw(Vec3 offset)
	{
		Graphics.DrawMesh(mesh, offset.ToVector3(), Quaternion.identity, material, 0);
	}

	private Vec3 bezierCurve(Vec3 p1, Vec3 cp1, Vec3 p2, Vec3 cp2, float value)
	{
		var a = Vec3.Lerp(p1, cp1, value);
		var b = Vec3.Lerp(cp2, p2, value);
		var c = Vec3.Lerp(cp1, cp2, value);

		a = Vec3.Lerp(a, c, value);
		b = Vec3.Lerp(c, b, value);
		return Vec3.Lerp(a, b, value);
	}

	public Vec3 getPoint(float value)
	{
		return bezierCurve(point1.point, point1.controlPoint, point2.point, point2.controlPoint, value);
	}

	public Vec3 getPoint(float value, Vec3 cameraPosition, float delta, out Vec3 normal)
	{
		var p = getPoint(value);
		var z = cameraPosition - p;
		z.x = Mathf.Abs(z.x);
		z.y = Mathf.Abs(z.y);
		z.z = Mathf.Abs(z.z);
		
		var dMin = getPoint(value - delta) - p;
		var dMag = getPoint(value + delta) - p;

		var n = dMin.Cross(z);
		var n2 = -dMag.Cross(z);
		normal = ((n + n2) * .5f).Normalize();
		
		return p;
	}
}