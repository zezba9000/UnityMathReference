using System.Runtime.InteropServices;

namespace UnityMathReference
{
	[StructLayout(LayoutKind.Sequential)]
	public struct Triangle2
	{
		#region Properties
		public Vec2 point1, point2, point3;
		#endregion

		#region Constructors
		public Triangle2(Vec2 point1, Vec2 point2, Vec2 point3)
		{
			this.point1 = point1;
			this.point2 = point2;
			this.point3 = point3;
		}
		#endregion
	}
}