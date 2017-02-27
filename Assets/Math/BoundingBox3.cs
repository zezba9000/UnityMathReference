using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Reign
{
	[StructLayout(LayoutKind.Sequential)]
	public struct BoundingBox3
	{
		#region Properties
		public Vec3 min, max;

		public float left
		{
			get {return min.x;}
			set {min.x = value;}
		}

		public float bottom
		{
			get {return min.y;}
			set {min.y = value;}
		}

		public float back
		{
			get {return min.z;}
			set {min.z = value;}
		}

		public float right
		{
			get {return max.x;}
			set {max.x = value;	}
		}

		public float top
		{
			get {return max.y;}
			set {max.y = value;}
		}

		public float front
		{
			get {return max.z;}
			set {max.z = value;}
		}
		#endregion

		#region Constructors
		public BoundingBox3(Vec3 min, Vec3 max)
        {
            this.min = min;
            this.max = max;
        }

		public static BoundingBox3 FromPoints(IList<Vec3> points)
        {
            BoundingBox3 boundingBox;
            boundingBox.min = points[0];
            boundingBox.max = boundingBox.min;
			foreach (var point in points)
            {
                if (point.x < boundingBox.min.x)
				{
                    boundingBox.min.x = point.x;
				}
                else if (point.x > boundingBox.max.x)
				{
                    boundingBox.max.x = point.x;
				}

                if (point.y < boundingBox.min.y)
				{
                    boundingBox.min.y = point.y;
				}
                else if (point.y > boundingBox.max.y)
				{
                    boundingBox.max.y = point.y;
				}

                if (point.z < boundingBox.min.z)
				{
                    boundingBox.min.z = point.z;
				}
                else if (point.z > boundingBox.max.z)
				{
                    boundingBox.max.z = point.z;
				}
            }

            return boundingBox;
        }
		#endregion

		#region Methods
		public bool Intersects(BoundingBox3 boundingBox)
        {
            return
				!(boundingBox.min.x > max.x || boundingBox.min.y > max.y || boundingBox.min.z > max.z ||
				  min.x > boundingBox.max.x || min.y > boundingBox.max.y || min.z > boundingBox.max.z);
        }

		public bool Intersects(BoundingSphere3 boundingSphere)
        {
		   Vec3 clampedLocation;
            if (boundingSphere.center.x > max.x)
			{
                clampedLocation.x = max.x;
			}
            else if (boundingSphere.center.x < min.x)
			{
                clampedLocation.x = min.x;
			}
            else
			{
                clampedLocation.x = boundingSphere.center.x;
			}

            if (boundingSphere.center.y > max.y)
			{
                clampedLocation.y = max.y;
			}
            else if (boundingSphere.center.y < min.y)
			{
                clampedLocation.y = min.y;
			}
            else
			{
                clampedLocation.y = boundingSphere.center.y;
			}

            if (boundingSphere.center.z > max.z)
			{
                clampedLocation.z = max.z;
			}
            else if (boundingSphere.center.z < min.z)
			{
                clampedLocation.z = min.z;
			}
            else
			{
                clampedLocation.z = boundingSphere.center.z;
			}

            return clampedLocation.DistanceSquared(boundingSphere.center) <= (boundingSphere.radius * boundingSphere.radius);
        }

		public BoundingBox3 Merge(BoundingBox3 boundingBox2)
        {
			BoundingBox3 result;
            if (min.x < boundingBox2.min.x) result.min.x = min.x;
            else result.min.x = boundingBox2.min.x;

            if (min.y < boundingBox2.min.y) result.min.y = min.y;
            else result.min.y = boundingBox2.min.y;

            if (min.z < boundingBox2.min.z) result.min.z = min.z;
            else result.min.z = boundingBox2.min.z;

            if (max.x > boundingBox2.max.x) result.max.x = max.x;
            else result.max.x = boundingBox2.max.x;

            if (max.y > boundingBox2.max.y) result.max.y = max.y;
            else result.max.y = boundingBox2.max.y;

            if (max.z > boundingBox2.max.z) result.max.z = max.z;
            else result.max.z = boundingBox2.max.z;

			return result;
        }
		#endregion
	}
}