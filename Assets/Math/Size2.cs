using System.Runtime.InteropServices;

namespace UnityMathReference
{
	[StructLayout(LayoutKind.Sequential)]
	public struct Size2
	{
		#region Fields
		public int width, height;

		public static readonly Size2 zero = new Size2();
		#endregion

		#region Constructors
		public Size2(int width, int height)
		{
			this.width = width;
			this.height = height;
		}
		#endregion

		#region Operators
		// +
		public static Size2 operator+(Size2 p1, Size2 p2)
		{
			return new Size2(p1.width + p2.width, p1.height + p2.height);
		}

		public static Size2 operator+(Size2 p1, int p2)
		{
			return new Size2(p1.width + p2, p1.height + p2);
		}

		public static Size2 operator+(int p1, Size2 p2)
		{
			return new Size2(p1 + p2.width, p1 + p2.height);
		}

		// -
		public static Size2 operator-(Size2 p1, Size2 p2)
		{
			return new Size2(p1.width - p2.width, p1.height - p2.height);
		}

		public static Size2 operator-(Size2 p1, int p2)
		{
			return new Size2(p1.width - p2, p1.height - p2);
		}

		public static Size2 operator-(int p1, Size2 p2)
		{
			return new Size2(p1 - p2.width, p1 - p2.height);
		}

		public static Size2 operator-(Size2 p2)
		{
			return new Size2(-p2.width, -p2.height);
		}

		// *
		public static Size2 operator*(Size2 p1, Size2 p2)
		{
			return new Size2(p1.width * p2.width, p1.height * p2.height);
		}

		public static Size2 operator*(Size2 p1, int p2)
		{
			return new Size2(p1.width * p2, p1.height * p2);
		}

		public static Size2 operator*(int p1, Size2 p2)
		{
			return new Size2(p1 * p2.width, p1 * p2.height);
		}

		// /
		public static Size2 operator/(Size2 p1, Size2 p2)
		{
			return new Size2(p1.width / p2.width, p1.height / p2.height);
		}

		public static Size2 operator/(Size2 p1, int p2)
		{
			return new Size2(p1.width / p2, p1.height / p2);
		}

		public static Size2 operator/(int p1, Size2 p2)
		{
			return new Size2(p1 / p2.width, p1 / p2.height);
		}

		// ==
		public static bool operator==(Size2 p1, Size2 p2) {return p1.width==p2.width && p1.height==p2.height;}
		public static bool operator!=(Size2 p1, Size2 p2) {return p1.width!=p2.width || p1.height!=p2.height;}

		// convert
		public Vec2 ToVec2()
		{
			return new Vec2(width, height);
		}
		#endregion
		
		#region Methods
		public override bool Equals(object obj)
		{
			return obj != null && (Size2)obj == this;
		}
		
		public override string ToString()
		{
			return string.Format("<{0}, {1}>", width, height);
		}
		
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public float FitWithinFrameScale(Size2 frame)
		{
			float frameSlope = frame.width / (float)frame.height;
			float objSlope = this.width / (float)this.height;
			if (objSlope > frameSlope) return frame.width / (float)this.width;
			else return frame.height / (float)this.height;
		}

		public Size2 FitWithinFrame(Size2 frame)
		{
			Size2 size;
			float scale = FitWithinFrameScale(frame);
			size.width = (int)(this.width * scale);
			size.height = (int)(this.height * scale);

			return size;
		}
		#endregion
	}
}