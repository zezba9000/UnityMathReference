using System;
using System.Runtime.InteropServices;

namespace UnityMathReference
{
	[StructLayout(LayoutKind.Sequential)]
	public struct Vec2
	{
		#region Properties
		public float x, y;
		#endregion

		#region Constructors
		public Vec2(float value)
		{
			x = value;
			y = value;
		}

		public Vec2(float x, float y)
		{
			this.x = x;
			this.y = y;
		}

		#if REIGN_UNITY_HELPER
		public static implicit operator Vec2(UnityEngine.Vector2 vec)
		{
			return new Vec2(vec.x, vec.y);
		}
		#endif

		public static readonly Vec2 one = new Vec2(1);
		public static readonly Vec2 minusOne = new Vec2(-1);
		public static readonly Vec2 zero = new Vec2(0);
		public static readonly Vec2 right = new Vec2(1, 0);
		public static readonly Vec2 left = new Vec2(-1, 0);
		public static readonly Vec2 up = new Vec2(0, 1);
		public static readonly Vec2 down = new Vec2(0, -1);
		#endregion

		#region Operators
		// +
		public static Vec2 operator+(Vec2 p1, Vec2 p2)
		{
			p1.x += p2.x;
			p1.y += p2.y;
			return p1;
		}

		public static Vec2 operator+(Vec2 p1, float p2)
		{
			p1.x += p2;
			p1.y += p2;
			return p1;
		}

		public static Vec2 operator+(float p1, Vec2 p2)
		{
			p2.x = p1 + p2.x;
			p2.y = p1 + p2.y;
			return p2;
		}

		// -
		public static Vec2 operator-(Vec2 p1, Vec2 p2)
		{
			p1.x -= p2.x;
			p1.y -= p2.y;
			return p1;
		}

		public static Vec2 operator-(Vec2 p1, float p2)
		{
			p1.x -= p2;
			p1.y -= p2;
			return p1;
		}

		public static Vec2 operator-(float p1, Vec2 p2)
		{
			p2.x = p1 - p2.x;
			p2.y = p1 - p2.y;
			return p2;
		}

		public static Vec2 operator-(Vec2 p2)
		{
			p2.x = -p2.x;
			p2.y = -p2.y;
			return p2;
		}

		// *
		public static Vec2 operator*(Vec2 p1, Vec2 p2)
		{
			p1.x *= p2.x;
			p1.y *= p2.y;
			return p1;
		}

		public static Vec2 operator*(Vec2 p1, float p2)
		{
			p1.x *= p2;
			p1.y *= p2;
			return p1;
		}

		public static Vec2 operator*(float p1, Vec2 p2)
		{
			p2.x = p1 * p2.x;
			p2.y = p1 * p2.y;
			return p2;
		}

		// /
		public static Vec2 operator/(Vec2 p1, Vec2 p2)
		{
			p1.x /= p2.x;
			p1.y /= p2.y;
			return p1;
		}

		public static Vec2 operator/(Vec2 p1, float p2)
		{
			p1.x /= p2;
			p1.y /= p2;
			return p1;
		}

		public static Vec2 operator/(float p1, Vec2 p2)
		{
			p2.x = p1 / p2.x;
			p2.y = p1 / p2.y;
			return p2;
		}

		// ==
		public static bool operator==(Vec2 p1, Vec2 p2) {return (p1.x==p2.x && p1.y==p2.y);}
		public static bool operator!=(Vec2 p1, Vec2 p2) {return (p1.x!=p2.x || p1.y!=p2.y);}

		// convert
		public Point2 ToPoint2()
		{
			return new Point2((int)x, (int)y);
		}

		public Size2 ToSize2()
		{
			return new Size2((int)x, (int)y);
		}

		#if REIGN_UNITY_HELPER
		public UnityEngine.Vector2 ToVector2()
		{
			return new UnityEngine.Vector2(x, y);
		}
		#endif
		#endregion

		#region Methods
		public override bool Equals(object obj)
		{
			return obj != null && (Vec2)obj == this;
		}

		public override string ToString()
		{
			return string.Format("<{0}, {1}>", x, y);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public float SlopeXY()
		{
			return x / y;
		}

		public float SlopeYX()
		{
			return y / x;
		}

		public Vec2 DegToRad()
		{
			return new Vec2(MathUtilities.DegToRad(x), MathUtilities.DegToRad(y));
		}

		public Vec2 RadToDeg()
		{
			return new Vec2(MathUtilities.RadToDeg(x), MathUtilities.RadToDeg(y));
		}

		public Vec2 Max(float value)
		{
			return new Vec2(Math.Max(x, value), Math.Max(y, value));
		}

		public Vec2 Max(Vec2 values)
		{
			return new Vec2(Math.Max(x, values.x), Math.Max(y, values.y));
		}

		public Vec2 Min(float value)
		{
			return new Vec2(Math.Min(x, value), Math.Min(y, value));
		}

		public Vec2 Min(Vec2 values)
		{
			return new Vec2(Math.Min(x, values.x), Math.Min(y, values.y));
		}

		public Vec2 Abs()
		{
			return new Vec2(Math.Abs(x), Math.Abs(y));
		}

		public Vec2 Pow(float value)
		{
			return new Vec2((float)Math.Pow(x, value), (float)Math.Pow(y, value));
		}

		public Vec2 Floor()
		{
			return new Vec2((float)Math.Floor(x), (float)Math.Floor(y));
		}

		public Vec2 Round()
		{
			return new Vec2((float)Math.Round(x), (float)Math.Round(y));
		}

		public float Length()
		{
			return (float)Math.Sqrt((x*x) + (y*y));
		}

		public float LengthSquared()
		{
			return (x*x) + (y*y);
		}

		public float Distance(Vec2 vector)
		{
			return (vector - this).Length();
		}
		
		public float DistanceSquared(Vec2 vector)
		{
			return (vector - this).Dot();
		}

		public float Dot()
		{
			return (x*x) + (y*y);
		}

		public float Dot(Vec2 vector)
		{
			return (x*vector.x) + (y*vector.y);
		}

		public Vec2 Normalize()
		{
			return this * (1 / (float)Math.Sqrt((x*x) + (y*y)));
		}

		public Vec2 Normalize(out float length)
		{
			float dis = (float)Math.Sqrt((x*x) + (y*y));
			length = dis;
			return this * (1/dis);
		}

		public Vec2 NormalizeSafe()
		{
			float dis = (float)Math.Sqrt((x*x) + (y*y));
			if (dis == 0) return new Vec2();
			else return this * (1/dis);
		}

		public Vec2 NormalizeSafe(out float length)
		{
			float dis = (float)Math.Sqrt((x*x) + (y*y));
			length = dis;
			if (dis == 0) return new Vec2();
			else return this * (1/dis);
		}

		public Vec2 Cross()
		{
			return new Vec2(-y, x);
		}

		public Vec2 Transform(Mat2 matrix)
		{
			return (matrix.x*x) + (matrix.y*y);
		}

		public Vec3 Transform(Mat2x3 matrix)
		{
			return new Vec3
			(
				(x * matrix.x.x) + (y * matrix.y.x),
				(x * matrix.x.y) + (y * matrix.y.y),
				(x * matrix.x.z) + (y * matrix.y.z)
			);
		}

		public Vec3 Transform(Mat3x2 matrix)
		{
			return new Vec3
			(
				(matrix.x.x * x) + (matrix.x.y * y),
				(matrix.y.x * x) + (matrix.y.y * y),
				(matrix.z.x * x) + (matrix.z.y * y)
			);
		}

		public bool AproxEqualsBox(Vec2 vector, float tolerance)
		{
			return
				(Math.Abs(x-vector.x) <= tolerance) &&
				(Math.Abs(y-vector.y) <= tolerance);
		}

		public bool ApproxEquals(Vec2 vector, float tolerance)
		{
			return (this.Distance(vector) <= tolerance);
		}

		public float Angle()
		{
			var vec = this.Normalize();
			float val = vec.x;
			val = (val > 1) ? 1 : val;
			val = (val < -1) ? -1 : val;
			return (float)Math.Acos(val);
		}

		public float Angle(Vec2 vector)
		{
			var vec = this.Normalize();
			float val = vec.Dot(vector.Normalize());
			val = (val > 1) ? 1 : val;
			val = (val < -1) ? -1 : val;
			return (float)Math.Acos(val);
		}

		public float Angle90()
		{
			var vec = this.Normalize();
			float val = Math.Abs(vec.x);
			val = (val > 1) ? 1 : val;
			return (float)Math.Acos(val);
		}

		public float Angle90(Vec2 vector)
		{
			var vec = this.Normalize();
			float val = Math.Abs(vec.Dot(vector.Normalize()));
			val = (val > 1) ? 1 : val;
			return (float)Math.Acos(val);
		}

		public float Angle180()
		{
			var vec = this.Normalize();
			return ((float)Math.Atan2(-vec.y, vec.x)) % MathUtilities.pi2;
		}

		public float Angle180(Vec2 vector)
		{
			var vec = this.Normalize();
			vector = vector.Normalize();
			return ((float)Math.Atan2((vec.x*vector.y)-(vec.y*vector.x), (vec.x*vector.x)+(vec.y*vector.y))) % MathUtilities.pi2;
		}

		public float Angle360()
		{
			var vec = this.Normalize();
			float value = ((float)Math.Atan2(-vec.y, vec.x)) % MathUtilities.pi2;
			return (value < 0) ? ((MathUtilities.pi+value)+MathUtilities.pi) : value;
		}

		public float Angle360(Vec2 vector)
		{
			var vec = this.Normalize();
			vector = vector.Normalize();
			float value = ((float)Math.Atan2((vec.x*vector.y)-(vec.y*vector.x), (vec.x*vector.x)+(vec.y*vector.y))) % MathUtilities.pi2;
			return (value < 0) ? ((MathUtilities.pi+value)+MathUtilities.pi) : value;
		}

		public static Vec2 Lerp(Vec2 start, Vec2 end, float interpolationAmount)
		{
			float startAmount = 1 - interpolationAmount;
			return new Vec2
			(
				(start.x * startAmount) + (end.x * interpolationAmount),
				(start.y * startAmount) + (end.y * interpolationAmount)
			);
		}
		#endregion
	}
}