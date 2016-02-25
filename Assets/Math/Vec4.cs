using System;

namespace Reign
{
	public struct Vec4
	{
		#region Properties
		public float x, y, z, w;

		public float R
		{
			get {return x;}
			set {x = value;}
		}

		public float G
		{
			get {return y;}
			set {y = value;}
		}

		public float B
		{
			get {return z;}
			set {z = value;}
		}

		public float A
		{
			get {return w;}
			set {w = value;}
		}
		#endregion

		#region Constructors
		public Vec4(float value)
		{
			x = value;
			y = value;
			z = value;
			w = value;
		}

		public Vec4(float x, float y, float z, float w)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}

		public Vec4(Vec2 vector, float z, float w)
		{
			x = vector.x;
			y = vector.y;
			this.z = z;
			this.w = w;
		}

		public Vec4(Vec3 vector, float w)
		{
			x = vector.x;
			y = vector.y;
			z = vector.z;
			this.w = w;
		}

		public static implicit operator Vec4(UnityEngine.Vector4 vec)
		{
			return new Vec4(vec.x, vec.y, vec.z, vec.w);
		}

		public static readonly Vec4 one = new Vec4(1);
		public static readonly Vec4 minusOne = new Vec4(-1);
		public static readonly Vec4 zero = new Vec4(0);
		public static readonly Vec4 right = new Vec4(1, 0, 0, 0);
		public static readonly Vec4 left = new Vec4(-1, 0, 0, 0);
		public static readonly Vec4 up = new Vec4(0, 1, 0, 0);
		public static readonly Vec4 down = new Vec4(0, -1, 0, 0);
		public static readonly Vec4 forward = new Vec4(0, 0, 1, 0);
		public static readonly Vec4 backward = new Vec4(0, 0, -1, 0);
		public static readonly Vec4 high = new Vec4(0, 0, 0, 1);
		public static readonly Vec4 low = new Vec4(0, 0, 0, -1);
		#endregion

		#region Operators
		// +
		public static Vec4 operator+(Vec4 p1, Vec4 p2)
		{
			p1.x += p2.x;
			p1.y += p2.y;
			p1.z += p2.z;
			p1.w += p2.w;
			return p1;
		}

		public static Vec4 operator+(Vec4 p1, Vec3 p2)
		{
			p1.x += p2.x;
			p1.y += p2.y;
			p1.z += p2.z;
			return p1;
		}

		public static Vec4 operator+(Vec3 p1, Vec4 p2)
		{
			p2.x = p1.x + p2.x;
			p2.y = p1.y + p2.y;
			p2.z = p1.z + p2.z;
			return p2;
		}

		public static Vec4 operator+(Vec4 p1, Vec2 p2)
		{
			p1.x += p2.x;
			p1.y += p2.y;
			return p1;
		}

		public static Vec4 operator+(Vec2 p1, Vec4 p2)
		{
			p2.x = p1.x + p2.x;
			p2.y = p1.y + p2.y;
			return p2;
		}

		public static Vec4 operator+(Vec4 p1, float p2)
		{
			p1.x += p2;
			p1.y += p2;
			p1.z += p2;
			p1.w += p2;
			return p1;
		}

		public static Vec4 operator+(float p1, Vec4 p2)
		{
			p2.x = p1 + p2.x;
			p2.y = p1 + p2.y;
			p2.z = p1 + p2.z;
			p2.w = p1 + p2.w;
			return p2;
		}

		// -
		public static Vec4 operator-(Vec4 p1, Vec4 p2)
		{
			p1.x -= p2.x;
			p1.y -= p2.y;
			p1.z -= p2.z;
			p1.w -= p2.w;
			return p1;
		}

		public static Vec4 operator-(Vec4 p1, Vec3 p2)
		{
			p1.x -= p2.x;
			p1.y -= p2.y;
			p1.z -= p2.z;
			return p1;
		}

		public static Vec4 operator-(Vec3 p1, Vec4 p2)
		{
			p2.x = p1.x - p2.x;
			p2.y = p1.y - p2.y;
			p2.z = p1.z - p2.z;
			return p2;
		}

		public static Vec4 operator-(Vec4 p1, Vec2 p2)
		{
			p1.x -= p2.x;
			p1.y -= p2.y;
			return p1;
		}

		public static Vec4 operator-(Vec2 p1, Vec4 p2)
		{
			p2.x = p1.x - p2.x;
			p2.y = p1.y - p2.y;
			return p2;
		}

		public static Vec4 operator-(Vec4 p1, float p2)
		{
			p1.x -= p2;
			p1.y -= p2;
			p1.z -= p2;
			p1.w -= p2;
			return p1;
		}

		public static Vec4 operator-(float p1, Vec4 p2)
		{
			p2.x = p1 - p2.x;
			p2.y = p1 - p2.y;
			p2.z = p1 - p2.z;
			p2.w = p1 - p2.w;
			return p2;
		}

		public static Vec4 operator-(Vec4 p2)
		{
			p2.x = -p2.x;
			p2.y = -p2.y;
			p2.z = -p2.z;
			p2.w = -p2.w;
			return p2;
		}

		// *
		public static Vec4 operator*(Vec4 p1, Vec4 p2)
		{
			p1.x *= p2.x;
			p1.y *= p2.y;
			p1.z *= p2.z;
			p1.w *= p2.w;
			return p1;
		}

		public static Vec4 operator*(Vec4 p1, Vec3 p2)
		{
			p1.x *= p2.x;
			p1.y *= p2.y;
			p1.z *= p2.z;
			return p1;
		}

		public static Vec4 operator*(Vec3 p1, Vec4 p2)
		{
			p2.x = p1.x * p2.x;
			p2.y = p1.y * p2.y;
			p2.z = p1.z * p2.z;
			return p2;
		}

		public static Vec4 operator*(Vec4 p1, Vec2 p2)
		{
			p1.x *= p2.x;
			p1.y *= p2.y;
			return p1;
		}

		public static Vec4 operator*(Vec2 p1, Vec4 p2)
		{
			p2.x = p1.x * p2.x;
			p2.y = p1.y * p2.y;
			return p2;
		}

		public static Vec4 operator*(Vec4 p1, float p2)
		{
			p1.x *= p2;
			p1.y *= p2;
			p1.z *= p2;
			p1.w *= p2;
			return p1;
		}

		public static Vec4 operator*(float p1, Vec4 p2)
		{
			p2.x = p1 * p2.x;
			p2.y = p1 * p2.y;
			p2.z = p1 * p2.z;
			p2.w = p1 * p2.w;
			return p2;
		}

		// /
		public static Vec4 operator/(Vec4 p1, Vec4 p2)
		{
			p1.x /= p2.x;
			p1.y /= p2.y;
			p1.z /= p2.z;
			p1.w /= p2.w;
			return p1;
		}

		public static Vec4 operator/(Vec4 p1, Vec3 p2)
		{
			p1.x /= p2.x;
			p1.y /= p2.y;
			p1.z /= p2.z;
			return p1;
		}

		public static Vec4 operator/(Vec3 p1, Vec4 p2)
		{
			p2.x = p1.x / p2.x;
			p2.y = p1.y / p2.y;
			p2.z = p1.z / p2.z;
			return p2;
		}

		public static Vec4 operator/(Vec4 p1, Vec2 p2)
		{
			p1.x /= p2.x;
			p1.y /= p2.y;
			return p1;
		}

		public static Vec4 operator/(Vec2 p1, Vec4 p2)
		{
			p2.x = p1.x / p2.x;
			p2.y = p1.y / p2.y;
			return p2;
		}

		public static Vec4 operator/(Vec4 p1, float p2)
		{
			p1.x /= p2;
			p1.y /= p2;
			p1.z /= p2;
			p1.w /= p2;
			return p1;
		}

		public static Vec4 operator/(float p1, Vec4 p2)
		{
			p2.x = p1 / p2.x;
			p2.y = p1 / p2.y;
			p2.z = p1 / p2.z;
			p2.w = p1 / p2.w;
			return p2;
		}

		// ==
		public static bool operator==(Vec4 p1, Vec4 p2) {return (p1.x==p2.x && p1.y==p2.y && p1.z==p2.z && p1.w==p2.w);}
		public static bool operator!=(Vec4 p1, Vec4 p2) {return (p1.x!=p2.x || p1.y!=p2.y || p1.z!=p2.z || p1.w!=p2.w);}

		// convert
		public Vec2 ToVector2()
		{
			return new Vec2(x, y);
		}

		public Vec3 ToVector3()
		{
			return new Vec3(x, y, z);
		}

		public Color4 ToColor4()
		{
			return new Color4((byte)(x*255), (byte)(y*255), (byte)(z*255), (byte)(w*255));
		}
		#endregion

		#region Methods
		public override bool Equals(object obj)
		{
			return obj != null && (Vec4)obj == this;
		}

		public override string ToString()
		{
			return string.Format("<{0}, {1}, {2}, {3}>", x, y, z, w);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public Vec4 Max(float value)
		{
			return new Vec4(Math.Max(x, value), Math.Max(y, value), Math.Max(z, value), Math.Max(w, value));
		}

		public Vec4 Max(Vec4 values)
		{
			return new Vec4(Math.Max(x, values.x), Math.Max(y, values.y), Math.Max(z, values.z), Math.Max(w, values.w));
		}

		public Vec4 Min(float value)
		{
			return new Vec4(Math.Min(x, value), Math.Min(y, value), Math.Min(z, value), Math.Min(w, value));
		}

		public Vec4 Min(Vec4 values)
		{
			return new Vec4(Math.Min(x, values.x), Math.Min(y, values.y), Math.Min(z, values.z), Math.Min(w, values.w));
		}

		public Vec4 Abs()
		{
			return new Vec4(Math.Abs(x), Math.Abs(y), Math.Abs(z), Math.Abs(w));
		}

		public Vec4 Pow(float power)
		{
			return new Vec4((float)Math.Pow(x, power), (float)Math.Pow(y, power), (float)Math.Pow(z, power), (float)Math.Pow(w, power));
		}

		public Vec4 Floor()
		{
			return new Vec4((float)Math.Floor(x), (float)Math.Floor(y), (float)Math.Floor(z), (float)Math.Floor(w));
		}

		public Vec4 Round()
		{
			return new Vec4((float)Math.Round(x), (float)Math.Round(y), (float)Math.Round(z), (float)Math.Round(w));
		}

		public float Length()
		{
			return (float)Math.Sqrt((x*x) + (y*y) + (z*z) + (w*w));
		}

		public float LengthSquared()
		{
			return (x*x) + (y*y) + (z*z) + (w*w);
		}
		
		public float Distance(Vec4 vector)
		{
			return (vector - this).Length();
		}
		
		public float DistanceSquared(Vec4 vector)
		{
			return (vector - this).Dot();
		}

		public float Dot()
		{
			return (x*x) + (y*y) + (z*z) + (w*w);
		}

		public float Dot(Vec4 vector)
		{
			return (x*vector.x) + (y*vector.y) + (z*vector.z) + (w*vector.w);
		}

		public Vec4 Normalize()
		{
			return this * (1 / (float)Math.Sqrt((x*x) + (y*y) + (z*z) + (w*w)));
		}

		public Vec4 Normalize(out float length)
		{
			float dis = (float)Math.Sqrt((x*x) + (y*y) + (z*z) + (w*w));
			length = dis;
			return this * (1/dis);
		}

		public Vec4 NormalizeSafe()
		{
			float dis = (float)Math.Sqrt((x*x) + (y*y) + (z*z) + (w*w));
			if (dis == 0) return new Vec4();
			else return this * (1/dis);
		}

		public Vec4 NormalizeSafe(out float length)
		{
			float dis = (float)Math.Sqrt((x*x) + (y*y) + (z*z) + (w*w));
			length = dis;
			if (dis == 0) return new Vec4();
			else return this * (1/dis);
		}

		public Vec4 Transform(Mat4 matrix)
		{
			return (matrix.x*x) + (matrix.y*y) + (matrix.z*z) + (matrix.w*w);
		}

		public Vec4 Multiply(Mat4 matrix)
		{
			return new Vec4
			(
				(matrix.x.x*x) + (matrix.x.y*y) + (matrix.x.z*z) + (matrix.x.w*w),
				(matrix.y.x*x) + (matrix.y.y*y) + (matrix.y.z*z) + (matrix.y.w*w),
				(matrix.z.x*x) + (matrix.z.y*y) + (matrix.z.z*z) + (matrix.z.w*w),
				(matrix.w.x*x) + (matrix.w.y*y) + (matrix.w.z*z) + (matrix.w.w*w)
			);
		}

		public bool AproxEqualsBox(Vec4 vector, float tolerance)
		{
			return
				(Math.Abs(x-vector.x) <= tolerance) &&
				(Math.Abs(y-vector.y) <= tolerance) &&
				(Math.Abs(z-vector.z) <= tolerance) &&
				(Math.Abs(w-vector.w) <= tolerance);
		}

		public bool ApproxEquals(Vec4 vector, float tolerance)
		{
			return (Distance(vector) <= tolerance);
		}

		public Vec4 Project(Mat4 projectionMatrix, Mat4 viewMatrix, int viewX, int viewY, int viewWidth, int viewHeight)
		{
			var result = this;
			result = result.Multiply(viewMatrix);
			result = result.Multiply(projectionMatrix);
			
			float wDelta = 1 / result.w;
			result.x *= wDelta;
			result.y *= wDelta;
			result.z *= wDelta;
			
			result.x = (result.x * .5f) + .5f;
			result.y = (result.y * .5f) + .5f;
			result.z = (result.z * .5f) + .5f;

			result.x = (result.x * viewWidth) + viewX;
			result.y = (result.y * viewHeight) + viewY;

			return result;
		}

		public Vec4 UnProject(Mat4 viewProjInverse, int viewX, int viewY, int viewWidth, int viewHeight)
		{
			var result = this;
			result.x = (result.x - viewX) / viewWidth;
			result.y = (result.y - viewY) / viewHeight;
			result = (result * 2) - 1;

			result = result.Transform(viewProjInverse);
			float wDelta = 1 / result.w;
			result.x *= wDelta;
			result.y *= wDelta;
			result.z *= wDelta;
			
			return result;
		}

		public Vec4 UnProject(Mat4 projectionMatrix, Mat4 viewMatrix, int viewX, int viewY, int viewWidth, int viewHeight)
		{
			var viewProjInverse = viewMatrix.Multiply(projectionMatrix).Invert();
			
			var result = this;
			result.x = (result.x - viewX) / viewWidth;
			result.y = (result.y - viewY) / viewHeight;
			result = (result * 2) - 1;

			result = result.Transform(viewProjInverse);
			float wDelta = 1 / result.w;
			result.x *= wDelta;
			result.y *= wDelta;
			result.z *= wDelta;
			
			return result;
		}
		#endregion
	}
}