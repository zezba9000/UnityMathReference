using System;
using System.Runtime.InteropServices;

namespace UnityMathReference
{
	public enum ContainmentTypes
	{
		Contains,
		Intersects,
		Disjoint
	}

	[StructLayout(LayoutKind.Sequential)]
	public class Frustum3
	{
		#region Properties
		public Plane3 left, right, bottom, top, near, far;
		public Mat4 matrix;
		#endregion

		#region Constructors
		public Frustum3(Mat4 matrix)
		{
			
		}
		#endregion

		#region Methods
		public ContainmentTypes Contains(Bound3 boundingBox)
		{
			throw new NotImplementedException();
		}

		public static void Contains(ref Frustum3 boundingFrustum, ref Bound3 boundingBox, out ContainmentTypes result)
		{
			throw new NotImplementedException();
		}

		public ContainmentTypes Contains(Sphere3 boundingSphere)
		{
			throw new NotImplementedException();
		}

		public static void Contains(ref Frustum3 boundingFrustum, ref Sphere3 boundingSphere, out ContainmentTypes result)
		{
			throw new NotImplementedException();
		}

		public ContainmentTypes Contains(Vec3 point)
		{
			throw new NotImplementedException();
		}

		public static void Contains(ref Frustum3 boundingFrustum, ref Vec3 point, out ContainmentTypes result)
		{
			throw new NotImplementedException();
		}

		public ContainmentTypes Contains(Frustum3 boundingFrustum)
		{
			throw new NotImplementedException();
		}

		public static void Contains(ref Frustum3 boundingFrustum1, ref Frustum3 boundingFrustum2, out ContainmentTypes result)
		{
			throw new NotImplementedException();
		}

		public bool Intersects(Bound3 boundingBox)
		{
			throw new NotImplementedException();
		}

		public static void Intersects(ref Frustum3 boundingFrustum, ref Bound3 boundingBox, out bool result)
		{
			throw new NotImplementedException();
		}

		public bool Intersects(Sphere3 boundingSphere)
		{
			throw new NotImplementedException();
		}

		public static void Intersects(ref Frustum3 boundingFrustum, ref Sphere3 boundingSphere, out bool result)
		{
			throw new NotImplementedException();
		}

		public bool Intersects(Vec3 point)
		{
			throw new NotImplementedException();
		}

		public static void Intersects(ref Frustum3 boundingFrustum, ref Vec3 point, out bool result)
		{
			throw new NotImplementedException();
		}

		public bool Intersects(Frustum3 boundingFrustum)
		{
			throw new NotImplementedException();
		}

		public static void Intersects(ref Frustum3 boundingFrustum1, ref Frustum3 boundingFrustum2, out bool result)
		{
			throw new NotImplementedException();
		}
		#endregion
	}
}