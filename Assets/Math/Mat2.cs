using System.Runtime.InteropServices;

namespace UnityMathReference
{
	[StructLayout(LayoutKind.Sequential)]
	public struct Mat2
	{
		#region Properties
		public Vec2 x, y;

		public Vec2 right {get{return x;}}
		public Vec2 left {get{return -x;}}
		public Vec2 up {get{return y;}}
		public Vec2 down {get{return -y;}}

		public static readonly Mat2 zero = new Mat2();
		#endregion

		#region Constructors
		public Mat2(float value)
		{
			x = new Vec2(value);
			y = new Vec2(value);
		}

		public Mat2(Vec2 x, Vec2 y)
		{
			this.x = x;
			this.y = y;
		}

		public static Mat2 FromCross(Vec2 xVector)
		{
			return new Mat2(xVector, new Vec2(-xVector.y, xVector.x));
		}

		public static readonly Mat2 identity = new Mat2
		(
		    new Vec2(1, 0),
		    new Vec2(0, 1)
		);
		#endregion

		#region Operators
		// +
		public static Mat2 operator+(Mat2 p1, Mat2 p2)
		{
			p1.x += p2.x;
			p1.y += p2.y;
			return p1;
		}

		public static Mat2 operator+(Mat2 p1, Vec2 p2)
		{
			p1.x += p2;
			p1.y += p2;
			return p1;
		}

		public static Mat2 operator+(Vec2 p1, Mat2 p2)
		{
			p2.x = p1 + p2.x;
			p2.y = p1 + p2.y;
			return p2;
		}

		public static Mat2 operator+(Mat2 p1, float p2)
		{
			p1.x += p2;
			p1.y += p2;
			return p1;
		}

		public static Mat2 operator+(float p1, Mat2 p2)
		{
			p2.x = p1 + p2.x;
			p2.y = p1 + p2.y;
			return p2;
		}

		// -
		public static Mat2 operator-(Mat2 p1, Mat2 p2)
		{
			p1.x -= p2.x;
			p1.y -= p2.y;
			return p1;
		}

		public static Mat2 operator-(Mat2 p1, Vec2 p2)
		{
			p1.x -= p2;
			p1.y -= p2;
			return p1;
		}

		public static Mat2 operator-(Vec2 p1, Mat2 p2)
		{
			p2.x = p1 - p2.x;
			p2.y = p1 - p2.y;
			return p2;
		}

		public static Mat2 operator-(Mat2 p1, float p2)
		{
			p1.x -= p2;
			p1.y -= p2;
			return p1;
		}

		public static Mat2 operator-(float p1, Mat2 p2)
		{
			p2.x = p1 - p2.x;
			p2.y = p1 - p2.y;
			return p2;
		}

		public static Mat2 operator-(Mat2 p2)
		{
			p2.x = -p2.x;
			p2.y = -p2.y;
			return p2;
		}

		// *
		public static Mat2 operator*(Mat2 p1, Mat2 p2)
		{
			p1.x *= p2.x;
			p1.y *= p2.y;
			return p1;
		}

		public static Mat2 operator*(Mat2 p1, Vec2 p2)
		{
			p1.x *= p2;
			p1.y *= p2;
			return p1;
		}

		public static Mat2 operator*(Vec2 p1, Mat2 p2)
		{
			p2.x = p1 * p2.x;
			p2.y = p1 * p2.y;
			return p2;
		}

		public static Mat2 operator*(Mat2 p1, float p2)
		{
			p1.x *= p2;
			p1.y *= p2;
			return p1;
		}

		public static Mat2 operator*(float p1, Mat2 p2)
		{
			p2.x = p1 * p2.x;
			p2.y = p1 * p2.y;
			return p2;
		}

		// /
		public static Mat2 operator/(Mat2 p1, Mat2 p2)
		{
			p1.x /= p2.x;
			p1.y /= p2.y;
			return p1;
		}

		public static Mat2 operator/(Mat2 p1, Vec2 p2)
		{
			p1.x /= p2;
			p1.y /= p2;
			return p1;
		}

		public static Mat2 operator/(Vec2 p1, Mat2 p2)
		{
			p2.x = p1 / p2.x;
			p2.y = p1 / p2.y;
			return p2;
		}

		public static Mat2 operator/(Mat2 p1, float p2)
		{
			p1.x /= p2;
			p1.y /= p2;
			return p1;
		}

		public static Mat2 operator/(float p1, Mat2 p2)
		{
			p2.x = p1 / p2.x;
			p2.y = p1 / p2.y;
			return p2;
		}

		// ==
		public static bool operator==(Mat2 p1, Mat2 p2) {return (p1.x==p2.x && p1.y==p2.y);}
		public static bool operator!=(Mat2 p1, Mat2 p2) {return (p1.x!=p2.x || p1.y!=p2.y);}
		#endregion
		
		#region Methods
		public override bool Equals(object obj)
		{
			return obj != null && (Mat2)obj == this;
		}

		public override string ToString()
		{
			return string.Format("{0} : {1}", x, y);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public Mat2 Abs()
		{
			return new Mat2(x.Abs(), y.Abs());
		}

		public Mat2 Transpose()
		{
			return new Mat2
			(
				new Vec2(x.x, y.x),
				new Vec2(x.y, y.y)
			);
		}

		public Mat2 Multiply(Mat2 matrix)
		{
			return new Mat2
			(
				new Vec2((matrix.x.x*x.x) + (matrix.x.y*y.x), (matrix.x.x*x.y) + (matrix.x.y*y.y)),
				new Vec2((matrix.y.x*x.x) + (matrix.y.y*y.x), (matrix.y.x*x.y) + (matrix.y.y*y.y))
			);
		}

		public float Determinant()
		{
			return x.x * y.y - x.y * y.x;
		}

		public Mat2 Invert()
        {
            float determinant = 1 / (x.x * y.y - x.y * y.x);
			Mat2 result;
            result.x.x = y.y * determinant;
            result.x.y = -x.y * determinant;

            result.y.x = -y.x * determinant;
            result.y.y = x.x * determinant;

			return result;
        }
		#endregion
	}

	#if MATH_UNITY_HELPER
	public static class Mat2Ext
	{
		public static Mat2 ToMat3x2(this UnityEngine.Matrix4x4 self)
		{
			return new Mat2
			(
				new Vec2(self.m00, self.m01),
				new Vec2(self.m10, self.m11)
			);
		}
	}
	#endif
}