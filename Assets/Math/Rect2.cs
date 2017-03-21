using System.Runtime.InteropServices;

namespace UnityMathReference
{
	[StructLayout(LayoutKind.Sequential)]
	public struct Rect2
	{
		#region Properties
		public Point2 position;
		public Size2 size;

		public int left {get{return position.x;}}
		public int right {get{return position.x + size.width;}}
		public int bottom {get{return position.y;}}
		public int top {get{return position.y + size.height;}}

		public static readonly Rect2 zero = new Rect2();
		#endregion

		#region Constructors
		public Rect2(int x, int y, int width, int height)
		{
			position.x = x;
			position.y = y;
			size.width = width;
			size.height = height;
		}

		public Rect2(Point2 position, Size2 size)
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