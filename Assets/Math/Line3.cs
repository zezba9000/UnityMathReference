namespace Reign
{
	public struct Line3
	{
		#region Properties
		public Vec3 point1, point2;
		#endregion

		#region Constructors
		public Line3(Vec3 point1, Vec3 point2)
		{
			this.point1 = point1;
			this.point2 = point2;
		}
		#endregion

		#region Methods
		public Line3 Transform(Mat3 matrix)
		{
			return new Line3(point1.Transform(matrix), point2.Transform(matrix));
		}

		public float Length()
		{
			return (point1 - point2).Length();
		}

		public Vec3 InersectPlane(Vec3 planeNormal, Vec3 planeLocation)
		{
			float dot = (-(planeNormal.x*planeLocation.x) - (planeNormal.y*planeLocation.y) - (planeNormal.z*planeLocation.z));
			float dot3 = (planeNormal.x*(point2.x-point1.x)) + (planeNormal.y*(point2.y-point1.y)) + (planeNormal.z*(point2.z-point1.z));
			float dot2 = -((dot + (planeNormal.x*point1.x) + (planeNormal.y*point1.y) + (planeNormal.z*point1.z)) / dot3);
			return (point1 + (dot2*(point2-point1)));
		}

		public static void InersectPlane(ref Line3 line, ref Vec3 planeNormal, ref Vec3 planeLocation, out Vec3 result)
		{
			float dot = (-(planeNormal.x*planeLocation.x) - (planeNormal.y*planeLocation.y) - (planeNormal.z*planeLocation.z));
			float dot3 = (planeNormal.x*(line.point2.x-line.point1.x)) + (planeNormal.y*(line.point2.y-line.point1.y)) + (planeNormal.z*(line.point2.z-line.point1.z));
			float dot2 = -((dot + (planeNormal.x*line.point1.x) + (planeNormal.y*line.point1.y) + (planeNormal.z*line.point1.z)) / dot3);
			result = (line.point1 + (dot2*(line.point2-line.point1)));
		}

		//public bool InersectTriangle(out Vector3f pInersectPoint, Vector3f pPolygonPoint1, Vector3f pPolygonPoint2, Vector3f pPolygonPoint3, Vector3f pPolygonNormal, Bound3D pPolygonBoundingBox, Line3f pLine)
		//{
		//    pInersectPoint = Inersect(pPolygonNormal, pPolygonPoint1, pLine);
		//    if (pInersectPoint.WithinTriangle(pPolygonBoundingBox) == false) return false;
		//    return Within(pPolygonPoint1, pPolygonPoint2, pPolygonPoint3);
		//}

		public Line3 Inersect(Line3 line)
		{
		   Vec3 vector = (point1 - line.point1), vector2 = (line.point2 - line.point1), vector3 = (point2 - point1);
		   float dot1 = vector.Dot(vector2);
		   float dot2 = vector2.Dot(vector3);
		   float dot3 = vector.Dot(vector3);
		   float dot4 = vector2.Dot();
		   float dot5 = vector3.Dot();
		   float mul1 = (((dot1 * dot2) - (dot3 * dot4)) / ((dot5 * dot4) - (dot2 * dot2)));
		   float mul2 = (dot1 + (dot2 * mul1)) / dot4;
		   return new Line3((point1 + (mul1 * vector3)), (line.point1 + (mul2 * vector2)));
		}
		#endregion
	}
}