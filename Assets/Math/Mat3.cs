using System;
using System.Runtime.InteropServices;

namespace UnityMathReference
{
	[StructLayout(LayoutKind.Sequential)]
	public struct Mat3
	{
		#region Properties
		public Vec3 x, y, z;

		public Vec3 right {get{return x;}}
		public Vec3 left {get{return -x;}}
		public Vec3 up {get{return y;}}
		public Vec3 down {get{return -y;}}
		public Vec3 front {get{return z;}}
		public Vec3 back {get{return -z;}}

		public static readonly Mat3 zero = new Mat3();
		#endregion

		#region Constructors
		public Mat3(float value)
		{
			x = new Vec3(value);
			y = new Vec3(value);
			z = new Vec3(value);
		}

		public Mat3(Vec3 x, Vec3 y, Vec3 z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		public static Mat3 FromScale(float scale)
		{
			return new Mat3
			(
				new Vec3(scale, 0, 0),
				new Vec3(0, scale, 0),
				new Vec3(0, 0, scale)
			);
		}

		public static void FromScale(float scale, out Mat3 result)
		{
			result.x = new Vec3(scale, 0, 0);
			result.y = new Vec3(0, scale, 0);
			result.z = new Vec3(0, 0, scale);
		}

		public static Mat3 FromScale(Vec3 scale)
		{
			return new Mat3
			(
				new Vec3(scale.x, 0, 0),
				new Vec3(0, scale.y, 0),
				new Vec3(0, 0, scale.z)
			);
		}

		public static void FromScale(Vec3 scale, out Mat3 result)
		{
			result.x = new Vec3(scale.x, 0, 0);
			result.y = new Vec3(0, scale.y, 0);
			result.z = new Vec3(0, 0, scale.z);
		}

		public static Mat3 FromOuterProduct(Vec3 vector1, Vec3 vector2)
        {
			return new Mat3
			(
				new Vec3(vector1.x * vector2.x, vector1.x * vector2.y, vector1.x * vector2.z),
				new Vec3(vector1.y * vector2.x, vector1.y * vector2.y, vector1.y * vector2.z),
				new Vec3(vector1.z * vector2.x, vector1.z * vector2.y, vector1.z * vector2.z)
			);
        }

		public static Mat3 FromEuler(Vec3 euler)
		{
			float cx = (float)System.Math.Cos(euler.x);
			float sx = (float)System.Math.Sin(euler.x);
			float cy = (float)System.Math.Cos(euler.y);
			float sy = (float)System.Math.Sin(euler.y);
			float cz = (float)System.Math.Cos(euler.z);
			float sz = (float)System.Math.Sin(euler.z);

			// multiply ZYX
			return new Mat3
			(
			    new Vec3(cy*cz, cz*sx*sy - cx*sz, cx*cz*sy + sx*sz),
			    new Vec3(cy*sz, cx*cz + sx*sy*sz, -cz*sx + cx*sy*sz),
			    new Vec3(-sy, cy*sx, cx*cy)
			);
		}

		public static Mat3 FromEuler(float eulerX, float eulerY, float eulerZ)
		{
			float cx = (float)System.Math.Cos(eulerX);
			float sx = (float)System.Math.Sin(eulerX);
			float cy = (float)System.Math.Cos(eulerY);
			float sy = (float)System.Math.Sin(eulerY);
			float cz = (float)System.Math.Cos(eulerZ);
			float sz = (float)System.Math.Sin(eulerZ);

			// multiply ZYX
			return new Mat3
			(
			    new Vec3(cy*cz, cz*sx*sy - cx*sz, cx*cz*sy + sx*sz),
			    new Vec3(cy*sz, cx*cz + sx*sy*sz, -cz*sx + cx*sy*sz),
			    new Vec3(-sy, cy*sx, cx*cy)
			);
		}

		public static Mat3 FromQuaternion(Quat quaternion)
		{
			var squared = new Vec4(quaternion.x*quaternion.x, quaternion.y*quaternion.y, quaternion.z*quaternion.z, quaternion.w*quaternion.w);
			float invSqLength = 1 / (squared.x + squared.y + squared.z + squared.w);

			float temp1 = quaternion.x * quaternion.y;
			float temp2 = quaternion.z * quaternion.w;
			float temp3 = quaternion.x * quaternion.z;
			float temp4 = quaternion.y * quaternion.w;
			float temp5 = quaternion.y * quaternion.z;
			float temp6 = quaternion.x * quaternion.w;

			return new Mat3
			(
				new Vec3((squared.x-squared.y-squared.z+squared.w) * invSqLength, 2*(temp1-temp2) * invSqLength, 2*(temp3+temp4) * invSqLength),
				new Vec3(2*(temp1+temp2) * invSqLength, (-squared.x+squared.y-squared.z+squared.w) * invSqLength, 2*(temp5-temp6) * invSqLength),
				new Vec3(2*(temp3-temp4) * invSqLength, 2*(temp5+temp6) * invSqLength, (-squared.x-squared.y+squared.z+squared.w) * invSqLength)
			);
		}

		public Mat3 FromRotationAxis(Vec3 axis, float angle)
		{
			Quat quaternion = Quat.FromRotationAxis(axis, angle);
			return FromQuaternion(quaternion);
		}

		public static Mat3 LookAt(Vec3 forward, Vec3 up)
		{
			var Z = forward.Normalize();
			var X = up.Cross(Z).Normalize();
			var Y = Z.Cross(X);

			return new Mat3(X, Y, Z);
		}

		public static Mat3 FromCross(Vec3 vector)
        {
			Mat3 result;
            result.x.x = 0;
            result.x.y = -vector.z;
            result.x.z = vector.y;

            result.y.x = vector.z;
            result.y.y = 0;
            result.y.z = -vector.x;

            result.z.x = -vector.y;
            result.z.y = vector.x;
			result.z.z = 0;
			return result;
        }

		public static readonly Mat3 identity = new Mat3
		(
		    new Vec3(1, 0, 0),
		    new Vec3(0, 1, 0),
		    new Vec3(0, 0, 1)
		);
		#endregion

		#region Operators
		// +
		public static Mat3 operator+(Mat3 p1, Mat3 p2)
		{
			p1.x += p2.x;
			p1.y += p2.y;
			p1.z += p2.z;
			return p1;
		}

		public static Mat3 operator+(Mat3 p1, Vec3 p2)
		{
			p1.x += p2;
			p1.y += p2;
			p1.z += p2;
			return p1;
		}

		public static Mat3 operator+(Vec3 p1, Mat3 p2)
		{
			p2.x = p1 + p2.x;
			p2.y = p1 + p2.y;
			p2.z = p1 + p2.z;
			return p2;
		}

		public static Mat3 operator+(Mat3 p1, float p2)
		{
			p1.x += p2;
			p1.y += p2;
			p1.z += p2;
			return p1;
		}

		public static Mat3 operator+(float p1, Mat3 p2)
		{
			p2.x = p1 + p2.x;
			p2.y = p1 + p2.y;
			p2.z = p1 + p2.z;
			return p2;
		}

		// -
		public static Mat3 operator-(Mat3 p1, Mat3 p2)
		{
			p1.x -= p2.x;
			p1.y -= p2.y;
			p1.z -= p2.z;
			return p1;
		}

		public static Mat3 operator-(Mat3 p1, Vec3 p2)
		{
			p1.x -= p2;
			p1.y -= p2;
			p1.z -= p2;
			return p1;
		}

		public static Mat3 operator-(Vec3 p1, Mat3 p2)
		{
			p2.x = p1 - p2.x;
			p2.y = p1 - p2.y;
			p2.z = p1 - p2.z;
			return p2;
		}

		public static Mat3 operator-(Mat3 p1, float p2)
		{
			p1.x -= p2;
			p1.y -= p2;
			p1.z -= p2;
			return p1;
		}

		public static Mat3 operator-(float p1, Mat3 p2)
		{
			p2.x = p1 - p2.x;
			p2.y = p1 - p2.y;
			p2.z = p1 - p2.z;
			return p2;
		}

		public static Mat3 operator-(Mat3 p2)
		{
			p2.x = -p2.x;
			p2.y = -p2.y;
			p2.z = -p2.z;
			return p2;
		}

		// *
		public static Mat3 operator*(Mat3 p1, Mat3 p2)
		{
			p1.x *= p2.x;
			p1.y *= p2.y;
			p1.z *= p2.z;
			return p1;
		}

		public static Mat3 operator*(Mat3 p1, Vec3 p2)
		{
			p1.x *= p2;
			p1.y *= p2;
			p1.z *= p2;
			return p1;
		}

		public static Mat3 operator*(Vec3 p1, Mat3 p2)
		{
			p2.x = p1 * p2.x;
			p2.y = p1 * p2.y;
			p2.z = p1 * p2.z;
			return p2;
		}

		public static Mat3 operator*(Mat3 p1, float p2)
		{
			p1.x *= p2;
			p1.y *= p2;
			p1.z *= p2;
			return p1;
		}

		public static Mat3 operator*(float p1, Mat3 p2)
		{
			p2.x = p1 * p2.x;
			p2.y = p1 * p2.y;
			p2.z = p1 * p2.z;
			return p2;
		}

		// /
		public static Mat3 operator/(Mat3 p1, Mat3 p2)
		{
			p1.x /= p2.x;
			p1.y /= p2.y;
			p1.z /= p2.z;
			return p1;
		}

		public static Mat3 operator/(Mat3 p1, Vec3 p2)
		{
			p1.x /= p2;
			p1.y /= p2;
			p1.z /= p2;
			return p1;
		}

		public static Mat3 operator/(Vec3 p1, Mat3 p2)
		{
			p2.x = p1 / p2.x;
			p2.y = p1 / p2.y;
			p2.z = p1 / p2.z;
			return p2;
		}

		public static Mat3 operator/(Mat3 p1, float p2)
		{
			p1.x /= p2;
			p1.y /= p2;
			p1.z /= p2;
			return p1;
		}

		public static Mat3 operator/(float p1, Mat3 p2)
		{
			p2.x = p1 / p2.x;
			p2.y = p1 / p2.y;
			p2.z = p1 / p2.z;
			return p2;
		}

		// ==
		public static bool operator==(Mat3 p1, Mat3 p2) {return (p1.x==p2.x && p1.y==p2.y && p1.z==p2.z);}
		public static bool operator!=(Mat3 p1, Mat3 p2) {return (p1.x!=p2.x || p1.y!=p2.y || p1.z!=p2.z);}
		#endregion
		
		#region Methods
		public override bool Equals(object obj)
		{
			return obj != null && (Mat3)obj == this;
		}

		public override string ToString()
		{
			return string.Format("{0} : {1} : {2}", x, y, z);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public Vec3 Euler()
		{
			if (z.x < 1)
			{
				if (z.x > -1)
				{
					return new Vec3((float)Math.Atan2(z.y, z.z), (float)Math.Asin(-z.x), (float)Math.Atan2(y.x, x.x));
				}
				else
				{
					return new Vec3(0, MathUtilities.piHalf, -(float)Math.Atan2(y.z, y.y));
				}
			}
			else
			{
				return new Vec3(0, -MathUtilities.piHalf, (float)Math.Atan2(-y.z, y.y));
			}
		}

		public static void Euler(ref Mat3 matrix, out Vec3 result)
		{
			if (matrix.z.x < 1)
			{
				if (matrix.z.x > -1)
				{
					result.x = (float)Math.Atan2(matrix.z.y, matrix.z.z);
					result.y = (float)Math.Asin(-matrix.z.x);
					result.z = (float)Math.Atan2(matrix.y.x, matrix.x.x);
				}
				else
				{
					result.x = 0;
					result.y = MathUtilities.piHalf;
					result.z = -(float)Math.Atan2(matrix.y.z, matrix.y.y);
				}
			}
			else
			{
				result.x = 0;
				result.y = -MathUtilities.piHalf;
				result.z = (float)Math.Atan2(-matrix.y.z, matrix.y.y);
			}
		}
		
		public Mat3 Abs()
		{
			return new Mat3(x.Abs(), y.Abs(), z.Abs());
		}

		public Mat3 Transpose()
		{
			return new Mat3
			(
				new Vec3(x.x, y.x, z.x),
				new Vec3(x.y, y.y, z.y),
				new Vec3(x.z, y.z, z.z)
			);
		}

		public Mat3 Multiply(Mat3 matrix)
		{
			return new Mat3
			(
				new Vec3((matrix.x.x*x.x) + (matrix.x.y*y.x) + (matrix.x.z*z.x), (matrix.x.x*x.y) + (matrix.x.y*y.y) + (matrix.x.z*z.y), (matrix.x.x*x.z) + (matrix.x.y*y.z) + (matrix.x.z*z.z)),
				new Vec3((matrix.y.x*x.x) + (matrix.y.y*y.x) + (matrix.y.z*z.x), (matrix.y.x*x.y) + (matrix.y.y*y.y) + (matrix.y.z*z.y), (matrix.y.x*x.z) + (matrix.y.y*y.z) + (matrix.y.z*z.z)),
				new Vec3((matrix.z.x*x.x) + (matrix.z.y*y.x) + (matrix.z.z*z.x), (matrix.z.x*x.y) + (matrix.z.y*y.y) + (matrix.z.z*z.y), (matrix.z.x*x.z) + (matrix.z.y*y.z) + (matrix.z.z*z.z))
			);
		}

		public Mat3 MultiplyTransposed(Mat3 matrix)
        {
			return new Mat3
			(
				new Vec3
				(
					x.x * matrix.x.x + y.x * matrix.y.x + z.x * matrix.z.x,
					x.x * matrix.x.y + y.x * matrix.y.y + z.x * matrix.z.y,
					x.x * matrix.x.z + y.x * matrix.y.z + z.x * matrix.z.z
				),
				new Vec3
				(
					x.y * matrix.x.x + y.y * matrix.y.x + z.y * matrix.z.x,
					x.y * matrix.x.y + y.y * matrix.y.y + z.y * matrix.z.y,
					x.y * matrix.x.z + y.y * matrix.y.z + z.y * matrix.z.z
				),
				new Vec3
				(
					x.z * matrix.x.x + y.z * matrix.y.x + z.z * matrix.z.x,
					x.z * matrix.x.y + y.z * matrix.y.y + z.z * matrix.z.y,
					x.z * matrix.x.z + y.z * matrix.y.z + z.z * matrix.z.z
				)
			);
        }

		public float Determinant()
        {
            return x.x * y.y * z.z + x.y * y.z * z.x + x.z * y.x * z.y -
                   z.x * y.y * x.z - z.y * y.z * x.x - z.z * y.x * x.y;
        }

		public Mat3 Invert()
        {
            float determinant = 1 / this.Determinant();

			return new Mat3
			(
				new Vec3
				(
					(y.y * z.z - y.z * z.y) * determinant,
					(x.z * z.y - z.z * x.y) * determinant,
					(x.y * y.z - y.y * x.z) * determinant
				),
				new Vec3
				(
					(y.z * z.x - y.x * z.z) * determinant,
					(x.x * z.z - x.z * z.x) * determinant,
					(x.z * y.x - x.x * y.z) * determinant
				),
				new Vec3
				(
					(y.x * z.y - y.y * z.x) * determinant,
					(x.y * z.x - x.x * z.y) * determinant,
					(x.x * y.y - x.y * y.x) * determinant
				)
			);
        }

		public Mat3 RotateAroundAxisX(float angle)
		{
			float tCos = (float)Math.Cos(angle), tSin = (float)Math.Sin(angle);
			return new Mat3
			(
				x,
				new Vec3((y.x*tCos) - (z.x*tSin), (y.y*tCos) - (z.y*tSin), (y.z*tCos) - (z.z*tSin)),
				new Vec3((y.x*tSin) + (z.x*tCos), (y.y*tSin) + (z.y*tCos), (y.z*tSin) + (z.z*tCos))
			);
		}

		public Mat3 RotateAroundAxisY(float angle)
		{
			float tCos = (float)Math.Cos(angle), tSin = (float)Math.Sin(angle);
			return new Mat3
			(
				new Vec3((z.x*tSin) + (x.x*tCos), (z.y*tSin) + (x.y*tCos), (z.z*tSin) + (x.z*tCos)),
				y,
				new Vec3((z.x*tCos) - (x.x*tSin), (z.y*tCos) - (x.y*tSin), (z.z*tCos) - (x.z*tSin))
			);
		}

		public Mat3 RotateAroundAxisZ(float angle)
		{
			float tCos = (float)Math.Cos(angle), tSin = (float)Math.Sin(angle);
			return new Mat3
			(
				new Vec3((x.x*tCos) - (y.x*tSin), (x.y*tCos) - (y.y*tSin), (x.z*tCos) - (y.z*tSin)),
				new Vec3((x.x*tSin) + (y.x*tCos), (x.y*tSin) + (y.y*tCos), (x.z*tSin) + (y.z*tCos)),
				z
			);
		}

		public Mat3 RotateAroundWorldAxisX(float angle)
		{
			angle = -angle;
			float tCos = (float)Math.Cos(angle), tSin = (float)Math.Sin(angle);
			return new Mat3
			(
				new Vec3(x.x, (x.y*tCos) - (x.z*tSin), (x.y*tSin) + (x.z*tCos)),
				new Vec3(y.x, (y.y*tCos) - (y.z*tSin), (y.y*tSin) + (y.z*tCos)),
				new Vec3(z.x, (z.y*tCos) - (z.z*tSin), (z.y*tSin) + (z.z*tCos))
			);
		}

		public Mat3 RotateAroundWorldAxisY(float angle)
		{
			angle = -angle;
			float tCos = (float)Math.Cos(angle), tSin = (float)Math.Sin(angle);
			return new Mat3
			(
				new Vec3((x.z*tSin) + (x.x*tCos), x.y, (x.z*tCos) - (x.x*tSin)),
				new Vec3((y.z*tSin) + (y.x*tCos), y.y, (y.z*tCos) - (y.x*tSin)),
				new Vec3((z.z*tSin) + (z.x*tCos), z.y, (z.z*tCos) - (z.x*tSin))
			);
		}

		public Mat3 RotateAroundWorldAxisZ(float angle)
		{
			angle = -angle;
			float tCos = (float)Math.Cos(angle), tSin = (float)Math.Sin(angle);
			return new Mat3
			(
				new Vec3((x.x*tCos) - (x.y*tSin), (x.x*tSin) + (x.y*tCos), x.z),
				new Vec3((y.x*tCos) - (y.y*tSin), (y.x*tSin) + (y.y*tCos), y.z),
				new Vec3((z.x*tCos) - (z.y*tSin), (z.x*tSin) + (z.y*tCos), z.z)
			);
		}

		public Mat3 RotateAround(Vec3 axis, float angle)
		{
			// rotate into world space
			var quaternion = Quat.FromRotationAxis(axis, 0).Conjugate();
			var worldSpaceMatrix = this.Multiply(FromQuaternion(quaternion));

			// rotate back to matrix space
			quaternion = Quat.FromRotationAxis(axis, angle);
			var qMat = Mat3.FromQuaternion(quaternion);
			worldSpaceMatrix = worldSpaceMatrix.Multiply(qMat);
			return worldSpaceMatrix;
		}
		#endregion
	}

	#if MATH_UNITY_HELPER
	public static class Mat3Ext
	{
		public static Mat3 ToMat3(this UnityEngine.Matrix4x4 self)
		{
			return new Mat3
			(
				new Vec3(self.m00, self.m01, self.m02),
				new Vec3(self.m10, self.m11, self.m12),
				new Vec3(self.m20, self.m21, self.m22)
			);
		}
	}
	#endif
}