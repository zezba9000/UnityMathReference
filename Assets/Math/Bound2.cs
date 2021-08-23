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

		public Bound2(IList<Vec2> points)
		{
			max = min = points[0];
			foreach (var point in points)
			{
				if (point.x < min.x) min.x = point.x;
				else if (point.x > max.x) max.x = point.x;

				if (point.y < min.y) min.y = point.y;
				else if (point.y > max.y) max.y = point.y;
			}
		}

		public Bound2(Line2 line)
		{
			max = min = line.point1;
			MergeSelf(line.point2);
		}

		public Bound2(Triangle2 triangle)
		{
			max = min = triangle.point1;
			MergeSelf(triangle.point2);
			MergeSelf(triangle.point3);
		}
		#endregion

		#region Operators
		public static Bound2 operator +(Bound2 p1, Vec2 p2)
		{
			return new Bound2(p1.min + p2, p1.max + p2);
		}

		public static Bound2 operator -(Bound2 p1, Vec2 p2)
		{
			return new Bound2(p1.min - p2, p1.max - p2);
		}

		public static Bound2 operator *(Bound2 p1, Vec2 p2)
		{
			return new Bound2(p1.min * p2, p1.max * p2);
		}

		public static Bound2 operator /(Bound2 p1, Vec2 p2)
		{
			return new Bound2(p1.min / p2, p1.max / p2);
		}
		#endregion

		#region Methods
		public bool Intersects(Bound2 boundingBox)
        {
            return
			!(
				boundingBox.min.x > max.x || boundingBox.min.y > max.y ||
				min.x > boundingBox.max.x || min.y > boundingBox.max.y
			);
        }

		public bool Intersects(Sphere2 boundingSphere)
        {
			Vec2 clampedLocation;
            if (boundingSphere.center.x > max.x) clampedLocation.x = max.x;
            else if (boundingSphere.center.x < min.x) clampedLocation.x = min.x;
            else clampedLocation.x = boundingSphere.center.x;

            if (boundingSphere.center.y > max.y) clampedLocation.y = max.y;
            else if (boundingSphere.center.y < min.y) clampedLocation.y = min.y;
            else clampedLocation.y = boundingSphere.center.y;

            return clampedLocation.DistanceSquared(boundingSphere.center) <= (boundingSphere.radius * boundingSphere.radius);
        }

		public Bound2 Merge(Bound2 boundingBox)
        {
			var result = this;
			result.MergeSelf(boundingBox);
			return result;
		}

		public Bound2 Merge(Vec2 vector)
		{
			var result = this;
			result.MergeSelf(vector);
			return result;
		}

		public void MergeSelf(Bound2 boundingBox)
		{
			if (boundingBox.min.x < min.x) min.x = boundingBox.min.x;
			if (boundingBox.max.x > max.x) max.x = boundingBox.max.x;

			if (boundingBox.min.y < min.y) min.y = boundingBox.min.y;
			if (boundingBox.max.y > max.y) max.y = boundingBox.max.y;
		}

		public void MergeSelf(Vec2 vector)
		{
			if (vector.x < min.x) min.x = vector.x;
			if (vector.x > max.x) max.x = vector.x;

			if (vector.y < min.y) min.y = vector.y;
			if (vector.y > max.y) max.y = vector.y;
		}
		#endregion
	}
}