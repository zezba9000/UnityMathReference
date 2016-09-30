
namespace Reign
{
	struct RayTraceHit
	{
		public Vec3 normal, position;
		public Color4 color;
	}

	class RayTraceCamera
	{
		public Vec3 position, forward, up, right;
		public float fovH, fovV;
		public Size2 screenSize;

		public RayTraceCamera(Vec3 position, Vec3 forward, Vec3 up, float fovH, float fovV, Size2 screenSize)
		{
			this.position = position;
			this.forward = forward.Normalize();
			right = up.Cross(forward).Normalize();
			this.up = forward.Cross(right);

			this.fovH = fovH;
			this.fovV = fovV;
			this.screenSize = screenSize;
		}

		public Vec3 GetRay(int x, int y)
		{
			float xVec = x / (float)screenSize.width;
			float yVec = y / (float)screenSize.height;
			var vecX = Vec3.Lerp(-right, right, xVec);
			var vecY = Vec3.Lerp(-up, up, yVec);
			return (vecX + vecY + forward).Normalize();
		}
	}

	static class RayTraceEngine
	{
		private static RayTraceSphere[] spheres;

		public static void Init()
		{
			spheres = new RayTraceSphere[1];
			spheres[0] = new RayTraceSphere(new Vec3(), 1);
		}

		public static void Render()
		{
			for (int i = 0; i != spheres.Length; ++i)
			{
				
			}
		}
	}
}