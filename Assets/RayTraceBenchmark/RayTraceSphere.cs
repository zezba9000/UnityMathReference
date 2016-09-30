

namespace Reign
{
	class RayTraceSphere
	{
		public Vec3 position;
		public float radius;

		public RayTraceSphere(Vec3 position, float radius)
		{
			this.position = position;
			this.radius = radius;
		}

		public bool RayIntersect(Ray3 ray, out RayTraceHit hit)
		{
			Vec3 p1, p2, n1, n2;
			if (ray.IntersectRaySphere(Vec3.zero, 1, out p1, out p2, out n1, out n2))
			{
				hit.normal = n1;
				hit.position = p1;
				hit.color = new Color4(1, 1, 1, 1);
				return true;
			}

			hit = new RayTraceHit();
			return false;
		}
	}
}