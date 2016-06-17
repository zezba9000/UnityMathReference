namespace Reign
{
	public struct Plane3
	{
		#region Properties
		public Vec3 normal;
		public float distance;
		#endregion

		#region Constructors
		public Plane3(Vec3 normal, float distance)
		{
			this.normal = normal;
			this.distance = distance;
		}
		#endregion

		#region Methods
		public float DotCoordinate(Vec3 vector)
		{
			return (normal.x * vector.x) + (normal.y * vector.y) + (normal.z * vector.z) + distance;
		}
		#endregion
	}
}