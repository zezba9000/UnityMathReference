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
		public bool IntersectPlaneX(float planePosition, out Vec3 point)
		{
			point = origin;
			if (direction.x == 0) return false;

			float dis = planePosition - origin.x;
			float slopeY = direction.y / direction.x;
			float slopeZ = direction.z / direction.x;
			point = new Vec3(planePosition, (slopeY * dis) + origin.y, (slopeZ * dis) + origin.z);
			return true;
		}

		public bool IntersectPlaneY(float planePosition, out Vec3 point)
		{
			point = origin;
			if (direction.y == 0) return false;

			float dis = planePosition - origin.y;
			float slopeX = direction.x / direction.y;
			float slopeZ = direction.z / direction.y;
			point = new Vec3((slopeX * dis) + origin.x, planePosition, (slopeZ * dis) + origin.z);
			return true;
		}

		public bool IntersectPlaneZ(float planePosition, out Vec3 point)
		{
			point = origin;
			if (direction.z == 0) return false;

			float dis = planePosition - origin.z;
			float slopeX = direction.x / direction.z;
			float slopeY = direction.y / direction.z;
			point = new Vec3((slopeX * dis) + origin.x, (slopeY * dis) + origin.y, planePosition);
			return true;
		}

		public bool IntersectPlane(Vec3 planeNormal, Vec3 planeLocation, out Vec3 point)
		{
			float denom = planeNormal.Dot(direction);
			if (Math.Abs(denom) > 0.0001f)
			{
				float t = (planeLocation - origin).Dot(planeNormal) / denom;
				if (t >= 0)
				{
					point = origin + (direction * t);
					return true;
				}
			}

			point = origin;
			return false;
		}

		public bool IntersectTriangle(Vec3 trianglePoint1, Vec3 trianglePoint2, Vec3 trianglePoint3, Vec3 triangleNormal, out Vec3 intersectionPoint)
		{
			if (!this.IntersectPlane(triangleNormal, trianglePoint1, out intersectionPoint)) return false;
			return intersectionPoint.WithinTriangle(trianglePoint1, trianglePoint2, trianglePoint3);
		}

		public bool IntersectTriangle(Triangle3 triangle, Vec3 triangleNormal, out Vec3 intersectionPoint)
		{
			return IntersectTriangle(triangle.point1, triangle.point2, triangle.point3, triangleNormal, out intersectionPoint);
		}

		public bool IntersectTriangle(Triangle3 triangle, out Vec3 intersectionPoint)
		{
			return IntersectTriangle(triangle, triangle.Normal(), out intersectionPoint);
		}

		public bool IntersectsBounds(Bound3 boundingBox, out float result)
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

		public bool IntersectRaySphere(Sphere3 sphere, out Vec3 point1, out Vec3 point2)
		{
			Vec3 d = origin - sphere.center;
			float a = direction.Dot(direction);
			float b = d.Dot(direction);
			float c = d.Dot() - sphere.radius * sphere.radius;

			float disc = b * b - a * c;
			if (disc < 0.0f)
			{
				point1 = origin;
				point2 = origin;
				return false;
			}

			float sqrtDisc = (float)Math.Sqrt(disc);
			float invA = 1.0f / a;
			float t1 = (-b - sqrtDisc) * invA;
			float t2 = (-b + sqrtDisc) * invA;
			
			point1 = origin + t1 * direction;
			point2 = origin + t2 * direction;

			return true;
		}

		public bool IntersectRaySphere(Sphere3 sphere, out Vec3 point1, out Vec3 point2, out Vec3 normal1, out Vec3 normal2)
		{
			if (!IntersectRaySphere(sphere, out point1, out point2))
			{
				normal1 = Vec3.zero;
				normal2 = Vec3.zero;
				return false;
			}

			float invRadius = 1.0f / sphere.radius;
			normal1 = (point1 - sphere.center) * invRadius;
			normal2 = (point2 - sphere.center) * invRadius;
			return true;
		}

		public bool IntersectRayBox(Box3 box, out Vec3 point1, out Vec3 point2)
		{
			var s = box.size / 2;
			var min = (box.center - s) - origin;
			var max = (box.center + s) - origin;
			float near = float.MinValue;
			float far = float.MaxValue;

			// X
			float t1 = min.x / direction.x;
			float t2 = max.x / direction.x;
			float tMin = Math.Min(t1, t2);
			float tMax = Math.Max(t1, t2);
			if (tMin > near) near = tMin;
			if (tMax < far) far = tMax;
			if (near > far || far < 0)
			{
				point1 = Vec3.zero;
				point2 = Vec3.zero;
				return false;
			}

			// Y
			t1 = min.y / direction.y;
			t2 = max.y / direction.y;
			tMin = Math.Min(t1, t2);
			tMax = Math.Max(t1, t2);
			if (tMin > near) near = tMin;
			if (tMax < far) far = tMax;
			if (near > far || far < 0)
			{
				point1 = Vec3.zero;
				point2 = Vec3.zero;
				return false;
			}

			// Z
			t1 = min.z / direction.z;
			t2 = max.z / direction.z;
			tMin = Math.Min(t1, t2);
			tMax = Math.Max(t1, t2);
			if (tMin > near) near = tMin;
			if (tMax < far) far = tMax;
			if (near > far || far < 0)
			{
				point1 = Vec3.zero;
				point2 = Vec3.zero;
				return false;
			}

			point1 = origin + direction * near;
			point2 = origin + direction * far;
			return true;
		}

		public Line3 IntersectRay(Ray3 ray)
		{
			Vec3 vector = origin - ray.origin;
			float dot1 = vector.Dot(ray.direction);
			float dot2 = ray.direction.Dot(direction);
			float dot3 = vector.Dot(direction);
			float mul1 = ((dot1 * dot2) - dot3) / (1f - (dot2 * dot2));
			float mul2 = dot1 + (dot2 * mul1);
			return new Line3(origin + (mul1 * direction), ray.origin + (mul2 * ray.direction));
		}
		#endregion
	}
}