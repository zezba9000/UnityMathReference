using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace UnityMathReference
{
	[StructLayout(LayoutKind.Sequential)]
	public struct Bound2
	{
		#region Properties
		public Vec2 min, max;

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

		public float GetWidth()
		{
			return max.x - min.x;
		}

		public float GetHeight()
		{
			return max.y - min.y;
		}

		public Vec2 GetCenter()
		{
			return (min + max) * .5f;
		}
		#endregion

		#region Constructors
		public Bound2(Vec2 min, Vec2 max)
        {
            this.min = min;
            this.max = max;
        }

		public static Bound2 FromPoints(IList<Vec2> points)
        {
            Bound2 boundingBox;
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
            }

            return boundingBox;
        }
		#endregion

		#region Methods
		public bool Intersects(Bound2 boundingBox)
        {
            return
				!(boundingBox.min.x > max.x || boundingBox.min.y > max.y ||
				  min.x > boundingBox.max.x || min.y > boundingBox.max.y);
        }

		public bool Intersects(Sphere2 boundingSphere)
        {
		   Vec2 clampedLocation;
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

            return clampedLocation.DistanceSquared(boundingSphere.center) <= (boundingSphere.radius * boundingSphere.radius);
        }

		public Bound2 Merge(Bound2 boundingBox)
        {
			Bound2 result = this;
			if (result.min.x < boundingBox.min.x) result.min.x = boundingBox.min.x;
			if (result.max.x > boundingBox.max.x) result.max.x = boundingBox.max.x;

			if (result.min.y < boundingBox.min.y) result.min.y = boundingBox.min.y;
			if (result.max.y > boundingBox.max.y) result.max.y = boundingBox.max.y;

			return result;
        }

		public Bound2 Merge(Vec2 vector)
		{
			Bound2 result = this;
			if (result.min.x < vector.x) result.min.x = vector.x;
			if (result.max.x > vector.x) result.max.x = vector.x;

			if (result.min.y < vector.y) result.min.y = vector.y;
			if (result.max.y > vector.y) result.max.y = vector.y;

			return result;
		}
		#endregion
	}
}