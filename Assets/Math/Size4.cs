using System.Runtime.InteropServices;

namespace UnityMathReference
{
	[StructLayout(LayoutKind.Sequential)]
	public struct Size4
	{
		#region Properties
		public int width, height, depth, time;
		#endregion
	}
}