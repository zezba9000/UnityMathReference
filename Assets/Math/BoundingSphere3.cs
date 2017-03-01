using System.Runtime.InteropServices;

namespace UnityMathReference
{
	[StructLayout(LayoutKind.Sequential)]
	public struct BoundingSphere3
	{
		#region Properties
		public Vec3 center;
		public float radius;
		#endregion

		#region Constructors
		public BoundingSphere3(Vec3 center, float radius)
        {
            this.center = center;
            this.radius = radius;
        }
		#endregion

		#region Methods
		public bool Intersects(BoundingBox3 boundingBox)
        {
			Vec3 clampedLocation;
            if (center.x > boundingBox.max.x)
			{
                clampedLocation.x = boundingBox.max.x;
			}
            else if (center.x < boundingBox.min.x)
			{
                clampedLocation.x = boundingBox.min.x;
			}
            else
			{
                clampedLocation.x = center.x;
			}

            if (center.y > boundingBox.max.y)
			{
                clampedLocation.y = boundingBox.max.y;
			}
            else if (center.y < boundingBox.min.y)
			{
                clampedLocation.y = boundingBox.min.y;
			}
            else
			{
                clampedLocation.y = center.y;
			}

            if (center.z > boundingBox.max.z)
			{
                clampedLocation.z = boundingBox.max.z;
			}
            else if (center.z < boundingBox.min.z)
			{
                clampedLocation.z = boundingBox.min.z;
			}
            else
			{
                clampedLocation.z = center.z;
			}

            return clampedLocation.DistanceSquared(center) <= (radius * radius);
        }
		#endregion
	}
}