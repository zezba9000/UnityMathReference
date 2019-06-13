using System.Runtime.InteropServices;

namespace UnityMathReference
{
	[StructLayout(LayoutKind.Sequential)]
	public struct Size3
	{
		#region Properties
		public int width, height, depth;

		public static readonly Size3 zero = new Size3();
		#endregion

		#region Constructors
		public Size3(int width, int height, int depth)
		{
			this.width = width;
			this.height = height;
			this.depth = depth;
		}
		#endregion

		#region Operators
		// +
		public static Size3 operator+(Size3 p1, Size3 p2)
		{
			p1.width += p2.width;
			p1.height += p2.height;
			p1.depth += p2.depth;
			return p1;
		}

		public static Size3 operator+(Size3 p1, int p2)
		{
			p1.width += p2;
			p1.height += p2;
			p1.depth += p2;
			return p1;
		}

		public static Size3 operator+(int p1, Size3 p2)
		{
			p2.width = p1 + p2.width;
			p2.height = p1 + p2.height;
			p2.depth = p1 + p2.depth;
			return p2;
		}

		// -
		public static Size3 operator-(Size3 p1, Size3 p2)
		{
			p1.width -= p2.width;
			p1.height -= p2.height;
			p1.depth -= p2.depth;
			return p1;
		}

		public static Size3 operator-(Size3 p1, int p2)
		{
			p1.width -= p2;
			p1.height -= p2;
			p1.depth -= p2;
			return p1;
		}

		public static Size3 operator-(int p1, Size3 p2)
		{
			p2.width = p1 - p2.width;
			p2.height = p1 - p2.height;
			p2.depth = p1 - p2.depth;
			return p2;
		}

		public static Size3 operator-(Size3 p2)
		{
			p2.width = -p2.width;
			p2.height = -p2.height;
			p2.depth = -p2.depth;
			return p2;
		}

		// *
		public static Size3 operator*(Size3 p1, Size3 p2)
		{
			p1.width *= p2.width;
			p1.height *= p2.height;
			p1.depth *= p2.depth;
			return p1;
		}

		public static Size3 operator*(Size3 p1, int p2)
		{
			p1.width *= p2;
			p1.height *= p2;
			p1.depth *= p2;
			return p1;
		}

		public static Size3 operator*(int p1, Size3 p2)
		{
			p2.width = p1 * p2.width;
			p2.height = p1 * p2.height;
			p2.depth = p1 * p2.depth;
			return p2;
		}

		// /
		public static Size3 operator/(Size3 p1, Size3 p2)
		{
			p1.width /= p2.width;
			p1.height /= p2.height;
			p1.depth /= p2.depth;
			return p1;
		}

		public static Size3 operator/(Size3 p1, int p2)
		{
			p1.width /= p2;
			p1.height /= p2;
			p1.depth /= p2;
			return p1;
		}

		public static Size3 operator/(int p1, Size3 p2)
		{
			p2.width = p1 / p2.width;
			p2.height = p1 / p2.height;
			p2.depth = p1 / p2.depth;
			return p2;
		}

		// ==
		public static bool operator==(Size3 p1, Size3 p2) {return (p1.width==p2.width && p1.height==p2.height && p1.depth==p2.depth);}
		public static bool operator!=(Size3 p1, Size3 p2) {return (p1.width!=p2.width || p1.height!=p2.height || p1.depth!=p2.depth);}

		// convert
		public Vec3 ToVec3()
		{
			return new Vec3(width, height, depth);
		}
		#endregion
		
		#region Methods
		public override bool Equals(object obj)
		{
			return obj != null && (Size3)obj == this;
		}
		
		public override string ToString()
		{
			return string.Format("<{0}, {1}, {2}>", width, height, depth);
		}
		
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
		#endregion
	}
}