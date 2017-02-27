using System;
using System.Runtime.InteropServices;

namespace Reign
{
	[StructLayout(LayoutKind.Sequential)]
	public struct Mat4
	{
		#region Properties
		public Vec4 x, y, z, w;

		public Vec4 right {get{return x;}}
		public Vec4 left {get{return -x;}}
		public Vec4 up {get{return y;}}
		public Vec4 down {get{return -y;}}
		public Vec4 front {get{return z;}}
		public Vec4 back {get{return -z;}}
		public Vec4 high {get{return w;}}
		public Vec4 low {get{return -w;}}

		public Vec3 translation
		{
			get{return new Vec3(x.w, y.w, z.w);}
			set
			{
				x.w = value.x;
				y.w = value.y;
				z.w = value.z;
			}	
		}
		#endregion

		#region Constructors
		public Mat4(float value)
		{
			x = new Vec4(value);
			y = new Vec4(value);
			z = new Vec4(value);
			w = new Vec4(value);
		}

		public Mat4(Vec4 x, Vec4 y, Vec4 z, Vec4 w)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}

		public static Mat4 FromMatrix3(Mat3 matrix)
		{
			return new Mat4
			(
				new Vec4(matrix.x.x, matrix.x.y, matrix.x.z, 0),
				new Vec4(matrix.y.x, matrix.y.y, matrix.y.z, 0),
				new Vec4(matrix.z.x, matrix.z.y, matrix.z.z, 0),
				new Vec4(0, 0, 0, 1)
			);
		}

		public static Mat4 FromAffineTransform(AffineTransform3 transform)
		{
			return new Mat4
			(
				new Vec4(transform.Transform.x, transform.Translation.x),
				new Vec4(transform.Transform.y, transform.Translation.y),
				new Vec4(transform.Transform.z, transform.Translation.z),
				new Vec4(0, 0, 0, 1)
			);
		}

		public static Mat4 FromAffineTransform(Mat3 transform, Vec3 position)
		{
			return new Mat4
			(
				new Vec4(transform.x, position.x),
				new Vec4(transform.y, position.y),
				new Vec4(transform.z, position.z),
				new Vec4(0, 0, 0, 1)
			);
		}

		public static Mat4 FromAffineTransform(Mat3 transform, Vec3 scale, Vec3 position)
		{
			return new Mat4
			(
				new Vec4(transform.x * scale.x, position.x),
				new Vec4(transform.y * scale.y, position.y),
				new Vec4(transform.z * scale.z, position.z),
				new Vec4(0, 0, 0, 1)
			);
		}

		public static Mat4 FromQuaternion(Quat quaternion)
		{
			var squared = new Vec4(quaternion.x*quaternion.x, quaternion.y*quaternion.y, quaternion.z*quaternion.z, quaternion.w*quaternion.w);
			float invSqLength = 1 / (squared.x + squared.y + squared.z + squared.w);

			float temp1 = quaternion.x * quaternion.y;
			float temp2 = quaternion.z * quaternion.w;
			float temp3 = quaternion.x * quaternion.z;
			float temp4 = quaternion.y * quaternion.w;
			float temp5 = quaternion.y * quaternion.z;
			float temp6 = quaternion.x * quaternion.w;

			return new Mat4
			(
				new Vec4((squared.x-squared.y-squared.z+squared.w) * invSqLength, 2*(temp1-temp2) * invSqLength, 2*(temp3+temp4) * invSqLength, 0),
				new Vec4(2*(temp1+temp2) * invSqLength, (-squared.x+squared.y-squared.z+squared.w) * invSqLength, 2*(temp5-temp6) * invSqLength, 0),
				new Vec4(2*(temp3-temp4) * invSqLength, 2*(temp5+temp6) * invSqLength, (-squared.x-squared.y+squared.z+squared.w) * invSqLength, 0),
				new Vec4(0, 0, 0, 1)
			);
		}

		public Mat4 FromRotationAxis(Vec3 axis, float angle)
		{
			Quat quaternion = Quat.FromRotationAxis(axis, angle);
			return Mat4.FromQuaternion(quaternion);
		}

		public static Mat4 FromRigidTransform(RigidTransform3 transform)
		{
			return Mat4.FromAffineTransform(Mat3.FromQuaternion(transform.rotation), transform.position);
		}

		public static readonly Mat4 identity = new Mat4
		(
		    new Vec4(1, 0, 0, 0),
		    new Vec4(0, 1, 0, 0),
		    new Vec4(0, 0, 1, 0),
		    new Vec4(0, 0, 0, 1)
		);
		#endregion

		#region Operators
		// +
		public static Mat4 operator+(Mat4 p1, Mat4 p2)
		{
			p1.x += p2.x;
			p1.y += p2.y;
			p1.z += p2.z;
			p1.w += p2.w;
			return p1;
		}

		public static Mat4 operator+(Mat4 p1, Vec4 p2)
		{
			p1.x += p2;
			p1.y += p2;
			p1.z += p2;
			p1.w += p2;
			return p1;
		}

		public static Mat4 operator+(Vec4 p1, Mat4 p2)
		{
			p2.x = p1 + p2.x;
			p2.y = p1 + p2.y;
			p2.z = p1 + p2.z;
			p2.w = p1 + p2.w;
			return p2;
		}

		public static Mat4 operator+(Mat4 p1, float p2)
		{
			p1.x += p2;
			p1.y += p2;
			p1.z += p2;
			p1.w += p2;
			return p1;
		}

		public static Mat4 operator+(float p1, Mat4 p2)
		{
			p2.x = p1 + p2.x;
			p2.y = p1 + p2.y;
			p2.z = p1 + p2.z;
			p2.w = p1 + p2.w;
			return p2;
		}

		// -
		public static Mat4 operator-(Mat4 p1, Mat4 p2)
		{
			p1.x -= p2.x;
			p1.y -= p2.y;
			p1.z -= p2.z;
			p1.w -= p2.w;
			return p1;
		}

		public static Mat4 operator-(Mat4 p1, Vec4 p2)
		{
			p1.x -= p2;
			p1.y -= p2;
			p1.z -= p2;
			p1.w -= p2;
			return p1;
		}

		public static Mat4 operator-(Vec4 p1, Mat4 p2)
		{
			p2.x = p1 - p2.x;
			p2.y = p1 - p2.y;
			p2.z = p1 - p2.z;
			p2.w = p1 - p2.w;
			return p2;
		}

		public static Mat4 operator-(Mat4 p1, float p2)
		{
			p1.x -= p2;
			p1.y -= p2;
			p1.z -= p2;
			p1.w -= p2;
			return p1;
		}

		public static Mat4 operator-(float p1, Mat4 p2)
		{
			p2.x = p1 - p2.x;
			p2.y = p1 - p2.y;
			p2.z = p1 - p2.z;
			p2.w = p1 - p2.w;
			return p2;
		}

		public static Mat4 operator-(Mat4 p2)
		{
			p2.x = -p2.x;
			p2.y = -p2.y;
			p2.z = -p2.z;
			p2.w = -p2.w;
			return p2;
		}

		// *
		public static Mat4 operator*(Mat4 p1, Mat4 p2)
		{
			p1.x *= p2.x;
			p1.y *= p2.y;
			p1.z *= p2.z;
			p1.w *= p2.w;
			return p1;
		}

		public static Mat4 operator*(Mat4 p1, Vec4 p2)
		{
			p1.x *= p2;
			p1.y *= p2;
			p1.z *= p2;
			p1.w *= p2;
			return p1;
		}

		public static Mat4 operator*(Vec4 p1, Mat4 p2)
		{
			p2.x = p1 * p2.x;
			p2.y = p1 * p2.y;
			p2.z = p1 * p2.z;
			p2.w = p1 * p2.w;
			return p2;
		}

		public static Mat4 operator*(Mat4 p1, float p2)
		{
			p1.x *= p2;
			p1.y *= p2;
			p1.z *= p2;
			p1.w *= p2;
			return p1;
		}

		public static Mat4 operator*(float p1, Mat4 p2)
		{
			p2.x = p1 * p2.x;
			p2.y = p1 * p2.y;
			p2.z = p1 * p2.z;
			p2.w = p1 * p2.w;
			return p2;
		}

		// /
		public static Mat4 operator/(Mat4 p1, Mat4 p2)
		{
			p1.x /= p2.x;
			p1.y /= p2.y;
			p1.z /= p2.z;
			p1.w /= p2.w;
			return p1;
		}

		public static Mat4 operator/(Mat4 p1, Vec4 p2)
		{
			p1.x /= p2;
			p1.y /= p2;
			p1.z /= p2;
			p1.w /= p2;
			return p1;
		}

		public static Mat4 operator/(Vec4 p1, Mat4 p2)
		{
			p2.x = p1 / p2.x;
			p2.y = p1 / p2.y;
			p2.z = p1 / p2.z;
			p2.w = p1 / p2.w;
			return p2;
		}

		public static Mat4 operator/(Mat4 p1, float p2)
		{
			p1.x /= p2;
			p1.y /= p2;
			p1.z /= p2;
			p1.w /= p2;
			return p1;
		}

		public static Mat4 operator/(float p1, Mat4 p2)
		{
			p2.x = p1 / p2.x;
			p2.y = p1 / p2.y;
			p2.z = p1 / p2.z;
			p2.w = p1 / p2.w;
			return p2;
		}

		// ==
		public static bool operator==(Mat4 p1, Mat4 p2) {return (p1.x==p2.x && p1.y==p2.y && p1.z==p2.z && p1.w==p2.w);}
		public static bool operator!=(Mat4 p1, Mat4 p2) {return (p1.x!=p2.x || p1.y!=p2.y || p1.z!=p2.z || p1.w!=p2.w);}

		// convert
		public Mat3 ToMat3()
		{
			return new Mat3(x.ToVector3(), y.ToVector3(), z.ToVector3());
		}
		#endregion
		
		#region Methods
		public override bool Equals(object obj)
		{
			return obj != null && (Mat4)obj == this;
		}

		public override string ToString()
		{
			return string.Format("{0} : {1} : {2} : {3}", x, y, z, w);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public Mat4 Abs()
		{
			return new Mat4(x.Abs(), y.Abs(), z.Abs(), w.Abs());
		}

		public Mat4 Transpose()
		{
			return new Mat4
			(
				new Vec4(x.x, y.x, z.x, w.x),
				new Vec4(x.y, y.y, z.y, w.y),
				new Vec4(x.z, y.z, z.z, w.z),
				new Vec4(x.w, y.w, z.w, w.w)
			);
		}

		public Mat4 Multiply(Mat2 matrix)
		{
			return new Mat4
			(
				new Vec4((matrix.x.x*x.x) + (matrix.x.y*y.x), (matrix.x.x*x.y) + (matrix.x.y*y.y), x.z, x.w),
				new Vec4((matrix.y.x*x.x) + (matrix.y.y*y.x), (matrix.y.x*x.y) + (matrix.y.y*y.y), y.z, y.w),
				z,
				w
			);
		}

		public Mat4 Multiply(Mat3 matrix)
		{
			return new Mat4
			(
				new Vec4((matrix.x.x*x.x) + (matrix.x.y*y.x) + (matrix.x.z*z.x), (matrix.x.x*x.y) + (matrix.x.y*y.y) + (matrix.x.z*z.y), (matrix.x.x*x.z) + (matrix.x.y*y.z) + (matrix.x.z*z.z), x.w),
				new Vec4((matrix.y.x*x.x) + (matrix.y.y*y.x) + (matrix.y.z*z.x), (matrix.y.x*x.y) + (matrix.y.y*y.y) + (matrix.y.z*z.y), (matrix.y.x*x.z) + (matrix.y.y*y.z) + (matrix.y.z*z.z), y.w),
				new Vec4((matrix.z.x*x.x) + (matrix.z.y*y.x) + (matrix.z.z*z.x), (matrix.z.x*x.y) + (matrix.z.y*y.y) + (matrix.z.z*z.y), (matrix.z.x*x.z) + (matrix.z.y*y.z) + (matrix.z.z*z.z), z.w),
				w
			);
		}

		public Mat4 Multiply(Mat4 matrix)
		{
			return new Mat4
			(
				new Vec4((matrix.x.x*x.x) + (matrix.x.y*y.x) + (matrix.x.z*z.x) + (matrix.x.w*w.x), (matrix.x.x*x.y) + (matrix.x.y*y.y) + (matrix.x.z*z.y) + (matrix.x.w*w.y), (matrix.x.x*x.z) + (matrix.x.y*y.z) + (matrix.x.z*z.z) + (matrix.x.w*w.z), (matrix.x.x*x.w) + (matrix.x.y*y.w) + (matrix.x.z*z.w) + (matrix.x.w*w.w)),
				new Vec4((matrix.y.x*x.x) + (matrix.y.y*y.x) + (matrix.y.z*z.x) + (matrix.y.w*w.x), (matrix.y.x*x.y) + (matrix.y.y*y.y) + (matrix.y.z*z.y) + (matrix.y.w*w.y), (matrix.y.x*x.z) + (matrix.y.y*y.z) + (matrix.y.z*z.z) + (matrix.y.w*w.z), (matrix.y.x*x.w) + (matrix.y.y*y.w) + (matrix.y.z*z.w) + (matrix.y.w*w.w)),
				new Vec4((matrix.z.x*x.x) + (matrix.z.y*y.x) + (matrix.z.z*z.x) + (matrix.z.w*w.x), (matrix.z.x*x.y) + (matrix.z.y*y.y) + (matrix.z.z*z.y) + (matrix.z.w*w.y), (matrix.z.x*x.z) + (matrix.z.y*y.z) + (matrix.z.z*z.z) + (matrix.z.w*w.z), (matrix.z.x*x.w) + (matrix.z.y*y.w) + (matrix.z.z*z.w) + (matrix.z.w*w.w)),
				new Vec4((matrix.w.x*x.x) + (matrix.w.y*y.x) + (matrix.w.z*z.x) + (matrix.w.w*w.x), (matrix.w.x*x.y) + (matrix.w.y*y.y) + (matrix.w.z*z.y) + (matrix.w.w*w.y), (matrix.w.x*x.z) + (matrix.w.y*y.z) + (matrix.w.z*z.z) + (matrix.w.w*w.z), (matrix.w.x*x.w) + (matrix.w.y*y.w) + (matrix.w.z*z.w) + (matrix.w.w*w.w))
			);
		}

		public float Determinant()
        {
            float det1 = z.z * w.w - z.w * w.z;
            float det2 = z.y * w.w - z.w * w.y;
            float det3 = z.y * w.z - z.z * w.y;
            float det4 = z.x * w.w - z.w * w.x;
            float det5 = z.x * w.z - z.z * w.x;
            float det6 = z.x * w.y - z.y * w.x;

            return
                (x.x * ((y.y * det1 - y.z * det2) + y.w * det3)) -
                (x.y * ((y.x * det1 - y.z * det4) + y.w * det5)) +
                (x.z * ((y.x * det2 - y.y * det4) + y.w * det6)) -
                (x.w * ((y.x * det3 - y.y * det5) + y.z * det6));
        }

		public Mat4 Invert()
		{
			float determinant = 1 / Determinant();

			var mat = new Mat4
			(
				new Vec4
				(
					((y.y * z.z * w.w) + (y.z * z.w * w.y) + (y.w * z.y * w.z) - (y.y * z.w * w.z) - (y.z * z.y * w.w) - (y.w * z.z * w.y)) * determinant,
					((x.y * z.w * w.z) + (x.z * z.y * w.w) + (x.w * z.z * w.y) - (x.y * z.z * w.w) - (x.z * z.w * w.y) - (x.w * z.y * w.z)) * determinant,
					((x.y * y.z * w.w) + (x.z * y.w * w.y) + (x.w * y.y * w.z) - (x.y * y.w * w.z) - (x.z * y.y * w.w) - (x.w * y.z * w.y)) * determinant,
					((x.y * y.w * z.z) + (x.z * y.y * z.w) + (x.w * y.z * z.y) - (x.y * y.z * z.w) - (x.z * y.w * z.y) - (x.w * y.y * z.z)) * determinant
				),
				new Vec4
				(
					((y.x * z.w * w.z) + (y.z * z.x * w.w) + (y.w * z.z * w.x) - (y.x * z.z * w.w) - (y.z * z.w * w.x) - (y.w * z.x * w.z)) * determinant,
					((x.x * z.z * w.w) + (x.z * z.w * w.x) + (x.w * z.x * w.z) - (x.x * z.w * w.z) - (x.z * z.x * w.w) - (x.w * z.z * w.x)) * determinant,
					((x.x * y.w * w.z) + (x.z * y.x * w.w) + (x.w * y.z * w.x) - (x.x * y.z * w.w) - (x.z * y.w * w.x) - (x.w * y.x * w.z)) * determinant,
					((x.x * y.z * z.w) + (x.z * y.w * z.x) + (x.w * y.x * z.z) - (x.x * y.w * z.z) - (x.z * y.x * z.w) - (x.w * y.z * z.x)) * determinant
				),
				new Vec4
				(
					((y.x * z.y * w.w) + (y.y * z.w * w.x) + (y.w * z.x * w.y) - (y.x * z.w * w.y) - (y.y * z.x * w.w) - (y.w * z.y * w.x)) * determinant,
					((x.x * z.w * w.y) + (x.y * z.x * w.w) + (x.w * z.y * w.x) - (x.x * z.y * w.w) - (x.y * z.w * w.x) - (x.w * z.x * w.y)) * determinant,
					((x.x * y.y * w.w) + (x.y * y.w * w.x) + (x.w * y.x * w.y) - (x.x * y.w * w.y) - (x.y * y.x * w.w) - (x.w * y.y * w.x)) * determinant,
					((x.x * y.w * z.y) + (x.y * y.x * z.w) + (x.w * y.y * z.x) - (x.x * y.y * z.w) - (x.y * y.w * z.x) - (x.w * y.x * z.y)) * determinant
				),
				new Vec4
				(
					((y.x * z.z * w.y) + (y.y * z.x * w.z) + (y.z * z.y * w.x) - (y.x * z.y * w.z) - (y.y * z.z * w.x) - (y.z * z.x * w.y)) * determinant,
					((x.x * z.y * w.z) + (x.y * z.z * w.x) + (x.z * z.x * w.y) - (x.x * z.z * w.y) - (x.y * z.x * w.z) - (x.z * z.y * w.x)) * determinant,
					((x.x * y.z * w.y) + (x.y * y.x * w.z) + (x.z * y.y * w.x) - (x.x * y.y * w.z) - (x.y * y.z * w.x) - (x.z * y.x * w.y)) * determinant,
					((x.x * y.y * z.z) + (x.y * y.z * z.x) + (x.z * y.x * z.y) - (x.x * y.z * z.y) - (x.y * y.x * z.z) - (x.z * y.y * z.x)) * determinant
				)
			);
			
			return mat.Transpose();
		}

		public static Mat4 View(Vec3 position, Vec3 lookAt, Vec3 upVector)
		{
			var forward = (lookAt - position).Normalize();
			var xVec = forward.Cross(upVector).Normalize();
			upVector = xVec.Cross(forward);
			
			Mat4 result;
			result.x.x = xVec.x;
			result.x.y = xVec.y;
			result.x.z = xVec.z;
			result.x.w = position.Dot(-xVec);

			result.y.x = upVector.x;
			result.y.y = upVector.y;
			result.y.z = upVector.z;
			result.y.w = position.Dot(-upVector);

			result.z.x = -forward.x;
			result.z.y = -forward.y;
			result.z.z = -forward.z;
			result.z.w = position.Dot(forward);

			result.w.x = 0;
			result.w.y = 0;
			result.w.z = 0;
			result.w.w = 1;

			return result;
		}

		public static Mat4 Perspective(float fov, float aspect, float near, float far)
		{
			float top = near * (float)Math.Tan(fov * .5f);
			float bottom = -top;
			float right = top * aspect;
			float left = -right;

			return Frustum(left, right, bottom, top, near, far);
		}

		public static Mat4 Frustum(float left, float right, float bottom, float top, float near, float far)
		{
			float width = right - left;
			float height = top - bottom;
			float depth = far - near;
			float n = near * 2;

			Mat4 result;
			result.x.x = n/width;
			result.x.y = 0;
			result.x.z = (right+left)/width;
			result.x.w = 0;

			result.y.x = 0;
			result.y.y = n/height;
			result.y.z = (top+bottom)/height;
			result.y.w = 0;

			result.z.x = 0;
			result.z.y = 0;
			result.z.z = -(far+near)/depth;
			result.z.w = -(n*far)/depth;

			result.w.x = 0;
			result.w.y = 0;
			result.w.z = -1;
			result.w.w = 0;

			return result;
		}

		public static Mat4 Orthographic(float width, float height, float near, float far)
		{
			return Orthographic(0, width, 0, height, near, far);
		}

		public static Mat4 Orthographic(float left, float right, float bottom, float top, float near, float far)
		{
			float width = right - left;
			float height = top - bottom;
			float depth = far - near;

			Mat4 result;
			result.x.x = 2/width;
			result.x.y = 0;
			result.x.z = 0;
			result.x.w = -(right+left)/width;

			result.y.x = 0;
			result.y.y = 2/height;
			result.y.z = 0;
			result.y.w = -(top+bottom)/height;

			result.z.x = 0;
			result.z.y = 0;
			result.z.z = -2/depth;
			result.z.w = -(far+near)/depth;

			result.w.x = 0;
			result.w.y = 0;
			result.w.z = 0;
			result.w.w = 1;

			return result;
		}

		public static Mat4 OrthographicCentered(float width, float height, float near, float far)
		{
			return OrthographicCentered(0, width, 0, height, near, far);
		}

		public static Mat4 OrthographicCentered(float left, float right, float bottom, float top, float near, float far)
		{
			float width = right - left;
			float height = top - bottom;
			float depth = far - near;

			Mat4 result;
			result.x.x = (2/width);
			result.x.y = 0;
			result.x.z = 0;
			result.x.w = 0;

			result.y.x = 0;
			result.y.y = (2/height);
			result.y.z = 0;
			result.y.w = 0;

			result.z.x = 0;
			result.z.y = 0;
			result.z.z = (-2)/depth;
			result.z.w = -((far+near)/depth);

			result.w.x = 0;
			result.w.y = 0;
			result.w.z = 0;
			result.w.w = 1;
			return result;
		}
		#endregion
	}
}