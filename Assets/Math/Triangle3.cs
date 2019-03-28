using System.Runtime.InteropServices;

namespace UnityMathReference
{
	[StructLayout(LayoutKind.Sequential)]
	public struct Triangle3
	{
		#region Properties
		public Vec3 point1, point2, point3;
		#endregion

		#region Constructors
		public Triangle3(Vec3 point1, Vec3 point2, Vec3 point3)
		{
			this.point1 = point1;
			this.point2 = point2;
			this.point3 = point3;
		}
		#endregion

		#region Methods
		public Vec3 Normal()
		{
			Vec3 vector1 = (point1-point2);
			Vec3 vector2 = (point3-point1);
			return new Vec3(-((vector1.y*vector2.z) - (vector1.z*vector2.y)), -((vector1.z*vector2.x) - (vector1.x*vector2.z)), -((vector1.x*vector2.y) - (vector1.y*vector2.x))).Normalize();
		}
		#endregion
	}
}