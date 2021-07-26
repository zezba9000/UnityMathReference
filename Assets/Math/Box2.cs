using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace UnityMathReference
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Box2
    {
		#region Properties
		public Vec2 center, size;
		#endregion

		#region Constructors
		public Box2(Vec2 center, Vec2 size)
		{
			this.center = center;
			this.size = size;
		}
		#endregion
	}
}