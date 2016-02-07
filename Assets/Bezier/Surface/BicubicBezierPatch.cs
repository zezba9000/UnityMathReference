using System.Runtime.InteropServices;
using UnityEngine;

struct BicubicBezierPatchPoint
{
	public Vector3 point, controlPoint1, controlPoint2, surfaceControlPoint;

	public BicubicBezierPatchPoint(Vector3 point, Vector3 controlPoint1, Vector3 controlPoint2, Vector3 surfaceControlPoint)
	{
		this.point = point;
		this.controlPoint1 = controlPoint1;
		this.controlPoint2 = controlPoint2;
		this.surfaceControlPoint = surfaceControlPoint;
	}
}

class BicubicBezierPatch
{
	public BicubicBezierPatchPoint point1, point2, point3, point4;
	public Mesh mesh;
	public Material material;

	public int density;
	public Vector3[] vertices, normals;
	public Vector2[] uvs;
	public int[] indices;

	private static void getIdentity(out BicubicBezierPatchPoint point1, out BicubicBezierPatchPoint point2, out BicubicBezierPatchPoint point3, out BicubicBezierPatchPoint point4)
	{
		const float size = 1;

		var cpX = new Vector3(size*.6666f, 0, 0);
		var cpY = new Vector3(0, size*.6666f, 0);

		var pos = new Vector3(-size, -size, 0);
		var surface = new Vector3(pos.x+cpX.x, pos.y+cpY.y, 0);
		point1 = new BicubicBezierPatchPoint(pos, pos + cpX, pos + cpY, surface);

		pos = new Vector3(-size, size, 0);
		surface = new Vector3(pos.x+cpX.x, pos.y-cpY.y, 0);
		point2 = new BicubicBezierPatchPoint(pos, pos - cpY, pos + cpX, surface);

		pos = new Vector3(size, size, 0);
		surface = new Vector3(pos.x-cpX.x, pos.y-cpY.y, 0);
		point3 = new BicubicBezierPatchPoint(pos, pos - cpX, pos - cpY, surface);

		pos = new Vector3(size, -size, 0);
		surface = new Vector3(pos.x-cpX.x, pos.y+cpY.y, 0);
		point4 = new BicubicBezierPatchPoint(pos, pos + cpY, pos - cpX, surface);
	}

	public void SetIdentity()
	{
		getIdentity(out point1, out point2, out point3, out point4);
	}

	public static BicubicBezierPatch CreateIdentity(int density, Material material)
	{
		BicubicBezierPatchPoint point1, point2, point3, point4;
		getIdentity(out point1, out point2, out point3, out point4);
		return new BicubicBezierPatch(point1, point2, point3, point4, density, material);
	}

	public BicubicBezierPatch(BicubicBezierPatchPoint point1, BicubicBezierPatchPoint point2, BicubicBezierPatchPoint point3, BicubicBezierPatchPoint point4, int density, Material material)
	{
		this.point1 = point1;
		this.point2 = point2;
		this.point3 = point3;
		this.point4 = point4;

		this.density = density;
		this.material = material;

		// create mesh
		int vertexCount = density * density;
		int indexCount = ((density-1) * (density-1)) * 6;
		vertices = new Vector3[vertexCount];
		normals = new Vector3[vertexCount];
		uvs = new Vector2[vertexCount];
		indices = new int[indexCount];

		// compute mesh verts
		float delta = 1.0f / density;
		for (int y = 0; y != density; ++y)
		{
			float v = y / (float)(density - 1);
			for (int x = 0; x != density; ++x)
			{
				float u = x / (float)(density - 1);
				int i = x + (y * density);
				
				vertices[i] = getPoint(u, v, delta, out normals[i]);
				uvs[i] = new Vector2(u, v);
			}
		}

		// comput indices
		int indexOffset = 0;
		for (int y = 0; y != density-1; ++y)
		{
			for (int x = 0; x != density-1; ++x)
			{
				int i = x + (y * density);
				int ix = i + 1;
				int iy = i + density;
				int ixy = iy + 1;

				indices[indexOffset] = i;
				indices[indexOffset+1] = iy;
				indices[indexOffset+2] = ixy;
				
				indices[indexOffset+3] = i;
				indices[indexOffset+4] = ixy;
				indices[indexOffset+5] = ix;

				indexOffset += 6;
			}
		}

		// set buffers
		mesh = new Mesh();
		mesh.MarkDynamic();
		mesh.vertices = vertices;
		mesh.uv = uvs;
		mesh.normals = normals;
		mesh.SetIndices(indices, MeshTopology.Triangles, 0);
	}

	public void UpdateMesh()
	{
		// compute mesh verts
		float delta = 1.0f / density;
		for (int y = 0; y != density; ++y)
		{
			float v = y / (float)(density - 1);
			for (int x = 0; x != density; ++x)
			{
				float u = x / (float)(density - 1);
				int i = x + (y * density);
				
				vertices[i] = getPoint(u, v, delta, out normals[i]);
			}
		}

		// update buffers
		mesh.normals = normals;
		mesh.vertices = vertices;
	}

	public void Draw(Vector3 offset)
	{
		Graphics.DrawMesh(mesh, offset, Quaternion.identity, material, 10);
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

	public Vector3 getPoint(float x, float y)
	{
		var p1 = bezierCurve(point1.point, point1.controlPoint2, point2.point, point2.controlPoint1, y);
		var p2 = bezierCurve(point4.point, point4.controlPoint1, point3.point, point3.controlPoint2, y);
		var cp1 = bezierCurve(point1.controlPoint1, point1.surfaceControlPoint, point2.controlPoint2, point2.surfaceControlPoint, y);
		var cp2 = bezierCurve(point4.controlPoint2, point4.surfaceControlPoint, point3.controlPoint1, point3.surfaceControlPoint, y);
		return bezierCurve(p1, cp1, p2, cp2, x);
	}

	public Vector3 getPoint(float x, float y, float delta, out Vector3 normal)
	{
		var p = getPoint(x, y);
		
		var dpxMin = getPoint(x - delta, y) - p;
		var dpxMag = getPoint(x + delta, y) - p;
		var dpyMin = getPoint(x, y - delta) - p;
		var dpyMag = getPoint(x, y + delta) - p;

		var n = cross(dpxMin, dpyMin);
		var n2 = cross(dpxMag, dpyMag);
		normal = ((n + n2) * .5f);
		normal.Normalize();
		
		return p;
	}
}