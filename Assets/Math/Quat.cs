using System;
using System.Runtime.InteropServices;

namespace Reign
{
	[StructLayout(LayoutKind.Sequential)]
	public struct Quat
	{
		#region Properties
		public float x, y, z, w;
		#endregion

		#region Constructors
		public Quat(float value)
		{
			x = value;
			y = value;
			z = value;
			w = value;
		}

		public Quat(float x, float y, float z, float w)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}

		public Quat(Vec2 vector, float z, float w)
		{
			x = vector.x;
			y = vector.y;
			this.z = z;
			this.w = w;
		}

		public Quat(Vec3 vector, float w)
		{
			x = vector.x;
			y = vector.y;
			z = vector.z;
			this.w = w;
		}

		public static Quat LookAt(Vec3 forward, Vec3 up)
		{
			Mat3 mat = Mat3.LookAt(forward, up);
			Quat q = Quat.FromMatrix3(mat);
			return q;
		}

		public static Quat FromMatrix3(Mat3 matrix)
		{
			float w = (float)Math.Sqrt(1 + matrix.x.x + matrix.y.y + matrix.z.z) * .5f;
			float delta = 1 / (w * 4);
			return new Quat
			(
				(matrix.z.y - matrix.y.z) * delta,
				(matrix.x.z - matrix.z.x) * delta,
				(matrix.y.x - matrix.x.y) * delta,
				w
			);
		}

		public static Quat FromMatrix4(Mat4 matrix)
		{
			float w = (float)Math.Sqrt(1 + matrix.x.x + matrix.y.y + matrix.z.z) * .5f;
			float delta = 1 / (w * 4);
			return new Quat
			(
				(matrix.z.y - matrix.y.z) * delta,
				(matrix.x.z - matrix.z.x) * delta,
				(matrix.y.x - matrix.x.y) * delta,
				w
			);
		}

		public static Quat FromRotationAxis(Vec3 axis, float angle)
		{
			angle *= .5f;
			var sin = (float)Math.Sin(angle);
			return new Quat
			(
				axis.x * sin,
				axis.y * sin,
				axis.z * sin,
				(float)Math.Cos(angle)
			);
		}

		public static Quat FromRotationAxis(float axisX, float axisY, float axisZ, float angle)
		{
			angle *= .5f;
			var sin = (float)Math.Sin(angle);
			return new Quat
			(
				axisX * sin,
				axisY * sin,
				axisZ * sin,
				(float)Math.Cos(angle)
			);
		}

		public static Quat FromSphericalRotation(float latitude, float longitude, float angle)
		{
			angle *= .5f;
			float sa = (float)Math.Sin(angle);
			float cLat = (float)Math.Cos(latitude);
			float sLat = (float)Math.Sin(latitude);
			float cLong = (float)Math.Cos(longitude);
			float sLong = (float)Math.Sin(longitude);
			return new Quat(sa*cLat*sLong, sa*sLat, sa*sLat*cLong, (float)Math.Cos(angle));
		}

		public static Quat FromEuler(Vec3 euler)
		{
			euler.x *= .5f;
			euler.y *= .5f;
			euler.z *= .5f;

			float c1 = (float)Math.Cos(euler.y);
			float s1 = (float)Math.Sin(euler.y);
			float c2 = (float)Math.Cos(euler.z);
			float s2 = (float)Math.Sin(euler.z);
			float c3 = (float)Math.Cos(euler.x);
			float s3 = (float)Math.Sin(euler.x);
			float c1c2 = c1*c2;
			float s1s2 = s1*s2;
			float s1c2 = s1*c2;
			float c1s2 = c1*s2;

			Quat q;
			q.w = c1c2*c3 - s1s2*s3;
  			q.x = c1c2*s3 + s1s2*c3;
			q.y = s1c2*c3 + c1s2*s3;
			q.z = c1s2*c3 - s1c2*s3;
			return q;
		}

		public static Quat FromEuler(float eulerX, float eulerY, float eulerZ)
		{
			eulerX *= .5f;
			eulerY *= .5f;
			eulerZ *= .5f;

			float c1 = (float)Math.Cos(eulerY);
			float s1 = (float)Math.Sin(eulerY);
			float c2 = (float)Math.Cos(eulerZ);
			float s2 = (float)Math.Sin(eulerZ);
			float c3 = (float)Math.Cos(eulerX);
			float s3 = (float)Math.Sin(eulerX);
			float c1c2 = c1*c2;
			float s1s2 = s1*s2;
			float s1c2 = s1*c2;
			float c1s2 = c1*s2;

			Quat q;
			q.w = c1c2*c3 - s1s2*s3;
  			q.x = c1c2*s3 + s1s2*c3;
			q.y = s1c2*c3 + c1s2*s3;
			q.z = c1s2*c3 - s1c2*s3;
			return q;
		}

		public static readonly Quat Identity = new Quat(0, 0, 0, 1);
		#endregion

		#region Operators
		// +
		public static Quat operator+(Quat p1, Quat p2)
		{
			p1.x += p2.x;
			p1.y += p2.y;
			p1.z += p2.z;
			p1.w += p2.w;
			return p1;
		}

		public static Quat operator+(Quat p1, float p2)
		{
			p1.x += p2;
			p1.y += p2;
			p1.z += p2;
			p1.w += p2;
			return p1;
		}

		public static Quat operator+(float p1, Quat p2)
		{
			p2.x = p1 + p2.x;
			p2.y = p1 + p2.y;
			p2.z = p1 + p2.z;
			p2.w = p1 + p2.w;
			return p2;
		}

		// -
		public static Quat operator-(Quat p1, Quat p2)
		{
			p1.x -= p2.x;
			p1.y -= p2.y;
			p1.z -= p2.z;
			p1.w -= p2.w;
			return p1;
		}

		public static Quat operator-(Quat p1, float p2)
		{
			p1.x -= p2;
			p1.y -= p2;
			p1.z -= p2;
			p1.w -= p2;
			return p1;
		}

		public static Quat operator-(float p1, Quat p2)
		{
			p2.x = p1 - p2.x;
			p2.y = p1 - p2.y;
			p2.z = p1 - p2.z;
			p2.w = p1 - p2.w;
			return p2;
		}

		public static Quat operator-(Quat p2)
		{
			p2.x = -p2.x;
			p2.y = -p2.y;
			p2.z = -p2.z;
			p2.w = -p2.w;
			return p2;
		}

		// *
		public static Quat operator*(Quat p1, Quat p2)
		{
			p1.x *= p2.x;
			p1.y *= p2.y;
			p1.z *= p2.z;
			p1.w *= p2.w;
			return p1;
		}

		public static Quat operator*(Quat p1, float p2)
		{
			p1.x *= p2;
			p1.y *= p2;
			p1.z *= p2;
			p1.w *= p2;
			return p1;
		}

		public static Quat operator*(float p1, Quat p2)
		{
			p2.x = p1 * p2.x;
			p2.y = p1 * p2.y;
			p2.z = p1 * p2.z;
			p2.w = p1 * p2.w;
			return p2;
		}

		// /
		public static Quat operator/(Quat p1, Quat p2)
		{
			p1.x /= p2.x;
			p1.y /= p2.y;
			p1.z /= p2.z;
			p1.w /= p2.w;
			return p1;
		}

		public static Quat operator/(Quat p1, float p2)
		{
			p1.x /= p2;
			p1.y /= p2;
			p1.z /= p2;
			p1.w /= p2;
			return p1;
		}

		public static Quat operator/(float p1, Quat p2)
		{
			p2.x = p1 / p2.x;
			p2.y = p1 / p2.y;
			p2.z = p1 / p2.z;
			p2.w = p1 / p2.w;
			return p2;
		}

		// ==
		public static bool operator==(Quat p1, Quat p2) {return (p1.x==p2.x && p1.y==p2.y && p1.z==p2.z && p1.w==p2.w);}
		public static bool operator!=(Quat p1, Quat p2) {return (p1.x!=p2.x || p1.y!=p2.y || p1.z!=p2.z || p1.w!=p2.w);}

		// convert
		public Vec4 ToVec4() {return new Vec4(x, y, z, w);}
		#endregion

		#region Methods
		public override bool Equals(object obj)
		{
			return obj != null && (Quat)obj == this;
		}

		public override string ToString()
		{
			return string.Format("<{0}, {1}, {2}, {3}>", x, y, z, w);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public float Length()
		{
			return (float)Math.Sqrt((x*x) + (y*y) + (z*z) + (w*w));
		}

		public Quat Normalize()
		{
			return this * (1 / (float)Math.Sqrt((x*x) + (y*y) + (z*z) + (w*w)));
		}

		public Quat Normalize(out float length)
		{
			float dis = (float)Math.Sqrt((x*x) + (y*y) + (z*z) + (w*w));
			length = dis;
			return this * (1/dis);
		}

		public Quat NormalizeSafe()
		{
			float dis = (float)Math.Sqrt((x*x) + (y*y) + (z*z) + (w*w));
			if (dis == 0) return new Quat();
			else return this * (1/dis);
		}

		public Quat NormalizeSafe(out float length)
		{
			float dis = (float)Math.Sqrt((x*x) + (y*y) + (z*z) + (w*w));
			length = dis;
			if (dis == 0) return new Quat();
			else return this * (1/dis);
		}

		public Quat Multiply(Quat quaternion)
		{
			return new Quat
			(
				w*quaternion.x + x*quaternion.w + y*quaternion.z - z*quaternion.y,
				w*quaternion.y - x*quaternion.z + y*quaternion.w + z*quaternion.x,
				w*quaternion.z + x*quaternion.y - y*quaternion.x + z*quaternion.w,
				w*quaternion.w - x*quaternion.x - y*quaternion.y - z*quaternion.z
			);
		}

		public Quat Concatenate(Quat quaternion)
		{
			return new Quat
			(
				quaternion.w*x + quaternion.x*w + quaternion.y*z - quaternion.z*y,
				quaternion.w*y - quaternion.x*z + quaternion.y*w + quaternion.z*x,
				quaternion.w*z + quaternion.x*y - quaternion.y*x + quaternion.z*w,
				quaternion.w*w - quaternion.x*x - quaternion.y*y - quaternion.z*z
			);
		}

		public Quat Conjugate()
		{
			return new Quat(-x, -y, -z, w);
		}

		public void RotationAxis(out Vec3 axis, out float angle)
		{
			angle = (float)Math.Acos(w) * MathUtilities.pi2;
			float sinAngle = (float)Math.Sqrt(1 - (w*w));
			if (sinAngle == 0) sinAngle = 1;
			sinAngle = 1 / sinAngle;
			axis = new Vec3(x*sinAngle, y*sinAngle, z*sinAngle);
		}

		public void SphericalRotation(out float latitude, out float longitude, out float angle)
		{
			angle = (float)Math.Acos(w) * MathUtilities.pi2;
			float sinAngle = (float)Math.Sqrt(1 - (w*w));
			if (sinAngle == 0) sinAngle = 1;
			sinAngle = 1 / sinAngle;

			float x = this.x * sinAngle;
			float y = this.y * sinAngle;
			float z = this.z * sinAngle;

			latitude = -(float)Math.Asin(y);
			if ((x*x) + (z*z) == 0) longitude = 0;
			else longitude = (float)Math.Atan2(x, z) * MathUtilities.pi;
			if (longitude < 0) longitude += MathUtilities.pi2;
		}

		public void Euler(out Vec3 euler)
		{
			float sqx = x*x;
			float sqy = y*y;
			float sqz = z*z;
			float sqw = w*w;

			float unit = sqx + sqy + sqz + sqw;
			float test = x*y + z*w;

			euler.y = (float)Math.Atan2(2*y*w - 2*x*z, sqx - sqy - sqz + sqw);
			euler.z = (float)Math.Asin(2*test/unit);
			euler.x = (float)Math.Atan2(2*x*w - 2*y*z, -sqx + sqy - sqz + sqw);
		}

		public Quat Slerp(Quat target, float interpolationAmount)
		{
			var start = this;

			float cosHalfTheta = start.w * target.w + start.x * target.x + start.y * target.y + start.z * target.z;
            if (cosHalfTheta < 0)
            {
                //Negating a quaternion results in the same orientation, but we need cosHalfTheta to be positive to get the shortest path.
                target = -target;
                cosHalfTheta = -cosHalfTheta;
            }

            // If the orientations are similar enough, then just pick one of the inputs.
            if (cosHalfTheta > .999999f) return start;

            // Calculate temporary values.
            float halfTheta = (float)Math.Acos(cosHalfTheta);
            float sinHalfTheta = (float)Math.Sqrt(1.0 - cosHalfTheta * cosHalfTheta);

            //Check to see if we're 180 degrees away from the target.
            if (Math.Abs(sinHalfTheta) < 0.00001f) return (start + target) * .5f;

            //Blend the two quaternions to get the result!
			float aFraction = (float)Math.Sin((1 - interpolationAmount) * halfTheta) / sinHalfTheta;
            float bFraction = (float)Math.Sin(interpolationAmount * halfTheta) / sinHalfTheta;
			return start * aFraction + target * bFraction;
		}
		#endregion
	}
}