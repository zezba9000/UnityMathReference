using System.Runtime.InteropServices;

namespace UnityMathReference
{
	[StructLayout(LayoutKind.Sequential)]
	public struct BoundingSphere2
	{
		#region Properties
		public Vec2 center;
		public float radius;
		#endregion

		#region Constructors
		public BoundingSphere2(Vec2 center, float radius)
        {
            this.center = center;
            this.radius = radius;
        }
		#endregion

		#region Methods
		public bool Intersects(BoundingBox2 boundingBox)
        {
		   Vec2 clampedLocation;
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

            return clampedLocation.DistanceSquared(center) <= (radius * radius);
        }
		#endregion
	}
}