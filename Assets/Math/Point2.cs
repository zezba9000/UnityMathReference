using System.Runtime.InteropServices;

namespace UnityMathReference
{
	[StructLayout(LayoutKind.Sequential)]
	public struct Point2
	{
		#region Properties
		public int x, y;
		#endregion

		#region Constructors
		public Point2(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		public Point2(int value)
		{
			x = value;
			y = value;
		}

		public static readonly Point2 one = new Point2(1);
		public static readonly Point2 minusOne = new Point2(-1);
		public static readonly Point2 zero = new Point2(0);
		public static readonly Point2 right = new Point2(1, 0);
		public static readonly Point2 left = new Point2(-1, 0);
		public static readonly Point2 up = new Point2(0, 1);
		public static readonly Point2 down = new Point2(0, -1);
		#endregion

		#region Operators
		// +
		public static Point2 operator+(Point2 p1, Point2 p2)
		{
			p1.x += p2.x;
			p1.y += p2.y;
			return p1;
		}

		public static Point2 operator+(Point2 p1, int p2)
		{
			p1.x += p2;
			p1.y += p2;
			return p1;
		}

		public static Point2 operator+(int p1, Point2 p2)
		{
			p2.x = p1 + p2.x;
			p2.y = p1 + p2.y;
			return p2;
		}

		// -
		public static Point2 operator-(Point2 p1, Point2 p2)
		{
			p1.x -= p2.x;
			p1.y -= p2.y;
			return p1;
		}

		public static Point2 operator-(Point2 p1, int p2)
		{
			p1.x -= p2;
			p1.y -= p2;
			return p1;
		}

		public static Point2 operator-(int p1, Point2 p2)
		{
			p2.x = p1 - p2.x;
			p2.y = p1 - p2.y;
			return p2;
		}

		public static Point2 operator-(Point2 p2)
		{
			p2.x = -p2.x;
			p2.y = -p2.y;
			return p2;
		}

		// *
		public static Point2 operator*(Point2 p1, Point2 p2)
		{
			p1.x *= p2.x;
			p1.y *= p2.y;
			return p1;
		}

		public static Point2 operator*(Point2 p1, int p2)
		{
			p1.x *= p2;
			p1.y *= p2;
			return p1;
		}

		public static Point2 operator*(int p1, Point2 p2)
		{
			p2.x = p1 * p2.x;
			p2.y = p1 * p2.y;
			return p2;
		}

		// /
		public static Point2 operator/(Point2 p1, Point2 p2)
		{
			p1.x /= p2.x;
			p1.y /= p2.y;
			return p1;
		}

		public static Point2 operator/(Point2 p1, int p2)
		{
			p1.x /= p2;
			p1.y /= p2;
			return p1;
		}

		public static Point2 operator/(int p1, Point2 p2)
		{
			p2.x = p1 / p2.x;
			p2.y = p1 / p2.y;
			return p2;
		}

		// ==
		public static bool operator==(Point2 p1, Point2 p2) {return (p1.x==p2.x && p1.y==p2.y);}
		public static bool operator!=(Point2 p1, Point2 p2) {return (p1.x!=p2.x || p1.y!=p2.y);}

		// convert
		public Vec2 ToVec2()
		{
			return new Vec2(x, y);
		}
		#endregion

		#region Methods
		public override bool Equals(object obj)
		{
			return obj != null && (Point2)obj == this;
		}
		
		public override string ToString()
		{
			return string.Format("<{0}, {1}>", x, y);
		}
		
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
		
		public bool Intersects(Rect2 rect)
		{
			return x >= rect.left && x <= rect.right && y >= rect.bottom && y <= rect.top;
		}

		public void Intersects(Rect2 rect, out bool result)
		{
			result = x >= rect.left && x <= rect.right && y >= rect.bottom && y <= rect.top;
		}

		public bool Intersects(BoundingBox2 boundingBox)
		{
			return x >= boundingBox.left && x <= boundingBox.right && y >= boundingBox.bottom && y <= boundingBox.top;
		}

		public void Intersects(BoundingBox2 boundingBox, out bool result)
		{
			result = x >= boundingBox.left && x <= boundingBox.right && y >= boundingBox.bottom && y <= boundingBox.top;
		}
		#endregion
	}
}