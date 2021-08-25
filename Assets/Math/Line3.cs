using System.Runtime.InteropServices;

namespace UnityMathReference
{
	[StructLayout(LayoutKind.Sequential)]
	public struct Line3
	{
		#region Properties
		public Vec3 point1, point2;
		#endregion

		#region Constructors
		public Line3(Vec3 point1, Vec3 point2)
		{
			this.point1 = point1;
			this.point2 = point2;
		}
		#endregion

		#region Methods
		public Line3 Transform(Mat3 matrix)
		{
			return new Line3(point1.Transform(matrix), point2.Transform(matrix));
		}

		public float Length()
		{
			return (point1 - point2).Length();
		}

		public Line3 IntersectLine(Vec3 point1, Vec3 point2)
		{
			Vec3 vector = (this.point1 - point1), vector2 = (point2 - point1), vector3 = (this.point2 - this.point1);
			float dot1 = vector.Dot(vector2);
			float dot2 = vector2.Dot(vector3);
			float dot3 = vector.Dot(vector3);
			float dot4 = vector2.Dot();
			float dot5 = vector3.Dot();
			float mul1 = ((dot1 * dot2) - (dot3 * dot4)) / ((dot5 * dot4) - (dot2 * dot2));
			float mul2 = (dot1 + (dot2 * mul1)) / dot4;
			return new Line3(this.point1 + (mul1 * vector3), point1 + (mul2 * vector2));
		}

		public Line3 IntersectLine(Line3 line)
		{
			return IntersectLine(line.point1, line.point2);
		}

		public Line3 ClosestLineToLine(Vec3 point1, Vec3 point2)
		{
			var result = IntersectLine(point1, point2);
			var boundThis = new Bound3(this);
			var boundOther = new Bound3(point1);
			boundOther.MergeSelf(point2);

			var lastPoint2 = result.point2;
			if (!result.point1.WithinBound(boundThis))
			{
				var p1 = this.point1.ClosestPointToLine(point1, point2);
				var p2 = this.point2.ClosestPointToLine(point1, point2);
				if (this.point1.Distance(p1) <= this.point2.Distance(p2)) result.point2 = p1;
				else result.point2 = p2;
			}

			if (!lastPoint2.WithinBound(boundOther))
			{
				var p1 = point1.ClosestPointToLine(this);
				var p2 = point2.ClosestPointToLine(this);
				if (point1.Distance(p1) <= point2.Distance(p2)) result.point1 = p1;
				else result.point1 = p2;
			}

			result.point1 = result.point1.KeepWithinBound(boundThis);
			result.point2 = result.point2.KeepWithinBound(boundOther);
			return result;
		}

		public Line3 ClosestLineToLine(Line3 line)
		{
			return ClosestLineToLine(line.point1, line.point2);
		}

		public Line3 ClosestLineToTriangle(Vec3 point1, Vec3 point2, Vec3 point3)
		{
			var ray = new Ray3(this.point1, (this.point2 - this.point1).Normalize());
			var normal = Triangle3.Normal(point1, point2, point3);
			if (ray.IntersectTriangle(point1, point2, point3, normal, out var intersectionPoint))
			{
				float dis1 = this.point1.Distance(intersectionPoint);
				float dis2 = this.point2.Distance(intersectionPoint);
				float lineLength = Length();
				float disSum = (dis1 + dis2) - 0.0001f;
				if (disSum <= lineLength) return new Line3(intersectionPoint, intersectionPoint);
			}

			var p1 = this.point1.ClosestPointToTriangle(point1, point2, point3);
			var p2 = this.point2.ClosestPointToTriangle(point1, point2, point3);
			float pd1 = this.point1.Distance(p1);
			float pd2 = this.point2.Distance(p2);

			var l1 = ClosestLineToLine(point1, point2);
			var l2 = ClosestLineToLine(point2, point3);
			var l3 = ClosestLineToLine(point3, point1);
			float d1 = l1.Length();
			float d2 = l2.Length();
			float d3 = l3.Length();

			if (d1 <= d2 && d1 <= d3)
			{
				if (d1 <= pd1 && d1 <= pd2) return l1;
			}
			else if (d2 <= d1 && d2 <= d3)
			{
				if (d2 <= pd1 && d2 <= pd2) return l2;
			}
			else if (d3 <= pd1 && d3 <= pd2)
			{
				return l3;
			}

			if (pd1 <= pd2) return new Line3(this.point1, p1);
			return new Line3(this.point2, p2);
		}

		public Line3 ClosestLineToTriangle(Triangle3 triangle)
		{
			return ClosestLineToTriangle(triangle.point1, triangle.point2, triangle.point3);
		}
		#endregion
	}
}