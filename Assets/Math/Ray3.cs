using System;
using System.Runtime.InteropServices;

namespace UnityMathReference
{
	[StructLayout(LayoutKind.Sequential)]
	public struct Ray3
	{
		#region Properties
		public Vec3 origin, direction;
		#endregion

		#region Constructors
		public Ray3(Vec3 origin, Vec3 direction)
        {
            this.origin = origin;
            this.direction = direction;
        }
		#endregion

		#region Methods
		public Vec3 InersectPlaneX(float planePosition)
		{
			if (direction.x == 0) return origin;
			if ((planePosition >= origin.x && direction.x <= origin.x) || (planePosition <= origin.x && direction.x >= origin.x)) return origin;

			float dis = planePosition - origin.x;
			float slopeY = direction.y / direction.x;
			float slopeZ = direction.z / direction.x;
			return new Vec3(planePosition, (slopeY * dis) + origin.y, (slopeZ * dis) + origin.z);
		}

		public Vec3 InersectPlaneY(float planePosition)
		{
			if (direction.y == 0) return origin;
			if ((planePosition >= origin.y && direction.y <= origin.y) || (planePosition <= origin.y && direction.y >= origin.y)) return origin;

			float dis = planePosition - origin.y;
			float slopeX = direction.x / direction.y;
			float slopeZ = direction.z / direction.y;
			return new Vec3((slopeX * dis) + origin.x, planePosition, (slopeZ * dis) + origin.z);
		}

		public Vec3 InersectPlaneZ(float planePosition)
		{
			if (direction.z == 0) return origin;
			if ((planePosition >= origin.z && direction.z <= origin.z) || (planePosition <= origin.z && direction.z >= origin.z)) return origin;

			float dis = planePosition - origin.z;
			float slopeX = direction.x / direction.z;
			float slopeY = direction.y / direction.z;
			return new Vec3((slopeX * dis) + origin.x, (slopeY * dis) + origin.y, planePosition);
		}

		public bool Intersects(Bound3 boundingBox, out float result)
        {
			// X
            if (Math.Abs(direction.x) < MathUtilities.epsilon && (origin.x < boundingBox.min.x || origin.x > boundingBox.max.x))
            {
                //If the ray isn't pointing along the axis at all, and is outside of the box's interval, then it can't be intersecting.
				result = 0;
                return false;
            }

            float tmin = 0, tmax = float.MaxValue;
            float inverseDirection = 1 / direction.x;
            float t1 = (boundingBox.min.x - origin.x) * inverseDirection;
            float t2 = (boundingBox.max.x - origin.x) * inverseDirection;
            if (t1 > t2)
            {
                float temp = t1;
                t1 = t2;
                t2 = temp;
            }

            tmin = Math.Max(tmin, t1);
            tmax = Math.Min(tmax, t2);
            if (tmin > tmax)
			{
				result = 0;
				return false;
			}

			// Y
            if (Math.Abs(direction.y) < MathUtilities.epsilon && (origin.y < boundingBox.min.y || origin.y > boundingBox.max.y))
            {                
                //If the ray isn't pointing along the axis at all, and is outside of the box's interval, then it can't be intersecting.
				result = 0;
                return false;
            }

            inverseDirection = 1 / direction.y;
            t1 = (boundingBox.min.y - origin.y) * inverseDirection;
            t2 = (boundingBox.max.y - origin.y) * inverseDirection;
            if (t1 > t2)
            {
                float temp = t1;
                t1 = t2;
                t2 = temp;
            }

            tmin = Math.Max(tmin, t1);
            tmax = Math.Min(tmax, t2);
            if (tmin > tmax)
			{
				result = 0;
				return false;
			}

			// Z
            if (Math.Abs(direction.z) < MathUtilities.epsilon && (origin.z < boundingBox.min.z || origin.z > boundingBox.max.z))
            {              
                //If the ray isn't pointing along the axis at all, and is outside of the box's interval, then it can't be intersecting.
				result = 0;
                return false;
            }

            inverseDirection = 1 / direction.z;
            t1 = (boundingBox.min.z - origin.z) * inverseDirection;
            t2 = (boundingBox.max.z - origin.z) * inverseDirection;
            if (t1 > t2)
            {
                float temp = t1;
                t1 = t2;
                t2 = temp;
            }

            tmin = Math.Max(tmin, t1);
            tmax = Math.Min(tmax, t2);
            if (tmin > tmax)
			{
				result = 0;
				return false;
			}
            result = tmin;

			return true;
        }

		public bool IntersectRaySphere(Vec3 sphereCenter, float radius, out Vec3 point1, out Vec3 point2, out Vec3 normal1, out Vec3 normal2)
		{
			Vec3 d = origin - sphereCenter;
			float a = direction.Dot(direction);
			float b = d.Dot(direction);
			float c = d.Dot() - radius * radius;

			float disc = b * b - a * c;
			if (disc < 0.0f)
			{
				point1 = origin;
				point2 = origin;
				normal1 = Vec3.zero;
				normal2 = Vec3.zero;
				return false;
			}
	
			float sqrtDisc = (float)Math.Sqrt(disc);
			float invA = 1.0f / a;
			float t1 = (-b - sqrtDisc) * invA;
			float t2 = (-b + sqrtDisc) * invA;
	
			float invRadius = 1.0f / radius;
			point1 = origin + t1 * direction;
			point2 = origin + t2 * direction;
			normal1 = (point1 - sphereCenter) * invRadius;
			normal2 = (point2 - sphereCenter) * invRadius;
	
			return true;
		}
		#endregion
	}
}