using System.Runtime.InteropServices;

namespace Reign
{
	[StructLayout(LayoutKind.Sequential)]
	public struct Color4
	{
		#region Properties
		public byte r, g, b, a;

		public int Value
		{
			get
			{
				int color = r;
				color |= g << 8;
				color |= b << 16;
				color |= a << 24;
				return color;
			}
			set
			{
				r = (byte)(value & 0x000000FF);
				g = (byte)((value & 0x0000FF00) >> 8);
				b = (byte)((value & 0x00FF0000) >> 16);
				a = (byte)((value & 0xFF000000) >> 24);
			}
		}
		#endregion

		#region Operators
		// convert
		public Vec4 ToVec4()
		{
			return new Vec4(r/255f, g/255f, b/255f, a/255f);
		}
		#endregion

		#region Constructors
		public Color4(byte r, byte g, byte b, byte a)
		{
			this.r = r;
			this.g = g;
			this.b = b;
			this.a = a;
		}

		public Color4(int color)
		{
			r = (byte)(color & 0x000000FF);
			g = (byte)((color & 0x0000FF00) >> 8);
			b = (byte)((color & 0x00FF0000) >> 16);
			a = (byte)((color & 0xFF000000) >> 24);
		}
		#endregion

		#region Methods
		//// Converts from linear RGB space to sRGB.
		//float3 LinearToSRGB(in float3 color)
		//{
		//	return pow(color, 1/2.2f);
		//}
		//// Converts from sRGB space to linear RGB.
		//float3 SRGBToLinear(in float3 color)
		//{
		//	return pow(color, 2.2f);
		//}
		#endregion
	}
}