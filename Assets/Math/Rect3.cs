using System.Runtime.InteropServices;

namespace UnityMathReference
{
	[StructLayout(LayoutKind.Sequential)]
	public struct Rect3
	{
		#region Properties
		public Point3 position;
		public Size3 size;

		public int left {get{return position.x;}}
		public int right {get{return position.x + size.width;}}
		public int bottom {get{return position.y;}}
		public int top {get{return position.y + size.height;}}
		public int back {get{return position.z;}}
		public int front {get{return position.z + size.depth;}}

		public static readonly Rect3 zero = new Rect3();
		#endregion

		#region Constructors
		public Rect3(int x, int y, int z, int width, int height, int depth)
		{
			position.x = x;
			position.y = y;
			position.z = z;
			size.width = width;
			size.height = height;
			size.depth = depth;
		}

		public Rect3(Point3 position, Size3 size)
		{
			this.position = position;
			this.size = size;
		}
		#endregion

		#region Methods
		// TODO
		#endregion
	}
}