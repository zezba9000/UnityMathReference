using System;
using System.Runtime.InteropServices;

namespace UnityMathReference
{
	[StructLayout(LayoutKind.Sequential)]
	public struct Vec3
	{
		#region Properties
		public float x, y, z;

		public static readonly Vec3 one = new Vec3(1);
		public static readonly Vec3 minusOne = new Vec3(-1);
		public static readonly Vec3 zero = new Vec3();
		public static readonly Vec3 right = new Vec3(1, 0, 0);
		public static readonly Vec3 left = new Vec3(-1, 0, 0);
		public static readonly Vec3 up = new Vec3(0, 1, 0);
		public static readonly Vec3 down = new Vec3(0, -1, 0);
		public static readonly Vec3 forward = new Vec3(0, 0, 1);
		public static readonly Vec3 backward = new Vec3(0, 0, -1);
		#endregion

		#region Constructors
		public Vec3(float value)
		{
			x = value;
			y = value;
			z = value;
		}

		public Vec3(float x, float y, float z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		public Vec3(Vec2 vector, float z)
		{
			x = vector.x;
			y = vector.y;
			this.z = z;
		}

		public static Vec3 FromSphericalRotation(float latitude, float longitude)
		{
			return new Vec3
			(
				(float)(Math.Sin(latitude) * Math.Cos(longitude)),
				(float)Math.Sin(longitude),
				(float)(Math.Cos(latitude) * Math.Cos(longitude))
			);
		}
		#endregion

		#region Operators
		// +
		public static Vec3 operator+(Vec3 p1, Vec3 p2)
		{
			return new Vec3(p1.x + p2.x, p1.y + p2.y, p1.z + p2.z);
		}

		public static Vec3 operator+(Vec3 p1, Vec2 p2)
		{
			return new Vec3(p1.x + p2.x, p1.y + p2.y, p1.z);
		}

		public static Vec3 operator+(Vec2 p1, Vec3 p2)
		{
			return new Vec3(p1.x + p2.x, p1.y + p2.y, p2.z);
		}

		public static Vec3 operator+(Vec3 p1, float p2)
		{
			return new Vec3(p1.x + p2, p1.y + p2, p1.z + p2);
		}

		public static Vec3 operator+(float p1, Vec3 p2)
		{
			return new Vec3(p1 + p2.x, p1 + p2.y, p1 + p2.z);
		}

		// -
		public static Vec3 operator-(Vec3 p1, Vec3 p2)
		{
			return new Vec3(p1.x - p2.x, p1.y - p2.y, p1.z - p2.z);
		}

		public static Vec3 operator-(Vec3 p1, Vec2 p2)
		{
			return new Vec3(p1.x - p2.x, p1.x - p2.x, p1.z);
		}

		public static Vec3 operator-(Vec2 p1, Vec3 p2)
		{
			return new Vec3(p1.x - p2.x, p1.x - p2.x, p2.z);
		}

		public static Vec3 operator-(Vec3 p1, float p2)
		{
			return new Vec3(p1.x - p2, p1.y - p2, p1.z - p2);
		}

		public static Vec3 operator-(float p1, Vec3 p2)
		{
			return new Vec3(p1 - p2.x, p1 - p2.y, p1 - p2.z);
		}

		public static Vec3 operator-(Vec3 p1)
		{
			return new Vec3(-p1.x, -p1.y, -p1.z);
		}

		// *
		public static Vec3 operator*(Vec3 p1, Vec3 p2)
		{
			return new Vec3(p1.x * p2.x, p1.y * p2.y, p1.z * p2.z);
		}

		public static Vec3 operator*(Vec3 p1, Vec2 p2)
		{
			return new Vec3(p1.x * p2.x, p1.y * p2.y, p1.z);
		}

		public static Vec3 operator*(Vec2 p1, Vec3 p2)
		{
			return new Vec3(p1.x * p2.x, p1.y * p2.y, p2.z);
		}

		public static Vec3 operator*(Vec3 p1, float p2)
		{
			return new Vec3(p1.x * p2, p1.y * p2, p1.z * p2);
		}

		public static Vec3 operator*(float p1, Vec3 p2)
		{
			return new Vec3(p1 * p2.x, p1 * p2.y, p1 * p2.z);
		}

		// /
		public static Vec3 operator/(Vec3 p1, Vec3 p2)
		{
			return new Vec3(p1.x / p2.x, p1.y / p2.y, p1.z / p2.z);
		}

		public static Vec3 operator/(Vec3 p1, Vec2 p2)
		{
			return new Vec3(p1.x / p2.x, p1.y / p2.y, p1.z);
		}

		public static Vec3 operator/(Vec2 p1, Vec3 p2)
		{
			return new Vec3(p1.x / p2.x, p1.y / p2.y, p2.z);
		}

		public static Vec3 operator/(Vec3 p1, float p2)
		{
			return new Vec3(p1.x / p2, p1.y / p2, p1.z / p2);
		}

		public static Vec3 operator/(float p1, Vec3 p2)
		{
			return new Vec3(p1 / p2.x, p1 / p2.y, p1 / p2.z);
		}

		// ==
		public static bool operator==(Vec3 p1, Vec3 p2) {return p1.x==p2.x && p1.y==p2.y && p1.z==p2.z;}
		public static bool operator!=(Vec3 p1, Vec3 p2) {return p1.x!=p2.x || p1.y!=p2.y || p1.z!=p2.z;}

		// convert
		#if MATH_UNITY_HELPER
		public static implicit operator Vec3(UnityEngine.Vector3 vec)
		{
			return new Vec3(vec.x, vec.y, vec.z);
		}

		public UnityEngine.Vector3 ToVector3()
		{
			return new UnityEngine.Vector3(x, y, z);
		}
		#endif

		public Vec2 ToVec2()
		{
			return new Vec2(x, y);
		}

		public Point3 ToPoint3()
		{
			return new Point3((int)x, (int)y, (int)z);
		}

		public Size3 ToSize3()
		{
			return new Size3((int)x, (int)y, (int)z);
		}
		#endregion

		#region Methods
		public override bool Equals(object obj)
		{
			return obj != null && (Vec3)obj == this;
		}

		public override string ToString()
		{
			return string.Format("<{0}, {1}, {2}>", x, y, z);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public Vec3 DegToRad()
		{
			return new Vec3(MathUtilities.DegToRad(x), MathUtilities.DegToRad(y), MathUtilities.DegToRad(z));
		}

		public Vec3 RadToDeg()
		{
			return new Vec3(MathUtilities.RadToDeg(x), MathUtilities.RadToDeg(y), MathUtilities.RadToDeg(z));
		}

		public Vec3 Max(float value)
		{
			return new Vec3(Math.Max(x, value), Math.Max(y, value), Math.Max(z, value));
		}

		public Vec3 Max(Vec3 values)
		{
			return new Vec3(Math.Max(x, values.x), Math.Max(y, values.y), Math.Max(z, values.z));
		}

		public Vec3 Min(float value)
		{
			return new Vec3(Math.Min(x, value), Math.Min(y, value), Math.Min(z, value));
		}

		public Vec3 Min(Vec3 values)
		{
			return new Vec3(Math.Min(x, values.x), Math.Min(y, values.y), Math.Min(z, values.z));
		}

		public Vec3 Abs()
		{
			return new Vec3(Math.Abs(x), Math.Abs(y), Math.Abs(z));
		}

		public Vec3 Pow(float value)
		{
			return new Vec3((float)Math.Pow(x, value), (float)Math.Pow(y, value), (float)Math.Pow(z, value));
		}

		public Vec3 Floor()
		{
			return new Vec3((float)Math.Floor(x), (float)Math.Floor(y), (float)Math.Floor(z));
		}

		public Vec3 Round()
		{
			return new Vec3((float)Math.Round(x), (float)Math.Round(y), (float)Math.Round(z));
		}

		public float Length()
		{
			return (float)Math.Sqrt((x*x) + (y*y) + (z*z));
		}

		public float LengthSquared()
		{
			return (x*x) + (y*y) + (z*z);
		}

		public float Distance(Vec3 vector)
		{
			return (vector - this).Length();
		}
		
		public float DistanceSquared(Vec3 vector)
		{
			return (vector - this).Dot();
		}

		public float Dot()
		{
			return (x*x) + (y*y) + (z*z);
		}

		public float Dot(Vec3 vector)
		{
			return (x*vector.x) + (y*vector.y) + (z*vector.z);
		}

		public Vec3 Normalize()
		{
			return this * (1 / (float)Math.Sqrt((x*x) + (y*y) + (z*z)));
		}

		public Vec3 Normalize(out float length)
		{
			float dis = (float)Math.Sqrt((x*x) + (y*y) + (z*z));
			length = dis;
			return this * (1/dis);
		}

		public Vec3 NormalizeSafe()
		{
			float dis = (float)Math.Sqrt((x*x) + (y*y) + (z*z));
			if (dis == 0) return new Vec3();
			else return this * (1/dis);
		}

		public Vec3 NormalizeSafe(out float length)
		{
			float dis = (float)Math.Sqrt((x*x) + (y*y) + (z*z));
			length = dis;
			if (dis == 0) return new Vec3();
			else return this * (1/dis);
		}

		public Vec3 Cross(Vec3 vector)
		{
			return new Vec3(((y*vector.z) - (z*vector.y)), ((z*vector.x) - (x*vector.z)), ((x*vector.y) - (y*vector.x)));
		}

		public Vec3 Transform(Mat4 matrix)
		{
			return new Vec3
			(
				(x * matrix.x.x) + (y * matrix.y.x) + (z * matrix.z.x) + matrix.x.w,
				(x * matrix.x.y) + (y * matrix.y.y) + (z * matrix.z.y) + matrix.y.w,
				(x * matrix.x.z) + (y * matrix.y.z) + (z * matrix.z.z) + matrix.z.w
			);
		}

		public Vec3 TransformNormal(Mat4 matrix)
		{
			return new Vec3
			(
				(x * matrix.x.x) + (y * matrix.y.x) + (z * matrix.z.x),
				(x * matrix.x.y) + (y * matrix.y.y) + (z * matrix.z.y),
				(x * matrix.x.z) + (y * matrix.y.z) + (z * matrix.z.z)
			);
		}

		public Vec3 Transform(Mat3 matrix)
		{
			return (matrix.x*x) + (matrix.y*y) + (matrix.z*z);
		}

		public Vec3 TransformTranspose(Mat3 matrix)
		{
			return new Vec3
			(
				(x * matrix.x.x) + (y * matrix.x.y) + (z * matrix.x.z),
				(x * matrix.y.x) + (y * matrix.y.y) + (z * matrix.y.z),
				(x * matrix.z.x) + (y * matrix.z.y) + (z * matrix.z.z)
			);
		}

		public Vec2 Transform(Mat2x3 matrix)
		{
			return new Vec2
			(
				(matrix.x.x * x) + (matrix.x.y * y) + (matrix.x.z * z),
				(matrix.y.x * x) + (matrix.y.y * y) + (matrix.y.z * z)
			);
		}

		public Vec2 Transform(Mat3x2 matrix)
		{
			return new Vec2
			(
				(x * matrix.x.x) + (y * matrix.y.x) + (z * matrix.z.x),
				(x * matrix.x.y) + (y * matrix.y.y) + (z * matrix.z.y)
			);
		}

		public Vec3 Transform(AffineTransform3 transform)
		{
			return this.Transform(transform.Transform) + transform.Translation;
		}

		public Vec3 Transform(RigidTransform3 transform)
		{
			return this.Transform(transform.rotation) + transform.position;
		}

		public Vec3 TransformInversed(RigidTransform3 transform)
		{
			transform.rotation = transform.rotation.Conjugate();
			Vec3 result = this.Transform(transform.rotation);
			return result;
		}

		public Vec3 Transform(Quat quaternion)
		{
			float x2 = quaternion.x + quaternion.x;
			float y2 = quaternion.y + quaternion.y;
			float z2 = quaternion.z + quaternion.z;
			float xx2 = quaternion.x * x2;
			float xy2 = quaternion.x * y2;
			float xz2 = quaternion.x * z2;
			float yy2 = quaternion.y * y2;
			float yz2 = quaternion.y * z2;
			float zz2 = quaternion.z * z2;
			float wx2 = quaternion.w * x2;
			float wy2 = quaternion.w * y2;
			float wz2 = quaternion.w * z2;

			return new Vec3
			(
				x * ((1f - yy2) - zz2) + y * (xy2 - wz2) + z * (xz2 + wy2),
				x * (xy2 + wz2) + y * ((1f - xx2) - zz2) + z * (yz2 - wx2),
				x * (xz2 - wy2) + y * (yz2 + wx2) + z * ((1f - xx2) - yy2)
			);
		}

		public bool AproxEqualsBox(Vec3 vector, float tolerance)
		{
			return
				(Math.Abs(x-vector.x) <= tolerance) &&
				(Math.Abs(y-vector.y) <= tolerance) &&
				(Math.Abs(z-vector.z) <= tolerance);
		}

		public bool ApproxEquals(Vec3 vector, float tolerance)
		{
			return (Distance(vector) <= tolerance);
		}

		public Vec3 RotateAround(Vec3 axis, float angle)
		{
			// rotate into world space
			var quaternion = Quat.FromRotationAxis(axis, 0);
			quaternion = quaternion.Conjugate();
			var worldSpaceVector = this.Transform(quaternion);

			// rotate back to vector space
			quaternion = Quat.FromRotationAxis(axis, angle);
			worldSpaceVector = worldSpaceVector.Transform(quaternion);
			return worldSpaceVector;
		}

		public Vec3 Reflect(Vec3 planeNormal)
		{
			return this - (planeNormal * this.Dot(planeNormal) * 2);
		}

		public Vec3 Refract(Vec3 normal, float refractionIndex)
		{
			float cosI = -normal.Dot(this);
			float sinT2 = refractionIndex * refractionIndex * (1.0f - cosI * cosI);

			if (sinT2 > 1.0f) return this;

			float cosT = (float)Math.Sqrt(1.0f - sinT2);
			return this * refractionIndex + normal * (refractionIndex * cosI - cosT);
		}

		public Vec3 IntersectNormal(Vec3 normal)
		{
			return (normal * this.Dot(normal));
		}

		public Vec3 IntersectRay(Vec3 rayOrigin, Vec3 rayDirection)
		{
			return (rayDirection * (this-rayOrigin).Dot(rayDirection)) + rayOrigin;
		}

		public Vec3 IntersectLine(Line3 line)
		{
			Vec3 pointOffset = (this-line.point1), vector = (line.point2-line.point1).Normalize();
			return (vector * pointOffset.Dot(vector)) + line.point1;
		}

		public Vec3 IntersectPlane(Vec3 planeNormal)
		{
			return this - (planeNormal * this.Dot(planeNormal));
		}

		public Vec3 IntersectPlane(Vec3 planeNormal, Vec3 planeLocation)
		{
			return this - (planeNormal * (this-planeLocation).Dot(planeNormal));
		}

		/// <summary>
		/// Barycentric Technique
		/// </summary>
		public bool WithinTriangle(Vec3 trianglePoint1, Vec3 trianglePoint2, Vec3 trianglePoint3)
		{
			// Compute vectors        
			var v0 = trianglePoint3 - trianglePoint1;
			var v1 = trianglePoint2 - trianglePoint1;
			var v2 = this - trianglePoint1;

			// Compute dot products
			float dot00 = v0.Dot(v0);
			float dot01 = v0.Dot(v1);
			float dot02 = v0.Dot(v2);
			float dot11 = v1.Dot(v1);
			float dot12 = v1.Dot(v2);

			// Compute barycentric coordinates
			float invDenom = 1 / ((dot00 * dot11) - (dot01 * dot01));
			float u = ((dot11 * dot02) - (dot01 * dot12)) * invDenom;
			float v = ((dot00 * dot12) - (dot01 * dot02)) * invDenom;

			// Check if point is in triangle
			return (u >= 0) && (v >= 0) && (u + v < 1);
		}

		public bool WithinTriangle(Triangle3 triangle)
		{
			return WithinTriangle(triangle.point1, triangle.point2, triangle.point3);
		}

		public bool IntersectTriangle(Vec3 trianglePoint1, Vec3 trianglePoint2, Vec3 trianglePoint3, Vec3 triangleNormal, out Vec3 intersectionPoint)
		{
			intersectionPoint = this.IntersectPlane(triangleNormal, trianglePoint1);
			return intersectionPoint.WithinTriangle(trianglePoint1, trianglePoint2, trianglePoint3);
		}

		public bool IntersectTriangle(Triangle3 triangle, Vec3 triangleNormal, out Vec3 intersectionPoint)
		{
			return IntersectTriangle(triangle.point1, triangle.point2, triangle.point3, triangleNormal, out intersectionPoint);
		}

		public bool IntersectTriangle(Triangle3 triangle, out Vec3 intersectionPoint)
		{
			return IntersectTriangle(triangle, triangle.Normal(), out intersectionPoint);
		}

		public bool IntersectRectangle(Vec3 rectCenter, Quat rectRotation, Vec2 rectScale, out Vec3 intersectionPoint)
		{
			var pos = this - rectCenter;
			pos = pos.Transform(rectRotation.Inverse());
			rectScale *= .5f;
			if (pos.x >= -rectScale.x && pos.x <= rectScale.x && pos.y >= -rectScale.y && pos.y <= rectScale.y)
			{
				pos.z = 0;
				intersectionPoint = pos.Transform(rectRotation) + rectCenter;
				return true;
			}

			intersectionPoint = zero;
			return false;
		}

		public Vec3 ClosestPointToQuad(Vec3 quadPos, Quat quadRot, Vec2 quadSize)
		{
			return ClosestPointToQuad(quadPos, quadRot, quadRot.Inverse(), quadSize);
		}

		public Vec3 ClosestPointToQuad(Vec3 quadPos, Quat quadRot, Quat quadRotInv, Vec2 quadSize)
		{
			var p = (this - quadPos).Transform(quadRotInv);
			p.z = 0;
			if (p.x < -quadSize.x) p.x = -quadSize.x;
			if (p.x > quadSize.x) p.x = quadSize.x;
			if (p.y < -quadSize.y) p.y = -quadSize.y;
			if (p.y > quadSize.y) p.y = quadSize.y;
			return p.Transform(quadRot) + quadPos;
		}

		public float Angle(Vec3 vector)
		{
			var vec = this.Normalize();
			float val = vec.Dot(vector.Normalize());
			val = (val > 1) ? 1 : val;
			val = (val < -1) ? -1 : val;
			return (float)Math.Acos(val);
		}

		public static Vec3 Lerp(Vec3 start, Vec3 end, float interpolationAmount)
		{
			float startAmount = 1 - interpolationAmount;
			return new Vec3
			(
				(start.x * startAmount) + (end.x * interpolationAmount),
				(start.y * startAmount) + (end.y * interpolationAmount),
				(start.z * startAmount) + (end.z * interpolationAmount)
			);
		}

		public static Vec3 Hermite(Vec3 value1, Vec3 tangent1, Vec3 value2, Vec3 tangent2, float interpolationAmount)
		{
			float weightSquared = interpolationAmount * interpolationAmount;
			float weightCubed = interpolationAmount * weightSquared;
			float value1Blend = 2 * weightCubed - 3 * weightSquared + 1;
			float tangent1Blend = weightCubed - 2 * weightSquared + interpolationAmount;
			float value2Blend = -2 * weightCubed + 3 * weightSquared;
			float tangent2Blend = weightCubed - weightSquared;

			return new Vec3
			(
				value1.x * value1Blend + value2.x * value2Blend + tangent1.x * tangent1Blend + tangent2.x * tangent2Blend,
				value1.y * value1Blend + value2.y * value2Blend + tangent1.y * tangent1Blend + tangent2.y * tangent2Blend,
				value1.z * value1Blend + value2.z * value2Blend + tangent1.z * tangent1Blend + tangent2.z * tangent2Blend
			);
		}

		public void SphericalRotation(out float latitude, out float longitude)
		{
			var normal = this.Normalize();
			latitude = (float)Math.Atan2(normal.x, normal.z);
			longitude = (float)Math.Asin(normal.y);
		}

		public void SphericalRotation_PreNormalized(out float latitude, out float longitude)
		{
			latitude = (float)Math.Atan2(x, z);
			longitude = (float)Math.Asin(y);
		}
		#endregion
	}

	#if MATH_UNITY_HELPER
	public static class Vec3Ext
	{
		public static Vec3 ToVec3(this UnityEngine.Vector3 self)
		{
			return new Vec3(self.x, self.y, self.z);
		}
	}
	#endif
}