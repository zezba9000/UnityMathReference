using System.Runtime.InteropServices;

namespace UnityMathReference
{
	[StructLayout(LayoutKind.Sequential)]
	public struct Mat2x3
	{
		#region Properties
		public Vec3 x, y;
		#endregion

		#region Constructors
		public Mat2x3(float value)
		{
			x = new Vec3(value);
			y = new Vec3(value);
		}

		public Mat2x3(Vec3 x, Vec3 y)
		{
			this.x = x;
			this.y = y;
		}
		#endregion

		#region Operators
		// +
		public static Mat2x3 operator+(Mat2x3 p1, Mat2x3 p2)
		{
			p1.x += p2.x;
			p1.y += p2.y;
			return p1;
		}

		public static Mat2x3 operator+(Mat2x3 p1, Vec3 p2)
		{
			p1.x += p2;
			p1.y += p2;
			return p1;
		}

		public static Mat2x3 operator+(Vec3 p1, Mat2x3 p2)
		{
			p2.x = p1 + p2.x;
			p2.y = p1 + p2.y;
			return p2;
		}

		public static Mat2x3 operator+(Mat2x3 p1, float p2)
		{
			p1.x += p2;
			p1.y += p2;
			return p1;
		}

		public static Mat2x3 operator+(float p1, Mat2x3 p2)
		{
			p2.x = p1 + p2.x;
			p2.y = p1 + p2.y;
			return p2;
		}

		// -
		public static Mat2x3 operator-(Mat2x3 p1, Mat2x3 p2)
		{
			p1.x -= p2.x;
			p1.y -= p2.y;
			return p1;
		}

		public static Mat2x3 operator-(Mat2x3 p1, Vec3 p2)
		{
			p1.x -= p2;
			p1.y -= p2;
			return p1;
		}

		public static Mat2x3 operator-(Vec3 p1, Mat2x3 p2)
		{
			p2.x = p1 - p2.x;
			p2.y = p1 - p2.y;
			return p2;
		}

		public static Mat2x3 operator-(Mat2x3 p1, float p2)
		{
			p1.x -= p2;
			p1.y -= p2;
			return p1;
		}

		public static Mat2x3 operator-(float p1, Mat2x3 p2)
		{
			p2.x = p1 - p2.x;
			p2.y = p1 - p2.y;
			return p2;
		}

		public static Mat2x3 operator-(Mat2x3 p2)
		{
			p2.x = -p2.x;
			p2.y = -p2.y;
			return p2;
		}

		// *
		public static Mat2x3 operator*(Mat2x3 p1, Mat2x3 p2)
		{
			p1.x *= p2.x;
			p1.y *= p2.y;
			return p1;
		}

		public static Mat2x3 operator*(Mat2x3 p1, Vec3 p2)
		{
			p1.x *= p2;
			p1.y *= p2;
			return p1;
		}

		public static Mat2x3 operator*(Vec3 p1, Mat2x3 p2)
		{
			p2.x = p1 * p2.x;
			p2.y = p1 * p2.y;
			return p2;
		}

		public static Mat2x3 operator*(Mat2x3 p1, float p2)
		{
			p1.x *= p2;
			p1.y *= p2;
			return p1;
		}

		public static Mat2x3 operator*(float p1, Mat2x3 p2)
		{
			p2.x = p1 * p2.x;
			p2.y = p1 * p2.y;
			return p2;
		}

		// /
		public static Mat2x3 operator/(Mat2x3 p1, Mat2x3 p2)
		{
			p1.x /= p2.x;
			p1.y /= p2.y;
			return p1;
		}

		public static Mat2x3 operator/(Mat2x3 p1, Vec3 p2)
		{
			p1.x /= p2;
			p1.y /= p2;
			return p1;
		}

		public static Mat2x3 operator/(Vec3 p1, Mat2x3 p2)
		{
			p2.x = p1 / p2.x;
			p2.y = p1 / p2.y;
			return p2;
		}

		public static Mat2x3 operator/(Mat2x3 p1, float p2)
		{
			p1.x /= p2;
			p1.y /= p2;
			return p1;
		}

		public static Mat2x3 operator/(float p1, Mat2x3 p2)
		{
			p2.x = p1 / p2.x;
			p2.y = p1 / p2.y;
			return p2;
		}

		// ==
		public static bool operator==(Mat2x3 p1, Mat2x3 p2) {return (p1.x==p2.x && p1.y==p2.y);}
		public static bool operator!=(Mat2x3 p1, Mat2x3 p2) {return (p1.x!=p2.x || p1.y!=p2.y);}

		public override bool Equals(object obj)
		{
			return (Mat2x3)obj == this;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
		#endregion

		#region Methods
		public static void Multiply(ref Mat2x3 matrix1, ref Mat3 matrix2, out Mat2x3 result)
        {
            result.x.x = (matrix1.x.x * matrix2.x.x) + (matrix1.x.y * matrix2.y.x) + (matrix1.x.z * matrix2.z.x);
            result.x.y = (matrix1.x.x * matrix2.x.y) + (matrix1.x.y * matrix2.y.y) + (matrix1.x.z * matrix2.z.y);
            result.x.z = (matrix1.x.x * matrix2.x.z) + (matrix1.x.y * matrix2.y.z) + (matrix1.x.z * matrix2.z.z);

            result.y.x = (matrix1.y.x * matrix2.x.x) + (matrix1.y.y * matrix2.y.x) + (matrix1.y.z * matrix2.z.x);
            result.y.y = (matrix1.y.x * matrix2.x.y) + (matrix1.y.y * matrix2.y.y) + (matrix1.y.z * matrix2.z.y);
            result.y.z = (matrix1.y.x * matrix2.x.z) + (matrix1.y.y * matrix2.y.z) + (matrix1.y.z * matrix2.z.z);
        }

		public static void Multiply(ref Mat2x3 matrix1, ref Mat3x2 matrix2, out Mat2 result)
        {
            result.x.x = (matrix1.x.x * matrix2.x.x) + (matrix1.x.y * matrix2.y.x) + (matrix1.x.z * matrix2.z.x);
            result.x.y = (matrix1.x.x * matrix2.x.y) + (matrix1.x.y * matrix2.y.y) + (matrix1.x.z * matrix2.z.y);

            result.y.x = (matrix1.y.x * matrix2.x.x) + (matrix1.y.y * matrix2.y.x) + (matrix1.y.z * matrix2.z.x);
            result.y.y = (matrix1.y.x * matrix2.x.y) + (matrix1.y.y * matrix2.y.y) + (matrix1.y.z * matrix2.z.y);
        }

		public static void Transpose(Mat2x3 matrix, out Mat3x2 result)
        {
            result.x.x = matrix.x.x;
            result.x.y = matrix.y.x;

            result.y.x = matrix.x.y;
            result.y.y = matrix.y.y;

            result.z.x = matrix.x.z;
            result.z.y = matrix.y.z;
        }
		#endregion
	}

	#if MATH_UNITY_HELPER
	public static class Mat2x3Ext
	{
		public static Mat2x3 ToMat2x3(this UnityEngine.Matrix4x4 self)
		{
			return new Mat2x3
			(
				new Vec3(self.m00, self.m01, self.m02),
				new Vec3(self.m10, self.m11, self.m12)
			);
		}
	}
	#endif
}