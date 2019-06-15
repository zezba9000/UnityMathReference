using System;
using System.Runtime.InteropServices;

namespace UnityMathReference
{
	[StructLayout(LayoutKind.Sequential)]
	public struct Point3
	{
		#region Properties
		public int x, y, z;

		public static readonly Point3 one = new Point3(1);
		public static readonly Point3 minusOne = new Point3(-1);
		public static readonly Point3 zero = new Point3();
		public static readonly Point3 right = new Point3(1, 0, 0);
		public static readonly Point3 left = new Point3(-1, 0, 0);
		public static readonly Point3 up = new Point3(0, 1, 0);
		public static readonly Point3 down = new Point3(0, -1, 0);
		public static readonly Point3 forward = new Point3(0, 0, 1);
		public static readonly Point3 backward = new Point3(0, 0, -1);
		#endregion

		#region Constructors
		public Point3(int value)
		{
			x = value;
			y = value;
			z = value;
		}

		public Point3(int x, int y, int z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		public Point3(Point2 point, int z)
		{
			x = point.x;
			y = point.y;
			this.z = z;
		}
		#endregion

		#region Operators
		// +
		public static Point3 operator+(Point3 p1, Point3 p2)
		{
			return new Point3(p1.x + p2.x, p1.y + p2.y, p1.z + p2.z);
		}

		public static Point3 operator+(Point3 p1, Point2 p2)
		{
			return new Point3(p1.x + p2.x, p1.y + p2.y, p1.z);
		}

		public static Point3 operator+(Point2 p1, Point3 p2)
		{
			return new Point3(p1.x + p2.x, p1.y + p2.y, p2.z);
		}

		public static Point3 operator+(Point3 p1, int p2)
		{
			return new Point3(p1.x + p2, p1.y + p2, p1.z + p2);
		}

		public static Point3 operator+(int p1, Point3 p2)
		{
			return new Point3(p1 + p2.x, p1 + p2.y, p1 + p2.z);
		}

		// -
		public static Point3 operator-(Point3 p1, Point3 p2)
		{
			return new Point3(p1.x - p2.x, p1.y - p2.y, p1.z - p2.z);
		}

		public static Point3 operator-(Point3 p1, Point2 p2)
		{
			return new Point3(p1.x - p2.x, p1.y - p2.y, p1.z);
		}

		public static Point3 operator-(Point2 p1, Point3 p2)
		{
			return new Point3(p1.x - p2.x, p1.y - p2.y, p2.z);
		}

		public static Point3 operator-(Point3 p1, int p2)
		{
			return new Point3(p1.x - p2, p1.y - p2, p1.z - p2);
		}

		public static Point3 operator-(int p1, Point3 p2)
		{
			return new Point3(p1 - p2.x, p1 - p2.y, p1 - p2.z);
		}

		public static Point3 operator-(Point3 p2)
		{
			return new Point3(-p2.x, -p2.y, -p2.z);
		}

		// *
		public static Point3 operator*(Point3 p1, Point3 p2)
		{
			return new Point3(p1.x * p2.x, p1.y * p2.y, p1.z * p2.z);
		}

		public static Point3 operator*(Point3 p1, Point2 p2)
		{
			return new Point3(p1.x * p2.x, p1.y * p2.y, p1.z);
		}

		public static Point3 operator*(Point2 p1, Point3 p2)
		{
			return new Point3(p1.x * p2.x, p1.y * p2.y, p2.z);
		}

		public static Point3 operator*(Point3 p1, int p2)
		{
			return new Point3(p1.x * p2, p1.y * p2, p1.z * p2);
		}

		public static Point3 operator*(int p1, Point3 p2)
		{
			return new Point3(p1 * p2.x, p1 * p2.y, p1 * p2.z);
		}

		// /
		public static Point3 operator/(Point3 p1, Point3 p2)
		{
			return new Point3(p1.x / p2.x, p1.y / p2.y, p1.z / p2.z);
		}

		public static Point3 operator/(Point3 p1, Point2 p2)
		{
			return new Point3(p1.x / p2.x, p1.y / p2.y, p1.z);
		}

		public static Point3 operator/(Point2 p1, Point3 p2)
		{
			return new Point3(p1.x / p2.x, p1.y / p2.y, p2.z);
		}

		public static Point3 operator/(Point3 p1, int p2)
		{
			return new Point3(p1.x / p2, p1.y / p2, p1.z / p2);
		}

		public static Point3 operator/(int p1, Point3 p2)
		{
			return new Point3(p1 / p2.x, p1 / p2.y, p1 / p2.z);
		}

		// ==
		public static bool operator==(Point3 p1, Point3 p2) {return p1.x==p2.x && p1.y==p2.y && p1.z==p2.z;}
		public static bool operator!=(Point3 p1, Point3 p2) {return p1.x!=p2.x || p1.y!=p2.y || p1.z!=p2.z;}

		// convert
		public Point2 ToPoint2()
		{
			return new Point2(x, y);
		}

		public Vec2 ToVec2()
		{
			return new Vec2(x, y);
		}

		public Vec3 ToVec3()
		{
			return new Vec3(x, y, z);
		}
		#endregion

		#region Methods
		public override bool Equals(object obj)
		{
			return obj != null && (Point3)obj == this;
		}

		public override string ToString()
		{
			return string.Format("<{0}, {1}, {2}>", x, y, z);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
		
		public bool Intersects(Rect3 rect)
		{
			return x >= rect.left && x <= rect.right && y >= rect.bottom && y <= rect.top && z >= rect.back && z <= rect.front;
		}

		public void Intersects(Rect3 rect, out bool result)
		{
			result = x >= rect.left && x <= rect.right && y >= rect.bottom && y <= rect.top && z >= rect.back && z <= rect.front;
		}

		public bool Intersects(Bound3 boundingBox)
		{
			return x >= boundingBox.left && x <= boundingBox.right && y >= boundingBox.bottom && y <= boundingBox.top && z >= boundingBox.back && z <= boundingBox.front;
		}

		public void Intersects(Bound3 boundingBox, out bool result)
		{
			result = x >= boundingBox.left && x <= boundingBox.right && y >= boundingBox.bottom && y <= boundingBox.top && z >= boundingBox.back && z <= boundingBox.front;
		}
		#endregion
	}
}