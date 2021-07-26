using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace UnityMathReference
{
	[StructLayout(LayoutKind.Sequential)]
	public struct Box3
	{
		#region Properties
		public Vec3 center, size;
		#endregion

		#region Constructors
		public Box3(Vec3 center, Vec3 size)
		{
			this.center = center;
			this.size = size;
		}
		#endregion
	}
}