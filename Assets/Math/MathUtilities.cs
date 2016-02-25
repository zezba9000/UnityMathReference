using System;

namespace Reign
{
	public static class MathUtilities
	{
		#region Properties
		/// <summary>
		/// Large tolerance value.
		/// </summary>
		public const float bigEpsilon = 1E-5f;

		/// <summary>
		/// Tolerance value.
		/// </summary>
		public const float epsilon = 1E-7f;

		/// <summary>
		/// Represents the ratio of the circumference of a circle to its diameter, specified by the constant, π.
		/// </summary>
		public const float pi = 3.1415926535897932384626433832795f;

		/// <summary>
		/// Represents the ratio of the circumference of a circle to its diameter, specified by the constant, π, multiplied by 2.
		/// </summary>
		public const float pi2 = 6.283185307179586476925286766559f;

		/// <summary>
		/// Represents the ratio of the circumference of a circle to its diameter, specified by the constant, π, multiplied by 4.
		/// </summary>
		public const float pi4 = 12.566370614359172953850573533118f;

		/// <summary>
		/// Represents the ratio of the circumference of a circle to its diameter, specified by the constant, π, divided by 2.
		/// </summary>
		public const float piHalf = 1.5707963267948966192313216916398f;

		/// <summary>
		/// Represents the ratio of the circumference of a circle to its diameter, specified by the constant, π, divided by 4.
		/// </summary>
		public const float piQuarter = 0.78539816339744830961566084581988f;

		/// <summary>
		/// Represents 1 divided by the ratio of the circumference of a circle to its diameter, specified by the constant, π.
		/// </summary>
		public const float piDelta = 0.31830988618379067153776752674503f;

		/// <summary>
		/// Represents 1 divided by the ratio of the circumference of a circle to its diameter, specified by the constant, π, divided by 2.
		/// </summary>
		public const float piHalfDelta = 0.63661977236758134307553505349004f;

		/// <summary>
		/// Represents 1 divided by the ratio of the circumference of a circle to its diameter, specified by the constant, π, divided by 4.
		/// </summary>
		public const float piQuarterDelta = 1.2732395447351626861510701069801f;
		#endregion

		#region Methods
		/// <summary>
		/// Converts Degrees to Radians.
		/// </summary>
		/// <param name="degrees">Degrees</param>
		/// <returns>Radians</returns>
		public static float DegToRad(float degrees)
		{
			return ((degrees / 180) * pi);
		}

		public static void DegToRad(float degrees, out float result)
		{
			result = ((degrees / 180) * pi);
		}

		/// <summary>
		/// Converts Radians to Degrees.
		/// </summary>
		/// <param name="radians">Radians</param>
		/// <returns>Degrees</returns>
		public static float RadToDeg(float radians)
		{
			return ((radians / pi) * 180);
		}

		public static void RadToDeg(float radians, out float result)
		{
			result = ((radians / pi) * 180);
		}

		public static float Clamp(float value, float min, float max)
		{
			if (value < min) return min;
			else if (value > max) return max;

			return value;
		}

		public static float WrapAngle(float angle)
		{
			angle = (float)Math.IEEERemainder(angle, pi2);
			if (angle < -pi)
			{
				angle += pi2;
				return angle;
			}

			if (angle >= pi)
			{
				angle -= pi2;
			}

			return angle;
		}
		#endregion

		#region Extention Methods
		public static UnityEngine.Vector2[] ToVector2(this Vec2[] vectors)
		{
			var result = new UnityEngine.Vector2[vectors.Length];
			for (int i = 0; i != result.Length; ++i)
			{
				result[i] = vectors[i].ToVector2();
			}

			return result;
		}

		public static UnityEngine.Vector3[] ToVector3(this Vec3[] vectors)
		{
			var result = new UnityEngine.Vector3[vectors.Length];
			for (int i = 0; i != result.Length; ++i)
			{
				result[i] = vectors[i].ToVector3();
			}

			return result;
		}

		public static void CopyToVector2(this Vec2[] vectors, UnityEngine.Vector2[] unityVectors)
		{
			if (vectors.Length != unityVectors.Length) throw new Exception("Array sizes don't match");
			for (int i = 0; i != unityVectors.Length; ++i)
			{
				unityVectors[i] = vectors[i].ToVector2();
			}
		}

		public static void CopyToVector3(this Vec3[] vectors, UnityEngine.Vector3[] unityVectors)
		{
			if (vectors.Length != unityVectors.Length) throw new Exception("Array sizes don't match");
			for (int i = 0; i != unityVectors.Length; ++i)
			{
				unityVectors[i] = vectors[i].ToVector3();
			}
		}
		#endregion
	}
}