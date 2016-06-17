namespace Reign
{
	public struct Mat3x2
	{
		#region Properties
		public Vec2 x, y, z;
		#endregion

		#region Operators
		// +
		public static Mat3x2 operator+(Mat3x2 p1, Mat3x2 p2)
		{
			p1.x += p2.x;
			p1.y += p2.y;
			p1.z += p2.z;
			return p1;
		}

		public static Mat3x2 operator+(Mat3x2 p1, Vec2 p2)
		{
			p1.x += p2;
			p1.y += p2;
			p1.z += p2;
			return p1;
		}

		public static Mat3x2 operator+(Vec2 p1, Mat3x2 p2)
		{
			p2.x = p1 + p2.x;
			p2.y = p1 + p2.y;
			p2.z = p1 + p2.z;
			return p2;
		}

		public static Mat3x2 operator+(Mat3x2 p1, float p2)
		{
			p1.x += p2;
			p1.y += p2;
			p1.z += p2;
			return p1;
		}

		public static Mat3x2 operator+(float p1, Mat3x2 p2)
		{
			p2.x = p1 + p2.x;
			p2.y = p1 + p2.y;
			p2.z = p1 + p2.z;
			return p2;
		}

		// -
		public static Mat3x2 operator-(Mat3x2 p1, Mat3x2 p2)
		{
			p1.x -= p2.x;
			p1.y -= p2.y;
			p1.z -= p2.z;
			return p1;
		}

		public static Mat3x2 operator-(Mat3x2 p1, Vec2 p2)
		{
			p1.x -= p2;
			p1.y -= p2;
			p1.z -= p2;
			return p1;
		}

		public static Mat3x2 operator-(Vec2 p1, Mat3x2 p2)
		{
			p2.x = p1 - p2.x;
			p2.y = p1 - p2.y;
			p2.z = p1 - p2.z;
			return p2;
		}

		public static Mat3x2 operator-(Mat3x2 p1, float p2)
		{
			p1.x -= p2;
			p1.y -= p2;
			p1.z -= p2;
			return p1;
		}

		public static Mat3x2 operator-(float p1, Mat3x2 p2)
		{
			p2.x = p1 - p2.x;
			p2.y = p1 - p2.y;
			p2.z = p1 - p2.z;
			return p2;
		}

		public static Mat3x2 operator-(Mat3x2 p2)
		{
			p2.x = -p2.x;
			p2.y = -p2.y;
			p2.z = -p2.z;
			return p2;
		}

		// *
		public static Mat3x2 operator*(Mat3x2 p1, Mat3x2 p2)
		{
			p1.x *= p2.x;
			p1.y *= p2.y;
			p1.z *= p2.z;
			return p1;
		}

		public static Mat3x2 operator*(Mat3x2 p1, Vec2 p2)
		{
			p1.x *= p2;
			p1.y *= p2;
			p1.z *= p2;
			return p1;
		}

		public static Mat3x2 operator*(Vec2 p1, Mat3x2 p2)
		{
			p2.x = p1 * p2.x;
			p2.y = p1 * p2.y;
			p2.z = p1 * p2.z;
			return p2;
		}

		public static Mat3x2 operator*(Mat3x2 p1, float p2)
		{
			p1.x *= p2;
			p1.y *= p2;
			p1.z *= p2;
			return p1;
		}

		public static Mat3x2 operator*(float p1, Mat3x2 p2)
		{
			p2.x = p1 * p2.x;
			p2.y = p1 * p2.y;
			p2.z = p1 * p2.z;
			return p2;
		}

		// /
		public static Mat3x2 operator/(Mat3x2 p1, Mat3x2 p2)
		{
			p1.x /= p2.x;
			p1.y /= p2.y;
			p1.z /= p2.z;
			return p1;
		}

		public static Mat3x2 operator/(Mat3x2 p1, Vec2 p2)
		{
			p1.x /= p2;
			p1.y /= p2;
			p1.z /= p2;
			return p1;
		}

		public static Mat3x2 operator/(Vec2 p1, Mat3x2 p2)
		{
			p2.x = p1 / p2.x;
			p2.y = p1 / p2.y;
			p2.z = p1 / p2.z;
			return p2;
		}

		public static Mat3x2 operator/(Mat3x2 p1, float p2)
		{
			p1.x /= p2;
			p1.y /= p2;
			p1.z /= p2;
			return p1;
		}

		public static Mat3x2 operator/(float p1, Mat3x2 p2)
		{
			p2.x = p1 / p2.x;
			p2.y = p1 / p2.y;
			p2.z = p1 / p2.z;
			return p2;
		}

		// ==
		public static bool operator==(Mat3x2 p1, Mat3x2 p2) {return (p1.x==p2.x && p1.y==p2.y && p1.z==p2.z);}
		public static bool operator!=(Mat3x2 p1, Mat3x2 p2) {return (p1.x!=p2.x || p1.y!=p2.y || p1.z!=p2.z);}
		#endregion

		#region Methods
		public Mat2x3 transpose(Mat3x2 matrix)
        {
			Mat2x3 result;
            result.x.x = matrix.x.x;
            result.x.y = matrix.y.x;
            result.x.z = matrix.z.x;

            result.y.x = matrix.x.y;
            result.y.y = matrix.y.y;
            result.y.z = matrix.z.y;
			return result;
        }
		#endregion
	}
}