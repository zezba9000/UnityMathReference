using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace UnityMathReference
{
	[StructLayout(LayoutKind.Sequential)]
	public struct Bound3
	{
		#region Properties
		public Vec3 min, max;

		public static readonly Bound3 zero = new Bound3();

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

		public float GetWidth()
		{
			return max.x - min.x;
		}

		public float GetHeight()
		{
			return max.y - min.y;
		}

		public float GetDepth()
		{
			return max.z - min.z;
		}

		public Vec3 GetCenter()
		{
			return (min + max) * .5f;
		}
		#endregion

		#region Constructors
		public Bound3(Vec3 min, Vec3 max)
        {
            this.min = min;
            this.max = max;
        }

		public static Bound3 FromPoints(IList<Vec3> points)
        {
            Bound3 boundingBox;
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
		public bool Intersects(Bound3 boundingBox)
        {
            return
				!(boundingBox.min.x > max.x || boundingBox.min.y > max.y || boundingBox.min.z > max.z ||
				  min.x > boundingBox.max.x || min.y > boundingBox.max.y || min.z > boundingBox.max.z);
        }

		public bool Intersects(Sphere3 boundingSphere)
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

		public Bound3 Merge(Bound2 boundingBox)
		{
			Bound3 result = this;
			if (result.min.x < boundingBox.min.x) result.min.x = boundingBox.min.x;
			if (result.max.x > boundingBox.max.x) result.max.x = boundingBox.max.x;

			if (result.min.y < boundingBox.min.y) result.min.y = boundingBox.min.y;
			if (result.max.y > boundingBox.max.y) result.max.y = boundingBox.max.y;

			return result;
		}

		public Bound3 Merge(Bound3 boundingBox)
        {
			Bound3 result = this;
            if (result.min.x < boundingBox.min.x) result.min.x = boundingBox.min.x;
            if (result.max.x > boundingBox.max.x) result.max.x = boundingBox.max.x;

            if (result.min.y < boundingBox.min.y) result.min.y = boundingBox.min.y;
            if (result.max.y > boundingBox.max.y) result.max.y = boundingBox.max.y;

            if (result.min.z < boundingBox.min.z) result.min.z = boundingBox.min.z;
            if (result.max.z > boundingBox.max.z) result.max.z = boundingBox.max.z;

			return result;
        }

		public Bound3 Merge(Vec2 vector)
		{
			Bound3 result = this;
			if (result.min.x < vector.x) result.min.x = vector.x;
			if (result.max.x > vector.x) result.max.x = vector.x;

			if (result.min.y < vector.y) result.min.y = vector.y;
			if (result.max.y > vector.y) result.max.y = vector.y;

			return result;
		}

		public Bound3 Merge(Vec3 vector)
		{
			Bound3 result = this;
			if (result.min.x < vector.x) result.min.x = vector.x;
			if (result.max.x > vector.x) result.max.x = vector.x;

			if (result.min.y < vector.y) result.min.y = vector.y;
			if (result.max.y > vector.y) result.max.y = vector.y;

			if (result.min.z < vector.z) result.min.z = vector.z;
			if (result.max.z > vector.z) result.max.z = vector.z;

			return result;
		}
		#endregion
	}
}