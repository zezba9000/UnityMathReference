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

		public Bound3(IList<Vec3> points)
        {
            max = min = points[0];
			foreach (var point in points)
            {
                if (point.x < min.x) min.x = point.x;
                else if (point.x > max.x) max.x = point.x;

                if (point.y < min.y) min.y = point.y;
                else if (point.y > max.y) max.y = point.y;

                if (point.z < min.z) min.z = point.z;
                else if (point.z > max.z) max.z = point.z;
            }
        }

		public Bound3(Line3 line)
		{
			max = min = line.point1;
			MergeSelf(line.point2);
		}

		public Bound3(Triangle3 triangle)
		{
			max = min = triangle.point1;
			MergeSelf(triangle.point2);
			MergeSelf(triangle.point3);
		}
		#endregion

		#region Operators
		public static Bound3 operator+(Bound3 p1, Vec3 p2)
		{
			return new Bound3(p1.min + p2, p1.max + p2);
		}

		public static Bound3 operator -(Bound3 p1, Vec3 p2)
		{
			return new Bound3(p1.min - p2, p1.max - p2);
		}

		public static Bound3 operator *(Bound3 p1, Vec3 p2)
		{
			return new Bound3(p1.min * p2, p1.max * p2);
		}

		public static Bound3 operator /(Bound3 p1, Vec3 p2)
		{
			return new Bound3(p1.min / p2, p1.max / p2);
		}
		#endregion

		#region Methods
		public bool Intersects(Bound3 boundingBox)
        {
            return
			!(
				boundingBox.min.x > max.x || boundingBox.min.y > max.y || boundingBox.min.z > max.z ||
				min.x > boundingBox.max.x || min.y > boundingBox.max.y || min.z > boundingBox.max.z
			);
        }

		public bool Intersects(Sphere3 boundingSphere)
        {
			Vec3 clampedLocation;
			if (boundingSphere.center.x > max.x) clampedLocation.x = max.x;
			else if (boundingSphere.center.x < min.x) clampedLocation.x = min.x;
			else clampedLocation.x = boundingSphere.center.x;

			if (boundingSphere.center.y > max.y) clampedLocation.y = max.y;
			else if (boundingSphere.center.y < min.y) clampedLocation.y = min.y;
			else clampedLocation.y = boundingSphere.center.y;

			if (boundingSphere.center.z > max.z) clampedLocation.z = max.z;
			else if (boundingSphere.center.z < min.z) clampedLocation.z = min.z;
			else clampedLocation.z = boundingSphere.center.z;

			return clampedLocation.DistanceSquared(boundingSphere.center) <= (boundingSphere.radius * boundingSphere.radius);
        }

		public Bound3 Merge(Bound2 boundingBox)
		{
			var result = this;
			result.MergeSelf(boundingBox);
			return result;
		}

		public Bound3 Merge(Bound3 boundingBox)
        {
			var result = this;
			result.MergeSelf(boundingBox);
			return result;
		}

		public Bound3 Merge(Vec2 vector)
		{
			var result = this;
			result.MergeSelf(vector);
			return result;
		}

		public Bound3 Merge(Vec3 vector)
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

		public void MergeSelf(Bound3 boundingBox)
		{
			if (boundingBox.min.x < min.x) min.x = boundingBox.min.x;
			if (boundingBox.max.x > max.x) max.x = boundingBox.max.x;

			if (boundingBox.min.y < min.y) min.y = boundingBox.min.y;
			if (boundingBox.max.y > max.y) max.y = boundingBox.max.y;

			if (boundingBox.min.z < min.z) min.z = boundingBox.min.z;
			if (boundingBox.max.z > max.z) max.z = boundingBox.max.z;
		}

		public void MergeSelf(Vec2 vector)
		{
			if (vector.x < min.x) min.x = vector.x;
			if (vector.x > max.x) max.x = vector.x;

			if (vector.y < min.y) min.y = vector.y;
			if (vector.y > max.y) max.y = vector.y;
		}

		public void MergeSelf(Vec3 vector)
		{
			if (vector.x < min.x) min.x = vector.x;
			if (vector.x > max.x) max.x = vector.x;

			if (vector.y < min.y) min.y = vector.y;
			if (vector.y > max.y) max.y = vector.y;

			if (vector.z < min.z) min.z = vector.z;
			if (vector.z > max.z) max.z = vector.z;
		}

		public Bound3 Transform(Mat3 matrix)
		{
			Bound3 result;
			result.min = min.Transform(matrix);
			result.max = max.Transform(matrix);
			return result;
		}

		public Bound3 Transform(Mat4 matrix)
		{
			Bound3 result;
			result.min = min.Transform(matrix);
			result.max = max.Transform(matrix);
			return result;
		}
		#endregion
	}
}