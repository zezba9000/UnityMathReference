using System;

namespace Reign
{
	public struct Ray2
	{
		#region Properties
		public Vec2 origin, direction;
		#endregion

		#region Constructors
		public Ray2(Vec2 origin, Vec2 direction)
        {
            this.origin = origin;
            this.direction = direction;
        }
		#endregion

		#region Methods
		public Vec2 InersectPlaneX(float planePosition)
		{
			if (direction.x == 0) return origin;
			if ((planePosition >= origin.x && direction.x <= origin.x) || (planePosition <= origin.x && direction.x >= origin.x)) return origin;

			float dis = planePosition - origin.x;
			float slopeY = direction.y / direction.x;
			return new Vec2(planePosition, (slopeY * dis) + origin.y);
		}

		public Vec2 InersectPlaneY(float planePosition)
		{
			if (direction.y == 0) return origin;
			if ((planePosition >= origin.y && direction.y <= origin.y) || (planePosition <= origin.y && direction.y >= origin.y)) return origin;

			float dis = planePosition - origin.y;
			float slopeX = direction.x / direction.y;
			return new Vec2((slopeX * dis) + origin.x, planePosition);
		}

		public bool Intersects(BoundingBox2 boundingBox, out float result)
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

            result = tmin;
			return true;
        }

		public bool IntersectRayCircle(Vec2 circleCenter, float circleRadius, out Vec2 point1, out Vec2 point2, out Vec2 normal1, out Vec2 normal2)
		{
			Vec2 d = origin - circleCenter;
			float a = direction.Dot(direction);
			float b = d.Dot(direction);
			float c = d.Dot() - circleRadius * circleRadius;

			float disc = b * b - a * c;
			if (disc < 0.0f)
			{
				point1 = origin;
				point2 = origin;
				normal1 = Vec2.zero;
				normal2 = Vec2.zero;
				return false;
			}
	
			float sqrtDisc = (float)Math.Sqrt(disc);
			float invA = 1.0f / a;
			float t1 = (-b - sqrtDisc) * invA;
			float t2 = (-b + sqrtDisc) * invA;
	
			float invRadius = 1.0f / circleRadius;
			point1 = origin + t1 * direction;
			point2 = origin + t2 * direction;
			normal1 = (point1 - circleCenter) * invRadius;
			normal2 = (point2 - circleCenter) * invRadius;
	
			return true;
		}
		#endregion
	}
}