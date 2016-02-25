using UnityEngine;
using Reign;

struct BicubicBezierPatchPoint
{
	public Vec3 point, controlPoint1, controlPoint2, surfaceControlPoint;

	public BicubicBezierPatchPoint(Vec3 point, Vec3 controlPoint1, Vec3 controlPoint2, Vec3 surfaceControlPoint)
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
	public Vec3[] vertices, normals;
	public Vec2[] uvs;
	public int[] indices;

	private static void getIdentity(out BicubicBezierPatchPoint point1, out BicubicBezierPatchPoint point2, out BicubicBezierPatchPoint point3, out BicubicBezierPatchPoint point4)
	{
		const float size = 1;

		var cpX = new Vec3(size*.6666f, 0, 0);
		var cpY = new Vec3(0, size*.6666f, 0);

		var pos = new Vec3(-size, -size, 0);
		var surface = new Vec3(pos.x+cpX.x, pos.y+cpY.y, 0);
		point1 = new BicubicBezierPatchPoint(pos, pos + cpX, pos + cpY, surface);

		pos = new Vec3(-size, size, 0);
		surface = new Vec3(pos.x+cpX.x, pos.y-cpY.y, 0);
		point2 = new BicubicBezierPatchPoint(pos, pos - cpY, pos + cpX, surface);

		pos = new Vec3(size, size, 0);
		surface = new Vec3(pos.x-cpX.x, pos.y-cpY.y, 0);
		point3 = new BicubicBezierPatchPoint(pos, pos - cpX, pos - cpY, surface);

		pos = new Vec3(size, -size, 0);
		surface = new Vec3(pos.x-cpX.x, pos.y+cpY.y, 0);
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
		vertices = new Vec3[vertexCount];
		normals = new Vec3[vertexCount];
		uvs = new Vec2[vertexCount];
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
				uvs[i] = new Vec2(u, v);
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
		mesh.vertices = vertices.ToVector3();
		mesh.uv = uvs.ToVector2();
		mesh.normals = normals.ToVector3();
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

	public Vec3 getPoint(float x, float y)
	{
		var p1 = bezierCurve(point1.point, point1.controlPoint2, point2.point, point2.controlPoint1, y);
		var p2 = bezierCurve(point4.point, point4.controlPoint1, point3.point, point3.controlPoint2, y);
		var cp1 = bezierCurve(point1.controlPoint1, point1.surfaceControlPoint, point2.controlPoint2, point2.surfaceControlPoint, y);
		var cp2 = bezierCurve(point4.controlPoint2, point4.surfaceControlPoint, point3.controlPoint1, point3.surfaceControlPoint, y);
		return bezierCurve(p1, cp1, p2, cp2, x);
	}

	public Vec3 getPoint(float x, float y, float delta, out Vec3 normal)
	{
		var p = getPoint(x, y);
		
		var dpxMin = getPoint(x - delta, y) - p;
		var dpxMag = getPoint(x + delta, y) - p;
		var dpyMin = getPoint(x, y - delta) - p;
		var dpyMag = getPoint(x, y + delta) - p;

		var n = dpxMin.Cross(dpyMin);
		var n2 = dpxMag.Cross(dpyMag);
		normal = ((n + n2) * .5f).Normalize();
		
		return p;
	}
}